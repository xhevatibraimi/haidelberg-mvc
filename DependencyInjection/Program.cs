using System;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //INotificationService notificationService = new SMSNotificationService();
            INotificationService notificationService = new EmailNotificationService();
            notificationService.SendNotification(text: "asdasda");
            var orderService = new OrderService(notificationService);
            orderService.CreateOrder("120");
        }

        public class OrderService
        {
            private readonly INotificationService _notificationService;

            public OrderService(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public void CreateOrder(string id)
            {
                Console.WriteLine("creating order");
                _notificationService.SendNotification("order with id " + id + "is shipped");
                Console.WriteLine("logging order");
            }
        }

        public interface INotificationService
        {
            void SendNotification(string text);
        }

        public class SMSNotificationService : INotificationService
        {
            public void SendNotification(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                Console.WriteLine($"Sending SMS Notification '{text}'");
            }
        }

        public class EmailNotificationService : INotificationService
        {
            public void SendNotification(string text)
            {
                Console.WriteLine("sending email");
            }
        }
    }
}
