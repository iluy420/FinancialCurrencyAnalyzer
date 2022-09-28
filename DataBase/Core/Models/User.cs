using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Core.Models
{
    [Table(nameof(User))]
    public class User
    {

        #region id, логин, пароль
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// логин пользователя
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [MinLength(4, ErrorMessage = "Минимальная длина логина 4 символов")]
        [MaxLength(20, ErrorMessage = "Максимальная длина логина 15 символов")]
        public string Login { get; set; }

        /// <summary>
        /// пароль пользователя
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Password { get; set; }
        #endregion

        #region Email
        /// <summary>
        /// полная электронная почта пользователя
        /// </summary>
        [Required]
        public string EmailFull { get; set; }

        /// <summary>
        /// имя электронной почты пользователя
        /// </summary>
        [Required]
        public string EmailName { get; set; }

        /// <summary>
        /// домен электронной почты пользователя
        /// </summary>
        [Required]
        public string EmailDomain { get; set; }

        /// <summary>
        /// адрес электронной почты пользователя подтвержден
        /// </summary>
        [Required]
        public bool EmailConfirmed { get; set; }
        #endregion

        #region ФИО

        /// <summary>
        /// Имя 
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия 
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        #endregion

        //public ICollection<Сurrency> Сurrencys { get; set; }
    }
}
