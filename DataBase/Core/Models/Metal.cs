using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Core.Models
{
    [Table(nameof(Metal))]
    public class Metal
    {
        [Key]
        public string CodMet { get; set; }

        [Required]
        public string NameMet { get; set; }

        public ICollection<UserMetals> UserMetals { get; set; }
    }
}
