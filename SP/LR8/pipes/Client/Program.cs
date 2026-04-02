using System;
using System.IO;
using System.IO.Pipes;

namespace PipeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("запуск клиента");

            using var client = new NamedPipeClientStream(".", "DataPipe", PipeDirection.InOut);
            client.Connect();

            using var reader = new StreamReader(client);
            using var writer = new StreamWriter(client) { AutoFlush = true };

            string incomingData = reader.ReadLine();
            Console.WriteLine($"получено от сервера: {incomingData}");

            char[] charArray = incomingData.ToCharArray();
            Array.Reverse(charArray);
            string derivedData = new string(charArray).ToUpper();

            Console.WriteLine($"отправляем данные: {derivedData}");
            writer.WriteLine(derivedData);
            
            Console.WriteLine("🗲🗲🗲сеанс клиента - ВСЁ!!!!🗲🗲🗲");
        }
    }
}