using System.ComponentModel.DataAnnotations;

namespace RotaCerta.Models;

public class Destino
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string PaisDestino { get; set; }
    public string CidadeDestino { get; set; }
    public ICollection<PacoteTuristico> Pacotes { get; set; }
}