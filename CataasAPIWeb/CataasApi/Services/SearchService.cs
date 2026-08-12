using System.Text.RegularExpressions;
using CataasApi.DTOs;
using CataasApi.Interfaces;
using System.Linq;
using CataasApi.Data;
using CataasApi.DTOs;
using CataasApi.Models;
using Microsoft.EntityFrameworkCore;


namespace CataasApi.Services;

public class SearchService : ISearchService
{
    private readonly ICataasApi _cataasApi;
    private readonly AppDbContext _dbContext;

    public SearchService(ICataasApi cataasApi, AppDbContext dbContext)
    {
        _cataasApi = cataasApi;
        _dbContext = dbContext;
    }
    
    public async Task<SearchResultDto> BuscarAsync(string termo)
    {
        string termoNormalizado = Regex.Replace(termo, @"\s+", " ").Trim().ToLowerInvariant();
        List<CataasCatDto> resultadoCats = await _cataasApi.SearchByTagAsync(termoNormalizado, 10);
        
        const string baseUrl = "https://cataas.com/cat/";
        
        List<ImagemDTO> imagens = resultadoCats
            .Select(gato => new ImagemDTO
            {
                Id = gato.Id,
                Tags = gato.Tags,
                Mimetype = gato.Mimetype,
                Url = $"{baseUrl}{gato.Id}"
            })
            .ToList();

        int qtdResultado = imagens.Count;

        var historico = new SearchHistory
        {
            SearchContent = termo,
            DataHora = DateTime.Now,
            QuantidadeResultados = qtdResultado
        };
        
        _dbContext.SearchHistories.Add(historico);
        await _dbContext.SaveChangesAsync();

        return new SearchResultDto
        {
            ResultadoBusca = imagens,
            SucessoBusca = qtdResultado > 0,
            MensagemBusca = qtdResultado > 0 ? "Sucesso" : "Sua busca não trouxe resultados",
            QtdResultados = qtdResultado
        };
    }

    public async Task<List<SearchHistoryDto>> BuscarHistoryAsync()
    {
        
        var historico = await _dbContext.SearchHistories.OrderByDescending(h => h.DataHora).Take(10).ToListAsync();
        
        List<SearchHistoryDto> historicoUsuario = historico
            .Select(busca => new SearchHistoryDto()
            {
                SearchContent = busca.SearchContent,
                DataHora = busca.DataHora,
                QuantidadeResultados = busca.QuantidadeResultados,
            })
            .ToList();
        
        return historicoUsuario;
    }
}