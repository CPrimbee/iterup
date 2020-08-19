using Microsoft.EntityFrameworkCore;
using Desafio.Api.Entities;

namespace Desafio.Api.Data
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions<DesafioContext> options)
            : base(options)
        {
        }

        public DbSet<Etapa> Etapas { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
    }
}