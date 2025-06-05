using Microsoft.EntityFrameworkCore;

namespace ControlePetWeb.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
               .HasOne(u => u.Cliente)
               .WithOne(c => c.Usuario)
               .HasForeignKey<Cliente>(c => c.Cli_UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Clinica)
                .WithOne(cl => cl.Cliente)
                .HasForeignKey<Clinica>(cl => cl.Cln_ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
