using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TRILHAR.Business.Entities;
using TRILHAR.Infra.Data.EF;

namespace TRILHAR.Infra.Data.Mappings
{
    [DbContext(typeof(TrilharContext))]
    public class AlunoMap : IEntityTypeConfiguration<AlunoEntity>
    {
        public void Configure(EntityTypeBuilder<AlunoEntity> builder)
        {
            var aplicacao = new Aplicacao();

            builder.ToTable("Aluno","iquessistemas");

            builder.HasKey(b => new { b.Codigo });

            builder.Property(b => b.Codigo)
                .HasColumnName("Codigo");

            builder.Property(b => b.NomeCrianca)
                .HasColumnName("NomeCrianca");

            //builder.Property(b => b.NomeDestino)
            //    .HasColumnName("NODESTINO");

            //builder.Property(b => b.HoraEnvio)
            //    .HasColumnName("HOENVIO");

            //builder.Property(b => b.DataRetorno)
            //    .HasColumnName("DARETORNO");

            //builder.Property(b => b.TXParecer)
            //    .HasColumnName("TXPARECER");

            //builder.Property(b => b.InStatus)
            //   .HasColumnName("INSTATUS");

            //builder.Property(b => b.UltimaAlteracao)
            //   .HasColumnName("ULTALTERACAO");
        }
    }
}
