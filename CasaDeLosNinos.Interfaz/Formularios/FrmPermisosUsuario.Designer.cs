namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmPermisosUsuario
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
            lblRolUsuario = new Label();
            lblDivider = new Label();
            chkNinos = new CheckBox();
            chkAsistencia = new CheckBox();
            chkVoluntarios = new CheckBox();
            chkCajaChica = new CheckBox();
            chkReportes = new CheckBox();
            chkBitacoraEventos = new CheckBox();
            lblInfoExclusivos = new Label();
            lblSubtitulo = new Label();
            pnlAcciones = new FlowLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            panelCabecera.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
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
            lblTitulo.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(198, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "🔐  Permisos de Acceso";
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
            btnClose.Location = new Point(383, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnCancelar_Click;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(lblRolUsuario, 0, 1);
            tableLayoutPanel.Controls.Add(lblDivider, 0, 2);
            tableLayoutPanel.Controls.Add(chkNinos, 0, 3);
            tableLayoutPanel.Controls.Add(chkAsistencia, 0, 4);
            tableLayoutPanel.Controls.Add(chkVoluntarios, 0, 5);
            tableLayoutPanel.Controls.Add(chkCajaChica, 0, 6);
            tableLayoutPanel.Controls.Add(chkReportes, 0, 7);
            tableLayoutPanel.Controls.Add(chkBitacoraEventos, 0, 8);
            tableLayoutPanel.Controls.Add(lblInfoExclusivos, 0, 9);
            tableLayoutPanel.Controls.Add(lblSubtitulo, 0, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(1, 61);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Padding = new Padding(20, 8, 20, 4);
            tableLayoutPanel.RowCount = 10;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 71F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(418, 350);
            tableLayoutPanel.TabIndex = 0;
            // 
            // lblRolUsuario
            // 
            lblRolUsuario.Anchor = AnchorStyles.Left;
            lblRolUsuario.AutoSize = true;
            lblRolUsuario.Font = new Font("Microsoft Sans Serif", 8.5F, FontStyle.Italic);
            lblRolUsuario.Location = new Point(23, 87);
            lblRolUsuario.Name = "lblRolUsuario";
            lblRolUsuario.Size = new Size(202, 15);
            lblRolUsuario.TabIndex = 1;
            lblRolUsuario.Text = "Rol: Funcionario  |  Usuario: usuario";
            // 
            // lblDivider
            // 
            lblDivider.BorderStyle = BorderStyle.Fixed3D;
            lblDivider.Dock = DockStyle.Bottom;
            lblDivider.Location = new Point(23, 117);
            lblDivider.Name = "lblDivider";
            lblDivider.Size = new Size(372, 2);
            lblDivider.TabIndex = 2;
            // 
            // chkNinos
            // 
            chkNinos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkNinos.Font = new Font("Microsoft Sans Serif", 10F);
            chkNinos.Location = new Point(23, 123);
            chkNinos.Name = "chkNinos";
            chkNinos.Size = new Size(372, 28);
            chkNinos.TabIndex = 3;
            chkNinos.Text = "  Niños";
            chkNinos.UseVisualStyleBackColor = true;
            // 
            // chkAsistencia
            // 
            chkAsistencia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkAsistencia.Font = new Font("Microsoft Sans Serif", 10F);
            chkAsistencia.Location = new Point(23, 159);
            chkAsistencia.Name = "chkAsistencia";
            chkAsistencia.Size = new Size(372, 27);
            chkAsistencia.TabIndex = 4;
            chkAsistencia.Text = "  Asistencia";
            chkAsistencia.UseVisualStyleBackColor = true;
            // 
            // chkVoluntarios
            // 
            chkVoluntarios.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkVoluntarios.Font = new Font("Microsoft Sans Serif", 10F);
            chkVoluntarios.Location = new Point(23, 192);
            chkVoluntarios.Name = "chkVoluntarios";
            chkVoluntarios.Size = new Size(372, 25);
            chkVoluntarios.TabIndex = 5;
            chkVoluntarios.Text = "  Voluntarios";
            chkVoluntarios.UseVisualStyleBackColor = true;
            // 
            // chkCajaChica
            // 
            chkCajaChica.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkCajaChica.Font = new Font("Microsoft Sans Serif", 10F);
            chkCajaChica.Location = new Point(23, 223);
            chkCajaChica.Name = "chkCajaChica";
            chkCajaChica.Size = new Size(372, 26);
            chkCajaChica.TabIndex = 6;
            chkCajaChica.Text = "  Caja Chica";
            chkCajaChica.UseVisualStyleBackColor = true;
            // 
            // chkReportes
            // 
            chkReportes.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkReportes.Font = new Font("Microsoft Sans Serif", 10F);
            chkReportes.Location = new Point(23, 255);
            chkReportes.Name = "chkReportes";
            chkReportes.Size = new Size(372, 23);
            chkReportes.TabIndex = 7;
            chkReportes.Text = "  Reportes";
            chkReportes.UseVisualStyleBackColor = true;
            // 
            // chkBitacoraEventos
            // 
            chkBitacoraEventos.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            chkBitacoraEventos.Font = new Font("Microsoft Sans Serif", 10F);
            chkBitacoraEventos.Location = new Point(23, 280);
            chkBitacoraEventos.Name = "chkBitacoraEventos";
            chkBitacoraEventos.Size = new Size(372, 28);
            chkBitacoraEventos.TabIndex = 12;
            chkBitacoraEventos.Text = "  Bitácora de Eventos";
            chkBitacoraEventos.UseVisualStyleBackColor = true;
            // 
            // lblInfoExclusivos
            // 
            lblInfoExclusivos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblInfoExclusivos.AutoSize = true;
            lblInfoExclusivos.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Italic);
            lblInfoExclusivos.Location = new Point(23, 300);
            lblInfoExclusivos.Name = "lblInfoExclusivos";
            lblInfoExclusivos.Size = new Size(372, 13);
            lblInfoExclusivos.TabIndex = 8;
            lblInfoExclusivos.Text = "⚠  Gestión de Usuarios y Mantenimiento son exclusivos del rol Administrador.";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            lblSubtitulo.Location = new Point(23, 8);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(125, 17);
            lblSubtitulo.TabIndex = 0;
            lblSubtitulo.Text = "Nombre Usuario";
            // 
            // pnlAcciones
            // 
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Bottom;
            pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            pnlAcciones.Location = new Point(1, 378);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Padding = new Padding(0, 0, 20, 10);
            pnlAcciones.Size = new Size(418, 60);
            pnlAcciones.TabIndex = 1;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
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
            btnGuardar.IconColor = Color.Black;
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
            // FrmPermisosUsuario
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 439);
            Controls.Add(tableLayoutPanel);
            Controls.Add(pnlAcciones);
            Controls.Add(panelCabecera);
            Font = new Font("Microsoft Sans Serif", 9.5F);
            Name = "FrmPermisosUsuario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Permisos de Acceso";
            Load += FrmPermisosUsuario_Load;
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblRolUsuario;
        private System.Windows.Forms.Label lblDivider;
        private System.Windows.Forms.CheckBox chkNinos;
        private System.Windows.Forms.CheckBox chkAsistencia;
        private System.Windows.Forms.CheckBox chkVoluntarios;
        private System.Windows.Forms.CheckBox chkCajaChica;
        private System.Windows.Forms.CheckBox chkReportes;
        private System.Windows.Forms.CheckBox chkBitacoraEventos;
        private System.Windows.Forms.Label lblInfoExclusivos;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}
