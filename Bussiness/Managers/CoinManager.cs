using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Managers
{
 
    public class CoinManager : ICoinManager
    {
        private ICoinRepository coinRepository;

        private const string CoinImagesPath = "wwwroot/images/coins/";
        private IImageManager imageManager = new ImageManager(CoinImagesPath);
        private const int CoinHeightThumbnailSize = 200;
        private const int CoinWidthThumbnailSize = 200;

        public CoinManager(ICoinRepository coinRepository)
        {
            this.coinRepository = coinRepository;
        }

        public List<Coin> GetAllCoin()
        {
            return coinRepository.GetAll();
        }

        public Coin FindCoinById(int id)
        {
            return coinRepository.FindById(id);
        }

        public void SaveCoin(Coin coin)
        {
            if (coin.Value != 0)
            {
                coinRepository.Update(coin);
            }
            else
            {
                coinRepository.Insert(coin);
            }
        }

        private string GetThumbnailFileName(int coinId, bool full = true)
        {
            string result = $"{coinId}_thumb";
            if (full)
                result = CoinImagesPath + result + ".png";
            return result;
        }

        public bool ExistsThumbnailFile(int coinId)
        {
            string PathFile = GetThumbnailFileName(coinId);
            return File.Exists(PathFile);
        }

        public void RemoveThumbnailFile(int coinId)
        {
            string thumbFileName = GetThumbnailFileName(coinId);
            if (File.Exists(thumbFileName))
                File.Delete(thumbFileName);
        }

        public void SaveCoinImage(Coin coin, IFormFile image)
        {

            if (image == null)
                throw new Exception("Не удалось загрузить изображение!");

            // сохраняем изображение как миниатюру
            imageManager.SaveImage(image,
                GetThumbnailFileName(coin.Value, full: false),
                ImageManager.ImageExtension.Png,
                CoinWidthThumbnailSize, CoinHeightThumbnailSize);


            coinRepository.Update(coin);
        }

        private void CleanCoin(int coinId, bool removeImages = true)
        {
            if (removeImages)
            {
                coinRepository.Delete(coinId);
                RemoveThumbnailFile(coinId);
            }
        }
    }
}
