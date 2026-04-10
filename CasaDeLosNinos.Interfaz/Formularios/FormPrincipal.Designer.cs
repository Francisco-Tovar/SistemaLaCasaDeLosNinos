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
            btnAsistencia = new FontAwesome.Sharp.IconButton();
            btnNinos = new FontAwesome.Sharp.IconButton();
            panelLogo = new Panel();
            btnHome = new PictureBox();
            panelTitleBar = new Panel();
            btnTheme = new FontAwesome.Sharp.IconButton();
            btnMinimize = new FontAwesome.Sharp.IconButton();
            btnMaximize = new FontAwesome.Sharp.IconButton();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitleChildForm = new Label();
            iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            panelShadow = new Panel();
            panelDesktop = new Panel();
            lblBienvenida = new Label();
            lblOrg = new Label();
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
            panelMenu.BackColor = Color.FromArgb(31, 30, 68);
            panelMenu.Controls.Add(btnAsistencia);
            panelMenu.Controls.Add(btnNinos);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(2, 2);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(220, 617);
            panelMenu.TabIndex = 0;
            // 
            // btnAsistencia
            // 
            btnAsistencia.Dock = DockStyle.Top;
            btnAsistencia.FlatAppearance.BorderSize = 0;
            btnAsistencia.FlatStyle = FlatStyle.Flat;
            btnAsistencia.ForeColor = Color.Gainsboro;
            btnAsistencia.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            btnAsistencia.IconColor = Color.Gainsboro;
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
            btnNinos.ForeColor = Color.Gainsboro;
            btnNinos.IconChar = FontAwesome.Sharp.IconChar.Baby;
            btnNinos.IconColor = Color.Gainsboro;
            btnNinos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNinos.IconSize = 32;
            btnNinos.ImageAlign = ContentAlignment.MiddleLeft;
            btnNinos.Location = new Point(0, 140);
            btnNinos.Name = "btnNinos";
            btnNinos.Padding = new Padding(10, 0, 20, 0);
            btnNinos.Size = new Size(220, 60);
            btnNinos.TabIndex = 1;
            btnNinos.Text = "Gestión de Niños";
            btnNinos.TextAlign = ContentAlignment.MiddleLeft;
            btnNinos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNinos.UseVisualStyleBackColor = true;
            btnNinos.Click += btnNinos_Click;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(btnHome);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(220, 140);
            panelLogo.TabIndex = 0;
            // 
            // btnHome
            // 
            btnHome.Location = new Point(35, 23);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(146, 94);
            btnHome.SizeMode = PictureBoxSizeMode.Zoom;
            btnHome.TabIndex = 0;
            btnHome.TabStop = false;
            btnHome.Click += btnHome_Click;
            // 
            // panelTitleBar
            // 
            panelTitleBar.BackColor = Color.FromArgb(26, 25, 62);
            panelTitleBar.Controls.Add(btnTheme);
            panelTitleBar.Controls.Add(btnMinimize);
            panelTitleBar.Controls.Add(btnMaximize);
            panelTitleBar.Controls.Add(btnClose);
            panelTitleBar.Controls.Add(lblTitleChildForm);
            panelTitleBar.Controls.Add(iconCurrentChildForm);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(222, 2);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(760, 75);
            panelTitleBar.TabIndex = 1;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.FlatAppearance.BorderSize = 0;
            btnTheme.FlatStyle = FlatStyle.Flat;
            btnTheme.IconChar = FontAwesome.Sharp.IconChar.Moon;
            btnTheme.IconColor = Color.White;
            btnTheme.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTheme.IconSize = 25;
            btnTheme.Location = new Point(633, 0);
            btnTheme.Name = "btnTheme";
            btnTheme.Size = new Size(30, 30);
            btnTheme.TabIndex = 5;
            btnTheme.UseVisualStyleBackColor = true;
            btnTheme.Click += btnTheme_Click;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            btnMinimize.IconColor = Color.White;
            btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMinimize.IconSize = 20;
            btnMinimize.Location = new Point(669, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.TabIndex = 4;
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = FontAwesome.Sharp.IconChar.WindowMaximize;
            btnMaximize.IconColor = Color.White;
            btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMaximize.IconSize = 20;
            btnMaximize.Location = new Point(700, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(30, 30);
            btnMaximize.TabIndex = 3;
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.White;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(730, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 2;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblTitleChildForm
            // 
            lblTitleChildForm.AutoSize = true;
            lblTitleChildForm.ForeColor = Color.Gainsboro;
            lblTitleChildForm.Location = new Point(59, 23);
            lblTitleChildForm.Name = "lblTitleChildForm";
            lblTitleChildForm.Size = new Size(38, 17);
            lblTitleChildForm.TabIndex = 1;
            lblTitleChildForm.Text = "Inicio";
            // 
            // iconCurrentChildForm
            // 
            iconCurrentChildForm.BackColor = Color.FromArgb(26, 25, 62);
            iconCurrentChildForm.ForeColor = Color.MediumPurple;
            iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.House;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconCurrentChildForm.Location = new Point(21, 16);
            iconCurrentChildForm.Name = "iconCurrentChildForm";
            iconCurrentChildForm.Size = new Size(32, 32);
            iconCurrentChildForm.TabIndex = 0;
            iconCurrentChildForm.TabStop = false;
            // 
            // panelShadow
            // 
            panelShadow.BackColor = Color.FromArgb(26, 24, 58);
            panelShadow.Dock = DockStyle.Top;
            panelShadow.Location = new Point(222, 77);
            panelShadow.Name = "panelShadow";
            panelShadow.Size = new Size(760, 9);
            panelShadow.TabIndex = 2;
            // 
            // panelDesktop
            // 
            panelDesktop.BackColor = Color.FromArgb(34, 33, 74);
            panelDesktop.Controls.Add(lblBienvenida);
            panelDesktop.Controls.Add(lblOrg);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(222, 86);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(760, 533);
            panelDesktop.TabIndex = 3;
            // 
            // lblBienvenida
            // 
            lblBienvenida.Anchor = AnchorStyles.None;
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblBienvenida.ForeColor = Color.Gainsboro;
            lblBienvenida.Location = new Point(313, 228);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(135, 31);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido";
            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOrg
            // 
            lblOrg.Anchor = AnchorStyles.None;
            lblOrg.AutoSize = true;
            lblOrg.Font = new Font("Segoe UI", 11F);
            lblOrg.ForeColor = Color.LightGray;
            lblOrg.Location = new Point(322, 269);
            lblOrg.Name = "lblOrg";
            lblOrg.Size = new Size(145, 20);
            lblOrg.TabIndex = 1;
            lblOrg.Text = "La Casa de los Niños";
            lblOrg.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 621);
            ControlBox = false;
            Controls.Add(panelDesktop);
            Controls.Add(panelShadow);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9.75F);
            MinimumSize = new Size(800, 500);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "La Casa de los Niños";
            Load += FormPrincipal_Load;
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
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
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnClose;
        private FontAwesome.Sharp.IconButton btnTheme;
    }
}
