using ExpressiveAnnotations.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Classes;
using VMDCore.Data.Models;

namespace VMDCore.Models
{
    public class ManageCoinViewModel
    {
        public Coin Coin { get; set; }

        [RequiredIf("ExistsThumbnailFile == false", ErrorMessage = "Выберите изображение напитка!!!")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".bmp", ".gif", ".jpeg" })]
        [Display(Name = "Выберите изображение монеты")]
        public IFormFile UploadedImage { get; set; }

        // Заголовок формы
        public string FormCaption { get; set; }

        public ManageCoinViewModel()
        {
            Coin = new Coin();
        }

        public bool ExistsThumbnailFile { get; set; }
    }
}
