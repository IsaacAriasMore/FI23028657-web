using System.Text;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger en todos los entornos
app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", () => Results.Redirect("/swagger"))
    .WithName("Root")
    .WithOpenApi();


app.MapPost("/include/{position}", (
    int position,
    string value,
    [Microsoft.AspNetCore.Mvc.FromForm] string text,
    [Microsoft.AspNetCore.Mvc.FromHeader(Name = "xml")] bool xml = false) =>
{
    // Validaciones
    if (position < 0)
        return Results.BadRequest(new { error = "'position' must be 0 or higher" });
    
    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });
    
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

  
    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    

    if (position > words.Count)
        position = words.Count;
    
    words.Insert(position, value);
    string newText = string.Join(" ", words);

 
    var result = new Result { Ori = text, New = newText };

   
    return xml ? Results.Content(SerializeToXml(result), "application/xml") 
               : Results.Ok(new { ori = result.Ori, @new = result.New });
})
.WithName("Include")
.WithOpenApi();


app.MapPut("/replace/{length}", (
    int length,
    string value,
    [Microsoft.AspNetCore.Mvc.FromForm] string text,
    [Microsoft.AspNetCore.Mvc.FromHeader(Name = "xml")] bool xml = false) =>
{
    // Validaciones
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });
    
    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });
    
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

 
    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var newWords = words.Select(word => 
        word.Length == length ? value : word
    );
    string newText = string.Join(" ", newWords);


    var result = new Result { Ori = text, New = newText };

    return xml ? Results.Content(SerializeToXml(result), "application/xml") 
               : Results.Ok(new { ori = result.Ori, @new = result.New });
})
.WithName("Replace")
.WithOpenApi();


app.MapDelete("/erase/{length}", (
    int length,
    [Microsoft.AspNetCore.Mvc.FromForm] string text,
    [Microsoft.AspNetCore.Mvc.FromHeader(Name = "xml")] bool xml = false) =>
{
    // Validaciones
    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be greater than 0" });
    
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

 
    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var newWords = words.Where(word => word.Length != length);
    string newText = string.Join(" ", newWords);


    var result = new Result { Ori = text, New = newText };

    return xml ? Results.Content(SerializeToXml(result), "application/xml") 
               : Results.Ok(new { ori = result.Ori, @new = result.New });
})
.WithName("Erase")
.WithOpenApi();

app.Run();


static string SerializeToXml(Result result)
{
    var xmlSerializer = new XmlSerializer(typeof(Result));
    using var stringWriter = new StringWriter();
    xmlSerializer.Serialize(stringWriter, result);
    return stringWriter.ToString();
}