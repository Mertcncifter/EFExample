using EFExample.Application.Models.Product;
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
    public interface IProductService
    {
        public List<GetProductModel> List(Expression<Func<Product, bool>> predicate);
        public GetProductModel GetById(int id);
        public void Add(AddProductModel model);
        public void Update(UpdateProductModel model);
        public void Delete(int id);
        public GetProductModel Get(Expression<Func<Product, bool>> predicate);
    }
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;

        }
        public void Add(AddProductModel model)
        {
            Product entity = new Product()
            {
                CreatedDate = DateTime.Now,
                Statu = (StatusType)model.StatusType,
                ModifiedDate = DateTime.Now,
                Name = model.Name,
                Stock = model.Stock,
                CategoryId = model.CategoryId
            };

            _productRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            var deleteProduct = _productRepository.GetById(id);

            _productRepository.Delete(deleteProduct);
        }

        public GetProductModel Get(Expression<Func<Product, bool>> predicate)
        {
            var product = _productRepository.Get(predicate);
            GetProductModel getProduct = new GetProductModel()
            {
                Id = product.Id,
                StatusType = (int)product.Statu,
                Name = product.Name,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate,
            };

            return getProduct;
        }

        public GetProductModel GetById(int id)
        {
            var product = _productRepository.GetById(id);
            GetProductModel getProduct = new GetProductModel()
            {
                Id = product.Id,
                StatusType = (int)product.Statu,
                Name = product.Name,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate,
            };

            return getProduct;
        }

        public List<GetProductModel> List(Expression<Func<Product, bool>> predicate)
        {
            var listProduct = _productRepository.GetList(predicate);

            List<GetProductModel> getProductModels = new List<GetProductModel>();

            foreach (var product in listProduct)
            {

                GetProductModel getProduct = new GetProductModel()
                {
                    Id = product.Id,
                    StatusType = (int)product.Statu,
                    Name = product.Name,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CreatedDate = product.CreatedDate,
                    ModifiedDate = product.ModifiedDate,
                };
                getProductModels.Add(getProduct);
            }

            return getProductModels;
        }

        public void Update(UpdateProductModel model)
        {
            var updateProduct = _productRepository.GetById(model.Id);
            updateProduct.Name = model.Name;
            updateProduct.Stock = model.Stock;
            updateProduct.CategoryId = model.CategoryId;
            updateProduct.Statu = (StatusType)model.StatusType;
            updateProduct.ModifiedDate = DateTime.Now;

            _productRepository.Update(updateProduct);
        }
    }



}
