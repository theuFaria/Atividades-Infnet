using System.ComponentModel.DataAnnotations;

namespace WebAppTwoPointsHospital.Models;

public class Usuario
{
    public Usuario(string nome, string email, string senha, string cpf)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Cpf = cpf;
    }
    
    public Usuario() {}
    
    [Key]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "Informe seu nome")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe seu email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Informe sua senha")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres e no máximo 50")]
    public string Senha { get; set; }
    
    [Required(ErrorMessage = "Informe seu CPF")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números")]
    public string Cpf { get; set; }
    
}