﻿using KhoaLuanCoreApp.Data.EF.Repositories;
using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Ultilities.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaLuanCoreApp.Data.EF
{
     public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "320A, QL61, Vĩnh Hoà Hiệp, Châu Thành, tỉnh Kiên Giang",
                    Email = "tuanvi.2461998@gmail.com",
                    Name = "Shoes Shop",
                    Phone = "0328328190",
                    Status = Status.Active,
                    Website = "http://shoesshop.com",
                    Lat = 10508396,
                    Lng = 954583
                });
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Quyền",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Quản lý sản phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Loại sản phẩm",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Đơn hàng",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = "fa-chevron-down"  },

                    new Function() {Id = "CONTENT",Name = "Nội dung",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
                    new Function() {Id = "BLOG",Name = "Bài viết",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/blog/index",IconCss = "fa-table"  },
                    new Function() {Id = "PAGE",Name = "Trang",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/page/index",IconCss = "fa-table"  },

                    new Function() {Id = "UTILITY",Name = "Tiện ích",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "FOOTER",Name = "Footer",ParentId = "UTILITY",SortOrder = 1,Status = Status.Active,URL = "/admin/footer/index",IconCss = "fa-clone"  },
                    new Function() {Id = "FEEDBACK",Name = "Phản hồi",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/admin/feedback/index",IconCss = "fa-clone"  },
                    new Function() {Id = "ANNOUNCEMENT",Name = "Thông báo",ParentId = "UTILITY",SortOrder = 3,Status = Status.Active,URL = "/admin/announcement/index",IconCss = "fa-clone"  },
                    new Function() {Id = "CONTACT",Name = "Contact",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = "fa-clone"  },
                    new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 5,Status = Status.Active,URL = "/admin/slide/index",IconCss = "fa-clone"  }
                });
            }

            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }

            if (_context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                     new Color() {Name="Đen", Code="#000000" },
                    new Color() {Name="Trắng", Code="#FFFFFF"},
                    new Color() {Name="Đỏ", Code="#ff0000" },
                    new Color() {Name="Xanh", Code="#1000ff" },
                };
                _context.Colors.AddRange(listColor);
            }



            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name="Slide 1",Image="/client-side/images/slider/slide-1.jpg",Url="#",DisplayOrder = 0,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/slider/slide-2.jpg",Url="#",DisplayOrder = 1,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/slider/slide-3.jpg",Url="#",DisplayOrder = 2,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 1",Image="/client-side/images/brand1.png",Url="#",DisplayOrder = 1,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/brand2.png",Url="#",DisplayOrder = 2,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/brand3.png",Url="#",DisplayOrder = 3,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 4",Image="/client-side/images/brand4.png",Url="#",DisplayOrder = 4,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 5",Image="/client-side/images/brand5.png",Url="#",DisplayOrder = 5,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 6",Image="/client-side/images/brand6.png",Url="#",DisplayOrder = 6,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 7",Image="/client-side/images/brand7.png",Url="#",DisplayOrder = 7,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 8",Image="/client-side/images/brand8.png",Url="#",DisplayOrder = 8,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 9",Image="/client-side/images/brand9.png",Url="#",DisplayOrder = 9,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 10",Image="/client-side/images/brand10.png",Url="#",DisplayOrder = 10,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 11",Image="/client-side/images/brand11.png",Url="#",DisplayOrder = 11,GroupAlias = "brand",Status = true },
                };
                _context.Slides.AddRange(slides);
            }


            if (_context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="35" },
                    new Size() { Name="36"},
                    new Size() { Name="37" },
                    new Size() { Name="38" },
                    new Size() { Name="39" },
                    new Size() { Name="40" },
                    new Size() { Name="41" },
                    new Size() { Name="42" },
                    new Size() { Name="43" },
                    new Size() { Name="44" }
                };
                _context.Sizes.AddRange(listSize);
            }

            if (_context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Giày nam",ParentId = null,SeoAlias="giaynam",Status=Status.Active,SortOrder=1,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Product 1",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product1",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 2",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product2",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 3",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product3",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 4",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product4",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 5",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product5",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 6",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product6",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 7",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product7",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 8",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product8",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 9",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product9",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 10",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product10",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }
                    },

                    new ProductCategory() { Name="Giày nữ",ParentId = null,SeoAlias="giaynu",Status=Status.Active ,SortOrder=3,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Product 11",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product11",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 12",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product12",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 13",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product13",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 14",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product14",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 15",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product15",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 16",DateCreated=DateTime.Now, Image="/client-side/images/products/product-1.jpg",SeoAlias="product16",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 17",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product17",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 18",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product18",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 19",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product19",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "Product 20",DateCreated=DateTime.Now,Image="/client-side/images/products/product-1.jpg",SeoAlias="product20",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                        }}
                };
                _context.ProductCategories.AddRange(listProductCategory);
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Home's title",
                    Value1 = "Tedu Shop home",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Home Keyword",
                    Value1 = "shopping, sales",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Home Description",
                    Value1 = "Home tedu",
                    Status = Status.Active
                });
            }
            await _context.SaveChangesAsync();

        }
    }
}
