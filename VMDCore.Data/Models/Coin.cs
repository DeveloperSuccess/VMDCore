using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VMDCore.Data.Models
{
    public class Coin
    {
        public Coin()
        {
            isAvailable = true;
            NumberCoins = 0;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None), Key()]
        [Required(ErrorMessage = "Введите номинал монеты")]
        [Display(Name = "Номинал монеты")]
        [Range(0, int.MaxValue, ErrorMessage = "Номинал монеты не может быть отрицательным")]
        public int Value { get; set; }

        [Required(ErrorMessage = "Введите количество доступных монет в аппарате")]
        [Display(Name = "Количество доступных монет в аппарате")]
        [Range(0, 200, ErrorMessage = "Количество монет в аппарате может быть от 0 до 200")]
        public int NumberCoins { get; set; }

        [Display(Name = "Доступность для ввода")]
        public bool isAvailable { get; set; }

        [NotMapped]
        [Display(Name = "Сумма монет")]
        public int SumCoins => Value * NumberCoins;
    }
}
