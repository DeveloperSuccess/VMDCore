using ExpressiveAnnotations.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Classes;
using VMDCore.Data.Models;

namespace VMDCore.Models
{

    public class ManageDrinkViewModel
    {
        public Drink Drink { get; set; }

        [RequiredIf("ExistsThumbnailFile == false", ErrorMessage = "Выберите изображение напитка!!!")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".bmp", ".gif", ".jpeg" })]
        [Display(Name = "Выберите изображение напитка")]
        public IFormFile UploadedImage { get; set; }

        // Заголовок формы
        public string FormCaption { get; set; }

        public ManageDrinkViewModel()
        {
            Drink = new Drink();
        }

        public bool ExistsThumbnailFile { get; set; }


    }
}
