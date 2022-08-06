using BO_SPP.Common;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace BO_SPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Handle LowerCase JSON Property Names in ASP.Net Core
            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);           

            services.AddSingleton<IHtmlSanitizer>(_ => new HtmlSanitizer());

            services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new AntiXssConverter());
            });

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = 268435456;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseExceptionHandler("/error");



            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            //app.Use(async (ctx, next) =>
            //{
            //    ctx.Response.Headers.Add("Content-Security-Policy",
            //                             "default-src 'self' cdn.example.com;");
            //    await next();
            //});

            //app.UseMiddleware<AntiXssMiddleware>();

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();           

            app.UseSession(new SessionOptions()
            {
                Cookie = new CookieBuilder()
                {
                    Name = ".AspNetCore.Session.BOSPP"
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseIISPlatformHandler();
            //app.UseStatusCodePagesWithReExecute("/error/{0}");
            //app.UseHttpException();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}");
            });
        }

        //public class CspReportRequest
        //{
        //    [JsonProperty(PropertyName = "csp-report")]
        //    public CspReport CspReport { get; set; }
        //}

        //public class CspReport
        //{
        //    [JsonProperty(PropertyName = "document-uri")]
        //    public string DocumentUri { get; set; }

        //    [JsonProperty(PropertyName = "referrer")]
        //    public string Referrer { get; set; }

        //    [JsonProperty(PropertyName = "violated-directive")]
        //    public string ViolatedDirective { get; set; }

        //    [JsonProperty(PropertyName = "effective-directive")]
        //    public string EffectiveDirective { get; set; }

        //    [JsonProperty(PropertyName = "original-policy")]
        //    public string OriginalPolicy { get; set; }

        //    [JsonProperty(PropertyName = "blocked-uri")]
        //    public string BlockedUri { get; set; }

        //    [JsonProperty(PropertyName = "status-code")]
        //    public int StatusCode { get; set; }
        //}
    }
}
