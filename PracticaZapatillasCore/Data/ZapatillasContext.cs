using Microsoft.EntityFrameworkCore;
using PracticaZapatillasCore.Models;

namespace PracticaZapatillasCore.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            : base(options)
        { }

        public DbSet<Zapatilla> Zapatillas {get; set;}
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
    }
}
