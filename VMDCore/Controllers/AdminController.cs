using Microsoft.AspNetCore.Mvc;
using System;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Data.Models;
using VMDCore.Models;

namespace VMDCore.Controllers
{
    public class AdminController : Controller
    {
        IDrinkManager drinkManager;
        ICoinManager coinManager;

        public AdminController(IDrinkManager drinkManager, ICoinManager coinManager)
        {
            this.drinkManager = drinkManager;
            this.coinManager = coinManager;
        }

        // GET: AdminController
        public ActionResult Index(AdminViewModel model)
        {
            model.Drinks = drinkManager.GetAllDrink();
            model.Coins = coinManager.GetAllCoin();
            return View(model);
        }

        [HttpGet]
        public IActionResult ManageCoin(int id)
        {
            var model = new ManageCoinViewModel
            {
                FormCaption = id == 0
                     ? "Добавить монету"
                     : "Редактировать монету",
                ExistsThumbnailFile = coinManager.ExistsThumbnailFile(id)
            };

            //  model.ExistsThumbnailFile = coinManager.ExistsThumbnailFile(model.Coin.CoinId);


            model.Coin = id != 0
                ? coinManager.FindCoinById(id)
                    ?? throw new NullReferenceException("Монета не найдена.")
                : new Coin();

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageCoin(ManageCoinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Coin.Value == 0
                   ? "Добавить монету"
                    : "Редактировать монету";
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ManageCoin", model) });
            }

            if (model.ExistsThumbnailFile)
            {
                if (model.UploadedImage != null)
                {
                    coinManager.SaveCoin(model.Coin);
                    coinManager.SaveCoinImage(model.Coin, model.UploadedImage);
                }
                else
                {
                    coinManager.SaveCoin(model.Coin);
                }
            }
            else
            {
                if (model.UploadedImage != null)
                {
                    coinManager.SaveCoin(model.Coin);
                    coinManager.SaveCoinImage(model.Coin, model.UploadedImage);
                }
            }

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCoin", coinManager.GetAllCoin()) });
        }

        // POST: Admin/DeleteConfirmedCoin/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedCoin(int value)
        {
            coinManager.CleanCoin(value, true);

            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllCoin", coinManager.GetAllCoin()) });
        }


        [HttpGet]
        public IActionResult ManageDrink(int id)
        {
            var model = new ManageDrinkViewModel
            {
                FormCaption = id == 0
                     ? "Добавить напиток"
                     : "Редактировать напиток",
                ExistsThumbnailFile = drinkManager.ExistsThumbnailFile(id)
            };

            model.Drink = id != 0
                ? drinkManager.FindDrinkById(id)
                    ?? throw new NullReferenceException("Напиток не найден.")
                : new Drink();

            return View(model);
        }

        [HttpPost]
        public IActionResult ManageDrink(ManageDrinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Drink.DrinkId == 0
                   ? "Добавить напиток"
                    : "Редактировать напиток";
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "ManageDrink", model) });
            }

            if (model.ExistsThumbnailFile)
            {
                if (model.UploadedImage != null)
                {
                    drinkManager.SaveDrink(model.Drink);
                    drinkManager.SaveDrinkImage(model.Drink, model.UploadedImage);
                }
                else
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

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllDrink", drinkManager.GetAllDrink()) });
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedDrink(int id)
        {
            drinkManager.CleanDrink(id, true);
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAllDrink", drinkManager.GetAllDrink()) });
        }

        public void DeleteImageDrink(int drinkId)
        {
            drinkManager.RemoveThumbnailFile(drinkId);
        }

        public void DeleteImageCoin(int value)
        {
            coinManager.RemoveThumbnailFile(value);
        }
    }
}
