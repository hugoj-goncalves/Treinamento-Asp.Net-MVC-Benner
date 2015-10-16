using System.Data.Entity;
using TreinamentoBenner.Core.Model;

namespace TreinamentoBenner.Core.Context
{
    public class TreinamentoBennerContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Telefone> Telefones { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Artista> Artistas { get; set; }
    }
}
