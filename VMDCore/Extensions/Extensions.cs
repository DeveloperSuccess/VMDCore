using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMDCore.Extensions
{
    public static class Extensions
    {
        // где T: new () указывает, что данный тип должен предоставить непараметрический конструктор 
        public static T DeserializeToObject<T>(this ITempDataDictionary tempData, string key) where T : new()
        {
            // мы ищем все, что хранится под заданным ключом TempData
            string entry = tempData[key]?.ToString();

            // если мы находим что-то под этим ключом, мы десериализуем его из JSON в этот тип 
            // если нет, мы возвращаем новый экземпляр этого типа (будет полезен в расширении контроллера) 
            T result = entry == null
                ? new T()
                : JsonConvert.DeserializeObject<T>(entry);

            return result;
        }

        public static void SerializeObject<T>(this ITempDataDictionary tempData, T obj, string key)
        {
            tempData[key] = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

    /*    public static IServiceCollection AddImageProcessing(this IServiceCollection services)
        {
            return ImageManager.ConfigureImageProcessingMiddleWare(services);
        } */

        public static IHtmlContent Script(this IHtmlHelper helper, Func<object, HelperResult> template)
        {
            helper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return new StringHtmlContent(string.Empty);
        }

        public static IHtmlContent RenderScripts(this IHtmlHelper helper)
        {
            foreach (object key in helper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    if (helper.ViewContext.HttpContext.Items[key] is Func<object, HelperResult> template)
                    {
                        helper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return new StringHtmlContent(string.Empty);
        }


    }
}
