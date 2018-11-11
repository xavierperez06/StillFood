using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace StillFood.Common
{
    public static class Utils
    {
        public static decimal ObtenerMontoTotal(List<Models.Producto> pProductos)
        {
            decimal wMontoTotal = 0;

            foreach(Models.Producto wProducto in pProductos)
            {
                wMontoTotal = (decimal)(wMontoTotal + (wProducto.PrecioDescuento.Value * wProducto.Cantidad));
            }

            return wMontoTotal;
        }

        public static string ObtenerDescripcionFormaEntrega(int pIdFormaEntrega)
        {
            string wDescripcion = string.Empty;

            DAL.FormasEntregas wFormaEntregaDAL = new DAL.FormasEntregas();
            Entities.FormaEntrega wFormaEntrega = wFormaEntregaDAL.ObtenerFormaEntrega(pIdFormaEntrega);

            if (wFormaEntrega != null)
            {
                wDescripcion = wFormaEntrega.Nombre;
            }

            return wDescripcion;
        }

        public static string ObtenerDescripcionDireccion(int? pIdDireccion)
        {
            string wDescripcion = string.Empty;

            if (pIdDireccion.HasValue)
            {
                DAL.UsuariosDirecciones wDirecciones = new DAL.UsuariosDirecciones();
                Entities.UsuarioDireccion wDireccion = wDirecciones.ObtenerDireccion(pIdDireccion.Value);

                if (wDireccion != null)
                {
                    if (!string.IsNullOrWhiteSpace(wDireccion.Departamento) && wDireccion.Piso.HasValue)
                    {
                        wDescripcion = String.Concat(wDireccion.Calle, " ", wDireccion.Numero, " Piso: ", wDireccion.Piso, " Departamento: ", wDireccion.Departamento);
                    }
                    else if (string.IsNullOrWhiteSpace(wDireccion.Departamento) && wDireccion.Piso.HasValue)
                    {
                        wDescripcion = String.Concat(wDireccion.Calle, " ", wDireccion.Numero, " Piso: ", wDireccion.Piso);
                    }
                    else
                    {
                        wDescripcion = String.Concat(wDireccion.Calle, " ", wDireccion.Numero);
                    }
                }
            }

            return wDescripcion;
        }

        public static string ObtenerDescripcionFormaPago(int pIdFormaPago)
        {
            string wDescripcion = string.Empty;

            DAL.FormasPago wFormasPago = new DAL.FormasPago();
            Entities.FormaPago wFormaPago = wFormasPago.ObtenerFormaPago(pIdFormaPago);

            if (wFormaPago != null)
            {
                wDescripcion = wFormaPago.Nombre;
            }

            return wDescripcion;
        }

        public static string GenerarCodigo()
        {
            Random wObj = new Random();
            string wPosibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int wLongitud = wPosibles.Length;
            char wLetra;
            int wLongitudNuevaCadena = 6;
            string wNuevaCadena = "";
            for (int wI = 0; (wI <= (wLongitudNuevaCadena - 1)); wI++)
            {
                wLetra = wPosibles[wObj.Next(wLongitud)];
                wNuevaCadena = (wNuevaCadena + wLetra.ToString());
            }

            return wNuevaCadena;
        }

        public static string GetEnumDescription(Enum pValue)
        {
            FieldInfo wFi = pValue.GetType().GetField(pValue.ToString());

            DescriptionAttribute[] wAttributes =
                (DescriptionAttribute[])wFi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (wAttributes != null &&
                wAttributes.Length > 0)
                return wAttributes[0].Description;
            else
                return pValue.ToString();
        }

        public static string EncriptarContraseña(string pContraseña)
        {
            MD5 wMD5 = new MD5CryptoServiceProvider();

            //compute hash de los bytes del texto
            wMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pContraseña));

            //Obtengo el hash del resultado
            byte[] wResultado = wMD5.Hash;

            StringBuilder wSW = new StringBuilder();
            for (int i = 0; i < wResultado.Length; i++)
            {
                wSW.Append(wResultado[i].ToString("x2"));
            }

            return wSW.ToString();
        }

        public static bool CheckContraseña(string pContraseñaIngresada, string pContraseñaUsuario)
        {
            string wHash = EncriptarContraseña(pContraseñaIngresada);

            return string.Equals(wHash, pContraseñaUsuario);
        }

        public static string ObtenerNombreComercio(int pIdComercio)
        {
            string wDescripcion = string.Empty;

            DAL.Comercios wComerciosDAL = new DAL.Comercios();
            Entities.Comercio wComercio = wComerciosDAL.ObtenerComercio(pIdComercio);

            if(wComercio != null)
            {
                wDescripcion = wComercio.Nombre;
            }

            return wDescripcion;
        }

        public static string ObtenerNombreUsuario(int pIdUsuario)
        {
            string wNombre = string.Empty;

            DAL.Usuarios wUsuariosDAL = new DAL.Usuarios();
            Entities.Usuario wUsuario = wUsuariosDAL.ObtenerUsuario(pIdUsuario);

            if(wUsuario != null)
            {
                wNombre = wUsuario.NombreApellido;
            }

            return wNombre;
        }
    }
}
