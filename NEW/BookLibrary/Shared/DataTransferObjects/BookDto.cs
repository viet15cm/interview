namespace Shared.DataTransferObjects;

public class BookDto
{
    public string Id { get; init; }

    public string? Author { get; init; }

    public string? Title { get; init; }

    public string? Genre { get; init; }

    public string? Price { get; init; }

    public string? Publish_Date { get; set; }

    public string? Description { get; init; }
}

