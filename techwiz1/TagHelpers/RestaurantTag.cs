using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechWizProject.Services;

namespace TechWizProject.Views.Shared.TagHelpers.Restaurant
{
    [HtmlTargetElement("restaurant",TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RestaurantTag : TagHelper
    {
        [HtmlAttributeNotBound]

        [ViewContext]

        public ViewContext viewContext { get; set; }

        private IHtmlHelper htmlHelper;
        private IRestaurantService restaurantService;

        public RestaurantTag(IRestaurantService _restaurantService, IHtmlHelper _htmlHelper)
        {
            restaurantService = _restaurantService;

            htmlHelper = _htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (htmlHelper as IViewContextAware).Contextualize(viewContext);
            output.TagName = "";
            var loadRestaurant = await restaurantService.LoadRestaurant();
            htmlHelper.ViewBag.restaurants = loadRestaurant;
            output.Content.SetHtmlContent(await htmlHelper.PartialAsync("TagHelpers/Restaurant/Index"));
        }
    }
}
