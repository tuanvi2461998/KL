using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.Interfaces;
using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory : DomainEntity<int>, IHasSeoMetaData, ISwitchable, ISortable, IDateTracking
    {
        //
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        //
        public ProductCategory(string name, string description, int? parentId, int? homeOrder,
           string image, bool? homeFlag, int sortOrder, Status status, string seoAlias,string seoDescription)
        {
            Name = name;
            Description = description;
            ParentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            SortOrder = sortOrder;
            Status = status;
            SeoAlias = seoAlias;
            SeoDescription = seoDescription;
        }


        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public string SeoAlias { set; get; }
        public string SeoDescription { set; get; }
        public Status Status { set; get; }
        public int SortOrder { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public virtual ICollection<Product> Products { get; set; } // ICollection dùng để điều chỉnh 
    }
}