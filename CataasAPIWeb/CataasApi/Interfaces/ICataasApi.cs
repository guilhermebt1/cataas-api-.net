using CataasApi.DTOs;
namespace CataasApi.Interfaces;

public interface ICataasApi
{
    Task<List<CataasCatDto>> SearchByTagAsync(string tag, int limit);
}