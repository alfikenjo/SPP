using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ganss.XSS;
using Frontend_SPP.Common;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http.Features;

namespace Frontend_SPP
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

            CultureInfo[] supportedCultures = new[]
               {
                new CultureInfo("in"),
                new CultureInfo("en")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("in");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };

            });

            services.Configure<FormOptions>(x =>
            {

                x.ValueLengthLimit = int.MaxValue; // Limit on individual form values
                x.MultipartBodyLengthLimit = int.MaxValue; // Limit on form body size
                x.MultipartHeadersLengthLimit = int.MaxValue; // Limit on form header size
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;  //2GB
            });

            services.Configure<FormOptions>(options =>
            {
                // Set the limit to 256 MB
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(new SessionOptions()
            {
                Cookie = new CookieBuilder()
                {
                    Name = ".AspNetCore.Session.FOSPP"
                }
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization();
            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}");
            });
        }
    }
}
