using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RotaCerta.Models;

namespace RotaCerta.Data.Configuration;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        //Define a chave
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade); //Deletar um cliente apaga todas as reservas associadas a ele

        builder.HasOne(r => r.PacoteTuristico)
            .WithMany(p => p.Reservas)
            .HasForeignKey(r => r.PacoteTuristicoId)
            .OnDelete(DeleteBehavior.Cascade); // Deletar um pacote turistico vai apagar todas as reservas associadas a ele

        //O id do cliente, o pacote turistico e a data da reserva devem ser únicas, um cliente não pode reservar o mesmo pacote de viagem na mesma data.
        builder.HasIndex(r => new { ClienteId = r.UserId, r.PacoteTuristicoId, r.DataReserva })
            .IsUnique();
        
        builder.Property(r => r.IsConfirmada)
            .HasDefaultValue(false);
    }
}