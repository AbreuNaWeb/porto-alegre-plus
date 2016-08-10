using System.Data.Entity;

namespace PortoAlegrePlus.Models
{
    public class PortoAlegrePlusDBContext : DbContext
    {
        public PortoAlegrePlusDBContext() : base("PortoAlegrePlusDBContext")
        {
        }

        public DbSet<Reclamacao> Reclamacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}