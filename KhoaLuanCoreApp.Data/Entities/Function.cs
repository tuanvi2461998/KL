using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.Interfaces;
using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISwitchable, ISortable //Để quản lý chức năng của Admin
    {
        //Constructor
        public Function()
        {
        }

        //
        public Function(string name, string url, string parentId, string iconcss, int sortOrder)
        {
            Name = name;
            URL = url;
            ParentId = parentId;
            IconCss = iconcss;
            Status = Status.Active;
            SortOrder = sortOrder;
        }

        //

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string URL { get; set; }

        [MaxLength(128)]
        public string ParentId { get; set; }

        public string IconCss { get; set; }

        public Status Status { get; set; }
        public int SortOrder { get; set; }
    }
}