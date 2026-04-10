namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmObservaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelCaptura = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.lblContador = new System.Windows.Forms.Label();
            this.txtNuevaObservacion = new System.Windows.Forms.TextBox();
            this.lblAutorInfo = new System.Windows.Forms.Label();
            this.lblNueva = new System.Windows.Forms.Label();
            this.lblHistorial = new System.Windows.Forms.Label();
            this.panelHistorial = new System.Windows.Forms.Panel();
            this.panelEncabezado.SuspendLayout();
            this.panelCaptura.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(704, 54);
            this.panelEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(264, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📋  Bitácora de Observaciones — ";
            // 
            // panelCaptura
            // 
            this.panelCaptura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelCaptura.Controls.Add(this.btnGuardar);
            this.panelCaptura.Controls.Add(this.lblContador);
            this.panelCaptura.Controls.Add(this.txtNuevaObservacion);
            this.panelCaptura.Controls.Add(this.lblAutorInfo);
            this.panelCaptura.Controls.Add(this.lblNueva);
            this.panelCaptura.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCaptura.Location = new System.Drawing.Point(0, 401);
            this.panelCaptura.Name = "panelCaptura";
            this.panelCaptura.Padding = new System.Windows.Forms.Padding(12, 8, 12, 10);
            this.panelCaptura.Size = new System.Drawing.Size(704, 180);
            this.panelCaptura.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(522, 132);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(170, 32);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "💾  Guardar Observación";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.AlGuardarObservacion);
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(150)))));
            this.lblContador.Location = new System.Drawing.Point(12, 128);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(53, 15);
            this.lblContador.TabIndex = 3;
            this.lblContador.Text = "0 / 2000";
            // 
            // txtNuevaObservacion
            // 
            this.txtNuevaObservacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNuevaObservacion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNuevaObservacion.Location = new System.Drawing.Point(12, 52);
            this.txtNuevaObservacion.MaxLength = 2000;
            this.txtNuevaObservacion.Multiline = true;
            this.txtNuevaObservacion.Name = "txtNuevaObservacion";
            this.txtNuevaObservacion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNuevaObservacion.Size = new System.Drawing.Size(680, 70);
            this.txtNuevaObservacion.TabIndex = 2;
            this.txtNuevaObservacion.TextChanged += new System.EventHandler(this.AlCambiarTexto);
            // 
            // lblAutorInfo
            // 
            this.lblAutorInfo.AutoSize = true;
            this.lblAutorInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblAutorInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.lblAutorInfo.Location = new System.Drawing.Point(12, 30);
            this.lblAutorInfo.Name = "lblAutorInfo";
            this.lblAutorInfo.Size = new System.Drawing.Size(0, 15);
            this.lblAutorInfo.TabIndex = 1;
            // 
            // lblNueva
            // 
            this.lblNueva.AutoSize = true;
            this.lblNueva.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNueva.Location = new System.Drawing.Point(12, 10);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Size = new System.Drawing.Size(129, 17);
            this.lblNueva.TabIndex = 0;
            this.lblNueva.Text = "Nueva Observación:";
            // 
            // lblHistorial
            // 
            this.lblHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
            this.lblHistorial.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistorial.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHistorial.Location = new System.Drawing.Point(0, 54);
            this.lblHistorial.Name = "lblHistorial";
            this.lblHistorial.Padding = new System.Windows.Forms.Padding(12, 6, 0, 0);
            this.lblHistorial.Size = new System.Drawing.Size(704, 26);
            this.lblHistorial.TabIndex = 2;
            this.lblHistorial.Text = "Historial (más reciente primero):";
            // 
            // panelHistorial
            // 
            this.panelHistorial.AutoScroll = true;
            this.panelHistorial.BackColor = System.Drawing.Color.White;
            this.panelHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHistorial.Location = new System.Drawing.Point(0, 80);
            this.panelHistorial.Name = "panelHistorial";
            this.panelHistorial.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.panelHistorial.Size = new System.Drawing.Size(704, 321);
            this.panelHistorial.TabIndex = 3;
            // 
            // FrmObservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(704, 581);
            this.Controls.Add(this.panelHistorial);
            this.Controls.Add(this.lblHistorial);
            this.Controls.Add(this.panelCaptura);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(600, 520);
            this.Name = "FrmObservaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bitácora";
            this.Load += new System.EventHandler(this.FrmObservaciones_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelCaptura.ResumeLayout(false);
            this.panelCaptura.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCaptura;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblAutorInfo;
        private System.Windows.Forms.TextBox txtNuevaObservacion;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Panel panelHistorial;
    }
}
