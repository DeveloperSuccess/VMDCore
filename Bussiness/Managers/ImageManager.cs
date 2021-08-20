using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Web.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;

namespace VMDCore.Bussiness.Managers
{
    public class ImageManager : IImageManager
    {
        public enum ImageExtension { Bmp, Gif, Jpeg, Png }

        public string OutputDirectoryPath { get; private set; }

        public ImageManager(string outputDirectoryPath) => OutputDirectoryPath = outputDirectoryPath;

        /// <summary>
        /// Метод Wrapper, сохраняющий изображение из входа.
        /// </summary>
        /// <param name="file">Необработанный файл изображения для сохранения.</param>
        /// <param name="fileName">Имя файла получившегося изображения (без расширения).</param>
        /// <param name="extension">Формат, в котором должно быть сохранено изображение.</param>
        /// <param name="width">Ширина результирующего изображения (необязательно).</param>
        /// <param name="height">Высота результирующего изображения (необязательно).</param>
        public void SaveImage(IFormFile file, string fileName, ImageExtension extension, int width = 0, int height = 0)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var img = Image.Load(stream.ToArray());

                // ImageSharp сохранит соотношение сторон по умолчанию, если какой-либо размер == 0
                ResizeImage(img, width, height);

                IImageEncoder encoder;
                switch (extension)
                {
                    case ImageExtension.Bmp:
                        encoder = new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder();
                        fileName += ".bmp";
                        break;
                    case ImageExtension.Gif:
                        encoder = new SixLabors.ImageSharp.Formats.Gif.GifEncoder();
                        fileName += ".gif";
                        break;
                    case ImageExtension.Jpeg:
                        encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder();
                        fileName += ".jpeg";
                        break;
                    case ImageExtension.Png:
                        encoder = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                        fileName += ".png";
                        break;
                    default:
                        return;
                }

                img.Save(OutputDirectoryPath + fileName, encoder);
            }
        }

        private Image ResizeImage(Image image, int width = 0, int height = 0)
        {
            // ImageSharp will preserve the aspect ratio by default if any dimension == 0
            if (width > 0 || height > 0)
                image.Mutate(x => x.Resize(
                    new ResizeOptions()
                    {
                        Size = new Size(width, height)
                    }));

            return image;
        }

        public void ResizeImage(string path, int width = 0, int height = 0)
        {
            ResizeImage(Image.Load(path), width, height).Save(path);
        }

        public static IServiceCollection ConfigureImageProcessingMiddleWare(IServiceCollection services)
        {
            services.AddImageSharp();
            return services;
        }
    }
}
