using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class TheModel
{

    [Required(ErrorMessage = "Phrase is required")]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "Phrase must be between 5 and 25 characters")]
    public string? Phrase { get; set; }

    public Dictionary<char, int> Counts { get; set; } = [];

    public string Lower { get; set; } = string.Empty;

    public string Upper { get; set; } = string.Empty;
}