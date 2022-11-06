using EFExample.Application.Models.Product;
using EFExample.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EFExample.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(ILogger<ProductController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = _productService.List(null);
            return View(model);
        }

        public IActionResult Create()
        {
            var categories = _categoryService.List(null);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddProductModel model)
        {
            if (model != null)
            {
                _productService.Add(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var categories = _categoryService.List(null);
            var data = _productService.GetById(id);

            ViewBag.Categories = new SelectList(categories, "Id", "Name", data.CategoryId);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            UpdateProductModel model = new UpdateProductModel
            {
                Id = data.Id,
                Name = data.Name,
                StatusType = data.StatusType,
                Stock = data.Stock,
                CategoryId = data.CategoryId,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateProductModel model)
        {
            if (model != null)
            {
                _productService.Update(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
