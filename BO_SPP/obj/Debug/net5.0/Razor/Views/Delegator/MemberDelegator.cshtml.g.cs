#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\MemberDelegator.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9da75c7a53c5bd832b9ec593b724399db69c3d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Delegator_MemberDelegator), @"mvc.1.0.view", @"/Views/Delegator/MemberDelegator.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9da75c7a53c5bd832b9ec593b724399db69c3d6", @"/Views/Delegator/MemberDelegator.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_Delegator_MemberDelegator : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("FormInput"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\MemberDelegator.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 88, "\"", 144, 1);
#nullable restore
#line 7 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\MemberDelegator.cshtml"
WriteAttributeValue("", 94, Url.Content("~/Scripts/Pages/MemberDelegator.js"), 94, 50, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n");
            }
            );
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12 mb-3"">
        <div class=""d-flex align-items-center justify-content-between"">
            <div class=""navbar-breadcrumb"">
                <h5 class=""mb-0"">Delegator <span id=""DelegatorName""></span></h5>
                <small>Daftar Member</small>
            </div>
            <div");
            BeginWriteAttribute("class", " class=\"", 497, "\"", 505, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <button id=\"btnAdd\" onclick=\"return false;\" class=\"btn btn-sm btn-info button-icon mr-1\"><i class=\"ri-add-line m-0\"></i>Tambah Member</button>\r\n                <a id=\"btnCancel\"");
            BeginWriteAttribute("href", " href=\"", 702, "\"", 752, 1);
#nullable restore
#line 19 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\MemberDelegator.cshtml"
WriteAttributeValue("", 709, Url.Action("DaftarDelegator", "Delegator"), 709, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-sm btn-outline-primary button-icon"">Back</a>
            </div>
        </div>
    </div>

    <div class=""col-sm-12"">
        <div class=""card"">
            <div class=""card-body"">
                <div class=""LoaderOnGrid"">
                    <div class=""overlay ModalLoader"">
                        <i class=""fas fa-2x fa-spinner fa-spin"" style=""font-size:48px;color:red""></i>
                    </div>
                </div>
                <div class=""table-responsive "">
                    <table id=""TableData"" class=""data-tables table text-sm"">
                        <thead>
                            <tr>
                                <th>Profil</th>
                                <th>Nama Lengkap</th>
                                <th>Email</th>
                                <th>Phone No.</th>
                                <th>Status Akun</th>
                                <th style=""min-width: 50px"">Action</th>
                            </tr>
    ");
            WriteLiteral("                    </thead>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b9da75c7a53c5bd832b9ec593b724399db69c3d66884", async() => {
                WriteLiteral(@"
            <input type=""hidden"" name=""UserID"" id=""UserID"" />
            <div id=""divInput"" class=""card"" style=""display: none"">
                <div class=""card-header d-flex justify-content-between"">
                    <div class=""header-title"">
                        <h4 id=""FormTitle"" class=""card-title"">Form Registrasi Member Grup Delegator</h4>
                    </div>
                </div>
                <div class=""card-body"">
                    <div class=""row"">

                        <div class=""col-md-6 col-sm-12"">
                            <div class=""form-group"">
                                <label>Email *)</label>
                                <div class=""input-group"">
                                    <input type=""email"" id=""txtEmail"" name=""txtEmail"" class=""form-control"" onkeyup=""return forceLower(this);"" maxlength=""200""");
                BeginWriteAttribute("placeholder", " placeholder=\"", 2808, "\"", 2822, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                                    <div class=""input-group-prepend"">
                                        <button type=""button"" id=""btnSyncLDAP"" name=""btnSyncLDAP"" onclick=""GetLDAPAccByEmailByDelegatorID(); return false;"" class=""btn btn-outline-info""><i class=""fa fa-spinner fa-spin button-spinner""></i><i class=""fa fa-search mr-1""></i>&nbsp;Search</button>
                                    </div>
                                </div>
                            </div>

                            <div class=""form-group"">
                                <label>Nama Lengkap *)</label>
                                <input type=""text"" id=""txtFullname"" name=""txtFullname"" readonly class=""form-control"" maxlength=""100""");
                BeginWriteAttribute("placeholder", " placeholder=\"", 3564, "\"", 3578, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                            </div>

                            <div class=""form-group"">
                                <label>Nomor Handphone</label>
                                <input type=""text"" id=""txtMobile"" name=""txtMobile"" readonly class=""form-control"" maxlength=""50"" placeholder=""Mobile Phone"" />
                            </div>

                            <div class=""form-group"">
                                <label>Alamat</label>
                                <textarea style=""text-align: justify"" id=""txtAddress"" name=""txtAddress"" class=""form-control"" maxlength=""250"" height=""75px""></textarea>
                            </div>

                        </div>

                        <div class=""col-md-6 col-sm-12"">

                            <div class=""form-group"">
                                <label>Jabatan</label>
                                <input type=""text"" id=""txt_Jabatan"" name=""txt_Jabatan"" readonly class=""form-control"" maxlength=""200""");
                BeginWriteAttribute("placeholder", " placeholder=\"", 4588, "\"", 4602, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                            </div>

                            <div class=""form-group"">
                                <label>Departemen</label>
                                <input type=""text"" id=""txt_Divisi"" name=""txt_Divisi"" readonly class=""form-control"" maxlength=""200""");
                BeginWriteAttribute("placeholder", " placeholder=\"", 4889, "\"", 4903, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                            </div>

                        </div>

                        <div class=""col-md-12 col-sm-12"">
                            <button id=""btnSave"" type=""submit"" class=""btn btn-primary""><i class=""fa fa-spinner fa-spin button-spinner""></i><i class=""fa fa-save""></i>&nbsp;Invite</button>
                            <button id=""btnCloseForm"" onclick=""CloseForm(); return false;"" class=""btn bg-danger"">Cancel</button>
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
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    </div>\r\n</div>");
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
