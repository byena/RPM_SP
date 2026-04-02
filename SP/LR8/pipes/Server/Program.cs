using System;
using System.IO;
using System.IO.Pipes;

namespace PipeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("запуск сервера");
            
            using var server = new NamedPipeServerStream("DataPipe");
            
            Console.WriteLine("ждем соединения с клиентом");
            server.WaitForConnection();

            using var reader = new StreamReader(server);
            using var writer = new StreamWriter(server) { AutoFlush = true };

            string rawData = "csharp named pipes";
            Console.WriteLine($"отправляем данные: {rawData}");
            
            writer.WriteLine(rawData);

            string derivedData = reader.ReadLine();
            Console.WriteLine($"получены данные: {derivedData}");
            
            Console.WriteLine("сеанс сервера закончен");
        }
    }
}