using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Core.Models
{
    [Table(nameof(Сurrency))]
    public class Сurrency
    {
        [Key]
        public string Vcode { get; set; }

        [Required]
        public string Vname { get; set; }

        [Required]
        public string VEngname { get; set; }

        [Required]
        public string Vnom { get; set; }

        [Required]
        public string VcommonCode { get; set; }

        public string VnumCode { get; set; }

        public string VcharCode { get; set; }

        public ICollection<UserCurrency> UserCurrency { get; set; }
    }
}
