using System;
using SuperSocket.SocketBase;

namespace SuperWebSocket.Samples.BasicConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Нажмите любую кнопку для запуска сервера");

            Console.ReadKey();
            Console.WriteLine();

            var appServer = new WebSocketServer();

            if (!appServer.Setup(2012)) //Setup with listening port
            {
                Console.WriteLine("Ошибка!");
                Console.ReadKey();
                return;
            }
            
            appServer.NewMessageReceived += new SessionHandler<WebSocketSession, string>(appServer_NewMessageReceived);

            Console.WriteLine();

            if (!appServer.Start())
            {
                Console.WriteLine("Ошибка при старте!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Сервер запущен");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            appServer.Stop();

            Console.WriteLine();
            Console.WriteLine("Сервер остановлен");
            Console.ReadKey();
        }

        static void appServer_NewMessageReceived(WebSocketSession session, string message)
        {
            Console.WriteLine(DateTime.Now.ToLongDateString() + " " + message);
            session.Send(DateTime.Now + "Server: " + "Ответ получен");
        }
    }
}