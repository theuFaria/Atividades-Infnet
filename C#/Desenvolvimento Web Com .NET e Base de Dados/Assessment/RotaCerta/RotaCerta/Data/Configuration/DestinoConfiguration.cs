using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RotaCerta.Models;

namespace RotaCerta.Data.Configuration;

public class DestinoConfiguration : IEntityTypeConfiguration<Destino>
{
    public void Configure(EntityTypeBuilder<Destino> builder)
    {
        //Define a Chave
        builder.HasKey(d => d.Id);


        builder.HasData(
            new Destino { Id = 1, Nome = "Las Vegas", PaisDestino = "Estados Unidos", CidadeDestino = "Las Vegas" },
            new Destino { Id = 2, Nome = "Disney Orlando", PaisDestino = "Estados Unidos", CidadeDestino = "Orlando" },
            new Destino { Id = 3, Nome = "Rio de Janeiro", PaisDestino = "Brasil", CidadeDestino = "Rio de Janeiro" },
            new Destino { Id = 4, Nome = "Angra dos Reis", PaisDestino = "Brasil", CidadeDestino = "Angra dos Reis" }
            );
    }
}