using ApiFranquicia.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiFranquicia.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MetroProducto> MetroProductos{get;set;}
        public DbSet<TottusProducto> TottusProductos{get;set;}
        public DbSet<PlazaVeaProducto> PlazaVeaProductos{get;set;}
        public DbSet<WongProducto> WongProductos{get;set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

        }
    }
}