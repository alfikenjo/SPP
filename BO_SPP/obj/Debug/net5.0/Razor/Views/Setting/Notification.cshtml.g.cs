#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\Setting\Notification.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aaa6c4bbf624b649463212f5b5f5ccac3949903d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting_Notification), @"mvc.1.0.view", @"/Views/Setting/Notification.cshtml")]
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\_ViewImports.cshtml"
using BO_SPP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\_ViewImports.cshtml"
using BO_SPP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\Setting\Notification.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aaa6c4bbf624b649463212f5b5f5ccac3949903d", @"/Views/Setting/Notification.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_Setting_Notification : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormInput"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("FormInput"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-12"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\Setting\Notification.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 172, "\"", 225, 1);
#nullable restore
#line 9 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\Setting\Notification.cshtml"
WriteAttributeValue("", 178, Url.Content("~/Scripts/Pages/Notification.js"), 178, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n");
            }
            );
            WriteLiteral("\r\n<style>\r\n    form {\r\n        display: flex !important;\r\n    }\r\n</style>\r\n\r\n\r\n<div class=\"row\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aaa6c4bbf624b649463212f5b5f5ccac3949903d5507", async() => {
                WriteLiteral(@"
        <div class=""col-lg-6"">
            <div class=""card"">
                <div class=""card-header d-flex justify-content-between"">
                    <div class=""header-title"">
                        <h4 class=""card-title"">Mail Server Setting</h4>
                    </div>
                </div>
                <div class=""card-body"">
                    <div class=""acc-edit"">

                        <div class=""form-group""><label for=""SMTPAddress"">SMTP Address</label><input type=""text"" class=""form-control"" id=""SMTPAddress"" name=""SMTPAddress""></div>
                        <div class=""form-group""><label for=""SMTPPort"">SMTP Port</label><input type=""text"" class=""form-control"" id=""SMTPPort"" name=""SMTPPort""></div>
                        <div class=""form-group""><label for=""EmailAddress"">Email Address</label><input type=""text"" class=""form-control"" id=""EmailAddress"" name=""EmailAddress""></div>
                        <div class=""form-group""><label for=""Password"">Password</label><input type=""pa");
                WriteLiteral(@"ssword"" class=""form-control"" id=""Password"" name=""Password""></div>
                        <div class=""form-group""><label for=""SenderName"">Sender Name</label><input type=""text"" class=""form-control"" id=""SenderName"" name=""SenderName""></div>

                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-primary"" id=""EnableSSL"" name=""EnableSSL""");
                BeginWriteAttribute("checked", " checked=\"", 1888, "\"", 1898, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""EnableSSL"">Enable SSL</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-primary"" id=""UseDefaultCredentials"" name=""UseDefaultCredentials""");
                BeginWriteAttribute("checked", " checked=\"", 2234, "\"", 2244, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""UseDefaultCredentials"">Use Default Credentials</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-primary"" id=""UseAsync"" name=""UseAsync""");
                BeginWriteAttribute("checked", " checked=\"", 2579, "\"", 2589, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""UseAsync"">Enable Async</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-primary"" id=""EnableServices"" name=""EnableServices""");
                BeginWriteAttribute("checked", " checked=\"", 2912, "\"", 2922, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""EnableServices"">Enable Services</label></div></div>


                    </div>
                </div>
            </div>
        </div>
        <div class=""col-lg-6"">
            <div class=""card"">
                <div class=""card-header d-flex justify-content-between"">
                    <div class=""header-title"">
                        <h4 class=""card-title"">Notitication Center</h4>
                    </div>
                </div>
                <div class=""card-body"">
                    <div class=""acc-edit"">
                        <div class=""form-group"">
                            <small>Send notification on:</small>
                        </div>

                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-danger"" id=""NewUser"" name=""NewUser""");
                BeginWriteAttribute("checked", " checked=\"", 3877, "\"", 3887, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""NewUser"">New registered user</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-danger"" id=""NewRoleAssignment"" name=""NewRoleAssignment""");
                BeginWriteAttribute("checked", " checked=\"", 4221, "\"", 4231, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""NewRoleAssignment"">Any role assignment</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-danger"" id=""UserPasswordReset"" name=""UserPasswordReset""");
                BeginWriteAttribute("checked", " checked=\"", 4575, "\"", 4585, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""UserPasswordReset"">Resetting user password </label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-danger"" id=""Messaging"" name=""Messaging""");
                BeginWriteAttribute("checked", " checked=\"", 4918, "\"", 4928, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""Messaging"">Messaging</label></div></div>
                        <div class=""form-group""><div class=""custom-control custom-switch custom-switch-color custom-control-inline""><input type=""checkbox"" class=""custom-control-input bg-danger"" id=""ReminderServices"" name=""ReminderServices""");
                BeginWriteAttribute("checked", " checked=\"", 5252, "\"", 5262, 0);
                EndWriteAttribute();
                WriteLiteral(@"><label class=""custom-control-label"" for=""ReminderServices"">Reminder services</label></div></div>


                        

                    </div>
                </div>
                <div class=""card-footer"">
                    <div class=""form-group"">
                        <button id=""btnSaveDC"" type=""submit"" form=""FormInput"" class=""btn btn-primary mr-2""><i class=""fa fa-spinner fa-spin button-spinner""></i><i class=""fa fa-save""></i>&nbsp;Save</button>
                        <small id=""UpdatedOn"" class=""float-right""></small>
                    </div>
                </div>
            </div>
        </div>
    ");
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
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
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
