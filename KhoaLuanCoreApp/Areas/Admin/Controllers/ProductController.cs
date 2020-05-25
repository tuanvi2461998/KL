using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhoaLuanCoreApp.Application.Interface;
using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Ultilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KhoaLuanCoreApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService,
             IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region AJAX API
        [HttpGet]
        public IActionResult GetAll()
        {
           var model = _productService.GetAll();
            return new OkObjectResult(model);    
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var model = _productCategoryService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productService.Delete(id);
                _productService.Save();
                return new OkObjectResult(id);
            }
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
                if (productVm.Id == 0)
                {
                    _productService.Add(productVm);
                }
                else
                {
                    _productService.Update(productVm);
                }
                _productService.Save();
                return new OkObjectResult(productVm);
            }
        }
        #endregion
    }
}