using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace ClienteCasino
{
    class ManejoArchivo
    {
            private string nombreFichero;
            //private string rutaArchivo;
            private StreamWriter fichero;
            public ArrayList Datos = new ArrayList();

            /* CONSTRUCTOR DE LA CLASE */
            public ManejoArchivo()
            {
                nombreFichero = @"\SDMMC\PruebaCasino\MarcaFueraLinea.txt";
                //nombreFichero = "MarcaFueraLinea.txt";
                LecturaParametros();
            }

            /* CONSTRUCTOR DE LA CLASE */
            public ManejoArchivo(string nombre)
            {
                nombreFichero = nombre;
                LecturaParametros();
            }

            /* METODO EL CUAL RECIBE UN STRING, Y LA GUARDA EN UN ARCHIVO DE TEXTO PLANO */
            public void EscrituraMarcaFueraLinea(string marca)
            {
                try
                {
                    if (File.Exists(nombreFichero))
                    {
                        fichero = File.AppendText(nombreFichero);
                        fichero.WriteLine(marca);
                        fichero.Close();
                    }
                    else
                    {
                        fichero = File.CreateText(nombreFichero);
                        fichero.WriteLine(marca);
                        fichero.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido el sgt error {0}", ex.Message);
                }
            }

            /* METODO EL CUAL LEE DESDE UN ARCHIVO INI LOS PARAMETROS PARA CONFIGURAR EL MT380 */
            public void LecturaParametros()
            {
                string Ruta = @"\SDMMC\PruebaCasino\Configuracion.txt";
                //string RutaWindows = "Configuracion.txt";
                StreamReader fichero = File.OpenText(Ruta);
                //StreamReader fichero = File.OpenText(RutaWindows);
                string linea;

                /* ESTE ARRAY CONTIENE LA CANTIDAD DE CARACTERES QUE SE OCUPARAN EN LA FUNCION SUBSTRING COMO INDICE
                 * PARA ASI OBTENER LOS DATOS PARAMETRIZADOS */
                int[] Posiciones = { 9, 3, 7, 16, 11, 9, 8, 9, 19, 15, 13, 16}; 
                int contador = 0;

                while (contador <= Posiciones.Length - 1)
                {
                    linea = fichero.ReadLine();
                    Datos.Add(linea.Substring(Posiciones[contador]));
                    contador += 1;
                }
            }
        }
}

