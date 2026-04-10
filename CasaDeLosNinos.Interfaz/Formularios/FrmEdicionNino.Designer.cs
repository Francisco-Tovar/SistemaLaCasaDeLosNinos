namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmEdicionNino
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
            tabla = new TableLayoutPanel();
            lblNombreDesc = new Label();
            txtNombre = new TextBox();
            lblFechaDesc = new Label();
            panelFecha = new FlowLayoutPanel();
            dtpNacimiento = new DateTimePicker();
            chkTieneFechaNacimiento = new CheckBox();
            lblGeneroDesc = new Label();
            cboGenero = new ComboBox();
            lblEncargadoDesc = new Label();
            txtEncargado = new TextBox();
            lblTelefonoDesc = new Label();
            txtTelefono = new TextBox();
            lblDireccionDesc = new Label();
            txtDireccion = new TextBox();
            lblMensaje = new Label();
            pnlAcciones = new FlowLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            panelCabecera.SuspendLayout();
            tabla.SuspendLayout();
            panelFecha.SuspendLayout();
            pnlAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.BackColor = Color.FromArgb(26, 25, 62);
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(448, 75);
            panelCabecera.TabIndex = 0;
            panelCabecera.MouseDown += panelCabecera_MouseDown;
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
            btnClose.Location = new Point(418, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += AlHacerClickEnCancelar;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Comic Sans MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Gainsboro;
            lblTitulo.Location = new Point(14, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(185, 23);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "＋  Nuevo Beneficiario";
            // 
            // tabla
            // 
            tabla.ColumnCount = 2;
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tabla.Controls.Add(lblNombreDesc, 0, 0);
            tabla.Controls.Add(txtNombre, 1, 0);
            tabla.Controls.Add(lblFechaDesc, 0, 1);
            tabla.Controls.Add(panelFecha, 1, 1);
            tabla.Controls.Add(lblGeneroDesc, 0, 2);
            tabla.Controls.Add(cboGenero, 1, 2);
            tabla.Controls.Add(lblEncargadoDesc, 0, 3);
            tabla.Controls.Add(txtEncargado, 1, 3);
            tabla.Controls.Add(lblTelefonoDesc, 0, 4);
            tabla.Controls.Add(txtTelefono, 1, 4);
            tabla.Controls.Add(lblDireccionDesc, 0, 5);
            tabla.Controls.Add(txtDireccion, 1, 5);
            tabla.Controls.Add(lblMensaje, 0, 6);
            tabla.Controls.Add(pnlAcciones, 0, 7);
            tabla.Dock = DockStyle.Fill;
            tabla.Location = new Point(1, 61);
            tabla.Name = "tabla";
            tabla.Padding = new Padding(20, 15, 20, 10);
            tabla.RowCount = 8;
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tabla.Size = new Size(448, 358);
            tabla.TabIndex = 1;
            // 
            // lblNombreDesc
            // 
            lblNombreDesc.Dock = DockStyle.Fill;
            lblNombreDesc.ForeColor = Color.Silver;
            lblNombreDesc.Location = new Point(23, 15);
            lblNombreDesc.Name = "lblNombreDesc";
            lblNombreDesc.Size = new Size(157, 40);
            lblNombreDesc.TabIndex = 0;
            lblNombreDesc.Text = "Nombre completo:";
            lblNombreDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(45, 45, 81);
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Dock = DockStyle.Fill;
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.ForeColor = Color.Gainsboro;
            txtNombre.Location = new Point(186, 23);
            txtNombre.Margin = new Padding(3, 8, 3, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(239, 20);
            txtNombre.TabIndex = 1;
            // 
            // lblFechaDesc
            // 
            lblFechaDesc.Dock = DockStyle.Fill;
            lblFechaDesc.ForeColor = Color.Silver;
            lblFechaDesc.Location = new Point(23, 55);
            lblFechaDesc.Name = "lblFechaDesc";
            lblFechaDesc.Size = new Size(157, 40);
            lblFechaDesc.TabIndex = 2;
            lblFechaDesc.Text = "Nacimiento:";
            lblFechaDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelFecha
            // 
            panelFecha.Controls.Add(dtpNacimiento);
            panelFecha.Controls.Add(chkTieneFechaNacimiento);
            panelFecha.Dock = DockStyle.Fill;
            panelFecha.Location = new Point(183, 55);
            panelFecha.Margin = new Padding(0);
            panelFecha.Name = "panelFecha";
            panelFecha.Size = new Size(245, 40);
            panelFecha.TabIndex = 3;
            // 
            // dtpNacimiento
            // 
            dtpNacimiento.Enabled = false;
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.Location = new Point(3, 8);
            dtpNacimiento.Margin = new Padding(3, 8, 3, 3);
            dtpNacimiento.Name = "dtpNacimiento";
            dtpNacimiento.Size = new Size(100, 24);
            dtpNacimiento.TabIndex = 0;
            // 
            // chkTieneFechaNacimiento
            // 
            chkTieneFechaNacimiento.AutoSize = true;
            chkTieneFechaNacimiento.ForeColor = Color.Gainsboro;
            chkTieneFechaNacimiento.Location = new Point(109, 11);
            chkTieneFechaNacimiento.Margin = new Padding(3, 11, 3, 3);
            chkTieneFechaNacimiento.Name = "chkTieneFechaNacimiento";
            chkTieneFechaNacimiento.Size = new Size(82, 21);
            chkTieneFechaNacimiento.TabIndex = 1;
            chkTieneFechaNacimiento.Text = "Conocida";
            chkTieneFechaNacimiento.UseVisualStyleBackColor = true;
            chkTieneFechaNacimiento.CheckedChanged += AlCambiarCheckFecha;
            // 
            // lblGeneroDesc
            // 
            lblGeneroDesc.Dock = DockStyle.Fill;
            lblGeneroDesc.ForeColor = Color.Silver;
            lblGeneroDesc.Location = new Point(23, 95);
            lblGeneroDesc.Name = "lblGeneroDesc";
            lblGeneroDesc.Size = new Size(157, 40);
            lblGeneroDesc.TabIndex = 4;
            lblGeneroDesc.Text = "Género:";
            lblGeneroDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboGenero
            // 
            cboGenero.BackColor = Color.FromArgb(45, 45, 81);
            cboGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenero.FlatStyle = FlatStyle.Flat;
            cboGenero.ForeColor = Color.Gainsboro;
            cboGenero.FormattingEnabled = true;
            cboGenero.Items.AddRange(new object[] { "Masculino", "Femenino", "No especificado" });
            cboGenero.Location = new Point(186, 103);
            cboGenero.Margin = new Padding(3, 8, 3, 3);
            cboGenero.Name = "cboGenero";
            cboGenero.Size = new Size(239, 25);
            cboGenero.TabIndex = 5;
            // 
            // lblEncargadoDesc
            // 
            lblEncargadoDesc.Dock = DockStyle.Fill;
            lblEncargadoDesc.ForeColor = Color.Silver;
            lblEncargadoDesc.Location = new Point(23, 135);
            lblEncargadoDesc.Name = "lblEncargadoDesc";
            lblEncargadoDesc.Size = new Size(157, 40);
            lblEncargadoDesc.TabIndex = 6;
            lblEncargadoDesc.Text = "Encargado:";
            lblEncargadoDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEncargado
            // 
            txtEncargado.BackColor = Color.FromArgb(45, 45, 81);
            txtEncargado.BorderStyle = BorderStyle.None;
            txtEncargado.Dock = DockStyle.Fill;
            txtEncargado.Font = new Font("Segoe UI", 11F);
            txtEncargado.ForeColor = Color.Gainsboro;
            txtEncargado.Location = new Point(186, 143);
            txtEncargado.Margin = new Padding(3, 8, 3, 3);
            txtEncargado.Name = "txtEncargado";
            txtEncargado.Size = new Size(239, 20);
            txtEncargado.TabIndex = 7;
            // 
            // lblTelefonoDesc
            // 
            lblTelefonoDesc.Dock = DockStyle.Fill;
            lblTelefonoDesc.ForeColor = Color.Silver;
            lblTelefonoDesc.Location = new Point(23, 175);
            lblTelefonoDesc.Name = "lblTelefonoDesc";
            lblTelefonoDesc.Size = new Size(157, 40);
            lblTelefonoDesc.TabIndex = 8;
            lblTelefonoDesc.Text = "Teléfono:";
            lblTelefonoDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(45, 45, 81);
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Dock = DockStyle.Fill;
            txtTelefono.Font = new Font("Segoe UI", 11F);
            txtTelefono.ForeColor = Color.Gainsboro;
            txtTelefono.Location = new Point(186, 183);
            txtTelefono.Margin = new Padding(3, 8, 3, 3);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(239, 20);
            txtTelefono.TabIndex = 9;
            // 
            // lblDireccionDesc
            // 
            lblDireccionDesc.Dock = DockStyle.Fill;
            lblDireccionDesc.ForeColor = Color.Silver;
            lblDireccionDesc.Location = new Point(23, 215);
            lblDireccionDesc.Name = "lblDireccionDesc";
            lblDireccionDesc.Size = new Size(157, 60);
            lblDireccionDesc.TabIndex = 10;
            lblDireccionDesc.Text = "Dirección:";
            lblDireccionDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.FromArgb(45, 45, 81);
            txtDireccion.BorderStyle = BorderStyle.None;
            txtDireccion.Dock = DockStyle.Fill;
            txtDireccion.Font = new Font("Segoe UI", 11F);
            txtDireccion.ForeColor = Color.Gainsboro;
            txtDireccion.Location = new Point(186, 223);
            txtDireccion.Margin = new Padding(3, 8, 3, 3);
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(239, 49);
            txtDireccion.TabIndex = 11;
            // 
            // lblMensaje
            // 
            tabla.SetColumnSpan(lblMensaje, 2);
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblMensaje.ForeColor = Color.FromArgb(231, 76, 60);
            lblMensaje.Location = new Point(23, 275);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(402, 30);
            lblMensaje.TabIndex = 12;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlAcciones
            // 
            tabla.SetColumnSpan(pnlAcciones, 2);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Fill;
            pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            pnlAcciones.Location = new Point(20, 305);
            pnlAcciones.Margin = new Padding(0);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(408, 43);
            pnlAcciones.TabIndex = 14;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.Gainsboro;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.FromArgb(231, 76, 60);
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 24;
            btnCancelar.Location = new Point(298, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(107, 40);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += AlHacerClickEnCancelar;
            // 
            // btnGuardar
            // 
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.Gainsboro;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.FromArgb(46, 204, 113);
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(182, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(110, 40);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // FrmEdicionNino
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 33, 74);
            ClientSize = new Size(450, 440);
            Controls.Add(tabla);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmEdicionNino";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edición — La Casa de los Niños";
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            tabla.ResumeLayout(false);
            tabla.PerformLayout();
            panelFecha.ResumeLayout(false);
            panelFecha.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel tabla;
        private System.Windows.Forms.Label lblNombreDesc;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblFechaDesc;
        private System.Windows.Forms.FlowLayoutPanel panelFecha;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        private System.Windows.Forms.CheckBox chkTieneFechaNacimiento;
        private System.Windows.Forms.Label lblGeneroDesc;
        private System.Windows.Forms.ComboBox cboGenero;
        private System.Windows.Forms.Label lblEncargadoDesc;
        private System.Windows.Forms.TextBox txtEncargado;
        private System.Windows.Forms.Label lblTelefonoDesc;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblDireccionDesc;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
