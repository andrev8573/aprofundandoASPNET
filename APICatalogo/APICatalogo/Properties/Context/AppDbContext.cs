using APICatalogo.Domain; // Imports
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Properties.Context // Caminho
{
    public class AppDbContext : DbContext // Implementação que cria as tabelas e gerencia sessão no banco
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) // Inicia a sessão
            : base(options) 
        {
        }
     
        public DbSet<Categoria>? Categorias { get; set; } 
        public DbSet<Produto>? Produtos { get; set; }
    }
}
