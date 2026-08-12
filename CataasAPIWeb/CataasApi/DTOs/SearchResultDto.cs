

namespace CataasApi.DTOs;

public class SearchResultDto
{
    public List<ImagemDTO> ResultadoBusca { get; set; }
    public Boolean SucessoBusca { get; set; }
    public string MensagemBusca { get; set; }
    public int QtdResultados { get; set; }
}