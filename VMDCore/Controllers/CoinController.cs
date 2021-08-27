using ExpressiveAnnotations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Classes;
using VMDCore.Data.Models;
using VMDCore.Extensions;
using VMDCore.Models;

namespace VMDCore.Controllers
{
    public class CoinController : Controller
    {
        ICoinManager coinManager;

        public CoinController(ICoinManager coinManager)
        {
            this.coinManager = coinManager;
        }

        public IActionResult Index(CoinIndexViewModel model)
        {            
            model.Coins = coinManager.GetAllCoin();
            return View(model);
        }

        [HttpGet]
        public IActionResult Manage(int id)
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
        public IActionResult Manage(ManageCoinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Coin.Value == 0
                   ? "Добавить монету"
                    : "Редактировать монету";
                this.AddFlashMessage("Неверные параметры монеты!", FlashMessageType.Danger);
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Manage", model) });
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
            this.AddFlashMessage("Монета была успешно сохранена.", FlashMessageType.Success);

            var modelCoins = coinManager.GetAllCoin();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAllCoin", modelCoins) });
        }


        /*
        [HttpPost]
        public IActionResult Manage(ManageCoinViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.FormCaption = model.Coin.Value == 0
                   ? "Добавить монету"
                    : "Редактировать монету";
                this.AddFlashMessage("Неверные параметры монеты!", FlashMessageType.Danger);
                return View(model);
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
            this.AddFlashMessage("Монета была успешно сохранена.", FlashMessageType.Success);

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
        */
        public void DeleteImage(int coinId)
        {
            coinManager.RemoveThumbnailFile(coinId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
