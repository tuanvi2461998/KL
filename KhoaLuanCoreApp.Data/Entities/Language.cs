using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Data.Interfaces;
using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("Languares")]
    public class Language : DomainEntity<string>, ISwitchable
    {
        [Required]
        [StringLength(128)]
        public string  Name { get; set; }
        public bool IsDefault { get; set; }
        public string Resources { get; set; }


        public Status Status { get; set; }
    }
}
