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
            this.panelCabecera = new System.Windows.Forms.Panel();
            this.btnClose = new FontAwesome.Sharp.IconButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombreDesc = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblFechaDesc = new System.Windows.Forms.Label();
            this.panelFecha = new System.Windows.Forms.FlowLayoutPanel();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            this.chkTieneFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.lblGeneroDesc = new System.Windows.Forms.Label();
            this.cboGenero = new System.Windows.Forms.ComboBox();
            this.lblEncargadoDesc = new System.Windows.Forms.Label();
            this.txtEncargado = new System.Windows.Forms.TextBox();
            this.lblTelefonoDesc = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblDireccionDesc = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.panelCabecera.SuspendLayout();
            this.tabla.SuspendLayout();
            this.panelFecha.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCabecera
            // 
            this.panelCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelCabecera.Controls.Add(this.btnClose);
            this.panelCabecera.Controls.Add(this.lblTitulo);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Location = new System.Drawing.Point(0, 0);
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(450, 48);
            this.panelCabecera.TabIndex = 0;
            this.panelCabecera.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelCabecera_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this.btnClose.IconColor = System.Drawing.Color.Gainsboro;
            this.btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnClose.IconSize = 20;
            this.btnClose.Location = new System.Drawing.Point(420, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.AlHacerClickEnCancelar);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitulo.Location = new System.Drawing.Point(14, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(188, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "＋  Nuevo Beneficiario";
            // 
            // tabla
            // 
            this.tabla.ColumnCount = 2;
            this.tabla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tabla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tabla.Controls.Add(this.lblNombreDesc, 0, 0);
            this.tabla.Controls.Add(this.txtNombre, 1, 0);
            this.tabla.Controls.Add(this.lblFechaDesc, 0, 1);
            this.tabla.Controls.Add(this.panelFecha, 1, 1);
            this.tabla.Controls.Add(this.lblGeneroDesc, 0, 2);
            this.tabla.Controls.Add(this.cboGenero, 1, 2);
            this.tabla.Controls.Add(this.lblEncargadoDesc, 0, 3);
            this.tabla.Controls.Add(this.txtEncargado, 1, 3);
            this.tabla.Controls.Add(this.lblTelefonoDesc, 0, 4);
            this.tabla.Controls.Add(this.txtTelefono, 1, 4);
            this.tabla.Controls.Add(this.lblDireccionDesc, 0, 5);
            this.tabla.Controls.Add(this.txtDireccion, 1, 5);
            this.tabla.Controls.Add(this.lblMensaje, 0, 6);
            this.tabla.Controls.Add(this.panelBotones, 0, 7);
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(0, 48);
            this.tabla.Name = "tabla";
            this.tabla.Padding = new System.Windows.Forms.Padding(20, 15, 20, 10);
            this.tabla.RowCount = 8;
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabla.Size = new System.Drawing.Size(450, 402);
            this.tabla.TabIndex = 1;
            // 
            // lblNombreDesc
            // 
            this.lblNombreDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombreDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblNombreDesc.Location = new System.Drawing.Point(23, 15);
            this.lblNombreDesc.Name = "lblNombreDesc";
            this.lblNombreDesc.Size = new System.Drawing.Size(158, 40);
            this.lblNombreDesc.TabIndex = 0;
            this.lblNombreDesc.Text = "Nombre completo:";
            this.lblNombreDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNombre.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtNombre.Location = new System.Drawing.Point(187, 23);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(240, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // lblFechaDesc
            // 
            this.lblFechaDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblFechaDesc.Location = new System.Drawing.Point(23, 55);
            this.lblFechaDesc.Name = "lblFechaDesc";
            this.lblFechaDesc.Size = new System.Drawing.Size(158, 40);
            this.lblFechaDesc.TabIndex = 2;
            this.lblFechaDesc.Text = "Nacimiento:";
            this.lblFechaDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFecha
            // 
            this.panelFecha.Controls.Add(this.dtpNacimiento);
            this.panelFecha.Controls.Add(this.chkTieneFechaNacimiento);
            this.panelFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFecha.Location = new System.Drawing.Point(184, 55);
            this.panelFecha.Margin = new System.Windows.Forms.Padding(0);
            this.panelFecha.Name = "panelFecha";
            this.panelFecha.Size = new System.Drawing.Size(246, 40);
            this.panelFecha.TabIndex = 3;
            // 
            // dtpNacimiento
            // 
            this.dtpNacimiento.Enabled = false;
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNacimiento.Location = new System.Drawing.Point(3, 8);
            this.dtpNacimiento.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.Size = new System.Drawing.Size(100, 24);
            this.dtpNacimiento.TabIndex = 0;
            // 
            // chkTieneFechaNacimiento
            // 
            this.chkTieneFechaNacimiento.AutoSize = true;
            this.chkTieneFechaNacimiento.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkTieneFechaNacimiento.Location = new System.Drawing.Point(109, 11);
            this.chkTieneFechaNacimiento.Margin = new System.Windows.Forms.Padding(3, 11, 3, 3);
            this.chkTieneFechaNacimiento.Name = "chkTieneFechaNacimiento";
            this.chkTieneFechaNacimiento.Size = new System.Drawing.Size(81, 21);
            this.chkTieneFechaNacimiento.TabIndex = 1;
            this.chkTieneFechaNacimiento.Text = "Conocida";
            this.chkTieneFechaNacimiento.UseVisualStyleBackColor = true;
            this.chkTieneFechaNacimiento.CheckedChanged += new System.EventHandler(this.AlCambiarCheckFecha);
            // 
            // lblGeneroDesc
            // 
            this.lblGeneroDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGeneroDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblGeneroDesc.Location = new System.Drawing.Point(23, 95);
            this.lblGeneroDesc.Name = "lblGeneroDesc";
            this.lblGeneroDesc.Size = new System.Drawing.Size(158, 40);
            this.lblGeneroDesc.TabIndex = 4;
            this.lblGeneroDesc.Text = "Género:";
            this.lblGeneroDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGenero
            // 
            this.cboGenero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.cboGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGenero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGenero.ForeColor = System.Drawing.Color.Gainsboro;
            this.cboGenero.FormattingEnabled = true;
            this.cboGenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "No especificado"});
            this.cboGenero.Location = new System.Drawing.Point(187, 103);
            this.cboGenero.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.cboGenero.Name = "cboGenero";
            this.cboGenero.Size = new System.Drawing.Size(240, 25);
            this.cboGenero.TabIndex = 5;
            // 
            // lblEncargadoDesc
            // 
            this.lblEncargadoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEncargadoDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblEncargadoDesc.Location = new System.Drawing.Point(23, 135);
            this.lblEncargadoDesc.Name = "lblEncargadoDesc";
            this.lblEncargadoDesc.Size = new System.Drawing.Size(158, 40);
            this.lblEncargadoDesc.TabIndex = 6;
            this.lblEncargadoDesc.Text = "Encargado:";
            this.lblEncargadoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEncargado
            // 
            this.txtEncargado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.txtEncargado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEncargado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEncargado.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEncargado.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtEncargado.Location = new System.Drawing.Point(187, 143);
            this.txtEncargado.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.txtEncargado.Name = "txtEncargado";
            this.txtEncargado.Size = new System.Drawing.Size(240, 20);
            this.txtEncargado.TabIndex = 7;
            // 
            // lblTelefonoDesc
            // 
            this.lblTelefonoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelefonoDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblTelefonoDesc.Location = new System.Drawing.Point(23, 175);
            this.lblTelefonoDesc.Name = "lblTelefonoDesc";
            this.lblTelefonoDesc.Size = new System.Drawing.Size(158, 40);
            this.lblTelefonoDesc.TabIndex = 8;
            this.lblTelefonoDesc.Text = "Teléfono:";
            this.lblTelefonoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTelefono.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtTelefono.Location = new System.Drawing.Point(187, 183);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(240, 20);
            this.txtTelefono.TabIndex = 9;
            // 
            // lblDireccionDesc
            // 
            this.lblDireccionDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDireccionDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblDireccionDesc.Location = new System.Drawing.Point(23, 215);
            this.lblDireccionDesc.Name = "lblDireccionDesc";
            this.lblDireccionDesc.Size = new System.Drawing.Size(158, 60);
            this.lblDireccionDesc.TabIndex = 10;
            this.lblDireccionDesc.Text = "Dirección:";
            this.lblDireccionDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDireccion.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtDireccion.Location = new System.Drawing.Point(187, 223);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(240, 49);
            this.txtDireccion.TabIndex = 11;
            // 
            // lblMensaje
            // 
            this.tabla.SetColumnSpan(this.lblMensaje, 2);
            this.lblMensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblMensaje.Location = new System.Drawing.Point(23, 275);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(404, 30);
            this.lblMensaje.TabIndex = 12;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBotones
            // 
            this.tabla.SetColumnSpan(this.panelBotones, 2);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelBotones.Location = new System.Drawing.Point(20, 305);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(410, 87);
            this.panelBotones.TabIndex = 14;
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            this.btnCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 24;
            this.btnCancelar.Location = new System.Drawing.Point(300, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.AlHacerClickEnCancelar);
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 24;
            this.btnGuardar.Location = new System.Drawing.Point(184, 3);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.AlHacerClickEnGuardar);
            // 
            // FrmEdicionNino
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.panelCabecera);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEdicionNino";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edición — La Casa de los Niños";
            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.tabla.ResumeLayout(false);
            this.tabla.PerformLayout();
            this.panelFecha.ResumeLayout(false);
            this.panelFecha.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
