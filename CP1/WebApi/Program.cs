using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();


app.MapGet("/", () => Results.Redirect("/swagger"));


// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding
app.MapPost("/", ([FromHeader(Name = "xml")] bool xml = false) =>
{

    if (xml)
    {
        var xmlDoc = new XDocument(
            new XElement("numbers",
                list.Select(n => new XElement("number", n))
            )
        );
        
        var xmlString = xmlDoc.ToString();
        return Results.Content(xmlString, "application/xml", Encoding.UTF8);
    }
    

    return Results.Ok(list);
});


app.MapPut("/", ([FromForm] int quantity, [FromForm] string? type) =>
{

    // Claude Sonnet 4.5
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });
    }
    
  
    if (type != "int" && type != "float")
    {
        return Results.BadRequest(new { error = "'type' must be 'int' or 'float'" });
    }
    
   
    var random = new Random();
    if (type == "int")
    {
        for (; quantity > 0; quantity--)
        {
 
            list.Add(random.Next());
        }
    }
    else if (type == "float")
    {
        for (; quantity > 0; quantity--)
        {

            list.Add(random.NextSingle());
        }
    }
    

    return Results.Ok(list);
}).DisableAntiforgery();


app.MapDelete("/", ([FromForm] int quantity) =>
{

    // Claude Sonnet 4.5
    if (quantity <= 0)
    {
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });
    }
    

    if (list.Count < quantity)
    {
        return Results.BadRequest(new { error = $"The list only has {list.Count} element(s), cannot delete {quantity}" });
    }
    
    for (; quantity > 0; quantity--)
    {

        list.RemoveAt(0);
    }
    
    // Error fix: Retornar la lista con Status 200
    // Claude Sonnet 4.5
    return Results.Ok(list);
}).DisableAntiforgery();

// PATCH: Limpiar la lista
app.MapPatch("/", () =>
{
    // Update: Implementar limpieza de la lista
    // https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.clear
    list.Clear();
    
    // Error fix: Retornar la lista (vacía) con Status 200
    // Claude Sonnet 4.5
    return Results.Ok(list);
});

app.Run();