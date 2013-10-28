using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Data.SqlClient;

namespace Cliente
{
    /* MaximizeBox =  Ponlo en FALSE (Esto quita el botón maximizar)
       MinimizeBox =  Ponlo en FALSE (Esto quita el botón minimizar)
       ControlBox = Ponlo en FALSE (Esto quita el botón "X") 
       FormBorderStyle = Ponlo en None para quitar los bordes de la ventana */

    public partial class Form1 : Form
    {
        ManejoArchivo archivo = new ManejoArchivo();
        ConvierteCadena TrabajaCadena;
        
        private int evento;
        private string NomEmpre; 
        private string IP; 
        private int Ratios; 
        private string PuertoImpresora; 

        private string DirreccionIPServidor; 
        private int PuertoServidor;
        private int LargoCadena;
        private bool EstadoServidor;

        /* INICIO DE LA APLICACION */
        public Form1()
        {
            //Unitech.DisableTaskbar();
            //Unitech.DisableDesktop();
            //Unitech.DisableExploreToolbar(); 
 
            Updatefecha ejem = new Updatefecha();
            LargoCadena = Convert.ToInt32(archivo.Datos[10]);
            DirreccionIPServidor = archivo.Datos[8].ToString();
            PuertoServidor = Convert.ToInt32(archivo.Datos[9]);
            IP = archivo.Datos[1].ToString();
            EstadoServidorE();
            ejem.EstableHoraServidor();
            InitializeComponent();
        }

        public static void Main()
        {
            Application.Run(new Form1());
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            evento = 1;
            Timer_Menu.Enabled = true;
            IngresoMenu();
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            evento = 2;
            Timer_Menu.Enabled = true;
            IngresoMenu();
           
        }

        /* TIMER QUE CONTROLA EL ESTADO DE ONLINE O OFFLINE DEL RELOJ ESTE SE EJECUTA POR DEFECTO CADA 15 MIN */       
        private void timer1_Tick(object sender, EventArgs e)
        {
            /* SE ENVIA CADENA FANTASMA, VERIFICACION QUE SE REALIZA PARA SABER SI EL RELOJ ESTA EN LINEA */
            DirreccionIPServidor = archivo.Datos[8].ToString();
            PuertoServidor = Convert.ToInt32(archivo.Datos[9]);
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint DireccionServidor = new IPEndPoint(IPAddress.Parse(DirreccionIPServidor), PuertoServidor);

                byte[] sendResponse = new byte[70];
                sendResponse = Encoding.Default.GetBytes("1012ghost");
                client.Connect(DireccionServidor);    
                client.Send(sendResponse);
                EstadoServidor = true;
                client.Close();
            }
            catch (SocketException)
            {
                EstadoServidor = false;
            }
            
        }

        /* TIMER QUE SE ENCARGA DE ACTUALIZAR EL MENSAJE DE LBLMENSAJE LIMPIANDO Y DANDO EL FOCUS A TXTINGRESO */
        private void Timer_Restablece_Tick(object sender, EventArgs e)
        {
            lblMensaje.Text = "INGRESE SU PIN";
            lblRespuesta.Visible = false; 
            Timer_Restablece.Enabled = false;
            RestableceMenu();
        }

        /* FUNCION QUE REESTABLECE EL MENU DE MARCACION */
        public void RestableceMenu()
        {
            lblMensaje.Text = "MENU MARCACION";
            txtingreso.Visible = false;
            txtingreso.Text = string.Empty;
            txtingreso.Enabled = false;
            btnEnrolar.Visible = false;
            btnNoApta.Visible = false;
            btnElimina.Visible = false;
            btnVerifica.Visible = false;
            btnEntrada.Visible = true;
            btnSalida.Visible = true;
            evento = 0;
        }
       
        /* METODO QUE PERMITE MOSTRAR EL MENU DE MARCACION */
        public void IngresoMenu()
        {
            txtingreso.Visible = true;
            lblMensaje.Text = "INGRESE SU PIN";
            txtingreso.Text = string.Empty;
            txtingreso.Enabled = true;
            txtingreso.Focus();
            btnEntrada.Visible = false;
            btnSalida.Visible = false;
        }

        /* METODO QUE ACTIVA LAS OPCIONES DEL MENU DE ENROLAMIENTO */
        public void IngresoMenuAdmin()
        {
            txtingreso.Visible = true;
            lblMensaje.Text = "INGRESE SU PIN";
            btnEnrolar.Visible = true;
            btnNoApta.Visible = true;
            btnElimina.Visible = true;
            btnVerifica.Visible = true;
            btnEntrada.Visible = false;
            btnSalida.Visible = false;
        }

        /* RUTINA QUE CONTROLA EL INGRESO AL MENU DE ADMINISTRADOR DEL RELOJ */
        private void txtingreso_TextChanged(object sender, EventArgs e)
        {  
            if (txtingreso.TextLength == LargoCadena)
            {
                /* ASIGNACION DE PARAMETROS MEDIANTE LA FUNCION LecturaParametros */             
                TrabajaCadena = new ConvierteCadena();
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint DireccionServidor = new IPEndPoint(IPAddress.Parse(DirreccionIPServidor), PuertoServidor);
                string Mensaje = null;

                try
                {
                    byte[] bytesCliente = new byte[70];
                    byte[] sendResponse = new byte[70]; 
                    
                    sendResponse = Encoding.Default.GetBytes(TrabajaCadena.TransformaCadena(txtingreso.Text, evento));

                    if (EstadoServidor == false)
                    {
                        if (evento == 1)
                        {
                            string cadena = TrabajaCadena.MarcaEntradaFueraLinea(txtingreso.Text, IP);
                            archivo.EscrituraMarcaFueraLinea(cadena);
                            Imprime_OffLine(Mensaje, 3);
                            EstadoServidor = false;
                            txtingreso.Text = string.Empty;
                            lblRespuesta.Visible = true;
                            lblRespuesta.Text = Mensaje;
                            RestableceMenu();
                            Timer_Restablece.Enabled = true;
                        }
                        else
                        {
                            string cadena = TrabajaCadena.MarcaSalidaFueraLinea(txtingreso.Text, IP);
                            archivo.EscrituraMarcaFueraLinea(cadena);
                            Imprime_OffLine(Mensaje, 4);
                            EstadoServidor = false;
                            txtingreso.Text = string.Empty;
                            lblRespuesta.Visible = true;
                            lblRespuesta.Text = Mensaje;
                            RestableceMenu();
                            Timer_Restablece.Enabled = true;
                        }
                    }
                    else
                    {
                        client.Connect(DireccionServidor);
                        client.Send(sendResponse);

                        int bytesRec = client.Receive(bytesCliente);
                        Mensaje = Encoding.Default.GetString(bytesCliente, 0, bytesRec);
                        EstadoServidor = true;

                        /* SE INICIALIZA EL TIMER ENCARGADO DE GENERAR EVENTO DE ENVIO DE MARCAS
                         * FANTASMAS */
                        Timer_inline.Enabled = false;
                        Timer_inline.Enabled = true;

                        /* LLAMADA A LA FUNCION QUE SE ENCARGA DE IMPRIMIR EL VALE */
                        /* 06 EQUIVALE MARCA RESGISTRADA, 01 EQUIVALE A ALGUN ERROR EN LA MARCA */
                        if (Mensaje.Substring(0, 2) == "06")
                        {
                            Imprime_OnLine(Mensaje, evento);
                            Timer_Restablece.Enabled = true;
                            lblRespuesta.Visible = true;
                            lblRespuesta.Text = Mensaje;
                        }
                        else
                        {
                            Timer_Restablece.Enabled = true;
                            lblRespuesta.Visible = true;
                            lblRespuesta.Text = Mensaje;
                        }
                        txtingreso.Text = string.Empty;
                        Mensaje = string.Empty;

                        client.Close();
                        RestableceMenu();
                    }
                }
                catch (SocketException)
                {

                    /* SE AGREGA SECUENCIA IF .. ELSE LA CUAL DETERMINA SE ES ENTRADA FUERA DE LINEA O SALIDA FUERA DE
                     * LINEA, LA CUAL A SU VEZ ES GUARDADA EN UN ARCHIVO TXT PARA SU POSTERIOR RESCATE DESDE
                     * EL MPOINT */
                    client.Close();
                    lblRespuesta.Text = "Marca Resgistrada";

                    if (evento == 1)
                    {
                        string cadena = TrabajaCadena.MarcaEntradaFueraLinea(txtingreso.Text, IP);
                        archivo.EscrituraMarcaFueraLinea(cadena);
                        Imprime_OffLine(Mensaje, 3);
                        Timer_Restablece.Enabled = true;
                    }
                    else
                    {
                        string cadena = TrabajaCadena.MarcaSalidaFueraLinea(txtingreso.Text, IP);
                        archivo.EscrituraMarcaFueraLinea(cadena);
                        Imprime_OffLine(Mensaje, 4);
                        Timer_Restablece.Enabled = true;
                    }
                }
            }

            if (txtingreso.Text == "1012")
            {
                //Unitech.EnableTaskbar();
                //Unitech.EnableDesktop();
                //Unitech.EnableExploreToolbar();
                Application.Exit();
            }
            
            /* IF QUE CONTROLA EL ACCESO A MENU DE ADMINISTRACION */
            //if (txtingreso.Text == "1020")
            //{
            //    IngresoMenuAdmin();
            //}
        }

        /* TIMER QUE CONTROLA EL TIEMPO DE ESPERA EN EL INGRESO DEL PIN O RUT DEL CLIENTE 
           EL CUAL ESTA ESTABLECIDO EN 7 SEG */
        private void Timer_Menu_Tick(object sender, EventArgs e)
        {
            RestableceMenu();
        }

        /* SE MODIFICA FUNCION AGREGANDO EL ENCENDIDO DE LA CAMARA DEL RELOJ */
        public void Imprime_OnLine(string mensaje, int even)
        {
            /* ASIGNACION DE PARAMETROS */
            NomEmpre = archivo.Datos[0].ToString();
            IP = archivo.Datos[1].ToString();
            Ratios = Convert.ToInt32(archivo.Datos[2]);
            PuertoImpresora = archivo.Datos[3].ToString();

            serialPort1.BaudRate = Ratios;
            serialPort1.PortName = PuertoImpresora;

            string evento = null;
            if (even == 01)
            {
                evento = "Entrada";
            }
            else
            {
                if (even == 02)
                {
                    evento = "Salida";
                }
            }

            try
            {
                RelayAccess.SetCameraLED(1, true);
            }
            catch (Exception)
            {
            }

            try
            {
                serialPort1.Open();
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine(" *** ASISTENCIA EN LINEA *** ");
                serialPort1.WriteLine("EVENTO : " + evento);
                serialPort1.WriteLine("FECHA  : " + lblHoraServidor.Text);
                serialPort1.WriteLine("EMPRESA: " + NomEmpre);
                serialPort1.WriteLine("NOMBRE : " + mensaje.Substring(18,10));
                serialPort1.WriteLine("RELOJ  : " + IP);
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine(Convert.ToChar(27) + "i");
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

            try
            {
                RelayAccess.SetCameraLED(1, false);
            }
            catch (Exception)
            {
            }
        }

        /* SE MODIFICA FUNCION AGREGANDO EL ENCENDIDO DE LA CAMARA DEL RELOJ */
        public void Imprime_OffLine(string mensaje, int even)
        {
            /* ASIGNACION DE PARAMETROS */
            NomEmpre = archivo.Datos[0].ToString();
            IP = archivo.Datos[1].ToString();
            Ratios = Convert.ToInt32(archivo.Datos[2]);
            PuertoImpresora = archivo.Datos[3].ToString();

            serialPort1.BaudRate = Ratios;
            serialPort1.PortName = PuertoImpresora;

            string evento = null;
            if (even == 03)
            {
                evento = "Entrada";
            }
            else
            {
                if (even == 04)
                {
                    evento = "Salida";
                }
            }
           
            try
            {
                RelayAccess.SetCameraLED(1, true);
            }
            catch (Exception)
            {
            }

            try
            {
                serialPort1.Open();
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine(" *** ASISTENCIA REGISTRADA *** ");
                serialPort1.WriteLine("EVENTO : " + evento);
                serialPort1.WriteLine("FECHA  : " + lblHoraServidor.Text);
                serialPort1.WriteLine("EMPRESA: " + NomEmpre);
                serialPort1.WriteLine("NOMBRE : " + txtingreso.Text);
                serialPort1.WriteLine("RELOJ  : " + IP);
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine(Convert.ToChar(27) + "i");
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }

            try
            {
                RelayAccess.SetCameraLED(1, false);
            }
            catch (Exception)
            {
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            /* SE COMPRUEBA QUE SI LA LA HORA ACTUAL ES IGUAL A LA QUE SE ESTABLECE COMO HORA DE
             * ACTUALIZACION */
            if (DateTime.Now.ToString("HH:mm") == archivo.Datos[11].ToString())
            {
                Updatefecha fecha = new Updatefecha();
                fecha.EstableHoraServidor();
                lblHoraServidor.Text = DateTime.Now.ToString();
            }
            else
            {
                lblHoraServidor.Text = DateTime.Now.ToString();
            }
        }

        public void EstadoServidorE()
        {
            DirreccionIPServidor = archivo.Datos[8].ToString();
            PuertoServidor = Convert.ToInt32(archivo.Datos[9]);
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint DireccionServidor = new IPEndPoint(IPAddress.Parse(DirreccionIPServidor), PuertoServidor);

                byte[] sendResponse = new byte[70];
                sendResponse = Encoding.Default.GetBytes("1012ghost");
                client.Connect(DireccionServidor);

                client.Send(sendResponse);
                EstadoServidor = true;
                client.Close();
            }
            catch (SocketException)
            {
                EstadoServidor = false;
            }
        }

    }
}
    