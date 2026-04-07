using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RotaCerta.Models;

namespace RotaCerta.Data.Configuration;

public class PacoteTuristicoConfiguration : IEntityTypeConfiguration<PacoteTuristico>
{
    public void Configure(EntityTypeBuilder<PacoteTuristico> builder)
    {
        //Define a chave
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Destino)
            .WithMany(d => d.Pacotes)
            .HasForeignKey(d => d.DestinoId);
        
        //Propriedade IsDelete vem com valor padrão de false
        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);
    }
}