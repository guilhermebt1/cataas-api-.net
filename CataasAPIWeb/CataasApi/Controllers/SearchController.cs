using CataasApi.DTOs;
using CataasApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace CataasApi.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    
    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<SearchResultDto>> GetCataas([FromQuery(Name = "termo")] string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return BadRequest("Busca inválida");
        }
        var resultado = await _searchService.BuscarAsync(termo);
        return Ok(resultado);
    }

    [HttpGet("history")]
    public async Task<ActionResult<List<SearchHistoryDto>>> GetHistory()
    {
        var resultado = await _searchService.BuscarHistoryAsync();
        return Ok(resultado);
    }
    
}