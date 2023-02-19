using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record BookForManipulationDto
{
    [Required(ErrorMessage = "Author is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the author is 30 characters.")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "Title is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Genre is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the genre is 30 characters.")]
    public string? Genre { get; set; }

    [Required(ErrorMessage = "Price is a required field.")]
    public string? Price { get; set; }

    [Required(ErrorMessage = "Publish_Date is a required field.")]
    public string? Publish_Date { get; set; }

    [Required(ErrorMessage = "Description is a required field.")]
    public string? Description { get; set; }
}

