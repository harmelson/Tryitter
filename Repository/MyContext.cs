using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repository
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}

        // Adicionando models como DbSet
        public DbSet<Post> Post { get; set; } = null!;
        public DbSet<PostUser> PostUser { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verificamos se o banco de dados já foi configurado
            if (!optionsBuilder.IsConfigured)
            {
                // Buscamos o valor da connection string armazenada nas variáveis de ambiente
                // var connectionString = Environment.GetEnvironmentVariable("DOTNET_CONNECTION_STRING");

                // Executamos o método UseSqlServer e passamos a connection string a ele
                optionsBuilder.UseSqlServer("Server=tcp:tryitter-db-api.database.windows.net,1433;Initial Catalog=tryitter-db-api;Persist Security Info=False;User ID=harmelson;Password=6026167Gh!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<PostUser>()
                .HasMany(p => p.Post) 
                .WithOne(u => u.PostUser)
                .HasPrincipalKey(u => u.IdPost)
                .HasForeignKey(u => u.IdPost)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PostUser>()
                .HasOne(u => u.User) 
                .WithMany(p => p.PostUser)
                .HasForeignKey(p => p.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
