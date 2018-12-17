﻿using System;
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
            VerPedidosComercio = 13,
            DarAltaComercio = 14,
            DarAltaUsuario = 15,
            EnviarNotificaciones = 16,
            Reportes = 17
        }

        public enum eRoles
        {
            Administrador = 1,
            ComercianteAdministrador = 2,
            Consumidor = 3,
            Comerciante = 4
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
            [Description("En Preparación")]
            EnPreparacion = 2,
            Praparado = 3,
            Entregado = 4,
            Cancelado = 5
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

        public enum eResultadoAccion
        {
            Ok = 1,
            Error = 2
        }

        public enum eAccionCommand
        {
            IngresosDiarios = 1,
            IngresosMensuales = 2,
            ProductosVendidosPorCategoria = 3,
            Stock
        }
    }
}
