using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Classes;

namespace VMDCore.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderFlashMessages(this IHtmlHelper helper)
        {
            // мы можем найти массив со всеми сообщениями в формате json под ключом «Сообщения», десериализуем его с помощью метода расширения
            // Примечание: если мы не сохранили никаких сообщений, метод возвращает пустой экземпляр List - что совершенно не имеет значения
            var messageList = helper.ViewContext.TempData
                .DeserializeToObject<List<FlashMessage>>("Messages");

            var html = new HtmlContentBuilder();

            // Перебираем все сообщения и генерируем HTML
            foreach (var msg in messageList)
            {
                var container = new TagBuilder("div");
                container.AddCssClass($"alert alert-{ msg.Type.ToString().ToLower() }"); // adding Bootstrap CSS
                container.InnerHtml.SetContent(msg.Message);

                html.AppendHtml(container);
            }

            return html;
        }
       
    }
}
