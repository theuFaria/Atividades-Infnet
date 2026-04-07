using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;

namespace RotaCerta.Models;

public class Reserva
{
    [Key] public int Id { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    [Required(ErrorMessage = "Por favor preencha o campo Nome")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve conter entre 3 e 50 caracteres")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Por favor preencha a quantidade de pessoas")]
    public int QuantidadeDePessoas { get; set; }
    
    public decimal ValorTotal { get; set; }

    public int PacoteTuristicoId { get; set; }
    public PacoteTuristico? PacoteTuristico { get; set; }

    [Required(ErrorMessage = "Por favor preencha o campo Data")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DataReserva { get; set; }
    
    public bool IsConfirmada { get; set; } 
}