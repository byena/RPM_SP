using System;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

// получаем путь к системной папке "мои документы"
string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

// ищем все файлы .xlsx именно в документах
var files = Directory.GetFiles(documentsPath, "*.xlsx");

if (files.Length == 0)
{
    Console.WriteLine($"в папке {documentsPath} файлы .xlsx не найдены.");
    return;
}

// выводим список найденных файлов
Console.WriteLine($"список таблиц в ваших документах ({documentsPath}):");
for (int i = 0; i < files.Length; i++)
{
    var fileInfo = new FileInfo(files[i]);
    Console.WriteLine($"{i + 1}. {fileInfo.Name} (изменен: {fileInfo.LastWriteTime})");
}

// выбор файла
Console.Write("\nвведите номер файла: ");
if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > files.Length)
{
    Console.WriteLine("ошибка: выберите номер из списка.");
    return;
}

string selectedFile = files[choice - 1];

// открываем выбранный файл
try 
{
    using (var workbook = new XLWorkbook(selectedFile))
    {
        Console.WriteLine($"\n анализ файла: {Path.GetFileName(selectedFile)} ---");
        
        foreach (var sheet in workbook.Worksheets)
        {
            int cellCount = sheet.CellsUsed().Count();
            Console.WriteLine($"лист: [{sheet.Name}] | ячеек с данными: {cellCount}");
            
            // если ячейки есть, выводим первую строку (заголовки)
            if (cellCount > 0)
            {
                var firstRow = sheet.FirstRowUsed();
                var headers = firstRow.Cells().Select(c => c.Value.ToString());
                Console.WriteLine("заголовки: " + string.Join(" | ", headers));
            }
        }
    }
}
catch (Exception ex)
{
    // обработка ошибок(исключений)
    Console.WriteLine($"не удалось прочитать файл: {ex.Message}");
}