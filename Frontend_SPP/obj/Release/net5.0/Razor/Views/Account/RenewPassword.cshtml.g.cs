#pragma checksum "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12700f944af52868d6f6148c7ac66dbe919d16eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_RenewPassword), @"mvc.1.0.view", @"/Views/Account/RenewPassword.cshtml")]
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
#line 1 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml"
using Frontend_SPP.Resources.Home;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12700f944af52868d6f6148c7ac66dbe919d16eb", @"/Views/Account/RenewPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"321cf87556c48bdbe12bb2892c785e1479a78de1", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Account_RenewPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormRenewPassword"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("FormRenewPassword"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("mb-0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 2 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml"
  
    ViewData["Title"] = "Password Renewal";
    Layout = "~/Views/Shared/_LayoutSimple.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 173, "\"", 220, 1);
#nullable restore
#line 8 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml"
WriteAttributeValue("", 179, Url.Content("~/script/renewpassword.js"), 179, 41, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>    \r\n");
            }
            );
            WriteLiteral(@"
<style>
    #message {
        display: none;
        background: #f1f1f1;
        color: #000;
        position: relative;
        padding: 20px;
        margin-top: 10px;
    }

        #message p {
            padding: 0 35px 0 35px;
            font-size: 14px;
        }

    /* Add a green text color and a checkmark when the requirements are right */
    .valid {
        color: green;
    }

        .valid:before {
            position: relative;
            left: -35px;
            content: ""✔"";
        }

    /* Add a red text color and an ""x"" icon when the requirements are wrong */
    .invalid {
        color: red;
    }

        .invalid:before {
            position: relative;
            left: -35px;
            content: ""✖"";
        }
</style>

<input type=""hidden"" id=""isValid"" name=""isValid""");
            BeginWriteAttribute("value", " value=", 1096, "", 1119, 1);
#nullable restore
#line 49 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml"
WriteAttributeValue("", 1103, ViewBag.isValid, 1103, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
<div class=""section topmargin-sm mb-0"">
    <div id=""t-login"" class=""container clearfix"">

        <div class=""tabs mx-auto bottommargin-lg clearfix"" id=""div_renew_password"" data-animate=""bounceInRight"" style=""max-width: 750px;"">

            <div class=""tab-container"">

                <div class=""tab-content"">
                    <div class=""card mb-0"">
                        <div class=""card-body"" style=""padding: 40px;"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12700f944af52868d6f6148c7ac66dbe919d16eb7178", async() => {
                WriteLiteral(@"

                                <h3>Pulihkan Akun</h3>
                                <small>Silahkan membuat password baru untuk memulihkan akun Anda</small>
                                <div class=""row"">
                                    <div class=""col-md-12 form-group"">
                                        <label>Password</label>
                                        <input type=""password"" id=""Renew_Password"" name=""Renew_Password"" required pattern=""(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"" title=""Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"" class=""form-control"" />
                                    </div>

                                    <div class=""col-md-12 form-group"">
                                        <label>Re-enter Password</label>
                                        <input type=""password"" id=""Renew_Password_Reentered"" name=""Renew_Password_Reentered"" required pattern=""(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}");
                WriteLiteral(@""" title=""Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"" class=""form-control"" />
                                    </div>

                                    <div id=""message"" class=""mb-2"">
                                        <h3>Password must contain the following:</h3>
                                        <p id=""letter"" class=""invalid"">A <b>lowercase</b> letter</p>
                                        <p id=""capital"" class=""invalid"">A <b>capital (uppercase)</b> letter</p>
                                        <p id=""number"" class=""invalid"">A <b>number</b></p>
                                        <p id=""length"" class=""invalid"">Minimum <b>8 characters</b></p>
                                    </div>

                                    <div class=""col-12 form-group"">
                                        <button type=""submit"" formaction=""FormRenewPassword"" class=""button button-3d button-black m-0""><i class=""fa fa-spi");
                WriteLiteral("nner fa-spin button-spinner\"></i>SUBMIT</button>\r\n                                        <a class=\"button button-3d btn-outline-secondary m-0\"");
                BeginWriteAttribute("href", " href=\"", 3847, "\"", 3882, 1);
#nullable restore
#line 85 "C:\Users\NSR-PC043\source\repos\SPP_PTSMI\Frontend_SPP\Views\Account\RenewPassword.cshtml"
WriteAttributeValue("", 3854, Url.Action("Index", "Home"), 3854, 28, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">Cancel</a><br />\r\n                                    </div>\r\n                                </div>\r\n\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n        <input type=\"hidden\" id=\"UserID\" name=\"UserID\" />\r\n\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
