using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblestado = new System.Windows.Forms.Label();
            this.txtingreso = new System.Windows.Forms.TextBox();
            this.btnEntrada = new System.Windows.Forms.Button();
            this.btnSalida = new System.Windows.Forms.Button();
            this.lblHoraServidor = new System.Windows.Forms.Label();
            this.Timer_inline = new System.Windows.Forms.Timer();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.Timer_Restablece = new System.Windows.Forms.Timer();
            this.btnEnrolar = new System.Windows.Forms.Button();
            this.btnNoApta = new System.Windows.Forms.Button();
            this.btnVerifica = new System.Windows.Forms.Button();
            this.btnElimina = new System.Windows.Forms.Button();
            this.Timer_Menu = new System.Windows.Forms.Timer();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Timer_Segundero = new System.Windows.Forms.Timer();
            this.lblRespuesta = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblestado
            // 
            this.lblestado.Location = new System.Drawing.Point(24, 10);
            this.lblestado.Name = "lblestado";
            this.lblestado.Size = new System.Drawing.Size(217, 20);
            // 
            // txtingreso
            // 
            this.txtingreso.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Regular);
            this.txtingreso.Location = new System.Drawing.Point(24, 71);
            this.txtingreso.MaxLength = 13;
            this.txtingreso.Name = "txtingreso";
            this.txtingreso.PasswordChar = '*';
            this.txtingreso.Size = new System.Drawing.Size(260, 52);
            this.txtingreso.TabIndex = 3;
            this.txtingreso.Visible = false;
            this.txtingreso.TextChanged += new System.EventHandler(this.txtingreso_TextChanged);
            // 
            // btnEntrada
            // 
            this.btnEntrada.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.btnEntrada.ForeColor = System.Drawing.Color.Black;
            this.btnEntrada.Location = new System.Drawing.Point(24, 53);
            this.btnEntrada.Name = "btnEntrada";
            this.btnEntrada.Size = new System.Drawing.Size(128, 153);
            this.btnEntrada.TabIndex = 5;
            this.btnEntrada.Text = "Entrada";
            this.btnEntrada.Click += new System.EventHandler(this.btnEntrada_Click);
            // 
            // btnSalida
            // 
            this.btnSalida.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.btnSalida.ForeColor = System.Drawing.Color.Black;
            this.btnSalida.Location = new System.Drawing.Point(174, 53);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(128, 153);
            this.btnSalida.TabIndex = 6;
            this.btnSalida.Text = "Salida";
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            // 
            // lblHoraServidor
            // 
            this.lblHoraServidor.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.lblHoraServidor.ForeColor = System.Drawing.Color.White;
            this.lblHoraServidor.Location = new System.Drawing.Point(3, 0);
            this.lblHoraServidor.Name = "lblHoraServidor";
            this.lblHoraServidor.Size = new System.Drawing.Size(312, 34);
            this.lblHoraServidor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Timer_inline
            // 
            this.Timer_inline.Enabled = true;
            this.Timer_inline.Interval = 120000;
            this.Timer_inline.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Tahoma", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblMensaje.ForeColor = System.Drawing.Color.White;
            this.lblMensaje.Location = new System.Drawing.Point(73, 34);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(168, 16);
            this.lblMensaje.Text = "Menu Marcacion";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Timer_Restablece
            // 
            this.Timer_Restablece.Interval = 2000;
            this.Timer_Restablece.Tick += new System.EventHandler(this.Timer_Restablece_Tick);
            // 
            // btnEnrolar
            // 
            this.btnEnrolar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnEnrolar.Location = new System.Drawing.Point(85, 139);
            this.btnEnrolar.Name = "btnEnrolar";
            this.btnEnrolar.Size = new System.Drawing.Size(67, 67);
            this.btnEnrolar.TabIndex = 8;
            this.btnEnrolar.Text = "Enrolar";
            this.btnEnrolar.Visible = false;
            // 
            // btnNoApta
            // 
            this.btnNoApta.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnNoApta.Location = new System.Drawing.Point(174, 139);
            this.btnNoApta.Name = "btnNoApta";
            this.btnNoApta.Size = new System.Drawing.Size(67, 67);
            this.btnNoApta.TabIndex = 9;
            this.btnNoApta.Text = "No Apta";
            this.btnNoApta.Visible = false;
            // 
            // btnVerifica
            // 
            this.btnVerifica.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnVerifica.Location = new System.Drawing.Point(7, 139);
            this.btnVerifica.Name = "btnVerifica";
            this.btnVerifica.Size = new System.Drawing.Size(72, 67);
            this.btnVerifica.TabIndex = 10;
            this.btnVerifica.Text = "Verificar";
            this.btnVerifica.Visible = false;
            // 
            // btnElimina
            // 
            this.btnElimina.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnElimina.Location = new System.Drawing.Point(248, 139);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(67, 67);
            this.btnElimina.TabIndex = 11;
            this.btnElimina.Text = "Eliminar";
            this.btnElimina.Visible = false;
            // 
            // Timer_Menu
            // 
            this.Timer_Menu.Interval = 5000;
            this.Timer_Menu.Tick += new System.EventHandler(this.Timer_Menu_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 38400;
            this.serialPort1.Handshake = System.IO.Ports.Handshake.XOnXOff;
            this.serialPort1.PortName = "COM4";
            // 
            // Timer_Segundero
            // 
            this.Timer_Segundero.Enabled = true;
            this.Timer_Segundero.Interval = 1000;
            this.Timer_Segundero.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // lblRespuesta
            // 
            this.lblRespuesta.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular);
            this.lblRespuesta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblRespuesta.Location = new System.Drawing.Point(3, 0);
            this.lblRespuesta.Name = "lblRespuesta";
            this.lblRespuesta.Size = new System.Drawing.Size(312, 215);
            this.lblRespuesta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblRespuesta.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(318, 215);
            this.ControlBox = false;
            this.Controls.Add(this.lblRespuesta);
            this.Controls.Add(this.btnElimina);
            this.Controls.Add(this.btnVerifica);
            this.Controls.Add(this.btnNoApta);
            this.Controls.Add(this.btnEnrolar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblHoraServidor);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(this.btnEntrada);
            this.Controls.Add(this.txtingreso);
            this.Controls.Add(this.lblestado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "LOGAM CLIENT";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblestado;
        private System.Windows.Forms.TextBox txtingreso;
        private System.Windows.Forms.Button btnEntrada;
        private System.Windows.Forms.Button btnSalida;
        private Label lblHoraServidor;
        private Timer Timer_inline;
        private Label lblMensaje;
        public Timer Timer_Restablece;
        private Button btnEnrolar;
        private Button btnNoApta;
        private Button btnVerifica;
        private Button btnElimina;
        private Timer Timer_Menu;
        private System.IO.Ports.SerialPort serialPort1;
        private Timer Timer_Segundero;
        private Label lblRespuesta;
    }
}

