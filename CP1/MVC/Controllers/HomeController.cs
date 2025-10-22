using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;
        if (ViewBag.Valid)
        {
            // Improvement: Eliminar espacios usando LINQ a nivel de Character
            // https://learn.microsoft.com/en-us/dotnet/csharp/linq
            var charArray = model.Phrase!
                .ToCharArray()
                .Where(c => c != ' ')  // Filtrar espacios usando LINQ
                .ToList();

            charArray.ForEach(c =>
            {
                if (!model.Counts!.ContainsKey(c))
                {
                    model.Counts[c] = 0;
                }
                model.Counts[c]++;
                model.Lower += c.ToString().ToLower();
                model.Upper += c.ToString().ToUpper();
            });
        }
        return View(model);
    }
}
