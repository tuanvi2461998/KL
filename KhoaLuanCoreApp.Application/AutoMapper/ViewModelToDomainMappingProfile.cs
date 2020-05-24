using AutoMapper;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory());    
        }
    }
}
