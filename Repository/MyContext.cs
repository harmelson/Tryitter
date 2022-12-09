using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

namespace Tryitter.Repository
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) {}

        // Adicionando models como DbSet
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Post> PostUsers { get; set; } = null!;
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

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<PostUser>()
                .HasMany(p => p.Post) 
                .WithOne(u => u.PostUsers)
                .HasForeignKey(p => p.IdPost);

            mb.Entity<PostUser>()
                .HasOne(u => u.User) 
                .WithMany(p => p.PostUsers)
                .HasForeignKey(u => u.IdUser);
        }
    }
}
