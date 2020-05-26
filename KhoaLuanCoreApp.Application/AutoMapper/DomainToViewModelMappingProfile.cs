using AutoMapper;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Application.ViewModels.System;
using KhoaLuanCoreApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
        }
    }
}
