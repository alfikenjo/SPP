#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\CMS\ContactUs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3338b9c1ca0b1d163b2b730a0db1304de7ff6ce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CMS_ContactUs), @"mvc.1.0.view", @"/Views/CMS/ContactUs.cshtml")]
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\CMS\ContactUs.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3338b9c1ca0b1d163b2b730a0db1304de7ff6ce", @"/Views/CMS/ContactUs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_CMS_ContactUs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
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
#line 4 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\CMS\ContactUs.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 172, "\"", 222, 1);
#nullable restore
#line 9 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\SC\BO_SPP\Views\CMS\ContactUs.cshtml"
WriteAttributeValue("", 178, Url.Content("~/Scripts/Pages/ContactUs.js"), 178, 44, false);

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
                <h5 class=""mb-0"">Contact Us</h5>
                <small>Content Management System</small>
            </div>
        </div>
    </div>

    <div class=""col-sm-12"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c3338b9c1ca0b1d163b2b730a0db1304de7ff6ce5013", async() => {
                WriteLiteral(@"
            <input type=""hidden"" class=""form-control text-sm"" id=""Action"">
            <input type=""hidden"" class=""form-control text-sm"" id=""ID"">
            <input type=""hidden"" class=""form-control text-sm"" id=""Tipe"" value=""Contact Us"">

            <div id=""divInput"" class=""card"">
                <div class=""card-header"">
                    <p class=""float-left"">Konten Contact Us disediakan dalam (2) dua versi bahasa</p>
                    <div class=""float-right"">
                        <button type=""submit"" id=""btnSave"" class=""btn bg-primary""><i class=""fa fa-spinner fa-spin button-spinner""></i><i class=""fa fa-save mr-2""></i>Save</button>
                    </div>
                </div>
                <div class=""card-body"">
                    
                    <ul class=""nav nav-tabs"" id=""myTab-1"" role=""tablist"">
                        <li class=""nav-item"">
                            <a class=""nav-link active"" id=""nav_ID"" data-toggle=""tab"" href=""#tab_ID"" role=""tab"" aria-control");
                WriteLiteral(@"s=""tab_ID"" aria-selected=""true""><span class=""flag-icon flag-icon-idn header-misc-icon mr-2""></span>ID (Bahasa)</a>
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
                WriteLiteral(@"      <label>Sub-title *)</label>
                                <input type=""text"" id=""SubTitle_ID"" name=""SubTitle_ID"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Deskripsi *)</label>
                                <textarea style=""text-align: justify"" id=""Description_ID"" name=""Description_ID"" class=""form-control h100""></textarea>
                            </div>

                            <div class=""form-group"">
                                <label>Gambar Thumbnail *)</label>
                                <div class=""custom-file mb-3"">
                                    <input type=""file"" id=""Filename_ID"" name=""Filename_ID"" class=""custom-file-input"" accept=""image/*"">
                                    <label class=""custom-file-label"" for=""customFile"">Pilih gambar</label>
                                </div>
                                <small>(Format JP");
                WriteLiteral(@"G/JPEG/PNG dengan ukuran 1000 x 414 pixel)</small>
                            </div>

                            <div id=""div_Preview_ID"" style=""display: none"" class=""form-group"">
                                <label class=""vw-100"">Pratinjau Gambar Dekstop</label>
                                <img id=""Preview_ID"" class=""img-banner w-50"" />
                            </div>

                            <div class=""form-group"">
                                <label>Telepon *)</label>
                                <input type=""text"" id=""Value1_ID"" name=""Value1_ID"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Faksimili *)</label>
                                <input type=""text"" id=""Value2_ID"" name=""Value2_ID"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                         ");
                WriteLiteral(@"       <label>Email *)</label>
                                <input type=""text"" id=""Value3_ID"" name=""Value3_ID"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Surat *)</label>
                                <textarea id=""Value4_ID"" name=""Value4_ID"" class=""form-control""></textarea>
                            </div>
                        </div>
                        <div id=""tab_EN"" class=""tab-pane fade"" role=""tabpanel"" aria-labelledby=""nav_EN"">
                            <div class=""form-group"">
                                <label>Title *)</label>
                                <input type=""text"" id=""Title_EN"" name=""Title_EN"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Sub-title *)</label>
                                <input type=""text"" ");
                WriteLiteral(@"id=""SubTitle_EN"" name=""SubTitle_EN"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Description *)</label>
                                <textarea style=""text-align: justify"" id=""Description_EN"" name=""Description_EN"" class=""form-control h100""></textarea>
                            </div>

                            <div class=""form-group"">
                                <label>Thumbnail Image *)</label>
                                <div class=""custom-file mb-3"">
                                    <input type=""file"" id=""Filename_EN"" name=""Filename_EN"" class=""custom-file-input"" accept=""image/*"">
                                    <label class=""custom-file-label"" for=""customFile"">Choose Image</label>
                                </div>
                                <small>(JPG/JPEG/PNG within 1000 x 414 pixel size)</small>
                            </div>

    ");
                WriteLiteral(@"                        <div id=""div_Preview_EN"" style=""display: none"" class=""form-group"">
                                <label class=""vw-100"">Thumbnail Preview</label>
                                <img id=""Preview_EN"" class=""img-banner w-50"" />
                            </div>

                            <div class=""form-group"">
                                <label>Phone *)</label>
                                <input type=""text"" id=""Value1_EN"" name=""Value1_EN"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Faximile *)</label>
                                <input type=""text"" id=""Value2_EN"" name=""Value2_EN"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Email *)</label>
                                <input type=""text"" id=""Value3_EN"" name=""");
                WriteLiteral(@"Value3_EN"" class=""form-control"" maxlength=""255"" />
                            </div>

                            <div class=""form-group"">
                                <label>Mailbox *)</label>
                                <textarea id=""Value4_EN"" name=""Value4_EN"" class=""form-control""></textarea>
                            </div>

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
