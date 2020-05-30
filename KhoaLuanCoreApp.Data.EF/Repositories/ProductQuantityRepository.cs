using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.EF.Repositories
{
    public class ProductQuantityRepository : EFRepository<ProductQuantity, int>, IProductQuantityRepository
    {
        public ProductQuantityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
