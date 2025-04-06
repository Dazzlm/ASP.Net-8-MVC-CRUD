using CRUD.Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> opciones) :base(opciones)
        {
            
        }

        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Empleado>(tblE =>
            {
                tblE.HasKey(em => em.ID);
                tblE.Property(em => em.ID).UseIdentityColumn().ValueGeneratedOnAdd();
                tblE.HasIndex(em => em.Correo).IsUnique();
                tblE.HasIndex(em => em.Documento).IsUnique();
                tblE.HasIndex(em => em.Telefono).IsUnique();

            }) ;
            base.OnModelCreating(modelBuilder);
        }  
    }
}
