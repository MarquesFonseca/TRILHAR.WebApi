using Microsoft.EntityFrameworkCore;

namespace TRILHAR.Infra.Data.EF
{
    public class TrilharContext : DbContext
    {
        //public DbSet<CadastroFiscalEntity> CadastroFiscal { get; set; }

        public TrilharContext(DbContextOptions<TrilharContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CadastroFiscalMap());/
            base.OnModelCreating(modelBuilder);
        }
    }
}
