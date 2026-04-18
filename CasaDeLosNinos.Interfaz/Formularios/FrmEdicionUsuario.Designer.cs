namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmEdicionUsuario
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
            lblTitulo = new Label();
            btnClose = new FontAwesome.Sharp.IconButton();
            tableLayoutPanel = new TableLayoutPanel();
            lblNombreCompleto = new Label();
            txtNombreCompleto = new TextBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblRol = new Label();
            cmbRol = new ComboBox();
            lblPassword = new Label();
            pnlPassword = new Panel();
            txtPassword = new TextBox();
            btnShowPass = new FontAwesome.Sharp.IconButton();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            lblPasswordHint = new Label();
            pnlAcciones = new FlowLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            panelCabecera.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            pnlPassword.SuspendLayout();
            pnlAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(418, 60);
            panelCabecera.TabIndex = 10;
            panelCabecera.MouseDown += panelCabecera_MouseDown;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(145, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "👤 Editar Usuario";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.Gray;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(383, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnCancelar_Click;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel.Controls.Add(lblNombreCompleto, 0, 0);
            tableLayoutPanel.Controls.Add(txtNombreCompleto, 1, 0);
            tableLayoutPanel.Controls.Add(lblUsername, 0, 1);
            tableLayoutPanel.Controls.Add(txtUsername, 1, 1);
            tableLayoutPanel.Controls.Add(lblRol, 0, 2);
            tableLayoutPanel.Controls.Add(cmbRol, 1, 2);
            tableLayoutPanel.Controls.Add(lblPassword, 0, 3);
            tableLayoutPanel.Controls.Add(pnlPassword, 1, 3);
            tableLayoutPanel.Controls.Add(lblConfirmar, 0, 4);
            tableLayoutPanel.Controls.Add(txtConfirmar, 1, 4);
            tableLayoutPanel.Controls.Add(lblPasswordHint, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Top;
            tableLayoutPanel.Location = new Point(1, 61);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(20, 10, 20, 10);
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(418, 260);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblNombreCompleto
            // 
            lblNombreCompleto.Anchor = AnchorStyles.Left;
            lblNombreCompleto.AutoSize = true;
            lblNombreCompleto.Location = new Point(23, 24);
            lblNombreCompleto.Name = "lblNombreCompleto";
            lblNombreCompleto.Size = new Size(121, 17);
            lblNombreCompleto.TabIndex = 0;
            lblNombreCompleto.Text = "Nombre Completo:";
            // 
            // txtNombreCompleto
            // 
            txtNombreCompleto.Dock = DockStyle.Fill;
            txtNombreCompleto.Location = new Point(155, 18);
            txtNombreCompleto.Margin = new Padding(3, 8, 3, 3);
            txtNombreCompleto.Name = "txtNombreCompleto";
            txtNombreCompleto.Size = new Size(240, 24);
            txtNombreCompleto.TabIndex = 1;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Left;
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(23, 69);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(56, 17);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Usuario:";
            // 
            // txtUsername
            // 
            txtUsername.CharacterCasing = CharacterCasing.Lower;
            txtUsername.Dock = DockStyle.Fill;
            txtUsername.Location = new Point(155, 63);
            txtUsername.Margin = new Padding(3, 8, 3, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(240, 24);
            txtUsername.TabIndex = 3;
            // 
            // lblRol
            // 
            lblRol.Anchor = AnchorStyles.Left;
            lblRol.AutoSize = true;
            lblRol.Location = new Point(23, 114);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(30, 17);
            lblRol.TabIndex = 4;
            lblRol.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.Dock = DockStyle.Fill;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(155, 108);
            cmbRol.Margin = new Padding(3, 8, 3, 3);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(240, 25);
            cmbRol.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Left;
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(23, 159);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(77, 17);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña:";
            // 
            // pnlPassword
            // 
            pnlPassword.Controls.Add(txtPassword);
            pnlPassword.Controls.Add(btnShowPass);
            pnlPassword.Dock = DockStyle.Fill;
            pnlPassword.Location = new Point(152, 145);
            pnlPassword.Margin = new Padding(0);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(246, 45);
            pnlPassword.TabIndex = 7;
            // 
            // txtPassword
            // 
            txtPassword.Dock = DockStyle.Fill;
            txtPassword.Location = new Point(0, 0);
            txtPassword.Margin = new Padding(3, 8, 3, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(211, 24);
            txtPassword.TabIndex = 0;
            // 
            // btnShowPass
            // 
            btnShowPass.Dock = DockStyle.Right;
            btnShowPass.FlatAppearance.BorderSize = 0;
            btnShowPass.FlatStyle = FlatStyle.Flat;
            btnShowPass.IconChar = FontAwesome.Sharp.IconChar.Eye;
            btnShowPass.IconColor = Color.Gray;
            btnShowPass.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnShowPass.IconSize = 18;
            btnShowPass.Location = new Point(211, 0);
            btnShowPass.Name = "btnShowPass";
            btnShowPass.Size = new Size(35, 45);
            btnShowPass.TabIndex = 1;
            btnShowPass.UseVisualStyleBackColor = true;
            btnShowPass.Click += btnShowPass_Click;
            // 
            // lblConfirmar
            // 
            lblConfirmar.Anchor = AnchorStyles.Left;
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(23, 204);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(69, 17);
            lblConfirmar.TabIndex = 8;
            lblConfirmar.Text = "Confirmar:";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Dock = DockStyle.Fill;
            txtConfirmar.Location = new Point(155, 198);
            txtConfirmar.Margin = new Padding(3, 8, 3, 3);
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '●';
            txtConfirmar.Size = new Size(240, 24);
            txtConfirmar.TabIndex = 9;
            // 
            // lblPasswordHint
            // 
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblPasswordHint.ForeColor = Color.Gray;
            lblPasswordHint.Location = new Point(155, 235);
            lblPasswordHint.Name = "lblPasswordHint";
            lblPasswordHint.Size = new Size(173, 13);
            lblPasswordHint.TabIndex = 10;
            lblPasswordHint.Text = "(Dejar vacío para mantener actual)";
            lblPasswordHint.Visible = false;
            // 
            // pnlAcciones
            // 
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            pnlAcciones.Location = new Point(1, 324);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Padding = new Padding(0, 0, 20, 10);
            pnlAcciones.Size = new Size(418, 60);
            pnlAcciones.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            btnCancelar.IconColor = Color.FromArgb(231, 76, 60);
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 24;
            btnCancelar.Location = new Point(295, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 40);
            btnCancelar.TabIndex = 0;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.FromArgb(46, 204, 113);
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(189, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 40);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // FrmEdicionUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 385);
            Controls.Add(pnlAcciones);
            Controls.Add(tableLayoutPanel);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmEdicionUsuario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edición de Usuario";
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private FontAwesome.Sharp.IconButton btnShowPass;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Label lblPasswordHint;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}
