#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\CMS\SyaratDanKetentuan.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c746337702e862dfa0120dbddb266e28c1f8e73a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CMS_SyaratDanKetentuan), @"mvc.1.0.view", @"/Views/CMS/SyaratDanKetentuan.cshtml")]
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
#nullable restore
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\CMS\SyaratDanKetentuan.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c746337702e862dfa0120dbddb266e28c1f8e73a", @"/Views/CMS/SyaratDanKetentuan.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_CMS_SyaratDanKetentuan : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\CMS\SyaratDanKetentuan.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 172, "\"", 231, 1);
#nullable restore
#line 9 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\CMS\SyaratDanKetentuan.cshtml"
WriteAttributeValue("", 178, Url.Content("~/Scripts/Pages/SyaratDanKetentuan.js"), 178, 53, false);

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
                <h5 class=""mb-0"">Syarat dan Ketentuan</h5>
                <small>Content Management System</small>
            </div>
        </div>
    </div>

    <div class=""col-sm-12"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c746337702e862dfa0120dbddb266e28c1f8e73a5170", async() => {
                WriteLiteral(@"
            <input type=""hidden"" class=""form-control text-sm"" id=""Action"">
            <input type=""hidden"" class=""form-control text-sm"" id=""ID"">
            <input type=""hidden"" class=""form-control text-sm"" id=""Tipe"" value=""Syarat dan Ketentuan"">

            <div id=""divInput"" class=""card"">
                <div class=""card-header"">
                    <p class=""float-left"">Konten Syarat dan Ketentuan disediakan dalam dua versi bahasa</p>
                    <div class=""float-right"">
                        <button type=""submit"" id=""btnSave"" class=""btn bg-primary""><i class=""fa fa-spinner fa-spin button-spinner""></i><i class=""fa fa-save mr-2""></i>Save</button>
                    </div>
                </div>
                <div class=""card-body"">
                    
                    <ul class=""nav nav-tabs"" id=""myTab-1"" role=""tablist"">
                        <li class=""nav-item"">
                            <a class=""nav-link active"" id=""nav_ID"" data-toggle=""tab"" href=""#tab_ID"" role=""t");
                WriteLiteral(@"ab"" aria-controls=""tab_ID"" aria-selected=""true""><span class=""flag-icon flag-icon-idn header-misc-icon mr-2""></span>ID (Bahasa)</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link"" id=""nav_EN"" data-toggle=""tab"" href=""#tab_EN"" role=""tab"" aria-controls=""tab_EN"" aria-selected=""false""><span class=""flag-icon flag-icon-gbr header-misc-icon mr-2""></span>EN (English)</a>
                        </li>
                    </ul>
                    <div class=""tab-content"" id=""myTabContent-2"">
                        <div id=""tab_ID"" class=""tab-pane fade show active"" role=""tabpanel"" aria-labelledby=""nav_ID"">
                            <div class=""form-group"">
                                <label>Title *)</label>
                                <input type=""text"" id=""Title_ID"" name=""Title_ID"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
            ");
                WriteLiteral(@"                    <label>File PDF *)</label>
                                <div class=""custom-file mb-3"">
                                    <input type=""file"" id=""Filename_ID"" name=""Filename_ID"" class=""custom-file-input"" accept=""application/pdf"">
                                    <label class=""custom-file-label"" for=""customFile"">Pilih file</label>
                                </div>
                                <small>(Format PDF)</small>
                            </div>

                            <div id=""div_Preview_ID"" style=""display: none"" class=""custom-badge""><a id=""a_FileEvidence"" target=""_blank"" class=""badge badge-danger ml-2"" style=""cursor: pointer"">Preview File</a></div>
                        </div>
                        <div id=""tab_EN"" class=""tab-pane fade"" role=""tabpanel"" aria-labelledby=""nav_EN"">
                            <div class=""form-group"">
                                <label>Title *)</label>
                                <input type=""text"" id=""Titl");
                WriteLiteral(@"e_EN"" name=""Title_EN"" class=""form-control"" maxlength=""255"" />
                            </div>
                            
                            <div class=""form-group"">
                                <label>File PDF *)</label>
                                <div class=""custom-file mb-3"">
                                    <input type=""file"" id=""Filename_EN"" name=""Filename_EN"" class=""custom-file-input"" accept=""application/pdf"">
                                    <label class=""custom-file-label"" for=""customFile"">Pilih file</label>
                                </div>
                                <small>(Format PDF)</small>
                            </div>

                            <div id=""div_Preview_EN"" style=""display: none"" class=""custom-badge""><a id=""a_FileEvidence"" target=""_blank"" class=""badge badge-danger ml-2"" style=""cursor: pointer"">Preview File</a></div>
                        </div>

                    </div>
                </div>
                <div class=");
                WriteLiteral("\"card-footer\">\r\n                    <div class=\"form-group\">\r\n                        \r\n                    </div>\r\n                </div>\r\n            </div>\r\n        ");
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
            WriteLiteral("\r\n    </div>\r\n</div>");
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
