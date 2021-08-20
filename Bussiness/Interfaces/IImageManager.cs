using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VMDCore.Bussiness.Managers.ImageManager;

namespace VMDCore.Bussiness.Interfaces
{
    interface IImageManager
    {
        void SaveImage(IFormFile file, string fileName, ImageExtension extension, int width = 0, int height = 0);
        void ResizeImage(string path, int width = 0, int height = 0);
    }
}
