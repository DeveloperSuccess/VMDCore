using VMDCore.Bussiness.Interfaces;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Managers
{

    public class DrinkManager : IDrinkManager
    {
        private IDrinkRepository drinkRepository;

        public DrinkManager(IDrinkRepository drinkRepository)
        {
            this.drinkRepository = drinkRepository;
        }

        public Drink FindDrinkById(int id)
        {
            return drinkRepository.FindById(id);
        }

        public void SaveDrink(Drink drink)
        {
            if (drink.DrinkId != 0)
            {
                drinkRepository.Update(drink);
            }
            else
            {
                drinkRepository.Insert(drink);
            }
        }
    }
}