using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repository;
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) {}

    // Adicionando models como DbSet
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Post> PostUser { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Verificamos se o banco de dados já foi configurado
        if (!optionsBuilder.IsConfigured)
        {
            // Buscamos o valor da connection string armazenada nas variáveis de ambiente
            var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

            // Executamos o método UseSqlServer e passamos a connection string a ele
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
