namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmObservaciones
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelCaptura = new System.Windows.Forms.Panel();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
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
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelEncabezado.Controls.Add(this.btnClose);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(704, 54);
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelEncabezado_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnClose.IconColor = System.Drawing.Color.Gainsboro;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 20;
            this.btnClose.Location = new System.Drawing.Point(674, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.AlHacerClickEnCerrar);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitulo.Location = new System.Drawing.Point(12, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(264, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📋  Bitácora de Observaciones — ";
            // 
            // panelCaptura
            // 
            this.panelCaptura.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
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
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.PaperPlane;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 24;
            this.btnGuardar.Location = new System.Drawing.Point(520, 130);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(170, 40);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.AlGuardarObservacion);
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblContador.ForeColor = System.Drawing.Color.Silver;
            this.lblContador.Location = new System.Drawing.Point(12, 130);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(53, 15);
            this.lblContador.TabIndex = 3;
            this.lblContador.Text = "0 / 2000";
            // 
            // txtNuevaObservacion
            // 
            this.txtNuevaObservacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNuevaObservacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.txtNuevaObservacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNuevaObservacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNuevaObservacion.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtNuevaObservacion.Location = new System.Drawing.Point(12, 55);
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
            this.lblAutorInfo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblAutorInfo.ForeColor = System.Drawing.Color.Silver;
            this.lblAutorInfo.Location = new System.Drawing.Point(12, 32);
            this.lblAutorInfo.Name = "lblAutorInfo";
            this.lblAutorInfo.Size = new System.Drawing.Size(0, 15);
            this.lblAutorInfo.TabIndex = 1;
            // 
            // lblNueva
            // 
            this.lblNueva.AutoSize = true;
            this.lblNueva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNueva.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNueva.Location = new System.Drawing.Point(12, 10);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Size = new System.Drawing.Size(140, 19);
            this.lblNueva.TabIndex = 0;
            this.lblNueva.Text = "Nueva Observación:";
            // 
            // lblHistorial
            // 
            this.lblHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.lblHistorial.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistorial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistorial.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHistorial.Location = new System.Drawing.Point(0, 54);
            this.lblHistorial.Name = "lblHistorial";
            this.lblHistorial.Padding = new System.Windows.Forms.Padding(12, 10, 0, 0);
            this.lblHistorial.Size = new System.Drawing.Size(704, 30);
            this.lblHistorial.TabIndex = 2;
            this.lblHistorial.Text = "Historial:";
            // 
            // panelHistorial
            // 
            this.panelHistorial.AutoScroll = true;
            this.panelHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.panelHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHistorial.Location = new System.Drawing.Point(0, 84);
            this.panelHistorial.Name = "panelHistorial";
            this.panelHistorial.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelHistorial.Size = new System.Drawing.Size(704, 317);
            this.panelHistorial.TabIndex = 3;
            // 
            // FrmObservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(704, 581);
            this.Controls.Add(this.panelHistorial);
            this.Controls.Add(this.lblHistorial);
            this.Controls.Add(this.panelCaptura);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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

        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCaptura;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblAutorInfo;
        private System.Windows.Forms.TextBox txtNuevaObservacion;
        private System.Windows.Forms.Label lblContador;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Panel panelHistorial;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
