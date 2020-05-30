using AutoMapper;
using AutoMapper.QueryableExtensions;
using KhoaLuanCoreApp.Application.Interface;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.IRepositories;
using KhoaLuanCoreApp.Infrastructure.Interfaces;
using KhoaLuanCoreApp.Ultilities.Dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KhoaLuanCoreApp.Application.Implementation
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IProductQuantityRepository _productQuantityRepository;
        IProductImageRepository _productImageRepository;
        private IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository,
             IProductQuantityRepository productQuantityRepository,
        IProductImageRepository productImageRepository,
        IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _productImageRepository = productImageRepository;
            _productQuantityRepository = productQuantityRepository;
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

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 7].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 8].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
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
        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            return _productQuantityRepository.FindAll(x => x.ProductId == productId).ProjectTo<ProductQuantityViewModel>().ToList();
        }


        public List<ProductImageViewModel> GetImages(int productId)
        {
            return _productImageRepository.FindAll(x => x.ProductId == productId)
                .ProjectTo<ProductImageViewModel>().ToList();
        }

        public void AddImages(int productId, string[] images)
        {
            _productImageRepository.RemoveMultiple(_productImageRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var image in images)
            {
                _productImageRepository.Add(new ProductImage()
                {
                    Path = image,
                    ProductId = productId,
                    Caption = string.Empty
                });
            }

        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Add(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity
                });
            }
        }
    }
}
