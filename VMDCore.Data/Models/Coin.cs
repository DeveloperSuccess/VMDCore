using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMDCore.Data.Models
{
    public class Coin
    {
        public Coin()
        {
            isAvailable = true;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None), Key()]
        [Required(ErrorMessage = "Введите номинал монеты")]
        [Display(Name = "Номинал монеты")]
        [Range(0, int.MaxValue, ErrorMessage = "Номинал монеты не может быть отрицательным")]
        public int Value { get; set; }

        [Display(Name = "Доступность для ввода")]
        public bool isAvailable { get; set; }
    }
}
