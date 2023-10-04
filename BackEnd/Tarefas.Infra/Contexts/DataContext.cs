using Microsoft.EntityFrameworkCore;
using Tarefas.Domain.Entities;

namespace Tarefas.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>().ToTable("Tarefa");
            modelBuilder.Entity<Tarefa>().Property(x => x.Id);
            modelBuilder.Entity<Tarefa>().Property(x => x.Descricao).HasMaxLength(1000).HasColumnType("varchar");
            modelBuilder.Entity<Tarefa>().Property(x => x.Status).HasColumnType("bit");
            modelBuilder.Entity<Tarefa>().Property(x => x.Data);

        }
    }
}
