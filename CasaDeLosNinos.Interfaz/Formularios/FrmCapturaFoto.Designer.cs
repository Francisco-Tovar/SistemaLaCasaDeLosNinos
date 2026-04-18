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
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(498, 50);
            panelCabecera.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.Black;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(468, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.Location = new Point(30, 4);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(127, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Capturar Imagen";
            // 
            // picPreview
            // 
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Dock = DockStyle.Fill;
            picPreview.Location = new Point(1, 51);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(498, 348);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 1;
            picPreview.TabStop = false;
            // 
            // pnlAcciones
            // 
            pnlAcciones.Controls.Add(btnRotar);
            pnlAcciones.Controls.Add(btnCapturar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.Location = new Point(1, 399);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(498, 80);
            pnlAcciones.TabIndex = 2;
            // 
            // btnRotar
            // 
            btnRotar.FlatAppearance.BorderSize = 0;
            btnRotar.FlatStyle = FlatStyle.Flat;
            btnRotar.IconChar = FontAwesome.Sharp.IconChar.Refresh;
            btnRotar.IconColor = Color.Black;
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
            btnCapturar.FlatStyle = FlatStyle.Flat;
            btnCapturar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCapturar.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;
            btnCapturar.IconColor = Color.Black;
            btnCapturar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCapturar.IconSize = 32;
            btnCapturar.Location = new Point(174, 15);
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
            lblMensaje.Dock = DockStyle.Bottom;
            lblMensaje.Location = new Point(1, 379);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(498, 20);
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
