using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Core.Models
{
    [Table(nameof(UserMetals))]
    public class UserMetals
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public string CodMet { get; set; }
        public virtual Metal Metal { get; set; }
    }
}
