using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RotaCerta.Data.Configuration;
using RotaCerta.Models;

namespace RotaCerta.Data;

public class RotaCertaContext : IdentityDbContext<ApplicationUser>
{
    public RotaCertaContext(DbContextOptions<RotaCertaContext> options)
        : base(options) {}

    public DbSet<PacoteTuristico> PacoteTuristicos { get; set; }
    public DbSet<Destino> Destinos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DestinoConfiguration());
        modelBuilder.ApplyConfiguration(new PacoteTuristicoConfiguration());
        modelBuilder.ApplyConfiguration(new ReservaConfiguration());
    }
}