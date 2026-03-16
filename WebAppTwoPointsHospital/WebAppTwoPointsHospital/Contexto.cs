using Microsoft.EntityFrameworkCore;
using WebAppTwoPointsHospital.Models;

namespace WebAppTwoPointsHospital;

public class Contexto : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }
    
}