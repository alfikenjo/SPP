#pragma checksum "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "91df487d9b5c25daf24b4283b833373627c4a992"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Index), @"mvc.1.0.view", @"/Views/Dashboard/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\_ViewImports.cshtml"
using Frontend_SPP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\_ViewImports.cshtml"
using Frontend_SPP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
using Frontend_SPP.Resources.Home;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"91df487d9b5c25daf24b4283b833373627c4a992", @"/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"321cf87556c48bdbe12bb2892c785e1479a78de1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "FO - Dashboard";
    Layout = "~/Views/Shared/_Layout_FO.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<input type=\"hidden\" id=\"msg\" name=\"msg\"");
            BeginWriteAttribute("value", " value=\"", 264, "\"", 284, 1);
#nullable restore
#line 10 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
WriteAttributeValue("", 272, ViewBag.Msg, 272, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script>\r\n        var msg = $(\'#msg\').val();\r\n        if (msg)\r\n            CustomNotif(\"error|Oops|\" + msg + \"\");\r\n    </script>\r\n");
            }
            );
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-lg-12 mb-3\">\r\n        <div class=\"d-flex align-items-center justify-content-between\">\r\n            <h5 class=\"mb-0\">");
#nullable restore
#line 25 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                        Write(R_Home.Selamatdatang);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 25 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                              Write(HttpContextAccessor.HttpContext.Session.GetString("fn"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-12\">\r\n        <div class=\"card car-transparent\">\r\n            <div class=\"card-body p-0\">\r\n                <div class=\"profile-image position-relative\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 924, "\"", 966, 1);
#nullable restore
#line 33 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
WriteAttributeValue("", 930, Url.Content("~/image/landing4.jpg"), 930, 36, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid rounded w-100\"");
            BeginWriteAttribute("alt", " alt=\"", 999, "\"", 1005, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                </div>\r\n                <div class=\"profile-overly\">\r\n                    <h3 id=\"span_Fullname_2\">");
#nullable restore
#line 36 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Fullname"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                    <span id=""span_Unit""></span>
                </div>
            </div>
        </div>
    </div>


    <div class=""col-sm-6 col-md-6 col-lg-3"">
        <div class=""card card-block card-stretch card-height"">
            <div class=""card-body iq-total-content"">
                <div class=""top-block d-flex align-items-center justify-content-between"">
                    <p class=""mb-1"">");
#nullable restore
#line 48 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                               Write(R_Home.Jumlah);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <span id=\"sp_pengaduan\" class=\"badge badge-info rounded-normal\">");
#nullable restore
#line 49 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                                                               Write(ViewBag.Terkirim);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                </div>\r\n                <h4><span class=\"counter\">");
#nullable restore
#line 51 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                     Write(R_Home.Pengaduan);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></h4>
                <div class=""d-flex align-items-center justify-content-between mt-3 position-relative"">
                    <div class=""iq-progress-bar bg-info-light mt-3 iq-progress-bar-icon"">
                        <span class=""bg-info iq-progress progress-1"" data-percent=""100"">
                            <span class=""progress-text text-info""></span>
                        </span>
                    </div>
                    <div id=""total-chart-01"" class=""ml-4""></div>
                </div>
            </div>
        </div>
    </div>

    <div class=""col-sm-6 col-md-6 col-lg-3"">
        <div class=""card card-block card-stretch card-height"">
            <div class=""card-body iq-total-content"">
                <div class=""top-block d-flex align-items-center justify-content-between"">
                    <p class=""mb-1"">");
#nullable restore
#line 68 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                               Write(R_Home.Jumlah);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <span runat=\"server\" id=\"sp_diproses\" class=\"badge badge-warning rounded-normal\">");
#nullable restore
#line 69 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                                                                                Write(ViewBag.Diproses);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                </div>\r\n                <h4><span class=\"counter\">");
#nullable restore
#line 71 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                     Write(R_Home.Diproses);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></h4>
                <div class=""d-flex align-items-center justify-content-between mt-3 position-relative"">
                    <div class=""iq-progress-bar progress-4 bg-warning-light mt-3 iq-progress-bar-icon"">
                        <span class=""bg-warning"" data-percent=""100"">
                            <span class=""progress-text text-warning""></span>
                        </span>
                    </div>
                    <div id=""total-chart-04"" class=""ml-4""></div>
                </div>
            </div>
        </div>
    </div>

    <div class=""col-sm-6 col-md-6 col-lg-3"">
        <div class=""card card-block card-stretch card-height"">
            <div class=""card-body iq-total-content"">
                <div class=""top-block d-flex align-items-center justify-content-between"">
                    <p class=""mb-1"">");
#nullable restore
#line 88 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                               Write(R_Home.Jumlah);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <span runat=\"server\" id=\"sp_selesai\" class=\"badge badge-success rounded-normal\">");
#nullable restore
#line 89 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                                                                               Write(ViewBag.Selesai);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                </div>\r\n                <h4><span class=\"counter\">");
#nullable restore
#line 91 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                     Write(R_Home.Selesai);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></h4>
                <div class=""d-flex align-items-center justify-content-between mt-3 position-relative"">
                    <div class=""iq-progress-bar progress-4 bg-success-light mt-3 iq-progress-bar-icon"">
                        <span class=""bg-success"" data-percent=""100"">
                            <span class=""progress-text text-success""></span>
                        </span>
                    </div>
                    <div id=""total-chart-04"" class=""ml-4""></div>
                </div>
            </div>
        </div>
    </div>


    <div class=""col-sm-6 col-md-6 col-lg-3"">
        <div class=""card card-block card-stretch card-height"">
            <div class=""card-body iq-total-content"">
                <div class=""top-block d-flex align-items-center justify-content-between"">
                    <p class=""mb-1"">");
#nullable restore
#line 109 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                               Write(R_Home.Jumlah);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <span runat=\"server\" id=\"sp_ditolak\" class=\"badge badge-danger rounded-normal\">");
#nullable restore
#line 110 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                                                                              Write(ViewBag.Ditolak);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                </div>\r\n                <h4><span class=\"counter\">");
#nullable restore
#line 112 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Dashboard\Index.cshtml"
                                     Write(R_Home.Ditolak);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span></h4>
                <div class=""d-flex align-items-center justify-content-between mt-3 position-relative"">
                    <div class=""iq-progress-bar progress-4 bg-danger-light mt-3 iq-progress-bar-icon"">
                        <span class=""bg-danger"" data-percent=""100"">
                            <span class=""progress-text text-danger""></span>
                        </span>
                    </div>
                    <div id=""total-chart-04"" class=""ml-4""></div>
                </div>
            </div>
        </div>
    </div>





</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
