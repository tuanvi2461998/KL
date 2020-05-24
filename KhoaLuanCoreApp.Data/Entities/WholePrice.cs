using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("WholePrices")]   //Bảng giá bán sỉ từ số lượng bao nhiêu đến bao nhiêu thì bán bao nhiêu
    public class WholePrice : DomainEntity<int>
    {
        public int ProductId { get; set; }

        public int FromQuntity { get; set; }

        public int ToQuantity { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
