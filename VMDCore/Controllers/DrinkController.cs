using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
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
            };

            if (drinkManager.ExistsThumbnailFile(id))
            {
                using var stream = new MemoryStream();
                var formFile = new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
                ViewBag.ExistsThumbnailFile = "Ура";
            }

            model.UploadedImage = drinkManager.ExistsThumbnailFile(id);

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


            // сохраняем напиток и его отношения
            drinkManager.SaveDrink(model.Drink);

            drinkManager.SaveDrinkImage(model.Drink, model.UploadedImage);
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
