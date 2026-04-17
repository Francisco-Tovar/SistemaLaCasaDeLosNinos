namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmCapturaFoto
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
            panelCabecera = new Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new Label();
            picPreview = new PictureBox();
            pnlAcciones = new Panel();
            btnRotar = new FontAwesome.Sharp.IconButton();
            btnCapturar = new FontAwesome.Sharp.IconButton();
            lblMensaje = new Label();
            panelCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            pnlAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.BackColor = Color.FromArgb(26, 25, 62);
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(0, 0);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(500, 50);
            panelCabecera.TabIndex = 0;
            panelCabecera.MouseDown += (s, e) => DragForm();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.Gainsboro;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(470, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += (s, e) => this.Close();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Gainsboro;
            lblTitulo.Location = new Point(12, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(134, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Capturar Imagen";
            // 
            // picPreview
            // 
            picPreview.BackColor = Color.FromArgb(45, 45, 81);
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Dock = DockStyle.Fill;
            picPreview.Location = new Point(0, 50);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(500, 350);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // pnlAcciones
            // 
            pnlAcciones.BackColor = Color.FromArgb(34, 33, 74);
            pnlAcciones.Controls.Add(btnRotar);
            pnlAcciones.Controls.Add(btnCapturar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(0, 400);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(500, 80);
            pnlAcciones.TabIndex = 2;
            // 
            // btnRotar
            // 
            btnRotar.FlatAppearance.BorderSize = 0;
            btnRotar.FlatStyle = FlatStyle.Flat;
            btnRotar.ForeColor = Color.Gainsboro;
            btnRotar.IconChar = FontAwesome.Sharp.IconChar.Sync;
            btnRotar.IconColor = Color.FromArgb(241, 196, 15);
            btnRotar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRotar.IconSize = 28;
            btnRotar.Location = new Point(30, 15);
            btnRotar.Name = "btnRotar";
            btnRotar.Size = new Size(50, 50);
            btnRotar.TabIndex = 1;
            btnRotar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRotar.UseVisualStyleBackColor = true;
            btnRotar.Click += AlHacerClickEnRotar;
            // 
            // btnCapturar
            // 
            btnCapturar.Anchor = AnchorStyles.Top;
            btnCapturar.FlatAppearance.BorderColor = Color.FromArgb(46, 204, 113);
            btnCapturar.FlatStyle = FlatStyle.Flat;
            btnCapturar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCapturar.ForeColor = Color.FromArgb(46, 204, 113);
            btnCapturar.IconChar = FontAwesome.Sharp.IconChar.Camera;
            btnCapturar.IconColor = Color.FromArgb(46, 204, 113);
            btnCapturar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCapturar.IconSize = 32;
            btnCapturar.Location = new Point(175, 15);
            btnCapturar.Name = "btnCapturar";
            btnCapturar.Size = new Size(150, 50);
            btnCapturar.TabIndex = 0;
            btnCapturar.Text = " CAPTURAR";
            btnCapturar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCapturar.UseVisualStyleBackColor = true;
            btnCapturar.Click += AlHacerClickEnCapturar;
            // 
            // lblMensaje
            // 
            lblMensaje.BackColor = Color.FromArgb(20, 20, 40);
            lblMensaje.Dock = DockStyle.Bottom;
            lblMensaje.ForeColor = Color.Silver;
            lblMensaje.Location = new Point(0, 380);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(500, 20);
            lblMensaje.TabIndex = 3;
            lblMensaje.Text = "Alinee la cámara y presione Capturar";
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmCapturaFoto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 480);
            Controls.Add(lblMensaje);
            Controls.Add(picPreview);
            Controls.Add(pnlAcciones);
            Controls.Add(panelCabecera);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmCapturaFoto";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Capturar Foto";
            FormClosing += FrmCapturaFoto_FormClosing;
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            pnlAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel panelCabecera;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnClose;
        private PictureBox picPreview;
        private Panel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnCapturar;
        private FontAwesome.Sharp.IconButton btnRotar;
        private Label lblMensaje;
    }
}
