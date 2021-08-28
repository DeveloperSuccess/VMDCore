using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Interfaces
{
    public interface IDrinkManager
    {
        Drink FindDrinkById(int id);
        void SaveDrink(Drink drink);
        void SaveDrinkImage(Drink drink, IFormFile image);
        void RemoveThumbnailFile(int drinkId);
        List<Drink> GetAllDrink();
        bool ExistsThumbnailFile(int drinkId);
        void CleanDrink(int drinkId, bool removeImages = true);
    }

}
