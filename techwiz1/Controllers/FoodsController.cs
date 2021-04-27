using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechWizProject.Models;
using TechWizProject.Services;

namespace TechWizProject.Controllers
{

    [Route("foods")]
    public class FoodsController : Controller
    {
        private FoodService foodService;

        private IWebHostEnvironment webHostEnvironment;

        private IFoodsService foodsService;
        public FoodsController(FoodService _foodService, IFoodsService _foodsService, IWebHostEnvironment webHostEnvironment)
        {

            this.foodService = _foodService;
            this.foodsService = _foodsService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("index")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View(await foodService.GetAllFood());
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id, int? RestaurantId, int? CategoryId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await foodService.GetFoodDetail(id);
            var restaurantAndCategory = await foodService.GetRestaurantAndCategory(id, RestaurantId, CategoryId);
            string[] result = restaurantAndCategory.Split(new char[] { '-' });

            ViewBag.restaurant = result[0];
            ViewBag.category = result[1];
            if (food == null)
            {
                return NotFound();
            }

            return View("details", food);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View("create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("Id,RestaurantId,CategoryId,Name,Description,Price,Images,Status,CreatedAt,UpdatedAt")] Food food)
        {
            if (!ModelState.IsValid) return View("create", food);
            food.CreatedAt = DateTime.Now;
            food.UpdatedAt = DateTime.Now;
            food.Status = 1;
            await foodService.Create(food);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await foodService.Edit(id);
            ViewBag.restaurants = await foodService.GetRestaurant();
            ViewBag.categories = await foodService.GetCategory();
            if (food == null)
            {
                return NotFound();
            }
            return View("edit", food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RestaurantId,CategoryId,Name,Description,Price,Images,Status,CreatedAt,UpdatedAt")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View("edit", food);
            try
            {
                food.UpdatedAt = DateTime.Now;
                await foodService.Update(food);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!foodService.FoodExists(food.Id))
                {
                    return NotFound();
                }
                throw;
                
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await foodService.GetFoodDetail(id);
            if (food == null)
            {
                return NotFound();
            }
            food.UpdatedAt = DateTime.Now;
            food.Status = 0;
            await foodService.Update(food);
            return RedirectToAction("index");
        }

        [Route("menu")]
        public async Task<IActionResult> Index([FromQuery(Name = "page")] int page)
        {
            var loadFood = await foodsService.LoadFoods();
            Load(page, loadFood);
            return View("Menu");
        }
        public void Load(int page, List<Food> foods)
        {
            HttpContext.Session.SetString("pageNumber", page.ToString());
            var paging = foodsService.Paging(page, foods);
            ViewBag.paging = paging;
            ViewBag.pageNumber = paging.amountOfPages;
            ViewBag.foods = paging.foods;
            ViewBag.currentPage = HttpContext.Session.GetString("pageNumber");
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery(Name = "keyword")] string keyword, [FromQuery(Name = "page")] int page, [FromQuery(Name = "categoryId")] int categoryId, [FromQuery(Name = "restaurantId")] int restaurantId)
        {
            var search = await foodsService.SearchByFoodName(keyword);
            if (categoryId != 0)
            {
                search = await foodsService.FilterByCategories(keyword, categoryId);
                ViewBag.categoryId = categoryId;
                if (restaurantId != 0)
                {
                    search = await foodsService.SearchByFoodName(keyword, categoryId, restaurantId);
                    ViewBag.categoryId = categoryId;
                    ViewBag.restaurantId = restaurantId;
                }
            }
            else if (restaurantId != 0)
            {
                search = await foodsService.FilterByRestaurants(keyword, restaurantId);
                ViewBag.restaurantId = restaurantId;
                if (categoryId != 0)
                {
                    search = await foodsService.SearchByFoodName(keyword, categoryId, restaurantId);
                    ViewBag.categoryId = categoryId;
                    ViewBag.restaurantId = restaurantId;
                }
            }

            Load(page, search);

            ViewBag.keyword = keyword;

            if (ViewBag.pageNumber == 0)
            {
                ViewBag.message = "The dish was not found";
                return View("Menu");
            }
            else if (page == 0)
            {
                return RedirectToAction("menu", "StatusCode",
             new { statusCode = 404 });
            }
            else if (page > ViewBag.pageNumber)
            {
                return RedirectToAction("menu", "StatusCode",
             new { statusCode = 404 });
            }
            return View("Menu");
        }
    }
}
