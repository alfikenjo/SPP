#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\AddMemberDelegator.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39454d3dee14db45c703a2025dee21c18a77bc5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Delegator_AddMemberDelegator), @"mvc.1.0.view", @"/Views/Delegator/AddMemberDelegator.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39454d3dee14db45c703a2025dee21c18a77bc5f", @"/Views/Delegator/AddMemberDelegator.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_Delegator_AddMemberDelegator : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\AddMemberDelegator.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    \r\n    <script");
                BeginWriteAttribute("src", " src=\"", 92, "\"", 151, 1);
#nullable restore
#line 7 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\AddMemberDelegator.cshtml"
WriteAttributeValue("", 98, Url.Content("~/Scripts/Pages/AddMemberDelegator.js"), 98, 53, false);

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
                <h6 style=""font-size: 13px"" class=""mb-0"">Klik pilih pada data dibawah ini untuk didaftarkan pada Grup Delegator <span id=""DelegatorName""></span></h6>
                <small>Add Member Delegator</small>
            </div>
            <div");
            BeginWriteAttribute("class", " class=\"", 596, "\"", 604, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <button id=\"btnSync\" onclick=\"return false;\" class=\"btn btn-sm btn-outline-warning button-icon mr-1\"><i class=\"fa fa-history m-0\"></i>&nbsp;Sync from AD PTSMI</button>\r\n                <a id=\"btnCancel\"");
            BeginWriteAttribute("href", " href=\"", 826, "\"", 876, 1);
#nullable restore
#line 19 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Delegator\AddMemberDelegator.cshtml"
WriteAttributeValue("", 833, Url.Action("MemberDelegator", "Delegator"), 833, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-sm btn-outline-primary button-icon"">Cancel</a>
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
                <table class=""table table-bordered text-sm"" id=""TableData"" style=""width: 100%;"">
                    <thead>
                        <tr>
                            <th style=""min-width: 50px"">Action</th>
                            <th>Profil</th>
                            <th>Nama Lengkap</th>
                            <th>Email</th>
                            <th>Mobile</th>
                            <th>Grup Delegator</th>
                            <th>Status</th>
                        </tr>
                    </");
            WriteLiteral("thead>\r\n                </table>\r\n\r\n            </div>\r\n        </div>\r\n\r\n        \r\n    </div>\r\n</div>");
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
