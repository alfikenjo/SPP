#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5e214e1867d08b3e72a3b78f8c4e75bb200b179c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pengaduan_DaftarPengaduan), @"mvc.1.0.view", @"/Views/Pengaduan/DaftarPengaduan.cshtml")]
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\_ViewImports.cshtml"
using Frontend_SPP;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\_ViewImports.cshtml"
using Frontend_SPP.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
using Frontend_SPP.Resources.Home;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5e214e1867d08b3e72a3b78f8c4e75bb200b179c", @"/Views/Pengaduan/DaftarPengaduan.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"321cf87556c48bdbe12bb2892c785e1479a78de1", @"/Views/_ViewImports.cshtml")]
    public class Views_Pengaduan_DaftarPengaduan : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("Value", "Terkirim", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("Value", "Diproses", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("Value", "Ditolak", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("Value", "Selesai", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
  
    ViewData["Title"] = "Daftar Pengaduan";
    Layout = "~/Views/Shared/_Layout_FO.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 258, "\"", 307, 1);
#nullable restore
#line 12 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
WriteAttributeValue("", 264, Url.Content("~/script/DaftarPengaduan.js"), 264, 43, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n");
            }
            );
            WriteLiteral("\r\n");
#nullable restore
#line 15 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
Write(Html.Raw(TempData["msg"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    <input type=\"hidden\" id=\"culture\" name=\"culture\"");
            BeginWriteAttribute("value", " value=", 424, "", 502, 1);
#nullable restore
#line 17 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
WriteAttributeValue("", 431, HttpContextAccessor.HttpContext.Session.GetString("culture").ToLower(), 431, 71, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <div class=\"col-lg-12 mb-3\">\r\n        <div class=\"d-flex align-items-center justify-content-between\">\r\n            <div class=\"navbar-breadcrumb\">\r\n                <h5 class=\"mb-0\">");
#nullable restore
#line 21 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                            Write(R_Home.DaftarPengaduan);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <small>");
#nullable restore
#line 22 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                  Write(R_Home.SPPPTSMI);

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n            </div>\r\n            <div class=\"btn-group btn-group-toggle\">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 863, "\"", 1002, 1);
#nullable restore
#line 25 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
WriteAttributeValue("", 870, Url.Action("PengaduanForm", "Pengaduan", new { culture = @HttpContextAccessor.HttpContext.Session.GetString("culture").ToLower() }), 870, 132, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"btnAdd\" class=\"btn btn-sm btn-primary button-icon\"><i class=\"ri-add-line m-0\"></i>");
#nullable restore
#line 25 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                                                                                                                                                                                                                                Write(R_Home.BuatPengaduanBaru);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
            </div>
        </div>
    </div>

    <div class=""col-lg-12"">
        <div class=""col-lg-12 col-xl-12"" style=""padding-right: 0px; padding-left: 0px;"">
            <div id=""filter"" class=""card"">
                <div class=""card-body"">
                    <div class=""row align-items-center"">
                        <div class=""form-group col-md-3 col-sm-12"">
                            <label>");
#nullable restore
#line 36 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                              Write(R_Home.StatusPengaduan);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                            <select id=\"ddl_Status\" name=\"ddl_Status\" class=\"form-control select2\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e214e1867d08b3e72a3b78f8c4e75bb200b179c9831", async() => {
#nullable restore
#line 38 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                            Write(R_Home.Semua);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e214e1867d08b3e72a3b78f8c4e75bb200b179c11246", async() => {
#nullable restore
#line 39 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                                    Write(R_Home.Terkirim);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e214e1867d08b3e72a3b78f8c4e75bb200b179c12673", async() => {
#nullable restore
#line 40 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                                    Write(R_Home.Diproses);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e214e1867d08b3e72a3b78f8c4e75bb200b179c14100", async() => {
#nullable restore
#line 41 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                                   Write(R_Home.Ditolak);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5e214e1867d08b3e72a3b78f8c4e75bb200b179c15525", async() => {
#nullable restore
#line 42 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                                   Write(R_Home.Selesai);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                            </select>
                        </div>
                        <div class=""form-group col-md-6 col-sm-12 text-left"">
                            <label>&nbsp;</label><br />
                            <button id=""btn_View"" onclick=""btn_View_Click()"" class=""btn btn-outline-primary""><i class=""fa fa-spinner fa-spin button-spinner""></i><span class=""fa fa-search""></span>&nbsp;Search</button>
                            <button id=""btn_Reset"" onclick=""btn_Reset_Click()"" class=""btn btn-outline-danger""><i class=""fa fa-spinner fa-spin button-spinner""></i><span class=""fa fa-history""></span>&nbsp;Reset Filter</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class=""card card-block card-stretch card-height"">
                <div class=""card-body"">
                    <div class=""table-responsive"">
                        <input type=""hidden"" id=""hf_Email"" name=""hf_Email"" />
                        <");
            WriteLiteral(@"input type=""hidden"" id=""hf_ID"" name=""hf_ID"" />
                        <table id=""TableData"" class=""table data-table table-striped table-bordered text-sm text-nowrap"" width=""100%"">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>");
#nullable restore
#line 63 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                   Write(R_Home.NomorTiket);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                                    <th>");
#nullable restore
#line 64 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                   Write(R_Home.PihakTerlapor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                                    <th>");
#nullable restore
#line 65 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                   Write(R_Home.TanggalKejadian);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                                    <th>");
#nullable restore
#line 66 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\Frontend_SPP\Views\Pengaduan\DaftarPengaduan.cshtml"
                                   Write(R_Home.TanggalPengaduan);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



");
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
