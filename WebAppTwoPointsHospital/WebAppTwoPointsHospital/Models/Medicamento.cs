using System.ComponentModel.DataAnnotations;

namespace WebAppTwoPointsHospital.Models;

public class Medicamento
{
    
    public ICollection<Lote> Lotes { get; set; }
    [Required(ErrorMessage = "Preencha o Nome")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres")]
    public string Nome { get; set; }
    public Lote LoteAtual {  get; set; }
    [Required(ErrorMessage = "Preencha o Lote")]
    public string Lote { get; set; }

    public Medicamento(string nome, Lote loteAtual) { 
    
        Nome = nome;
        LoteAtual = loteAtual;
    }
    
    public Medicamento() { }
  
}