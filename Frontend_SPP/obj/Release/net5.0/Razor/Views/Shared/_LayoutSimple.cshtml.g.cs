#pragma checksum "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f87936a663b1045ccfa66f24d5603b1ac8909a00"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__LayoutSimple), @"mvc.1.0.view", @"/Views/Shared/_LayoutSimple.cshtml")]
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
#line 1 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
using Frontend_SPP.Resources.Home;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f87936a663b1045ccfa66f24d5603b1ac8909a00", @"/Views/Shared/_LayoutSimple.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"321cf87556c48bdbe12bb2892c785e1479a78de1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__LayoutSimple : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/functions.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("stretched"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html dir=\"ltr\" lang=\"en-US\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f87936a663b1045ccfa66f24d5603b1ac8909a004435", async() => {
                WriteLiteral(@"
    <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
    <meta name=""author"" content=""SPP PT.SMI"" />
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />

    <!-- Stylesheets
    ============================================= -->
    <link href=""https://fonts.googleapis.com/css?family=Lato:300,400,400i,700|Poppins:300,400,500,600,700|PT+Serif:400,400i&display=swap"" rel=""stylesheet"" type=""text/css"" />
    <link rel=""stylesheet""");
                BeginWriteAttribute("href", " href=\"", 606, "\"", 648, 1);
#nullable restore
#line 14 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 613, Url.Content("~/css/bootstrap.css"), 613, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 696, "\"", 730, 1);
#nullable restore
#line 15 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 703, Url.Content("~/style.css"), 703, 27, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 778, "\"", 817, 1);
#nullable restore
#line 16 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 785, Url.Content("~/css/swiper.css"), 785, 32, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 865, "\"", 902, 1);
#nullable restore
#line 17 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 872, Url.Content("~/css/dark.css"), 872, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 950, "\"", 993, 1);
#nullable restore
#line 18 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 957, Url.Content("~/css/font-icons.css"), 957, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1041, "\"", 1081, 1);
#nullable restore
#line 19 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1048, Url.Content("~/css/animate.css"), 1048, 33, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1129, "\"", 1176, 1);
#nullable restore
#line 20 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1136, Url.Content("~/css/magnific-popup.css"), 1136, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1226, "\"", 1265, 1);
#nullable restore
#line 22 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1233, Url.Content("~/css/custom.css"), 1233, 32, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/css\" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1313, "\"", 1356, 1);
#nullable restore
#line 23 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1320, Url.Content("~/css/sweetalert.css"), 1320, 36, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1390, "\"", 1435, 1);
#nullable restore
#line 25 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1397, Url.Content("~/css/intlTelInput.css"), 1397, 38, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <link rel=\"stylesheet\"");
                BeginWriteAttribute("href", " href=\"", 1467, "\"", 1509, 1);
#nullable restore
#line 26 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 1474, Url.Content("~/css/flag-icon.css"), 1474, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />

    <title>SPP PT.SMI</title>

    <style>
        .button-spinner {
            margin-right: 10px !important;
        }

        .content-wrap {
            padding-bottom: 0 !important;
            /*padding-top: 0 !important;*/
        }
    </style>

");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f87936a663b1045ccfa66f24d5603b1ac8909a0011239", async() => {
                WriteLiteral(@"

    <div id=""wrapper"" class=""clearfix"">
        <header id=""header"" class=""transparent-header page-section"" data-sticky-class=""not-dark"">
            <div id=""header-wrap"">
                <div class=""container"">
                    <div class=""header-row"">

                        <!-- Logo
                    ============================================= -->
                        <div id=""logo"">
                            <a href=""#"" data-href=""#slider"" class=""standard-logo"" data-dark-logo=""/images/logo-dark-ptsmi.png""><img src=""/images/logo-ptsmi.png"" alt=""Logo PTSMI""></a>
                            <a href=""#"" data-href=""#slider"" class=""retina-logo"" data-dark-logo=""/images/logo-dark-ptsmi@2x.png""><img src=""/images/logo-ptsmi@2x.png"" alt=""Logo PTSMI""></a>
                        </div><!-- #logo end -->

                        <div class=""header-misc"">

");
#nullable restore
#line 60 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
                              
                                var routeValues = this.ViewContext.RouteData.Values;
                                var controller = routeValues["controller"] as string;
                                var action = routeValues["action"] as string;
                            

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            ");
#nullable restore
#line 66 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
                       Write(Html.ActionLink("", @action, @controller, new { culture = "id" }, new { rel = "alternate", hreflang = "id", @class = "flag-icon flag-icon-idn header-misc-icon" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(" | ");
#nullable restore
#line 66 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
                                                                                                                                                                                             Write(Html.ActionLink("", @action, @controller, new { culture = "en" }, new { rel = "alternate", hreflang = "en", @class = "flag-icon flag-icon-gbr header-misc-icon" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>

                        <div id=""primary-menu-trigger"">
                            <svg class=""svg-trigger"" viewBox=""0 0 100 100""><path d=""m 30,33 h 40 c 3.722839,0 7.5,3.126468 7.5,8.578427 0,5.451959 -2.727029,8.421573 -7.5,8.421573 h -20""></path><path d=""m 30,50 h 40""></path><path d=""m 70,67 h -40 c 0,0 -7.5,-0.802118 -7.5,-8.365747 0,-7.563629 7.5,-8.634253 7.5,-8.634253 h 20""></path></svg>
                        </div>

                        <!-- Primary Navigation
                    ============================================= -->
                        <nav class=""primary-menu"">

                            <ul class=""menu-container one-page-menu"">
                                <li class=""menu-item current""><a class=""menu-link""");
                BeginWriteAttribute("href", " href=\"", 4260, "\"", 4295, 1);
#nullable restore
#line 78 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 4267, Url.Action("Index", "Home"), 4267, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("><div>");
#nullable restore
#line 78 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
                                                                                                                       Write(R_Home.Tentang_SPP);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</div></a></li>
                            </ul>

                        </nav><!-- #primary-menu end -->

                    </div>
                </div>
            </div>
            <div class=""header-wrap-clone""></div>
        </header>

        <section id=""slider"" class=""slider-element slider-parallax min-vh-60 min-vh-md-100 include-header"" style=""background: url(/images/landing/landing3.jpg) center;"">
            <div class=""slider-inner"">

                <div class=""vertical-middle slider-element-fade"">
                    <div class=""container py-5"">

                        <div class=""heading-block center border-bottom-0"">
                            <h1 data-animate=""fadeInUp"">WHISTLEBLOWING SYSTEM</h1>
                            <span data-animate=""fadeInUp"" data-delay=""300"">GOOD CORPORATE GOVERNANCE <strong>PT SMI</strong></span>
                        </div>

                    </div>

                    <a href=""#"" data-scrollto=""#section-features"" class=""one-");
                WriteLiteral("page-arrow\"><i class=\"icon-angle-down infinite animated fadeInDown\"></i></a>\r\n                </div>\r\n\r\n            </div>\r\n        </section>\r\n\r\n        <section id=\"content\">\r\n            <div class=\"content-wrap\">\r\n                ");
#nullable restore
#line 110 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
           Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
            </div>
        </section>

        <footer id=""footer"" class=""dark"">
            <div class=""container"">

                <!-- Footer Widgets
            ============================================= -->
                <div class=""footer-widgets-wrap"">

                    <div class=""row col-mb-50"">
                        <div class=""col-lg-12"">

                            <div class=""row col-mb-50"">
                                <div class=""col-md-4"">

                                    <div class=""widget clearfix"">

                                        <img src=""/images/logo-ptsmi-iso37001.png"" alt=""Image"" class=""footer-logo"">

                                        <p><strong>PT Sarana Multi Infrastruktur (Persero)</strong></p>



                                    </div>

                                </div>

                                <div class=""col-md-4"">
                                    <div class=""widget clearfix"">
                    ");
                WriteLiteral(@"                    <div style=""background: url('images/world-map.png') no-repeat center center; background-size: 100%;"">
                                            <address>
                                                <strong>Headquarters:</strong><br>
                                                Sahid Sudirman Center, Lantai 47-48<br>
                                                Jl. Jenderal Sudirman No. 86 <br>
                                                Jakarta 10220, Indonesia<br>
                                            </address>
                                            <a title=""Phone Number""><strong>Phone:</strong></a> (62-21) 8082 5288 (ext. 1004)<br>
                                            <a title=""Fax""><strong>Fax:</strong></a> (62-21) 8082 5258<br>
                                            <a href=""mailto:spp@ptsmi.co.id"" title=""Email Address""><strong>Email:</strong></a> spp@ptsmi.co.id
                                        </div>
                         ");
                WriteLiteral(@"           </div>

                                </div>

                                <div class=""col-md-4"">

                                    <div class=""widget widget_links clearfix"">

                                        <ul style=""margin-top:50px"">
                                            <li><a href=""#FAQ"">FAQ</a></li>
                                            <li><a href=""https://ptsmi.co.id/fraud-alert"" target=""_blank"">Fraud Alert</a></li>
                                            <li><a href=""https://ptsmi.co.id/policies-of-covid-19"">PT SMI's Policies about Covid-19</a></li>
                                        </ul>

                                    </div>

                                </div>


                            </div>

                        </div>


                    </div>

                </div><!-- .footer-widgets-wrap end -->

            </div>

            <!-- Copyrights
        ===========================================");
                WriteLiteral(@"== -->
            <div id=""copyrights"">
                <div class=""container"">

                    <div class=""row col-mb-30"">

                        <div class=""col-md-6 text-center text-md-start"">
                            Copyrights &copy;2022 PT Sarana Multi Infrastruktur (Persero). All Rights Reserved..<br>
                            <div class=""copyright-links""><a>Disclaimer</a> / <a href=""https://ptsmi.co.id/privacy-policy"" target=""_blank"">Privacy Policy</a></div>
                        </div>

                        <div class=""col-md-6 text-center text-md-end"">
                            <div class=""d-flex justify-content-center justify-content-md-end"">
                                <a href=""https://www.facebook.com/ptsmi/"" target=""_blank"" class=""social-icon si-small si-borderless si-facebook"">
                                    <i class=""icon-facebook""></i>
                                    <i class=""icon-facebook""></i>
                                </a>

        ");
                WriteLiteral(@"                        <a href=""https://www.instagram.com/ptsmi_id/"" target=""_blank"" class=""social-icon si-small si-borderless si-instagram"">
                                    <i class=""icon-instagram""></i>
                                    <i class=""icon-instagram""></i>
                                </a>

                                <a href=""https://www.youtube.com/channel/UCOvLDEbU4qetnKoiwNO0Oyg"" target=""_blank"" class=""social-icon si-small si-borderless si-youtube"">
                                    <i class=""icon-youtube""></i>
                                    <i class=""icon-youtube""></i>
                                </a>

                                <a href=""https://www.linkedin.com/company/ptsmi"" target=""_blank"" class=""social-icon si-small si-borderless si-linkedin"">
                                    <i class=""icon-linkedin""></i>
                                    <i class=""icon-linkedin""></i>
                                </a>
                            </div>");
                WriteLiteral(@"


                        </div>

                    </div>

                </div>
            </div><!-- #copyrights end -->
        </footer>
    </div>

    <div id=""gotoTop"" class=""icon-angle-up""></div>

    <!-- JavaScripts
    ============================================= -->
    <script");
                BeginWriteAttribute("src", " src=\"", 11024, "\"", 11060, 1);
#nullable restore
#line 231 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 11030, Url.Content("~/js/jquery.js"), 11030, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 11084, "\"", 11125, 1);
#nullable restore
#line 232 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 11090, Url.Content("~/js/plugins.min.js"), 11090, 35, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n\r\n    <!-- Footer Scripts\r\n    ============================================= -->\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f87936a663b1045ccfa66f24d5603b1ac8909a0023785", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 11280, "\"", 11352, 1);
#nullable restore
#line 238 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 11286, Url.Content("~/plugins/jquery-validation/jquery.validate.min.js"), 11286, 66, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 11376, "\"", 11420, 1);
#nullable restore
#line 239 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 11382, Url.Content("~/script/JValCustom.js"), 11382, 38, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 11444, "\"", 11519, 1);
#nullable restore
#line 240 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
WriteAttributeValue("", 11450, Url.Content("~/plugins/jquery-validation/additional-methods.min.js"), 11450, 69, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>    \r\n\r\n\r\n");
                WriteLiteral("    <script src=\"//cdn.jsdelivr.net/npm/sweetalert2@11\"></script>\r\n\r\n    \r\n\r\n    \r\n\r\n    <script type=\"text/javascript\">\r\n        var VP = \'");
#nullable restore
#line 251 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
             Write(Url.Content("~/"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
    </script>

    <script>
        function CustomNotif(msg) {
            var msgs = msg.split(""|"");
            var _type = msgs[0];
            var _title = msgs[1];
            var _html = msgs[2];
            var _script = msgs[3];
            _html = _html.toString().replace('\\n', '<br>');

            if (_script != """") {
                Swal.fire({
                    title: _title,
                    buttonsStyling: true,
                    confirmButtonClass: ""btn btn-success"",
                    html: _html,
                    type: _type
                }).then(function (result) { //RUNNING GIVEN SCRIPT ON CLOSE
                    eval(_script);
                }).catch(Swal.noop);
            }
            else {
                Swal.fire({
                    title: _title,
                    buttonsStyling: true,
                    confirmButtonClass: ""btn btn-success"",
                    html: _html,
                    type: _type
                }).");
                WriteLiteral(@"catch(Swal.noop);
            }

        }

        $("".button-spinner"").hide();
        $(document)
            .ajaxStart(function () {
                $("".button-spinner"").show();
            })
            .ajaxStop(function () {
                $("".button-spinner"").hide();
            });
    </script>

    ");
#nullable restore
#line 296 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Shared\_LayoutSimple.cshtml"
Write(await RenderSectionAsync("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n");
        }
        #pragma warning restore 1998
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
