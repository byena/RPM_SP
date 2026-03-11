using System;
using System.Net;
using System.IO;

class Program
{
    static void Main()
    {
        string url = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        string fileName = "open_statistics.csv";

        Console.WriteLine("начинаю загрузку файла...");

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, fileName);
        }

        Console.WriteLine("файл загружен. вывожу первые 10 строк содержимого:\n");

        string[] lines = File.ReadAllLines(fileName);
        
        int linesToShow = Math.Min(10, lines.Length);
        for (int i = 0; i < linesToShow; i++)
        {
            Console.WriteLine(lines[i]);
        }
    }
}