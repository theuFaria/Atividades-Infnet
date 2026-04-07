using System.ComponentModel.DataAnnotations;

namespace RotaCerta.Models;

public class Anotacao
{
    [Required(ErrorMessage = "Não é possivel criar uma anotação vazia")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Por favor preencha o nome do Arquivo")]
    public string? Titulo { get; set; }
    
    public List<string>? Arquivos { get; set; }
    
    
}