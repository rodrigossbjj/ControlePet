using Microsoft.EntityFrameworkCore;

namespace ControlePetWeb.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        //Tabela de usuários só armazena email, senha e campos especificados 
        public DbSet<Usuario> Usuarios { get; set; }
        //Todo Cliente é um veterinário, o termo Cliente se refere que ele é um Cliente do SISTEMA
        public DbSet<Cliente> Clientes { get; set; }
        //Tabela que guarda as informações da Clinica
        public DbSet<Clinica> Clinicas { get; set; }
        //Tabela que guarda as informações do Tutor
        public DbSet<Tutor> Tutores { get; set; }
        //Tabela que guarda as informações dos PETS
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Essa parte do código é reservada para configurações doo banco de dados
            A maior parte é usada para estabelecer os relaciomentos das tabelas e 
            como elas estão se comunicando. */


            //Relação 1 usário para 1 cliente 1:1
            modelBuilder.Entity<Usuario>()
               .HasOne(u => u.Cliente)
               .WithOne(c => c.Usuario)
               .HasForeignKey<Cliente>(c => c.Cli_UserId)
               .OnDelete(DeleteBehavior.Cascade);

            //Relação 1 cliente para 1 cliente para 1 clinica 1:1
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Clinica)
                .WithOne(cl => cl.Cliente)
                .HasForeignKey<Clinica>(cl => cl.Cln_ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            //Relacionamento 1:N entre Tutor e Pets, ou seja
            modelBuilder.Entity<Tutor>()
                .HasMany(t => t.Pets)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.pet_TutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
