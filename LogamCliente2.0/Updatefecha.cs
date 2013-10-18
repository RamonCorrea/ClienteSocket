using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;

namespace Cliente
{
    class Updatefecha
    {
        private string ServidorBD;
        private string NombreBD;
        private string Usuario;
        private string Password;

        /* ESTRUCTURA LA CUAL PERMITE MODIFICAR LA HORA DEL SISTEMA */
        public struct ComponeFecha
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }
        //[DllImport("kernel32.dll")]
        //[DllImport("coredll.dll")]
        [DllImport("coredll.dll")]
        public extern static void GetSystemTime(ref ComponeFecha sysTime);

        [DllImport("coredll.dll")]
        public extern static bool SetSystemTime(ref ComponeFecha sysTime);

        public void EstableHoraServidor()
        {
            ManejoArchivo archivo = new ManejoArchivo();
 
            /* ASIGNACION DE VALORES A PARAMETROS MEDIANTE LA FUNCION LecturaParametros */
            ServidorBD = archivo.Datos[4].ToString();
            NombreBD = archivo.Datos[5].ToString();
            Usuario = archivo.Datos[6].ToString();
            Password = archivo.Datos[7].ToString();


            /* SE ESPECIFICA AL SERVIDOR AL CUAL SE VA A CONECTAR EL CLIENTE PARA OBTENER LA HORA Y LA FECHA */
            SqlConnection conn = new SqlConnection("data source =" + ServidorBD + "; initial catalog =" + NombreBD + "; user id =" + Usuario + "; password =" + Password);
            SqlDataReader lectura;
            SqlCommand Fecha = new SqlCommand();
            ComponeFecha fecha = new ComponeFecha();

            try
            {
                /* SENTENCIAS LA CUALES ABREN LA CONEXION A LA BASE DE DATOS Y EJECUTAN EL COMANDO SQL */
                conn.Open();
                Fecha.Connection = conn;
                Fecha.CommandText = "SELECT GETDATE()as FechaActual, datepart(dw,getdate()) - 1 as DiaSemana";
                lectura = Fecha.ExecuteReader();

                while (lectura.Read())
                {
                    char[] delimitadores ={ '/', ' ', ':', '.', '-' };
                    string hora = lectura["FechaActual"].ToString();
                    string diaSemana = lectura["DiaSemana"].ToString();
                    string[] DatosHora = hora.Split(delimitadores);
                    int valor = Convert.ToInt32(DatosHora[3]);
                    valor = valor + (4 % 24);
                    DatosHora[3] = Convert.ToString(valor);

                    fecha.Year = (ushort)Convert.ToUInt16(DatosHora[2]);
                    fecha.Month = (ushort)Convert.ToUInt16(DatosHora[1]);
                    fecha.DayOfWeek = (ushort)Convert.ToUInt16(diaSemana);
                    fecha.Day = (ushort)Convert.ToUInt16(DatosHora[0]);
                    fecha.Hour = (ushort)Convert.ToUInt16(DatosHora[3]);
                    fecha.Minute = (ushort)Convert.ToUInt16(DatosHora[4]);
                    fecha.Second = (ushort)Convert.ToUInt16(DatosHora[5]);

                    SetSystemTime(ref fecha);
                }
                lectura.Close();
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
            }
        }
    }
}
