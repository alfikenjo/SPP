#pragma checksum "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "058ef0cfd3aac484091279cf60160aff086d91d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__TopNavigation), @"mvc.1.0.view", @"/Views/Shared/_TopNavigation.cshtml")]
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
#line 1 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"058ef0cfd3aac484091279cf60160aff086d91d0", @"/Views/Shared/_TopNavigation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d644b2e79124de71ac913850e232986376ca089", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__TopNavigation : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/image/logo-ptsmi-sidebar.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid rounded-normal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("logo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""iq-top-navbar"">
    <div class=""iq-navbar-custom"">
        <nav class=""navbar navbar-expand-lg navbar-light p-0"">
            <div class=""iq-navbar-logo d-flex align-items-center justify-content-between"">
                <i class=""ri-menu-line wrapper-menu""></i>
                <a href=""/"" class=""header-logo"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "058ef0cfd3aac484091279cf60160aff086d91d04850", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
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
            WriteLiteral(@"
                </a>
            </div>

            <div class=""navbar-breadcrumb d-none d-sm-block"">
                <nav aria-label=""breadcrumb"">
                    <ul class=""breadcrumb m-0"">
                        <li class=""breadcrumb-item""><a");
            BeginWriteAttribute("href", " href=\"", 783, "\"", 823, 1);
#nullable restore
#line 17 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 790, Url.Action("Index", "Dashboard"), 790, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Home</a></li>\r\n");
#nullable restore
#line 18 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                         if (ViewData["CurrentControllerName"].ToString() != "")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"breadcrumb-item\">");
#nullable restore
#line 20 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                                                   Write(ViewData["CurrentControllerName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 21 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <li class=\"breadcrumb-item active\" aria-current=\"page\">");
#nullable restore
#line 23 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                                                                          Write(ViewData["CurrentActionName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</li>
                    </ul>
                </nav>
            </div>

            <div class=""d-flex align-items-center"">
                <div class=""change-mode"">
                    <div class=""custom-control custom-switch custom-switch-icon custom-control-inline"">
                        <div class=""custom-switch-inner"">
                            <p class=""mb-0""></p>
                            <input type=""checkbox"" class=""custom-control-input"" id=""dark-mode"" data-active=""false"">
                            <label class=""custom-control-label"" for=""dark-mode"" data-mode=""toggle"">
                                <span class=""switch-icon-left""><i class=""a-left""></i></span>
                                <span class=""switch-icon-right""><i class=""a-right""></i></span>
                            </label>
                        </div>
                    </div>
                </div>
                <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbar");
            WriteLiteral(@"SupportedContent"" aria-controls=""navbarSupportedContent"" aria-label=""Toggle navigation"">
                    <i class=""ri-menu-3-line""></i>
                </button>
                <div class=""collapse navbar-collapse"" id=""navbarSupportedContent"">
                    <ul class=""navbar-nav ml-auto navbar-list align-items-center"">

");
#nullable restore
#line 47 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                         if (@HttpContextAccessor.HttpContext.Session.GetString("spp") != "")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <li id=""EksternalNotification"" class=""nav-item nav-icon dropdown"">
                                <a href=""#"" title=""Notifkasi pesan dari pelapor"" class=""search-toggle dropdown-toggle"" id=""dropdownMenuButton2"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                    <i class=""ri-mail-line text-danger""></i>
                                    <span id=""msg_dot"" name=""msg_dot"" class=""bg-danger count-mail"" style=""display: none""></span>
                                </a>
                                <div class=""iq-sub-dropdown dropdown-menu"" aria-labelledby=""dropdownMenuButton2"">
                                    <div class=""card shadow-none m-0"">
                                        <div class=""card-body p-0 "">
                                            <div class=""cust-title p-3"">
                                                <h5 name=""msg_count"" id=""msg_count"" class=""mb-0"">No Messages</h5>
                        ");
            WriteLiteral(@"                    </div>
                                            <div name=""div_msg"" id=""div_msg"" class=""p-3"">

                                            </div>
                                            <a name=""msg_link"" id=""msg_link"" style=""display: none""");
            BeginWriteAttribute("href", " href=\"", 3964, "\"", 4014, 1);
#nullable restore
#line 63 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 3971, Url.Action("DaftarPengaduan", "Pengaduan"), 3971, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""right-ic btn btn-primary btn-block position-relative p-2"" role=""button"">
                                                <div class=""dd-icon""><i class=""las la-arrow-right mr-0""></i></div>
                                                View All
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </li>
");
#nullable restore
#line 71 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        <li id=""InternalNotification"" class=""nav-item nav-icon dropdown"">
                            <a href=""#"" title=""Notifkasi pesan internal"" class=""search-toggle dropdown-toggle"" id=""dropdownMenuButton2"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                <i class=""ri-mail-line""></i>
                                <span id=""msg_dot_internal"" name=""msg_dot_internal"" class=""bg-primary count-mail"" style=""display: none""></span>
                            </a>
                            <div class=""iq-sub-dropdown dropdown-menu"" aria-labelledby=""dropdownMenuButton2"">
                                <div class=""card shadow-none m-0"">
                                    <div class=""card-body p-0 "">
                                        <div class=""cust-title p-3"">
                                            <h5 name=""msg_count_internal"" id=""msg_count_internal"" class=""mb-0"">No Messages</h5>
                                        </");
            WriteLiteral(@"div>
                                        <div name=""div_msg_internal"" id=""div_msg_internal"" class=""p-3"">

                                        </div>
                                        <a name=""msg_link_internal"" id=""msg_link_internal"" style=""display: none""");
            BeginWriteAttribute("href", " href=\"", 5812, "\"", 5862, 1);
#nullable restore
#line 87 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 5819, Url.Action("DaftarPengaduan", "Pengaduan"), 5819, 43, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""right-ic btn btn-primary btn-block position-relative p-2"" role=""button"">
                                            <div class=""dd-icon""><i class=""las la-arrow-right mr-0""></i></div>
                                            View All
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </li>

                        <li class=""nav-item iq-full-screen""><a href=""#""");
            BeginWriteAttribute("class", " class=\"", 6381, "\"", 6389, 0);
            EndWriteAttribute();
            WriteLiteral(@" id=""btnFullscreen""><i class=""ri-fullscreen-line""></i></a></li>
                        <li id=""UserProfile"" class=""caption-content"">
                            <a href=""#"" class=""iq-user-toggle"">
                                <image id=""img_Profile_1""");
            BeginWriteAttribute("src", " src=\"", 6648, "\"", 6711, 1);
#nullable restore
#line 99 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 6654, HttpContextAccessor.HttpContext.Session.GetString("img"), 6654, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""img-fluid rounded"" alt=""user""></image>
                            </a>
                            <div class=""iq-user-dropdown"">
                                <div class=""card"">
                                    <div class=""card-header d-flex justify-content-between align-items-center mb-0"">
                                        <div class=""header-title"">
                                            <h4 class=""card-title mb-0"">Profile</h4>
                                        </div>
                                        <div class=""close-data text-right badge badge-primary cursor-pointer""><i class=""ri-close-fill""></i></div>
                                    </div>
                                    <div class=""data-scrollbar"" data-scroll=""2"">
                                        <div class=""card-body"">
                                            <div class=""profile-header"">
                                                <div class=""cover-container text-center"">
       ");
            WriteLiteral("                                             <image id=\"img_Profile_2\"");
            BeginWriteAttribute("src", " src=\"", 7806, "\"", 7869, 1);
#nullable restore
#line 113 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 7812, HttpContextAccessor.HttpContext.Session.GetString("img"), 7812, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"rounded img-fluid avatar-80\" alt=\"profile-bg\"></image>\r\n                                                    <div class=\"profile-detail mt-3\">\r\n                                                        <h3 id=\"span_Fullname\">");
#nullable restore
#line 115 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                                                                          Write(HttpContextAccessor.HttpContext.Session.GetString("fn"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                                        <p id=\"span_Roles\" class=\"mb-1\">");
#nullable restore
#line 116 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
                                                                                   Write(HttpContextAccessor.HttpContext.Session.GetString("fr"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                    </div>\r\n                                                    <a");
            BeginWriteAttribute("href", " href=\"", 8427, "\"", 8467, 1);
#nullable restore
#line 118 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 8434, Url.Action("Signout", "Account"), 8434, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\">Sign Out</a>\r\n\r\n                                                </div>\r\n                                                <div class=\"profile-details my-4\">\r\n                                                    <a");
            BeginWriteAttribute("href", " href=\"", 8703, "\"", 8745, 1);
#nullable restore
#line 122 "D:\Clean Folder\Projects\ASIA SISTEM\SMI\WBS\GitHub\Workspace\SPP\BO_SPP\Views\Shared\_TopNavigation.cshtml"
WriteAttributeValue("", 8710, Url.Action("MyProfile", "Account"), 8710, 35, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""iq-sub-card bg-danger-light rounded-small p-2"">
                                                        <div class=""media align-items-center"">
                                                            <div class=""rounded iq-card-icon-small"">
                                                                <i class=""ri-profile-line""></i>
                                                            </div>
                                                            <div class=""media-body ml-3"">
                                                                <h6 class=""mb-0 "">Edit Profile</h6>
                                                                <p class=""mb-0 font-size-12"">Modify your personal details.</p>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>

                                    ");
            WriteLiteral(@"        </div>
                                            <div class=""p-3""></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>

        </nav>
    </div>
</div>");
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
