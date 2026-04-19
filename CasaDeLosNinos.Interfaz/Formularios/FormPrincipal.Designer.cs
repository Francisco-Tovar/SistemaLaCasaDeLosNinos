namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FormPrincipal
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
            panelMenu = new Panel();
            btnLogout = new FontAwesome.Sharp.IconButton();
            btnVoluntarios = new FontAwesome.Sharp.IconButton();
            btnCajaChica = new FontAwesome.Sharp.IconButton();
            btnUsuarios = new FontAwesome.Sharp.IconButton();
            btnAsistencia = new FontAwesome.Sharp.IconButton();
            btnNinos = new FontAwesome.Sharp.IconButton();
            panelLogo = new Panel();
            lblOrg = new Label();
            btnHome = new PictureBox();
            panelTitleBar = new Panel();
            btnTheme = new FontAwesome.Sharp.IconButton();
            btnMinimize = new FontAwesome.Sharp.IconButton();
            btnMaximize = new FontAwesome.Sharp.IconButton();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitleChildForm = new Label();
            iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            lblBienvenida = new Label();
            panelShadow = new Panel();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnHome).BeginInit();
            panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconCurrentChildForm).BeginInit();
            panelDesktop.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.Controls.Add(btnLogout);
            panelMenu.Controls.Add(btnCajaChica);
            panelMenu.Controls.Add(btnVoluntarios);
            panelMenu.Controls.Add(btnUsuarios);
            panelMenu.Controls.Add(btnAsistencia);
            panelMenu.Controls.Add(btnNinos);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(1, 1);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 619);
            panelMenu.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogout.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            btnLogout.IconColor = Color.Black;
            btnLogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLogout.IconSize = 32;
            btnLogout.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogout.Location = new Point(0, 559);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(10, 0, 20, 0);
            btnLogout.Size = new Size(220, 60);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnVoluntarios
            // 
            btnVoluntarios.Dock = DockStyle.Top;
            btnVoluntarios.FlatAppearance.BorderSize = 0;
            btnVoluntarios.FlatStyle = FlatStyle.Flat;
            btnVoluntarios.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVoluntarios.IconChar = FontAwesome.Sharp.IconChar.HandsHelping;
            btnVoluntarios.IconColor = Color.Black;
            btnVoluntarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnVoluntarios.IconSize = 32;
            btnVoluntarios.ImageAlign = ContentAlignment.MiddleLeft;
            btnVoluntarios.Location = new Point(0, 320);
            btnVoluntarios.Name = "btnVoluntarios";
            btnVoluntarios.Padding = new Padding(10, 0, 20, 0);
            btnVoluntarios.Size = new Size(220, 60);
            btnVoluntarios.TabIndex = 5;
            btnVoluntarios.Text = "Voluntarios";
            btnVoluntarios.TextAlign = ContentAlignment.MiddleLeft;
            btnVoluntarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVoluntarios.UseVisualStyleBackColor = true;
            btnVoluntarios.Click += btnVoluntarios_Click;
            // 
            // btnCajaChica
            // 
            btnCajaChica.Dock = DockStyle.Top;
            btnCajaChica.FlatAppearance.BorderSize = 0;
            btnCajaChica.FlatStyle = FlatStyle.Flat;
            btnCajaChica.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCajaChica.IconChar = FontAwesome.Sharp.IconChar.Coins;
            btnCajaChica.IconColor = Color.Black;
            btnCajaChica.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCajaChica.IconSize = 32;
            btnCajaChica.ImageAlign = ContentAlignment.MiddleLeft;
            btnCajaChica.Location = new Point(0, 380);
            btnCajaChica.Name = "btnCajaChica";
            btnCajaChica.Padding = new Padding(10, 0, 20, 0);
            btnCajaChica.Size = new Size(220, 60);
            btnCajaChica.TabIndex = 6;
            btnCajaChica.Text = "Caja Chica";
            btnCajaChica.TextAlign = ContentAlignment.MiddleLeft;
            btnCajaChica.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCajaChica.UseVisualStyleBackColor = true;
            btnCajaChica.Click += btnCajaChica_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Dock = DockStyle.Top;
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUsuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnUsuarios.IconColor = Color.Black;
            btnUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUsuarios.IconSize = 32;
            btnUsuarios.ImageAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.Location = new Point(0, 260);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Padding = new Padding(10, 0, 20, 0);
            btnUsuarios.Size = new Size(220, 60);
            btnUsuarios.TabIndex = 3;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // btnAsistencia
            // 
            btnAsistencia.Dock = DockStyle.Top;
            btnAsistencia.FlatAppearance.BorderSize = 0;
            btnAsistencia.FlatStyle = FlatStyle.Flat;
            btnAsistencia.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAsistencia.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            btnAsistencia.IconColor = Color.Black;
            btnAsistencia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAsistencia.IconSize = 32;
            btnAsistencia.ImageAlign = ContentAlignment.MiddleLeft;
            btnAsistencia.Location = new Point(0, 200);
            btnAsistencia.Name = "btnAsistencia";
            btnAsistencia.Padding = new Padding(10, 0, 20, 0);
            btnAsistencia.Size = new Size(220, 60);
            btnAsistencia.TabIndex = 2;
            btnAsistencia.Text = "Asistencia";
            btnAsistencia.TextAlign = ContentAlignment.MiddleLeft;
            btnAsistencia.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAsistencia.UseVisualStyleBackColor = true;
            btnAsistencia.Click += btnAsistencia_Click;
            // 
            // btnNinos
            // 
            btnNinos.Dock = DockStyle.Top;
            btnNinos.FlatAppearance.BorderSize = 0;
            btnNinos.FlatStyle = FlatStyle.Flat;
            btnNinos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNinos.IconChar = FontAwesome.Sharp.IconChar.Baby;
            btnNinos.IconColor = Color.Black;
            btnNinos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNinos.IconSize = 32;
            btnNinos.ImageAlign = ContentAlignment.MiddleLeft;
            btnNinos.Location = new Point(0, 140);
            btnNinos.Name = "btnNinos";
            btnNinos.Padding = new Padding(10, 0, 20, 0);
            btnNinos.Size = new Size(220, 60);
            btnNinos.TabIndex = 1;
            btnNinos.Text = "Niños";
            btnNinos.TextAlign = ContentAlignment.MiddleLeft;
            btnNinos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNinos.UseVisualStyleBackColor = true;
            btnNinos.Click += btnNinos_Click;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(lblOrg);
            panelLogo.Controls.Add(btnHome);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(220, 140);
            panelLogo.TabIndex = 0;
            // 
            // lblOrg
            // 
            lblOrg.Anchor = AnchorStyles.None;
            lblOrg.AutoSize = true;
            lblOrg.BackColor = Color.Transparent;
            lblOrg.Font = new Font("Segoe UI", 11F);
            lblOrg.ForeColor = Color.LightGray;
            lblOrg.Location = new Point(8, 112);
            lblOrg.Name = "lblOrg";
            lblOrg.Size = new Size(145, 20);
            lblOrg.TabIndex = 1;
            lblOrg.Text = "La Casa de los Niños";
            lblOrg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(35, 12);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(146, 94);
            btnHome.SizeMode = PictureBoxSizeMode.Zoom;
            btnHome.TabIndex = 0;
            btnHome.TabStop = false;
            btnHome.Click += btnHome_Click;
            // 
            // panelTitleBar
            // 
            panelTitleBar.Controls.Add(btnTheme);
            panelTitleBar.Controls.Add(btnMinimize);
            panelTitleBar.Controls.Add(btnMaximize);
            panelTitleBar.Controls.Add(btnClose);
            panelTitleBar.Controls.Add(lblTitleChildForm);
            panelTitleBar.Controls.Add(iconCurrentChildForm);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(221, 1);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(928, 75);
            panelTitleBar.TabIndex = 1;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.IconChar = FontAwesome.Sharp.IconChar.Moon;
            btnTheme.IconColor = Color.Black;
            btnTheme.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTheme.IconSize = 25;
            btnTheme.Location = new Point(801, 0);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(30, 30);
            btnTheme.TabIndex = 5;
            btnTheme.TabStop = false;
            btnTheme.UseVisualStyleBackColor = true;
            btnTheme.Click += btnTheme_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            btnMinimize.IconColor = Color.Black;
            btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMinimize.IconSize = 20;
            btnMinimize.Location = new Point(837, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.TabIndex = 4;
            btnMinimize.TabStop = false;
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            btnMaximize.IconColor = Color.Black;
            btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMaximize.IconSize = 20;
            btnMaximize.Location = new Point(868, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(30, 30);
            btnMaximize.TabIndex = 3;
            btnMaximize.TabStop = false;
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
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
            btnClose.Location = new Point(898, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 2;
            btnClose.TabStop = false;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblTitleChildForm
            // 
            lblTitleChildForm.AutoSize = true;
            lblTitleChildForm.Location = new Point(75, 12);
            lblTitleChildForm.Name = "lblTitleChildForm";
            lblTitleChildForm.Size = new Size(38, 17);
            lblTitleChildForm.TabIndex = 1;
            lblTitleChildForm.Text = "Inicio";
            // 
            // iconCurrentChildForm
            // 
            iconCurrentChildForm.BackColor = SystemColors.Control;
            iconCurrentChildForm.ForeColor = SystemColors.ActiveCaptionText;
            iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.House;
            iconCurrentChildForm.IconColor = SystemColors.ActiveCaptionText;
            iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconCurrentChildForm.Location = new Point(21, 12);
            iconCurrentChildForm.Name = "iconCurrentChildForm";
            iconCurrentChildForm.Size = new Size(32, 32);
            iconCurrentChildForm.TabIndex = 0;
            iconCurrentChildForm.TabStop = false;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.BackColor = Color.Transparent;
            lblBienvenida.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(6, 9);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(204, 47);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido";
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelShadow
            // 
            panelShadow.Dock = DockStyle.Top;
            panelShadow.Location = new Point(221, 76);
            panelShadow.Name = "panelShadow";
            panelShadow.Size = new Size(928, 9);
            panelShadow.TabIndex = 2;
            // 
            // panelDesktop
            // 
            panelDesktop.BackColor = SystemColors.ControlDark;
            panelDesktop.BackgroundImageLayout = ImageLayout.Zoom;
            panelDesktop.Controls.Add(lblBienvenida);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(221, 85);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(928, 535);
            panelDesktop.TabIndex = 3;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 621);
            ControlBox = false;
            Controls.Add(panelDesktop);
            Controls.Add(panelShadow);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9.75F);
            MinimumSize = new Size(1150, 600);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "La Casa de los Niños";
            Load += FormPrincipal_Load;
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnHome).EndInit();
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconCurrentChildForm).EndInit();
            panelDesktop.ResumeLayout(false);
            panelDesktop.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelLogo;
        private FontAwesome.Sharp.IconButton btnUsuarios;
        private FontAwesome.Sharp.IconButton btnAsistencia;
        private FontAwesome.Sharp.IconButton btnNinos;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panelTitleBar;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private System.Windows.Forms.Label lblTitleChildForm;
        private System.Windows.Forms.Panel panelShadow;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblOrg;
        private FontAwesome.Sharp.IconButton btnVoluntarios;
        private FontAwesome.Sharp.IconButton btnCajaChica;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnClose;
        private FontAwesome.Sharp.IconButton btnTheme;
        private FontAwesome.Sharp.IconButton btnLogout;
    }
}
