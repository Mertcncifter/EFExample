using EFExample.Application.Models.Category;
using EFExample.Domain.Entities;
using EFExample.Domain.Enums;
using EFExample.Repository.EFRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFExample.Application.Services
{

    public interface ICategoryService
    {
        public List<GetCategoryModel> List(Expression<Func<Category, bool>> predicate);
        public GetCategoryModel GetById(int id);
        public void Add(AddCategoryModel model);
        public void Update(UpdateCategoryModel model);
        public void Delete(int id);
        public GetCategoryModel Get(Expression<Func<Category, bool>> predicate);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }
        public void Add(AddCategoryModel model)
        {
            Category entity = new Category()
            {
                CreatedDate = DateTime.Now,
                Statu = (StatusType)model.StatusType,
                ModifiedDate = DateTime.Now,
                Name = model.Name
            };

            _categoryRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            var deleteCategory = _categoryRepository.GetById(id);

            _categoryRepository.Delete(deleteCategory);
        }

        public GetCategoryModel Get(Expression<Func<Category, bool>> predicate)
        {
            var category = _categoryRepository.Get(predicate);
            GetCategoryModel getCategory = new GetCategoryModel()
            {
                Id = category.Id,
                StatusType = (int)category.Statu,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
            };

            return getCategory;
        }

        public GetCategoryModel GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            GetCategoryModel getCategory = new GetCategoryModel()
            {
                Id = category.Id,
                StatusType = (int)category.Statu,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
            };

            return getCategory;
        }

        // WİTH INCLUDE
        public GetCategoryModel GetByIdWithInclude(int id)
        {
            var category = _categoryRepository.GetById(id, includeProperties: x => x.Products);
            GetCategoryModel getCategory = new GetCategoryModel()
            {
                Id = category.Id,
                StatusType = (int)category.Statu,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
            };

            return getCategory;
        }

        public List<GetCategoryModel> List(Expression<Func<Category, bool>> predicate)
        {
            var listCategory = _categoryRepository.GetList(predicate);

            List<GetCategoryModel> getCategoryModels = new List<GetCategoryModel>();

            foreach (var category in listCategory)
            {

                GetCategoryModel getCategory = new GetCategoryModel()
                {
                    Id = category.Id,
                    StatusType = (int)category.Statu,
                    Name = category.Name,
                    CreatedDate = category.CreatedDate,
                    ModifiedDate = category.ModifiedDate,
                };

                getCategoryModels.Add(getCategory);
            }

            return getCategoryModels;
        }

        public void Update(UpdateCategoryModel model)
        {
            var updateCategory = _categoryRepository.GetById(model.Id);
            updateCategory.Name = model.Name;
            updateCategory.Statu = (StatusType)model.StatusType;
            updateCategory.ModifiedDate = DateTime.Now;

            _categoryRepository.Update(updateCategory);
        }
    }
}
