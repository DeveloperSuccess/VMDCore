using Microsoft.AspNetCore.Mvc;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Models;

namespace VMDCore.Controllers
{
    public class HomeController : Controller
    {
        IDrinkManager drinkManager;
        ICoinManager coinManager;
        HomeIndexViewModel modelIndex;

        public HomeController(IDrinkManager drinkManager, ICoinManager coinManager)
        {
            this.drinkManager = drinkManager;
            this.coinManager = coinManager;            
        }

        public ActionResult Index(HomeIndexViewModel model)
        {
            model.Drinks = drinkManager.GetAllDrink();
            model.Coins = coinManager.GetAllCoin();
            modelIndex = model;
            return View(modelIndex);
        }

        /// <summary>
        /// Купить напиток
        /// </summary>
        /// <param name="id">ID напитка</param>
        /// <returns></returns>
        public ActionResult BuyDrink(int id, HomeIndexViewModel model)
        {
            var drink = drinkManager.FindDrinkById(id);
            if (drink.Stock > 0 && model.Balance >= drink.Price)
            {
                model.Balance -= drink.Price;
                drink.Stock -= 1;
                drinkManager.SaveDrink(drink);
            };
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Добавить монету на баланс
        /// </summary>
        /// <param name="value">Номинал (id) монеты</param>
        /// <returns></returns>
        public ActionResult AddCoin(int value)
        {
            var coin = coinManager.FindCoinById(value);
            // Если монеты доступны для ввода и их в автомате меньше 200
            if (coin.isAvailable == true && coin.NumberCoins < 200)
            {
                coin.NumberCoins += 1;
                modelIndex.Balance += coin.Value;
                coinManager.SaveCoin(coin);
            };
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Получить сдачу
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChange(HomeIndexViewModel model)
        {
            var coin = coinManager.GetAllCoin();

            // сортируем монеты в наличии по номиналу от больего к меньшьшему
            coin.Sort((x, y) => y.Value.CompareTo(x.Value));

            if (model.Balance > 0)
            {
                foreach (var p in coin)
                {
                    // Если баланс больше суммы монет с текущим номиналом
                    if (model.Balance >= p.SumCoins)
                    {
                        // Сколько монет текущего номинала можем выдать 
                        int numberCoinsTemp = model.Balance % p.SumCoins;
                        // Сколько списать с баланса после выдачи монет
                        int sumCoinsTemp = numberCoinsTemp;
                        // Выдаем монетки и списываем с баланса
                        model.Balance -= numberCoinsTemp;
                        // Уменьшаем количество монет в автомате
                        p.NumberCoins -= numberCoinsTemp;
                        // обновляем монеты в бд
                        coinManager.SaveCoin(p);
                        if (model.Balance > 0)
                        {
                            break;
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
