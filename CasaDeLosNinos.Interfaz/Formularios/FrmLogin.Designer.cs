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
            panelSide = new System.Windows.Forms.Panel();
            iconLogo = new FontAwesome.Sharp.IconPictureBox();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new System.Windows.Forms.Label();
            lblUsuario = new System.Windows.Forms.Label();
            txtUsuario = new System.Windows.Forms.TextBox();
            lblClave = new System.Windows.Forms.Label();
            txtContrasenera = new System.Windows.Forms.TextBox();
            lblError = new System.Windows.Forms.Label();
            btnIngresar = new FontAwesome.Sharp.IconButton();
            panelSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconLogo).BeginInit();
            SuspendLayout();
            // 
            // panelSide
            // 
            panelSide.BackColor = System.Drawing.Color.FromArgb(26, 25, 62);
            panelSide.Controls.Add(iconLogo);
            panelSide.Dock = System.Windows.Forms.DockStyle.Left;
            panelSide.Location = new System.Drawing.Point(0, 0);
            panelSide.Name = "panelSide";
            panelSide.Size = new System.Drawing.Size(250, 400);
            panelSide.TabIndex = 0;
            // 
            // iconLogo
            // 
            iconLogo.BackColor = System.Drawing.Color.FromArgb(26, 25, 62);
            iconLogo.ForeColor = System.Drawing.Color.MediumPurple;
            iconLogo.IconChar = FontAwesome.Sharp.IconChar.HomeUser;
            iconLogo.IconColor = System.Drawing.Color.MediumPurple;
            iconLogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconLogo.IconSize = 200;
            iconLogo.Location = new System.Drawing.Point(25, 100);
            iconLogo.Name = "iconLogo";
            iconLogo.Size = new System.Drawing.Size(200, 200);
            iconLogo.TabIndex = 0;
            iconLogo.TabStop = false;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = System.Drawing.Color.Gainsboro;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 24;
            btnClose.Location = new System.Drawing.Point(570, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(30, 30);
            btnClose.TabIndex = 8;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.Gainsboro;
            lblTitulo.Location = new System.Drawing.Point(280, 50);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(177, 30);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "INICIAR SESIÓN";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.ForeColor = System.Drawing.Color.Silver;
            lblUsuario.Location = new System.Drawing.Point(280, 110);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new System.Drawing.Size(56, 17);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = System.Drawing.Color.FromArgb(45, 45, 81);
            txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtUsuario.ForeColor = System.Drawing.Color.Gainsboro;
            txtUsuario.Location = new System.Drawing.Point(285, 135);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new System.Drawing.Size(280, 20);
            txtUsuario.TabIndex = 3;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.ForeColor = System.Drawing.Color.Silver;
            lblClave.Location = new System.Drawing.Point(280, 180);
            lblClave.Name = "lblClave";
            lblClave.Size = new System.Drawing.Size(77, 17);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña:";
            // 
            // txtContrasenera
            // 
            txtContrasenera.BackColor = System.Drawing.Color.FromArgb(45, 45, 81);
            txtContrasenera.BorderStyle = System.Windows.Forms.BorderStyle.None;
            txtContrasenera.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtContrasenera.ForeColor = System.Drawing.Color.Gainsboro;
            txtContrasenera.Location = new System.Drawing.Point(285, 205);
            txtContrasenera.Name = "txtContrasenera";
            txtContrasenera.Size = new System.Drawing.Size(280, 20);
            txtContrasenera.TabIndex = 5;
            txtContrasenera.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            lblError.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            lblError.Location = new System.Drawing.Point(285, 240);
            lblError.Name = "lblError";
            lblError.Size = new System.Drawing.Size(125, 15);
            lblError.TabIndex = 6;
            lblError.Text = "Credenciales inválidas";
            lblError.Visible = false;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = System.Drawing.Color.FromArgb(37, 36, 81);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnIngresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnIngresar.ForeColor = System.Drawing.Color.Gainsboro;
            btnIngresar.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
            btnIngresar.IconColor = System.Drawing.Color.Gainsboro;
            btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIngresar.IconSize = 24;
            btnIngresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnIngresar.Location = new System.Drawing.Point(285, 285);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new System.Drawing.Size(280, 45);
            btnIngresar.TabIndex = 7;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += AlHacerClickEnIngresar;
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(34, 33, 74);
            ClientSize = new System.Drawing.Size(600, 400);
            Controls.Add(btnClose);
            Controls.Add(btnIngresar);
            Controls.Add(lblError);
            Controls.Add(txtContrasenera);
            Controls.Add(lblClave);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(lblTitulo);
            Controls.Add(panelSide);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmLogin";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Acceso — La Casa de los Niños";
            panelSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Panel panelSide;
        private FontAwesome.Sharp.IconPictureBox iconLogo;
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
