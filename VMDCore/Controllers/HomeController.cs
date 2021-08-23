using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Models;

namespace VMDCore.Controllers
{
    public class HomeController : Controller
    {
        IDrinkManager drinkManager;
        ICoinManager coinManager;

        public HomeController(IDrinkManager drinkManager, ICoinManager coinManager)
        {
            this.drinkManager = drinkManager;
            this.coinManager = coinManager;
        }

        public ActionResult Index(HomeIndexViewModel model)
        {
            model.Drinks = drinkManager.GetAllDrink();
            model.Coins = coinManager.GetAllCoin();
            return View(model);
        }       
    }
}
