using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Classes;
using VMDCore.Data.Models;
using VMDCore.Extensions;
using VMDCore.Models;

namespace VMDCore.Controllers
{

    public class DrinkController : Controller
    {
        IDrinkManager drinkManager;
        

        public DrinkController(IDrinkManager drinkManager)
        {
            this.drinkManager = drinkManager;            
        }

        public IActionResult Index(DrinkIndexViewModel model)
        {
            model.Drinks = drinkManager.GetAllDrink();
            return View(model);
        }

        [HttpGet]
        public IActionResult Manage(int id)
        {
            var model = new ManageDrinkViewModel
            {
                FormCaption = id == 0
                     ? "Добавить напиток"
                     : "Редактировать напиток",
                ExistsThumbnailFile = drinkManager.ExistsThumbnailFile(id)
            };

            //  model.ExistsThumbnailFile = drinkManager.ExistsThumbnailFile(model.Drink.DrinkId);


            model.Drink = id != 0
                ? drinkManager.FindDrinkById(id)
                    ?? throw new NullReferenceException("Напиток не найден.")
                : new Drink();

            return View(model);
        }

        [HttpPost]
        public IActionResult Manage(ManageDrinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Drink.DrinkId == 0
                   ? "Добавить напиток"
                    : "Редактировать напиток";
                this.AddFlashMessage("Неверные параметры напитка!", FlashMessageType.Danger);
                return View(model);
            }

            if (model.ExistsThumbnailFile)
            {
                if (model.UploadedImage != null)
                {
                    drinkManager.SaveDrink(model.Drink);
                    drinkManager.SaveDrinkImage(model.Drink, model.UploadedImage);
                } else
                {
                    drinkManager.SaveDrink(model.Drink);
                }
            }
            else
            {
                if (model.UploadedImage != null)
                {
                    drinkManager.SaveDrink(model.Drink);
                    drinkManager.SaveDrinkImage(model.Drink, model.UploadedImage);
                }
            }
            this.AddFlashMessage("Напиток был успешно сохранен.", FlashMessageType.Success);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        public void DeleteImage(int drinkId)
        {
            drinkManager.RemoveThumbnailFile(drinkId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
