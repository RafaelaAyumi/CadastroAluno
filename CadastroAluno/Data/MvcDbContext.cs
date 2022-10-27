using CadastroAluno.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadastroAluno.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
