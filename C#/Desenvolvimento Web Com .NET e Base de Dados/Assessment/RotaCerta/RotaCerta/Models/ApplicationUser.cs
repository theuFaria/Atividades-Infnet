using Microsoft.AspNetCore.Identity;

namespace RotaCerta.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public string Nome { get; set; }
}