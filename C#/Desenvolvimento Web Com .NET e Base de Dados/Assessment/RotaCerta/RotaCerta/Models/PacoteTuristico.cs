using System.ComponentModel.DataAnnotations;

namespace RotaCerta.Models;

public class PacoteTuristico
{
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "Por favor digite o Nome do Pacote.")]
    [StringLength(50, ErrorMessage = "O Nome deve ter entre 3 e 50 letras.", MinimumLength = 3)]
    public string? Titulo { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime DataIda { get; set; }
    
    public int DestinoId { get; set; }   
    
    public Destino? Destino { get; set; } 
    
    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateTime DataRetorno { get; set; }

    [Required(ErrorMessage = "Por favor digite a Capacidade Máxima do pacote.")]
    public int CapacidadeMaxima { get; set; }

    public int CapacidadeDisponivel { get; set; }

    [Required(ErrorMessage = "Por favor digite o Preço do Pacote.")]
    public decimal PrecoPorPessoa { get; set; }
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public bool IsDeleted { get; set; } = false;
}