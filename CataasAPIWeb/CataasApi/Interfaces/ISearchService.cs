using CataasApi.DTOs;
using CataasApi.Models;

namespace CataasApi.Interfaces;

public interface ISearchService
{
    Task<SearchResultDto> BuscarAsync(string termo);
    Task<List<SearchHistoryDto>> BuscarHistoryAsync();
}