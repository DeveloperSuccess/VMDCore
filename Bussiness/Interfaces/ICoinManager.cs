using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Interfaces
{
    public interface ICoinManager
    {
        Coin FindCoinById(int id);
        void SaveCoin(Coin coin);
        void SaveCoinImage(Coin coin, IFormFile image);
        void RemoveThumbnailFile(int drinkId);
        List<Coin> GetAllCoin();
        bool ExistsThumbnailFile(int drinkId);
    }

}
