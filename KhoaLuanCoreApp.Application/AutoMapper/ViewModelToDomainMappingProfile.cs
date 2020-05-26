using AutoMapper;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Application.ViewModels.System;
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
            //
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Name, c.Description, c.ParentId, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoAlias, c.SeoDescription));
            //
            CreateMap<ProductViewModel, Product>()
           .ConstructUsing(c => new Product(c.Name, c.CategoryId, c.Image, c.Price, c.OriginalPrice,
           c.PromotionPrice, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Unit, c.Status,
          c.SeoAlias, c.SeoDescription));
            //
            CreateMap<AppUserViewModel, AppUser>()
           .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName,
           c.Email, c.PhoneNumber, c.Avatar, c.Status));

        }
    }
}
