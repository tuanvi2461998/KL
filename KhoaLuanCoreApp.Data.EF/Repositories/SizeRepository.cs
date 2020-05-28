using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.EF.Repositories
{
    public class SizeRepository : EFRepository<Size, int>, ISizeRepository
    {
        public SizeRepository(AppDbContext context) : base(context)
        {
        }
    }
}

