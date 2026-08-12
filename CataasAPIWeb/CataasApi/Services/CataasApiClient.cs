using CataasApi.Interfaces;
using CataasApi.DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Text.Json;
using System.Threading.Tasks;

namespace CataasApi.Services;

public class CataasApiClient : ICataasApi
{
    
    private readonly HttpClient _httpClient;

    public CataasApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task<List<CataasCatDto>> SearchByTagAsync(string tag, int limit)
    {
        string tagsEscapadas = Uri.EscapeDataString(tag);
        string urlCorreta = $"https://cataas.com/api/cats?tags={tagsEscapadas}&limit={limit}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(urlCorreta);
            response.EnsureSuccessStatusCode();
            List<CataasCatDto> resultado = await response.Content.ReadFromJsonAsync<List<CataasCatDto>>();
            return resultado;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erro na requisição: {e.Message}");
            return new List<CataasCatDto>(); 
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Erro ao deserializar o JSON: {e.Message}");
            return new List<CataasCatDto>();
        }
    }
}