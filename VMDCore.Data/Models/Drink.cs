using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMDCore.Data.Models
{
    public class Drink
    {

        public Drink()
        {
            ImagesCount = 0;
            Hidden = false;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int DrinkId { get; set; }
              
        [Required(ErrorMessage = "Введите название напитка")]
        [StringLength(255, ErrorMessage = "Название слишком длинное")]
        [Display(Name = "Название")]
        public string Title { get; set; }
                
       
        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public int Price { get; set; }               

        [Display(Name = "Общее количество изображений напитка")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество изображений не может быть отрицательным")]
        public int ImagesCount { get; set; }

        // Можно ли заказать напиток
        [Display(Name = "Hide")]
        public bool Hidden { get; set; }
    }
}
