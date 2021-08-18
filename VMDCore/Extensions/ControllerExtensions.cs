using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Classes;

namespace VMDCore.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddFlashMessage(this Controller controller, FlashMessage message)
        {
            // это то же самое, что и с расширением HtmlHelper; мы можем снова работать с пустым списком
            var list = controller.TempData.DeserializeToObject<List<FlashMessage>>("Messages");

            list.Add(message);

            // сохраняем расширенный список сообщений обратно в формат JSON и в коллекцию TempData
            controller.TempData.SerializeObject(list, "Messages");
        }

        public static void AddFlashMessage(this Controller controller, string message, FlashMessageType messageType)
        {
            controller.AddFlashMessage(new FlashMessage(message, messageType));
        }

        public static void AddDebugMessage(this Controller controller, Exception ex)
        {
            string message = ex.Message;
            if (ex.GetBaseException().Message != message)
                message += Environment.NewLine + ex.GetBaseException().Message;
            AddFlashMessage(controller, new FlashMessage(message, FlashMessageType.Danger));
        }
    }
}
