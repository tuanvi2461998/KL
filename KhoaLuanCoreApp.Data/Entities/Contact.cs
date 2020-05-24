using KhoaLuanCoreApp.Data.Enums;
using KhoaLuanCoreApp.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoaLuanCoreApp.Data.Entities
{
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>
    {
        //Constructor
        public Contact()
        {
        }
        //
        public Contact(string id, string name, string phone , string email,string website, string address, string other,
            double? lat, double? lag, Status status)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Website = website;
            Address = address;
            Other = other;
            Lat = lat;
            Lng = lag;
            Status = status;
        }
        //
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Website { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(250)]
        public string Other { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public Status Status { get; set; }
    }
}