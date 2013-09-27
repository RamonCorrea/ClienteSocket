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
        
        private int evento;
        private string NomEmpre; 
        private string IP; 
        private int Ratios; 
        private string PuertoImpresora; 

        private string ServidorBD; 
        private string NombreBD; 
        private string Usuario; 
        private string Password; 

        private string DirreccionIPServidor; 
        private int PuertoServidor;
        private int LargoCadena;

        public Form1()
        {
            Unitech.DisableTaskbar();
            Unitech.DisableDesktop();
            Unitech.DisableExploreToolbar();
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

        /* TIMER ENCARGADO DE ACTUALIZAR EL RELOJ DE EL CLIENTE */
        private void timer1_Tick(object sender, EventArgs e)
        {
            /* ASIGNACION DE VALORES A PARAMETROS MEDIANTE LA FUNCION LecturaParametros */
            ServidorBD = archivo.Datos[4].ToString();
            NombreBD = archivo.Datos[5].ToString();
            Usuario = archivo.Datos[6].ToString();
            Password = archivo.Datos[7].ToString();

            /* SE ESPECIFICA AL SERVIDOR AL CUAL SE VA A CONECTAR EL CLIENTE PARA OBTENER LA HORA Y LA FECHA */
            SqlConnection conn = new SqlConnection("data source =" + ServidorBD + "; initial catalog =" + NombreBD + "; user id =" + Usuario +"; password ="+ Password);
            SqlDataReader lectura;
            SqlCommand Fecha = new SqlCommand();

            try
            {
                /* SENTENCIAS LA CUALES ABREN LA CONEXION A LA BASE DE DATOS Y EJECUTAN EL COMANDO SQL */
                conn.Open();
                Fecha.Connection = conn;
                Fecha.CommandText = "SELECT convert(char(10),getdate(),103) +' '+ convert(char(10),getdate(),114) as 'Fecha_Actual'";
                lectura = Fecha.ExecuteReader();
                while (lectura.Read())
                {
                    lblHoraServidor.Text = lectura[0].ToString();
                    lblHoraServidor.Text = lblHoraServidor.Text.Remove(19, 2);
                }
                lectura.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                lblHoraServidor.Text = ex.Message;
                conn.Close();
            }
        }

        /* TIMER QUE SE ENCARGA DE ACTUALIZAR EL MENSAJE DE LBLMENSAJE LIMPIANDO Y DANDO EL FOCUS A TXTINGRESO */
        private void Timer_Restablece_Tick(object sender, EventArgs e)
        {
            lblMensaje.Text = "INGRESE SU PIN";
            Timer_Restablece.Enabled = false;
            txtingreso.Text = "";
            txtingreso.Focus();
            RestableceMenu();
        }

        /* FUNCION QUE REESTABLECE EL MENU DE MARCACION */
        public void RestableceMenu()
        {
            lblMensaje.Text = "MENU MARCACION";
            txtingreso.Visible = false;
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
            txtingreso.Text = "";
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
            LargoCadena = Convert.ToInt32(archivo.Datos[10]);
            if (txtingreso.TextLength == LargoCadena)
            {
                /* ASIGNACION DE PARAMETROS MEDIANTE LA FUNCION LecturaParametros */
                DirreccionIPServidor = archivo.Datos[8].ToString();
                PuertoServidor = Convert.ToInt32(archivo.Datos[9]);

                ConvierteCadena TrabajaCadena = new ConvierteCadena();
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint DireccionServidor = new IPEndPoint(IPAddress.Parse(DirreccionIPServidor), PuertoServidor);
                string Mensaje = null;

                try
                {
                    /*INSTRUCCIONES QUE SE ENCARGAN DE LA COMUNICACION DE EL SERVIDOR Y EL CLIENTE */
                    if (txtingreso.Text == string.Empty)
                    {
                        MessageBox.Show("Ingrese Texto");
                        txtingreso.Focus();
                    }
                    else
                    {
                        client.Connect(DireccionServidor);
                        byte[] bytesCliente = new byte[35];
                        byte[] sendResponse = new byte[35];

                        sendResponse = Encoding.Default.GetBytes(TrabajaCadena.TransformaCadena(txtingreso.Text, evento));
                        client.Send(sendResponse);
                        lblMensaje.Text = "ENVIANDO INFORMACION";

                        int bytesRec = client.Receive(bytesCliente);
                        Mensaje = Encoding.Default.GetString(bytesCliente, 0, bytesRec);

                        /* LLAMADA A LA FUNCION QUE SE ENCARGA DE IMPRIMIR EL VALE */
                        /* 06 EQUIVALE MARCA RESGISTRADA, 01 EQUIVALE A ALGUN ERROR EN LA MARCA */
                        if (Mensaje.Substring(0, 2) == "06")
                        {
                            Imprime_OnLine(Mensaje, evento);
                        }
                        else
                        {
                            if (Mensaje.Substring(0, 2) == "01")
                            {
                                lblMensaje.Text = "Personal no Existe";
                                Timer_Restablece.Enabled = true;
                            }
                        }

                        lblMensaje.Text = Mensaje;
                        txtingreso.Text = "";
                        txtingreso.Focus();
                        client.Close();
                        RestableceMenu();
                    }
                }
                catch (SocketException)
                {
                    /* SE AGREGA SECUENCIA IF .. ELSE LA CUAL DETERMINA SE ES ENTRADA FUERA DE LINEA O SALIDA FUERA DE
                     * LINEA, LA CUAL A SU VEZ ES GUARDADA EN UN ARCHIVO TXT PARA SU POSTERIOR RESCATE DESDE
                     * EL MPOINT */
                    ManejoArchivo fila = new ManejoArchivo();
                    lblMensaje.Text = "SERVIDOR OFFLINE";
                    IP = archivo.Datos[1].ToString();

                    if (evento == 1)
                    {
                        fila.EscrituraMarcaFueraLinea(TrabajaCadena.MarcaEntradaFueraLinea(txtingreso.Text, IP));
                        Imprime_OffLine(Mensaje, 3);
                        Timer_Restablece.Enabled = true;
                    }
                    else if(evento == 2)
                    {
                        fila.EscrituraMarcaFueraLinea(TrabajaCadena.MarcaSalidaFueraLinea(txtingreso.Text,IP));
                        Imprime_OffLine(Mensaje, 4);
                        Timer_Restablece.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Timer_Restablece.Enabled = true;
                }
            }

            if (txtingreso.Text == "1012")
            {
                Unitech.EnableTaskbar();
                Unitech.EnableDesktop();
                Unitech.EnableExploreToolbar();
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
        }

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
            if ((even == 01) || (even == 03))
            {
                evento = "Entrada";
            }
            else
            {
                if ((even == 02) || (even == 04))
                {
                    evento = "Salida";
                }
            }

            try
            {
                serialPort1.Open();
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine("");
                serialPort1.WriteLine(" *** ASISTENCIA FUERA DE LINEA *** ");
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
        }
    }
}
    