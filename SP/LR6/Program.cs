using System;
using System.Threading;
using ClosedXML.Excel;

class Program
{
    static void Main()
    {
        // укажи здесь свой путь к файлу и названия листов
        string filePath = @"C:\Users\byena\Documents\Лр 3 у Быстрых (Ветошкин Никита).xlsx";
        string sheet1Name = "Лист1";
        string sheet2Name = "Лист2";

        Thread thread1 = new Thread(() => ReadSheet(filePath, sheet1Name, 300));
        Thread thread2 = new Thread(() => ReadSheet(filePath, sheet2Name, 600));

        Console.WriteLine($"статус потока 1 перед запуском: {thread1.ThreadState}");
        Console.WriteLine($"статус потока 2 перед запуском: {thread2.ThreadState}");

        thread1.Start();
        thread2.Start();

        Console.WriteLine($"статус потока 1 после запуска: {thread1.ThreadState}");
        Console.WriteLine($"статус потока 2 после запуска: {thread2.ThreadState}");

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"статус потока 1 после завершения: {thread1.ThreadState}");
        Console.WriteLine($"статус потока 2 после завершения: {thread2.ThreadState}");
    }

    static void ReadSheet(string path, string sheetName, int delay)
    {
        using (var workbook = new XLWorkbook(path))
        {
            var worksheet = workbook.Worksheet(sheetName);
            var rows = worksheet.RowsUsed();

            foreach (var row in rows)
            {
                Console.WriteLine($"поток для {sheetName}: {row.Cell(1).Value}");
                Thread.Sleep(delay);
            }
        }
    }
}