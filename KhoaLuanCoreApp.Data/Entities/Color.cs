using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("Colors")]
    public class Color : DomainEntity<int>
    {   
        [MaxLength(250)]
        public string Name { get; set; }


        [MaxLength(250)]
        public string Code { get; set; }
    }
}
