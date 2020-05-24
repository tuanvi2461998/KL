using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.IRepositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory,int>
    {
       
        List<ProductCategory> GetByAlias(string alias);
    }
}
