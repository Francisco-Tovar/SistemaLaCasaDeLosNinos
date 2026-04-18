namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmLogin
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
            panelSide = new Panel();
            iconLogo = new PictureBox();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblClave = new Label();
            txtContrasenera = new TextBox();
            lblError = new Label();
            btnIngresar = new FontAwesome.Sharp.IconButton();
            lblCreditos = new Label();
            panelSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            SuspendLayout();
            // 
            // panelSide
            // 
            panelSide.Controls.Add(iconLogo);
            panelSide.Dock = DockStyle.Left;
            panelSide.Location = new Point(1, 1);
            panelSide.Name = "panelSide";
            panelSide.Size = new Size(250, 398);
            panelSide.TabIndex = 0;
            // 
            // iconLogo
            // 
            iconLogo.BackColor = Color.Transparent;
            iconLogo.Location = new Point(25, 100);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new Size(200, 200);
            iconLogo.SizeMode = PictureBoxSizeMode.Zoom;
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.Black;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 24;
            btnClose.Location = new Point(570, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 8;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(280, 50);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(178, 24);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "INICIAR SESIÓN";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(280, 110);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(56, 17);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.Location = new Point(285, 135);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(280, 20);
            txtUsuario.TabIndex = 3;
            txtUsuario.TextAlign = HorizontalAlignment.Center;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Location = new Point(280, 180);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(77, 17);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña:";
            // 
            // txtContrasenera
            // 
            txtContrasenera.BorderStyle = BorderStyle.None;
            txtContrasenera.Font = new Font("Segoe UI", 11F);
            txtContrasenera.Location = new Point(285, 205);
            txtContrasenera.Name = "txtContrasenera";
            txtContrasenera.Size = new Size(280, 20);
            txtContrasenera.TabIndex = 5;
            txtContrasenera.TextAlign = HorizontalAlignment.Center;
            txtContrasenera.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblError.Location = new Point(285, 240);
            lblError.Name = "lblError";
            lblError.Size = new Size(125, 15);
            lblError.TabIndex = 6;
            lblError.Text = "Credenciales inválidas";
            lblError.Visible = false;
            // 
            // btnIngresar
            // 
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnIngresar.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
            btnIngresar.IconColor = Color.Black;
            btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIngresar.IconSize = 24;
            btnIngresar.Location = new Point(285, 285);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(280, 45);
            btnIngresar.TabIndex = 7;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.TextAlign = ContentAlignment.MiddleRight;
            btnIngresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += AlHacerClickEnIngresar;
            // 
            // lblCreditos
            // 
            lblCreditos.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblCreditos.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblCreditos.Location = new Point(260, 359);
            lblCreditos.Name = "lblCreditos";
            lblCreditos.Size = new Size(150, 30);
            lblCreditos.TabIndex = 9;
            lblCreditos.Text = "Sistema Creado por:\r\nFrancisco Tovar @2026";
            lblCreditos.TextAlign = ContentAlignment.BottomLeft;
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(lblCreditos);
            Controls.Add(btnClose);
            Controls.Add(btnIngresar);
            Controls.Add(lblError);
            Controls.Add(txtContrasenera);
            Controls.Add(lblClave);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(lblTitulo);
            Controls.Add(panelSide);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Acceso — La Casa de los Niños";
            panelSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Panel panelSide;
        private System.Windows.Forms.PictureBox iconLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtContrasenera;
        private System.Windows.Forms.Label lblError;
        private FontAwesome.Sharp.IconButton btnIngresar;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Label lblCreditos;
    }
}
