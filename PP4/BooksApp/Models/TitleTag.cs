using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp.Models;

public class TitleTag
{
    [Key]
    public int TitleTagId { get; set; }
    
    [Required]
    [ForeignKey("Title")]
    public int TitleId { get; set; }
    
    [Required]
    [ForeignKey("Tag")]
    public int TagId { get; set; }
    
    public Title Title { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}