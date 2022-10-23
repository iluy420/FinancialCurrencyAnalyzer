using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Core.Models
{
    [Table(nameof(UserCurrency))]
    public class UserCurrency
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Key, Column(Order = 1)]
        public string Vcode { get; set; }
        public virtual Сurrency Сurrency { get; set; }
    }
}
