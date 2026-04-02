using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var items = new List<string> 
{ 
    "эл0", "эл1", "эл2", "эл3", "эл0", "эл4", "эл5", "эл6", "эл0", "эл7", "эл8", "эл9" 
};

app.MapGet("/api/items", () => items);

app.MapGet("/api/items/{index:int}", (int index) => 
{
    int realIndex = index < 0 ? items.Count + index : index;
    if (realIndex >= 0 && realIndex < items.Count) 
        return Results.Json(items[realIndex]);
    
    return Results.NotFound(new { message = "Индекс вне диапазона" });
});

app.MapGet("/api/items/multiple", ([FromQuery] int[] indices) => 
{
    var result = indices.Where(i => i >= 0 && i < items.Count).Select(i => items[i]).ToList();
    return Results.Json(result);
});

app.MapGet("/api/items/range", (int start, int end) => 
{
    var result = items.Skip(start).Take(end - start).ToList();
    return Results.Json(result);
});

app.MapGet("/api/items/count", (string value) => 
{
    int count = items.Count(i => i == value);
    return Results.Json(count);
});

app.MapPut("/api/items/{index:int}", (int index, [FromBody] ItemDto dto) => 
{
    if (index >= 0 && index < items.Count) 
    {
        items[index] = dto.Value;
        return Results.Ok(items);
    }
    return Results.NotFound();
});

app.MapPost("/api/items/append", ([FromBody] ItemDto dto) => 
{
    items.Add(dto.Value);
    return Results.Ok(items);
});

app.MapPost("/api/items/insert-middle", ([FromBody] ItemDto dto) => 
{
    int middle = items.Count / 2;
    items.Insert(middle, dto.Value);
    return Results.Ok(items);
});

app.Run();

public record ItemDto(string Value);