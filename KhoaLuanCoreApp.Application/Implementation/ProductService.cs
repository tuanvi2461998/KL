using AutoMapper;
using AutoMapper.QueryableExtensions;
using KhoaLuanCoreApp.Application.Interface;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.IRepositories;
using KhoaLuanCoreApp.Infrastructure.Interfaces;
using KhoaLuanCoreApp.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhoaLuanCoreApp.Application.Implementation
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        private IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public ProductViewModel Add(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            _productRepository.Add(product);
            return productVm;
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll(x=>x.ProductCategory).ProjectTo<ProductViewModel>().ToList();
        }

        public PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>().ToList();

            var paginationSet = new PagedResult<ProductViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel productVm)
        {
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            _productRepository.Update(product);
        }
    }
}
