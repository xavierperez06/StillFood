namespace StillFood.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StillFoodModel : DbContext
    {
        // El contexto se ha configurado para usar una cadena de conexión 'StillFoodModel' del archivo 
        // de configuración de la aplicación (App.config o Web.config). De forma predeterminada, 
        // esta cadena de conexión tiene como destino la base de datos 'StillFood.DAL.StillFoodModel' de la instancia LocalDb. 
        // 
        // Si desea tener como destino una base de datos y/o un proveedor de base de datos diferente, 
        // modifique la cadena de conexión 'StillFoodModel'  en el archivo de configuración de la aplicación.
        public DbSet<Entities.Comercio> Comercios { get; set; }
        public DbSet<Entities.Categoria> Categorias { get; set; }
        public DbSet<Entities.Usuario> Usuarios { get; set; }
        public DbSet<Entities.Producto> Productos { get; set; }
        public DbSet<Entities.Rol> Roles { get; set; }
        public DbSet<Entities.Permiso> Permisos { get; set; }
        public DbSet<Entities.UsuarioDireccion> UsuariosDirecciones { get; set; }
        public DbSet<Entities.FormaEntrega> FormasEntregas { get; set; }
        public DbSet<Entities.FormaPago> FormasPago { get; set; }
        public DbSet<Entities.NotaPedido> NotasPedidos { get; set; }
        public DbSet<Entities.NotaPedidoDetalle> NotasPedidosDetalle { get; set; }
        public DbSet<Entities.Log> Log { get; set; }

        public StillFoodModel()
            : base("name=StillFoodModel")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Comercio>().Ignore(t => t.Productos);
            //Con esto creo la tabla de relacion many-to-many de roles-permisos
            modelBuilder.Entity<Entities.Rol>()
                .HasMany<Entities.Permiso>(r => r.Permisos)
                .WithMany(p => p.Roles)
                .Map(rp =>
                {
                    rp.MapLeftKey("IdRol");
                    rp.MapRightKey("IdPermiso");
                    rp.ToTable("RolesPermisos");
                });
            //Con esto creo la tabla de relacion many-to-many de usuarios-roles
            modelBuilder.Entity<Entities.Usuario>()
                .HasMany<Entities.Rol>(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .Map(rp =>
                {
                    rp.MapLeftKey("IdUsuario");
                    rp.MapRightKey("IdRol");
                    rp.ToTable("UsuariosRoles");
                });
            //Con esto creo la tabla de relacion many-to-many de comercios-formaspago
            modelBuilder.Entity<Entities.Comercio>()
                .HasMany<Entities.FormaPago>(c => c.FormasPago)
                .WithMany(fp => fp.Comercios)
                .Map(rp =>
                {
                    rp.MapLeftKey("IdComercio");
                    rp.MapRightKey("IdFormaPago");
                    rp.ToTable("ComerciosFormasPago");
                });
        }
        // Agregue un DbSet para cada tipo de entidad que desee incluir en el modelo. Para obtener más información 
        // sobre cómo configurar y usar un modelo Code First, vea http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}