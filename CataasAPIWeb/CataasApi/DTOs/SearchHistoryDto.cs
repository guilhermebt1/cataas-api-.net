using CataasApi.Models;

namespace CataasApi.DTOs;

public class SearchHistoryDto
{
    public required string SearchContent { get; set; }
    public DateTime DataHora { get; set; }
    public int QuantidadeResultados { get; set; }
}