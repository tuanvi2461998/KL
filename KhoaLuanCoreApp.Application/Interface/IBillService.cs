using KhoaLuanCoreApp.Application.ViewModels.Product;
using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Ultilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhoaLuanCoreApp.Application.Interface
{
    public interface IBillService
    {
        void Create(BillViewModel billVm);
        void Update(BillViewModel billVm);

        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        BillViewModel GetDetail(int billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(int productId, int billId, int colorId, int sizeId);

        void UpdateStatus(int orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(int billId);

        List<ColorViewModel> GetColors();

        List<SizeViewModel> GetSizes();

        void Save();
    }
}
