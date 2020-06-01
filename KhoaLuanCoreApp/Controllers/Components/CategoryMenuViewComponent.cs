﻿using KhoaLuanCoreApp.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhoaLuanCoreApp.Controllers.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private IProductCategoryService _productCategoryService;
        public CategoryMenuViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_productCategoryService.GetAll());
        }
    }
}
