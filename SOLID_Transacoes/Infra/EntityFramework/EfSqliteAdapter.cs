using Microsoft.EntityFrameworkCore;
using SOLID_Transacoes.Domain.Models;

namespace SOLID_Transacoes.Infra.EntityFramework
{
    public class EfSqliteAdapter : DbContext
    {
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=C:\\WorkSpace\\VisualStudio\\SOLID_Transacoes\\SOLID_Transacoes\\Transacao.db");
        }
    }
}
