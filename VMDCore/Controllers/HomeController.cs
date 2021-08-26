using Microsoft.AspNetCore.Mvc;
using System;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Models;

namespace VMDCore.Controllers
{
    public class HomeController : Controller
    {
        IDrinkManager drinkManager;
        ICoinManager coinManager;
        IOperationManager operationManager;

        public HomeController(IDrinkManager drinkManager, ICoinManager coinManager, IOperationManager operationManager)
        {
            this.drinkManager = drinkManager;
            this.coinManager = coinManager;
            this.operationManager = operationManager;
        }

        [HttpGet]
        public ActionResult Index(HomeIndexViewModel model)
        {
            model.Drinks = drinkManager.GetAllDrink();
            model.Coins = coinManager.GetAllCoin();
            model.Operation = operationManager.FindOperationById(1);
            return View(model);
        }

        /// <summary>
        /// Купить напиток
        /// </summary>
        /// <param name="id">ID напитка</param>
        /// <returns></returns>
        public ActionResult BuyDrink(int id)
        {
            var operation = operationManager.FindOperationById(1);
            var drink = drinkManager.FindDrinkById(id);
            if (drink.Stock > 0 && operation.Balance >= drink.Price)
            {
                operation.Balance -= drink.Price;
                drink.Stock -= 1;
                drinkManager.SaveDrink(drink);
                operationManager.SaveOperation(operation);
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
            var operation = operationManager.FindOperationById(1);
            var coin = coinManager.FindCoinById(value);
            // Если монеты доступны для ввода и их в автомате меньше 200
            if (coin.isAvailable == true && coin.NumberCoins < 200)
            {
                coin.NumberCoins += 1;
                operation.Balance += coin.Value;
                coinManager.SaveCoin(coin);
                operationManager.SaveOperation(operation);
            };
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Получить сдачу
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChange()
        {
            var operation = operationManager.FindOperationById(1);
            var coin = coinManager.GetAllCoin();

            // сортируем монеты в наличии по номиналу от больего к меньшьшему
            coin.Sort((x, y) => y.Value.CompareTo(x.Value));

            if (operation.Balance > 0)
            {
                foreach (var p in coin)
                {
                    // Если баланс монет с текущим номиналом больше суммы монет с текущим номиналом
                    if (p.Value <= operation.Balance && p.NumberCoins > 0)
                    {
                        // Сколько монет текущего номинала максимум Нужно выдать
                        int maxNumberCoinsTemp = (int)Math.Floor((double)(operation.Balance / p.Value));
                        // Если монет в хранилище больше чем необх. кол-во, выдаем необх кол-во
                        // в противном случае отдаем все.
                        if (p.NumberCoins > maxNumberCoinsTemp)
                        {
                            p.NumberCoins -= maxNumberCoinsTemp;

                            // Выдаем монетки и списываем с баланса
                            operation.Balance -= maxNumberCoinsTemp * p.Value;
                        } else
                        {
                            p.NumberCoins -= p.NumberCoins;

                            // Выдаем монетки и списываем с баланса
                            operation.Balance -= p.NumberCoins * p.Value;
                        }
                        
                        // обновляем монеты в бд
                        coinManager.SaveCoin(p);
                        operationManager.SaveOperation(operation);
                        if (operation.Balance == 0)
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
