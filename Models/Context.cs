using Microsoft.EntityFrameworkCore;

namespace ControlePetWeb.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais se necessário
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Us_Email)
                .IsRequired();
        }
    }
}
