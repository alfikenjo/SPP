#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Account\Signin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f27071351e20b3b8d362603ea175793baba183e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Signin), @"mvc.1.0.view", @"/Views/Account/Signin.cshtml")]
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\_ViewImports.cshtml"
using BO_SPP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\_ViewImports.cshtml"
using BO_SPP.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f27071351e20b3b8d362603ea175793baba183e", @"/Views/Account/Signin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Signin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormInput"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "signin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Account\Signin.cshtml"
  
    ViewData["Title"] = "Login Page";
    Layout = "~/Views/Shared/_Layout_Login.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 133, "\"", 180, 1);
#nullable restore
#line 8 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Account\Signin.cshtml"
WriteAttributeValue("", 139, Url.Content("~/Scripts/Pages/Common.js"), 139, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@"></script>

    <script>
        $(document).ready(function () {
            var dark = localStorage.getItem(""dark"");
            if (dark == 'true')
                document.getElementById('logoimg').src = '../image/logo-ptsmi-besar-putih.png';
            else
                document.getElementById('logoimg').src = '../image/logo-ptsmi-besar.png';
        });
    </script>
");
            }
            );
            WriteLiteral(@"
<div class=""wrapper"">
    <section class=""login-content"">
        <div class=""container h-100"">
            <div class=""row align-items-center justify-content-center h-100"">
                <div class=""col-12"">
                    <div class=""row align-items-center"">
                        <div class=""col-lg-6 mb-lg-6 col-sm-12 mb-4 mt-lg-0 mt-4"">
                            <img id=""logoimg"" class=""img-fluid w-100""");
            BeginWriteAttribute("alt", " alt=\"", 1003, "\"", 1009, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                        </div>
                        <div class=""col-lg-6 col-sm-12"">
                            <h2 class=""mb-2 text-center"">SIGN IN</h2>
                            <p class=""text-center"">BACK OFFICE SPP<br /><small>Halaman khusus pengguna Internal SPP PTSMI yang terdaftar</small></p>
                          
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8f27071351e20b3b8d362603ea175793baba183e6777", async() => {
                WriteLiteral(@"
                                <div class=""row"">
                                    
                                    <div class=""col-lg-12"">
                                        <div class=""form-group input-group"">
                                            <input type=""text"" id=""email"" name=""email"" class=""form-control"" MaxLength=""200"" placeholder=""Username"" />                                            
                                            <div class=""input-group-prepend"">
                                                <input class=""form-control text-center"" readonly");
                BeginWriteAttribute("value", " value=\"", 2060, "\"", 2081, 0);
                WriteLiteral("@");
                WriteLiteral("ptsmi.co.id");
                EndWriteAttribute();
                WriteLiteral(@" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-lg-12"">
                                        <div class=""form-group"">
                                            <input type=""password"" runat=""server"" id=""password"" name=""password"" placeholder=""Password"" class=""form-control"" />                                            
                                        </div>
                                    </div>
                                    <div class=""col-lg-12"">
                                        <div class=""floating-label form-group"">
                                            <input type=""text"" id=""captcha"" name=""captcha"" placeholder="" "" class=""floating-input form-control"" />
                                            <label>Enter below code</label>
                                        </div>
                                    </");
                WriteLiteral(@"div>
                                    <div class=""col-lg-6"" style=""display: none"">
                                        <a href=""#"" class=""text-primary float-right"">Forgot Password?</a>
                                    </div>
                                    <div class=""col-md-6 col-sm-12"">
                                        <table>
                                            <tr>
                                                <td style=""height: 50px; width: 100px;"">
                                                    <img id=""imgCaptcha"" />
                                                </td>
                                                <td valign=""middle"">
                                                    <button type=""button"" id=""btnRefresh"" name=""btnRefresh"" onclick=""FillCapctha()"">Refresh</button>
                                                </td>
                                            </tr>
                                        </table>
                  ");
                WriteLiteral(@"                  </div>
                                    <div class=""col-md-6 col-sm-12"">
                                        <button type=""submit"" id=""btnLogin"" name=""btnLogin"" class=""btn btn-primary"">SIGN IN</button>
                                    </div>
                                    <div class=""col-12"">
                                        ");
#nullable restore
#line 75 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Account\Signin.cshtml"
                                   Write(ViewBag.error);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("                            \r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
