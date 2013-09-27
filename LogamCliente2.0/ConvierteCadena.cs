using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace Cliente
{
    public class ConvierteCadena
    {
        public string TransformaCadena(string cadena,int evento)
        {
                string cadena2 = cadena.PadLeft(20, '0');
                string FechaHora = DateTime.Now.ToString("yyyyMMddHHmmss");
                cadena2 += FechaHora;
                cadena2 = cadena2.Insert(34, Convert.ToString(evento));
                cadena2 = cadena2.Insert(0, "$");
                cadena2 = cadena2.Insert(36, "#");
                return cadena2;
        }

        /* ESTA DEEBE TENER EL SGT FORMATO TARJETA_NUMERO+YYYYMMDD+HHMMSS,+EVENTO+IP+TAREA */
        public string MarcaEntradaFueraLinea(string cadena,string ip)
        {
                string cadena2 = cadena.PadLeft(20, '0');
                string FechaHora = DateTime.Now.ToString("yyyyMMddHHmmss");
                cadena2 += FechaHora;
                cadena2 += "3";
                cadena2 += ",";
                cadena2 += ip;
                cadena2 += ",";
                cadena2 += "1";
                return cadena2;
        }

        /* ESTA DEEBE TENER EL SGT FORMATO TARJETA_NUMERO+YYYYMMDD+HHMMSS,+EVENTO+IP+TAREA */
        public string MarcaSalidaFueraLinea(string cadena,string ip)
        {
                string cadena2 = cadena.PadLeft(20, '0');
                string FechaHora = DateTime.Now.ToString("yyyyMMddHHmmss");
                cadena2 += FechaHora;
                cadena2 += "4";
                cadena2 += ",";
                cadena2 += ip;
                cadena2 += ",";
                cadena2 += "1";
                return cadena2;
         }
      }
}
