namespace CataasApi.DTOs;

public class ImagemDTO
{
    public required string Id { get; set; }
    public required List<string> Tags { get; set; }
    public required string Mimetype { get; set; }
    public required string Url { get; set; }
}