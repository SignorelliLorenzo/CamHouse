#pragma checksum "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3b34a0ec03c4c07537605bc9bd402544d128dc27"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Telecamere_Delete), @"mvc.1.0.view", @"/Views/Telecamere/Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b34a0ec03c4c07537605bc9bd402544d128dc27", @"/Views/Telecamere/Delete.cshtml")]
    public class Views_Telecamere_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Api_Pcto.Telecamera>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Delete</h1>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>Telecamera</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 15 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 18 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayFor(model => model.nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 21 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.link));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayFor(model => model.link));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 27 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.num_like));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayFor(model => model.num_like));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 33 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.num_salvati));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "C:\Users\ghilardi.17186\Source\Repos\CamHouse\Api_Pcto\Api_Pcto\Views\Telecamere\Delete.cshtml"
       Write(Html.DisplayFor(model => model.num_salvati));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    \r\n    <form asp-action=\"Delete\">\r\n        <input type=\"hidden\" asp-for=\"id\" />\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\r\n        <a asp-action=\"Index\">Back to List</a>\r\n    </form>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Api_Pcto.Telecamera> Html { get; private set; }
    }
}
#pragma warning restore 1591
