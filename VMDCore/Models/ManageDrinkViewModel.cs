﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Data.Models;

namespace VMDCore.Models
{
   
    public class ManageDrinkViewModel
    {
        public Drink Drink { get; set; }


       [Required]
        [Display(Name = "Выберите изображение напитка")]
        public IFormFile UploadedImage { get; set; }
        
        // Заголовок формы
        public string FormCaption { get; set; }

        public ManageDrinkViewModel()
        {
            Drink = new Drink();
        }
    }
}
