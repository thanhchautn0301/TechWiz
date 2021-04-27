using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechWizProject.TagHelpers
{
    [HtmlTargetElement("categories", TagStructure = TagStructure.NormalOrSelfClosing)]

    public class CategoriesTag : TagHelper
    {
        [HtmlAttributeNotBound]

        [ViewContext]
        public ViewContext viewContext { get; set; }

        private ICategoriesService categoriesService;

        private IHtmlHelper htmlHelper;

        public CategoriesTag(ICategoriesService _categoriesService, IHtmlHelper _htmlHelper)
        {
            categoriesService = _categoriesService;
            htmlHelper = _htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (htmlHelper as IViewContextAware).Contextualize(viewContext);
            output.TagName = "";
            var loadCategories = await categoriesService.LoadCategories();
            htmlHelper.ViewBag.categories = loadCategories;
            output.Content.SetHtmlContent(await htmlHelper.PartialAsync("TagHelpers/Categories/Index"));
        }
    }
}
