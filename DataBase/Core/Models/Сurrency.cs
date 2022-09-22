using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Core.Models
{
    [Table(nameof(Сurrency))]
    public class Сurrency
    {
        #region id и именования волюты
        /// <summary>
        /// id волюты
        /// </summary>
        [Key]
        public Guid СurrencyId { get; set; }

        /// <summary>
        /// буквенный код волюты
        /// </summary>
        [StringLength(3)]
        public string LetterCode { get; set; }

        /// <summary>
        /// цифровой код волюты
        /// </summary>
        [StringLength(3)]
        public string DigitalCode { get; set; }

        /// <summary>
        /// название волюты
        /// </summary>
        [Required]
        public string Name { get; set; }
        #endregion

        #region курс и прогнозы
        /// <summary>
        /// курс волюты
        /// </summary>
        [Required]
        public float Сourse { get; set; }

        /// <summary>
        /// прогноз волюты
        /// </summary>
        public float Forecast { get; set; }

        #endregion

        #region даты
        /// <summary>
        /// Дата обновления информации по валюте
        /// </summary>
        [Required(ErrorMessage = "Обновления тсутствует дата")]
        public DateTime DateOfUpdate { get; set; }

        /// <summary>
        /// Дата удаления из бд информации по валюте
        /// </summary>
        [Required(ErrorMessage = "Отсутствует дата очистки данних из бд")]
        public DateTime DateDeleteInfo { get; set; }
        #endregion
    }
}
