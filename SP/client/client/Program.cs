using System.Net.Http.Json;

class Program
{
    static HttpClient httpClient = new HttpClient();
    static string baseUrl = "http://localhost:5291/api/items"; 

    static async Task Main()
    {
        Console.WriteLine("Старт");

        var secondToLast = await httpClient.GetFromJsonAsync<string>($"{baseUrl}/-2");
        Console.WriteLine($"предпоследний элемент: {secondToLast}");

        var multiple = await httpClient.GetFromJsonAsync<List<string>>($"{baseUrl}/multiple?indices=0&indices=2&indices=5");
        Console.WriteLine($"элементы по индексам 0, 2 и 5: {string.Join(" ", multiple ?? new List<string>())}");

        var updateResponse = await httpClient.PutAsJsonAsync($"{baseUrl}/3", new ItemDto("измененный_эл"));
        var listAfterUpdate = await updateResponse.Content.ReadFromJsonAsync<List<string>>();
        Console.WriteLine($"список после изменения элемента в позиции 3: [{string.Join(", ", listAfterUpdate!)}]");

        var range = await httpClient.GetFromJsonAsync<List<string>>($"{baseUrl}/range?start=2&end=7");
        Console.WriteLine($"элементы в диапазоне от 2 до 7: [{string.Join(", ", range!)}]");

        var appendResponse = await httpClient.PostAsJsonAsync($"{baseUrl}/append", new ItemDto("эл10"));
        var listAfterAppend = await appendResponse.Content.ReadFromJsonAsync<List<string>>();
        Console.WriteLine($"список после добавления в конец: [{string.Join(", ", listAfterAppend!)}]");

        var insertResponse = await httpClient.PostAsJsonAsync($"{baseUrl}/insert-middle", new ItemDto("эл_центр"));
        var listAfterInsert = await insertResponse.Content.ReadFromJsonAsync<List<string>>();
        Console.WriteLine($"список после добавления в середину: [{string.Join(", ", listAfterInsert!)}]");

        var count0 = await httpClient.GetFromJsonAsync<int>($"{baseUrl}/count?value=эл0");
        Console.WriteLine($"количество повторений 'эл0': {count0}");

        var originalList = await httpClient.GetFromJsonAsync<List<string>>(baseUrl);
        var itemsCopy = new List<string>(originalList!);
        
        itemsCopy[0] = "модифицирован";
        Console.WriteLine($"первый элемент оригинала (на сервере): {originalList![0]}");
        Console.WriteLine($"первый элемент локальной копии: {itemsCopy[0]}");
    }
}

public record ItemDto(string Value);