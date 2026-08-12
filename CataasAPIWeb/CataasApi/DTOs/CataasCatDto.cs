namespace CataasApi.DTOs;

public class CataasCatDto
{
    public required string Id { get; set; }
    public required List<string> Tags { get; set; }
    public required string Mimetype { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}