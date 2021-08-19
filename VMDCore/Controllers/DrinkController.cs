using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        public IActionResult Manage(int id)
        {
            var model = new ManageDrinkViewModel
            {
                FormCaption = id == 0
                    ? "Добавить напиток"
                    : "Редактировать напиток",
            };

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
            this.AddFlashMessage("Напиток был успешно сохранен.", FlashMessageType.Success);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
