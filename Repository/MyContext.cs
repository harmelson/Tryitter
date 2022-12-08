using Microsoft.EntityFrameworkCore;

namespace Tryitter.Repository;
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) {}

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
