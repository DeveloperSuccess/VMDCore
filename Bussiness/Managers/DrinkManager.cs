using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Managers
{

    public class DrinkManager : IDrinkManager
    {
        private IDrinkRepository drinkRepository;

        private const string DrinkImagesPath = "wwwroot/images/drinks/";
        private IImageManager imageManager = new ImageManager(DrinkImagesPath);
        private const int DrinkHeightThumbnailSize = 350;
        private const int DrinkWidthThumbnailSize = 320;

        public DrinkManager(IDrinkRepository drinkRepository)
        {
            this.drinkRepository = drinkRepository;
        }

        public List<Drink> GetAllDrink()
        {
            return drinkRepository.GetAll();
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

        private string GetThumbnailFileName(int drinkId, bool full = true)
        {
            string result = $"{drinkId}_thumb";
            if (full)
                result = DrinkImagesPath + result + ".png";
            return result;
        }

        public bool ExistsThumbnailFile(int drinkId)
        {
            string PathFile = GetThumbnailFileName(drinkId);
            return File.Exists(PathFile);
        }

        public void RemoveThumbnailFile(int drinkId)
        {
            string thumbFileName = GetThumbnailFileName(drinkId);
            if (File.Exists(thumbFileName))
                File.Delete(thumbFileName);
        }

        public void SaveDrinkImage(Drink drink, IFormFile image)
        {

            if (image == null)
                throw new Exception("Не удалось загрузить изображение!");

            // сохраняем изображение как миниатюру
            imageManager.SaveImage(image,
                GetThumbnailFileName(drink.DrinkId, full: false),
                ImageManager.ImageExtension.Png,
                DrinkWidthThumbnailSize, DrinkHeightThumbnailSize);


            drinkRepository.Update(drink);
        }

        private void CleanDrink(int drinkId, bool removeImages = true)
        {
            if (removeImages)
            {
                drinkRepository.Delete(drinkId);
                RemoveThumbnailFile(drinkId);
            }
        }
    }
}