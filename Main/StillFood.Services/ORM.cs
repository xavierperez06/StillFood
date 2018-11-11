using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace StillFood.Services
{
    public static class ORM
    {
        #region Comercio
        public static Entities.Comercio ComercioModelToEntitie(Models.Comercio pComercio)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Comercio, Entities.Comercio>();
                cfg.CreateMap<Models.FormaPago, Entities.FormaPago>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Comercio, Entities.Comercio>(pComercio); 
        }

        public static List<Models.Comercio> ListaComercioEntitieAModel(List<Entities.Comercio> pListaComercios)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Comercio, Models.Comercio>();
                cfg.CreateMap<Entities.FormaPago, Models.FormaPago>();
            });

            return Mapper.Map<List<Entities.Comercio>, List<Models.Comercio>>(pListaComercios);
        }

        public static Models.Comercio ComercioEntitieToModel(Entities.Comercio pComercio)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Producto, Models.Producto>();
                cfg.CreateMap<Entities.Categoria, Models.Categoria>();
                cfg.CreateMap<Entities.FormaPago, Models.FormaPago>();
                cfg.CreateMap<Entities.Comercio, Models.Comercio>()
                .ForMember(a => a.Productos, b => b.ResolveUsing(c => c.Productos));
                });

            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Comercio, Models.Comercio>(pComercio);
        }

        #endregion

        #region Categorias
        public static Entities.Categoria CategoriaModelToEntitie(Models.Categoria pCategoria)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Categoria, Entities.Categoria>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Categoria, Entities.Categoria>(pCategoria);
        }

        public static List<Models.Categoria> ListaCategoriaEntitieAModel(List<Entities.Categoria> pListaCategorias)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.Categoria,Models.Categoria>());

            return Mapper.Map<List<Entities.Categoria>, List<Models.Categoria>>(pListaCategorias);
        }

        public static Models.Categoria CategoriaEntitieToModel(Entities.Categoria pCategoria)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Categoria, Models.Categoria>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Categoria, Models.Categoria>(pCategoria);
        }
        #endregion

        #region Usuarios
        public static Entities.Usuario UsuarioModelToEntitie(Models.Usuario pUsuario)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Permiso, Entities.Permiso>();
                cfg.CreateMap<Models.Rol, Entities.Rol>();
                cfg.CreateMap<Models.Usuario, Entities.Usuario>()
                .ForMember(u => u.Roles, x => x.ResolveUsing(y => y.Roles));
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Usuario, Entities.Usuario>(pUsuario);
        }

        public static Models.Usuario UsuarioEntitieToModel(Entities.Usuario pUsuario)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Permiso, Models.Permiso>();
                cfg.CreateMap<Entities.Rol, Models.Rol>();
                cfg.CreateMap<Entities.Usuario, Models.Usuario>()
                .ForMember(u=>u.Roles,x => x.ResolveUsing(y=>y.Roles));
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Usuario, Models.Usuario > (pUsuario);
        }

        public static List<Models.Usuario> ListaUsuariosEntitieAModel(List<Entities.Usuario> pListaUsuarios)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Usuario, Models.Usuario>();
            });

            return Mapper.Map<List<Entities.Usuario>, List<Models.Usuario>>(pListaUsuarios);
        }

        #endregion

        #region Productos
        public static List<Models.Producto> ListaProductosEntitieAModel(List<Entities.Producto> pListaProductos)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.Producto, Models.Producto>());

            return Mapper.Map<List<Entities.Producto>, List<Models.Producto>>(pListaProductos);
        }

        public static Models.Producto ProductoEntitieToModel(Entities.Producto pProducto)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Producto, Models.Producto>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Producto, Models.Producto>(pProducto);
        }

        public static Entities.Producto ProductoModelToEntitie(Models.Producto pProducto)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Producto, Entities.Producto>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Producto, Entities.Producto>(pProducto);
        }
        #endregion

        #region Permisos
        public static List<Models.Permiso> ListaPermisosEntitieAModel(List<Entities.Permiso> pListaPermisos)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.Permiso, Models.Permiso>());

            return Mapper.Map<List<Entities.Permiso>, List<Models.Permiso>>(pListaPermisos);
        }

        public static Entities.Permiso PermisoModelToEntitie(Models.Permiso pPermiso)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Permiso, Entities.Permiso>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Permiso, Entities.Permiso>(pPermiso);
        }

        public static Models.Permiso PermisoEntitieToModel(Entities.Permiso pPermiso)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Permiso, Models.Permiso>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Permiso, Models.Permiso>(pPermiso);
        }

        #endregion

        #region Roles
        public static List<Models.Rol> ListaRolesEntitieAModel(List<Entities.Rol> pListaRoles)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Rol, Models.Rol>();
            });

            return Mapper.Map<List<Entities.Rol>, List<Models.Rol>>(pListaRoles);
        }

        public static Entities.Rol RolModelToEntitie(Models.Rol pRol)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Rol, Entities.Rol>();
                cfg.CreateMap<Models.Permiso, Entities.Permiso>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Rol, Entities.Rol>(pRol);
        }

        public static Models.Rol RolEntitieToModel(Entities.Rol pRol)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Rol, Models.Rol>();
                cfg.CreateMap<Entities.Permiso, Models.Permiso>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Rol, Models.Rol>(pRol);
        }

        #endregion

        #region Usuarios Direcciones
        public static Entities.UsuarioDireccion UsuarioDireccionModelToEntitie(Models.UsuarioDireccion pUsuarioDireccion)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.UsuarioDireccion, Entities.UsuarioDireccion>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.UsuarioDireccion, Entities.UsuarioDireccion>(pUsuarioDireccion);
        }

        public static Models.UsuarioDireccion UsuarioDireccionEntitieToModel(Entities.UsuarioDireccion pUsuarioDireccion)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.UsuarioDireccion, Models.UsuarioDireccion>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.UsuarioDireccion, Models.UsuarioDireccion>(pUsuarioDireccion);
        }

        public static List<Models.UsuarioDireccion> ListaUsuariosDireccionesEntitieAModel(List<Entities.UsuarioDireccion> pListaUsuariosDirecciones)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.UsuarioDireccion, Models.UsuarioDireccion>());

            return Mapper.Map<List<Entities.UsuarioDireccion>, List<Models.UsuarioDireccion>>(pListaUsuariosDirecciones);
        }
        #endregion

        #region Formas de Entrega
        public static Models.FormaEntrega FormaEntregaEntitieToModel(Entities.FormaEntrega pFormaEntrega)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.FormaEntrega, Models.FormaEntrega>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.FormaEntrega, Models.FormaEntrega>(pFormaEntrega);
        }

        public static Entities.FormaEntrega FormaEntregaModelToEntitie(Models.FormaEntrega pFormaEntrega)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.FormaEntrega, Entities.FormaEntrega>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.FormaEntrega, Entities.FormaEntrega>(pFormaEntrega);
        }

        public static List<Models.FormaEntrega> ListaFormasEntregasEntitieAModel(List<Entities.FormaEntrega> pListaFormasEntregas)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.FormaEntrega, Models.FormaEntrega>());

            return Mapper.Map<List<Entities.FormaEntrega>, List<Models.FormaEntrega>>(pListaFormasEntregas);
        }
        #endregion  

        #region Formas de Pago
        public static Models.FormaPago FormaPagoEntitieToModel(Entities.FormaPago pFormaPago)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.FormaPago, Models.FormaPago>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.FormaPago, Models.FormaPago>(pFormaPago);
        }

        public static List<Models.FormaPago> ListaFormasPagoEntitieAModel(List<Entities.FormaPago> pListaFormasPago)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.FormaPago, Models.FormaPago>());

            return Mapper.Map<List<Entities.FormaPago>, List<Models.FormaPago>>(pListaFormasPago);
        }

        public static Entities.FormaPago FormaPagoModelToEntitie(Models.FormaPago pFormaPago)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.FormaPago, Entities.FormaPago>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.FormaPago, Entities.FormaPago>(pFormaPago);
        }
        #endregion

        #region Log
        public static Entities.Log LogModelToEntitie(Models.Log pLog)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Log, Entities.Log>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.Log, Entities.Log>(pLog);
        }

        public static Models.Log LogEntitieToModel(Entities.Log pLog)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Log, Models.Log>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.Log, Models.Log>(pLog);
        }
        #endregion

        #region Notas de Pedido
        public static Entities.NotaPedido NotaPedidoModelToEntitie(Models.NotaPedido pNotaPedido)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.NotaPedido, Entities.NotaPedido>();
                cfg.CreateMap<Models.NotaPedidoDetalle, Entities.NotaPedidoDetalle>();
                cfg.CreateMap<Models.Producto, Entities.Producto>();
                cfg.CreateMap<Models.Usuario, Entities.Usuario>().ForMember(u => u.Roles, x => x.ResolveUsing(y => y.Roles));
                cfg.CreateMap<Models.UsuarioDireccion, Entities.UsuarioDireccion>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Models.NotaPedido, Entities.NotaPedido>(pNotaPedido);
        }

        public static Models.NotaPedido NotaPedidoEntitieToModel(Entities.NotaPedido pNotaPedido)
        {
            var wConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.NotaPedido, Models.NotaPedido>();
                cfg.CreateMap<Entities.NotaPedidoDetalle, Models.NotaPedidoDetalle>();
                cfg.CreateMap<Entities.Producto, Models.Producto>();
                cfg.CreateMap<Entities.Usuario, Models.Usuario>();
                cfg.CreateMap<Entities.UsuarioDireccion, Models.UsuarioDireccion>();
            });
            IMapper wMapper = new Mapper(wConfig);

            return wMapper.Map<Entities.NotaPedido, Models.NotaPedido>(pNotaPedido);
        }

        public static List<Models.NotaPedido> ListaNotaPedidoEntitieAModel(List<Entities.NotaPedido> pListaNotaPedido)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.NotaPedido, Models.NotaPedido>();
                cfg.CreateMap<Entities.NotaPedidoDetalle, Models.NotaPedidoDetalle>();
                cfg.CreateMap<Entities.Producto, Models.Producto>();
            });

            return Mapper.Map<List<Entities.NotaPedido>, List<Models.NotaPedido>>(pListaNotaPedido);
        }
        #endregion

        #region Notas de Pedido Detalle
        public static List<Models.NotaPedidoDetalle> ListaNotaPedidoDetalleEntitieAModel(List<Entities.NotaPedidoDetalle> pListaNotaPedidoDetalle)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Entities.NotaPedidoDetalle, Models.NotaPedidoDetalle>());

            return Mapper.Map<List<Entities.NotaPedidoDetalle>, List<Models.NotaPedidoDetalle>>(pListaNotaPedidoDetalle);
        }

        public static List<Entities.NotaPedidoDetalle> ListaNotaPedidoDetalleModelAEntitie(List<Models.NotaPedidoDetalle> pListaNotaPedidoDetalle)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Models.NotaPedidoDetalle, Entities.NotaPedidoDetalle>());

            return Mapper.Map<List<Models.NotaPedidoDetalle>, List<Entities.NotaPedidoDetalle>>(pListaNotaPedidoDetalle);
        }
        #endregion
    }
}
