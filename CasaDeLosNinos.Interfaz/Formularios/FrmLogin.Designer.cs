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
            panelSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            SuspendLayout();
            // 
            // panelSide
            // 
            panelSide.BackColor = Color.FromArgb(26, 25, 62);
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
            btnClose.IconColor = Color.Gainsboro;
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
            lblTitulo.ForeColor = Color.Gainsboro;
            lblTitulo.Location = new Point(280, 50);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(178, 24);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "INICIAR SESIÓN";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.ForeColor = Color.Silver;
            lblUsuario.Location = new Point(280, 110);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(56, 17);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.FromArgb(45, 45, 81);
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Font = new Font("Segoe UI", 11F);
            txtUsuario.ForeColor = Color.Gainsboro;
            txtUsuario.Location = new Point(285, 135);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(280, 20);
            txtUsuario.TabIndex = 3;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.ForeColor = Color.Silver;
            lblClave.Location = new Point(280, 180);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(77, 17);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña:";
            // 
            // txtContrasenera
            // 
            txtContrasenera.BackColor = Color.FromArgb(45, 45, 81);
            txtContrasenera.BorderStyle = BorderStyle.None;
            txtContrasenera.Font = new Font("Segoe UI", 11F);
            txtContrasenera.ForeColor = Color.Gainsboro;
            txtContrasenera.Location = new Point(285, 205);
            txtContrasenera.Name = "txtContrasenera";
            txtContrasenera.Size = new Size(280, 20);
            txtContrasenera.TabIndex = 5;
            txtContrasenera.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblError.ForeColor = Color.FromArgb(231, 76, 60);
            lblError.Location = new Point(285, 240);
            lblError.Name = "lblError";
            lblError.Size = new Size(125, 15);
            lblError.TabIndex = 6;
            lblError.Text = "Credenciales inválidas";
            lblError.Visible = false;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(37, 36, 81);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnIngresar.ForeColor = Color.Gainsboro;
            btnIngresar.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
            btnIngresar.IconColor = Color.Gainsboro;
            btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIngresar.IconSize = 24;
            btnIngresar.ImageAlign = ContentAlignment.MiddleLeft;
            btnIngresar.Location = new Point(285, 285);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(280, 45);
            btnIngresar.TabIndex = 7;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += AlHacerClickEnIngresar;
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 33, 74);
            ClientSize = new Size(600, 400);
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
    }
}
