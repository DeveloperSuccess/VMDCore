using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
