using BooksApp.Data;
using BooksApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApp;

class Program
{
    static void Main(string[] args)
    {
        // Asegurar que existe la carpeta data
        Directory.CreateDirectory("data");

        using var context = new BooksContext();
        
        // Verificar si la base de datos está vacía
        if (!context.Authors.Any())
        {
            Console.WriteLine("La base de datos está vacía, por lo que será llenada a partir de los datos del archivo CSV.");
            Console.WriteLine();
            Console.Write("Procesando... ");
            
            LoadDataFromCsv(context);
            
            Console.WriteLine("Listo.");
        }
        else
        {
            Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.");
            Console.WriteLine();
            Console.Write("Procesando... ");
            
            GenerateTsvFiles(context);
            
            Console.WriteLine("Listo.");
        }
    }

    static void LoadDataFromCsv(BooksContext context)
    {
        var csvPath = "data/books.csv";
        
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"Error: No se encontró el archivo {csvPath}");
            return;
        }

        var lines = File.ReadAllLines(csvPath);
        
        // Diccionarios para evitar duplicados
        var authorsDict = new Dictionary<string, Author>();
        var tagsDict = new Dictionary<string, Tag>();

        // Saltar el encabezado
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = ParseCsvLine(lines[i]);
            
            if (parts.Length < 3) continue;

            var authorName = parts[0];
            var titleName = parts[1];
            var tagsString = parts[2];

            // Obtener o crear autor
            if (!authorsDict.ContainsKey(authorName))
            {
                var author = new Author { AuthorName = authorName };
                authorsDict[authorName] = author;
                context.Authors.Add(author);
            }

            // Crear título
            var title = new Title 
            { 
                TitleName = titleName,
                Author = authorsDict[authorName]
            };
            context.Titles.Add(title);

            // Procesar etiquetas
            var tagNames = tagsString.Split('|');
            foreach (var tagName in tagNames)
            {
                var trimmedTag = tagName.Trim();
                
                // Obtener o crear etiqueta
                if (!tagsDict.ContainsKey(trimmedTag))
                {
                    var tag = new Tag { TagName = trimmedTag };
                    tagsDict[trimmedTag] = tag;
                    context.Tags.Add(tag);
                }

                // Crear relación TitleTag
                var titleTag = new TitleTag
                {
                    Title = title,
                    Tag = tagsDict[trimmedTag]
                };
                context.TitleTags.Add(titleTag);
            }
        }

        context.SaveChanges();
    }

    static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var current = "";
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.Trim());
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current.Trim());
        return result.ToArray();
    }

    static void GenerateTsvFiles(BooksContext context)
    {
        // Obtener todos los datos con las relaciones
        var data = context.Titles
            .Include(t => t.Author)
            .Include(t => t.TitleTags)
                .ThenInclude(tt => tt.Tag)
            .ToList();

        // Agrupar por primera letra del autor
        var groupedData = data
            .SelectMany(title => title.TitleTags.Select(tt => new
            {
                AuthorName = title.Author.AuthorName,
                TitleName = title.TitleName,
                TagName = tt.Tag.TagName,
                FirstLetter = title.Author.AuthorName[0]
            }))
            .GroupBy(x => x.FirstLetter)
            .OrderByDescending(g => g.Key);

        foreach (var group in groupedData)
        {
            var fileName = $"data/{group.Key}.tsv";
            
            using var writer = new StreamWriter(fileName);
            
            // Escribir encabezado
            writer.WriteLine("AuthorName\tTitleName\tTagName");
            
            // Ordenar datos descendentemente
            var sortedData = group
                .OrderByDescending(x => x.AuthorName)
                .ThenByDescending(x => x.TitleName)
                .ThenByDescending(x => x.TagName);
            
            foreach (var item in sortedData)
            {
                writer.WriteLine($"{item.AuthorName}\t{item.TitleName}\t{item.TagName}");
            }
        }
    }
}