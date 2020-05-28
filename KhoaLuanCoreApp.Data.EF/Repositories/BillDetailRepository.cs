using KhoaLuanCoreApp.Data.Entities;
using KhoaLuanCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Data.EF.Repositories
{
    public class BillDetailRepository : EFRepository<BillDetail, int>, IBillDetailRepository
    {
        public BillDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}
