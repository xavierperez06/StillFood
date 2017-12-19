using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillFood.Common
{
    public class Enums
    {
        public enum eRolesPermisos
        {
            AdministrarProductos = 1,
            AdministrarCategorias = 2,
            AdministrarComercios = 3,
            AdministrarUsuarios = 4,
            AdministrarRoles = 5,
            AdministrarPermisos = 6,
            VerAdministracion = 7,
            AdministrarFormasPago = 8,
            AdministrarFormasEntrega = 9,
            AdministrarFormasPagoComercio = 10,
            VerComercios = 11,
            RealizarCompras = 12,
            VerPedidosComercio = 13
        }

        public enum eRoles
        {
            ComercianteAdministrador = 1,
            Comerciante = 2,
            Administrador = 3,
            Consumidor = 4
        }

        public enum eTiposUsuarios
        {
            [Description("Consumidor")]
            Consumidor = 1,
            [Description("Comerciante")]
            Comerciante = 2,
            [Description("Administrador")]
            Administrador = 3
        }

        public enum eEstadosUsuarios
        {
            [Description("Creado")]
            Creado = 1,
            [Description("Activo")]
            Activo = 2
        }

        public enum eResultadoEnvio
        {
            Ok = 1,
            Error = 2
        }

        public enum eResultadoRegistro
        {
            Ok = 1,
            ExisteUsuario = 2,
            FalloMail = 3
        }

        public enum eResultadoLogin
        {
            Logueado = 1,
            ContraseñaIncorrecta = 2,
            UsuarioIncorrecto = 3,
            Inactivo = 4
        }

        public enum eFormasEntrega
        {
            Domicilio = 1,
            RetiroEnLocal = 2
        }

        public enum eEstadosNotasPedido
        {
            Realizado = 1,
            EnPreparacion = 2,
            Praparado = 3,
            Enviado = 4,
            Terminado = 5,
            Cancelado = 6
        }

        public enum eFormasPago
        {
            Efectivo = 1,
            TarjetaCredito = 2,
            MercadoPago = 3
        }

        public enum eResultadoCambioContraseña
        {
            Ok = 1,
            Error = 2,
            MailInexistente = 3
        }
    }
}
