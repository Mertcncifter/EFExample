using EFExample.Application.Models.Category;
using EFExample.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFExample.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;


        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = _categoryService.List(null);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddCategoryModel model)
        {
            if (model != null)
            {
                _categoryService.Add(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var data = _categoryService.GetById(id);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            UpdateCategoryModel model = new UpdateCategoryModel
            {
                Id = data.Id,
                Name = data.Name,
                StatusType = data.StatusType,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryModel model)
        {
            if (model != null)
            {
                _categoryService.Update(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);

            return RedirectToAction("Index");
        }


    }
}
