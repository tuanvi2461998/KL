﻿using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Application.Interface
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();
        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);

        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        ProductViewModel GetById(int id);
        void Save();
        void ImportExcel(string filePath, int categoryId);
        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);

        void AddImages(int productId, string[] images);

        List<ProductImageViewModel> GetImages(int productId);

        List<ProductViewModel> GetLastest(int top);

        List<ProductViewModel> GetHotProduct(int top);
        List<ProductViewModel> GetRelatedProducts(int id, int top);

        List<ProductViewModel> GetUpsellProducts(int top);
    }
}
