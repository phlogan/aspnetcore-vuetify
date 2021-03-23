using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontualAPI.Models;

namespace PontualAPI.Config
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.NomeFantasia).IsRequired().HasMaxLength(250);
            builder.Property(e => e.RazaoSocial).IsRequired().HasMaxLength(250);
            builder.Property(e => e.CNPJ).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Telefone).IsRequired().HasMaxLength(30);
            builder.Property(e => e.DataCadastro).IsRequired();
        }
    }
}
