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
            this.lblSeparador = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelCabecera.SuspendLayout();
            this.tabla.SuspendLayout();
            this.panelFecha.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCabecera
            // 
            this.panelCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.panelCabecera.Controls.Add(this.lblTitulo);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Location = new System.Drawing.Point(0, 0);
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(424, 48);
            this.panelCabecera.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(14, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(188, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "＋  Nuevo Beneficiario";
            // 
            // tabla
            // 
            this.tabla.ColumnCount = 2;
            this.tabla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tabla.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
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
            this.tabla.Controls.Add(this.lblSeparador, 0, 7);
            this.tabla.Controls.Add(this.panelBotones, 0, 8);
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(0, 48);
            this.tabla.Name = "tabla";
            this.tabla.Padding = new System.Windows.Forms.Padding(16, 10, 16, 6);
            this.tabla.RowCount = 9;
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tabla.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabla.Size = new System.Drawing.Size(424, 373);
            this.tabla.TabIndex = 1;
            // 
            // lblNombreDesc
            // 
            this.lblNombreDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombreDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNombreDesc.Location = new System.Drawing.Point(19, 10);
            this.lblNombreDesc.Name = "lblNombreDesc";
            this.lblNombreDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblNombreDesc.Size = new System.Drawing.Size(142, 35);
            this.lblNombreDesc.TabIndex = 0;
            this.lblNombreDesc.Text = "Nombre completo *:";
            this.lblNombreDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombre
            // 
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Location = new System.Drawing.Point(167, 13);
            this.txtNombre.MaxLength = 150;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(238, 24);
            this.txtNombre.TabIndex = 1;
            // 
            // lblFechaDesc
            // 
            this.lblFechaDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFechaDesc.Location = new System.Drawing.Point(19, 45);
            this.lblFechaDesc.Name = "lblFechaDesc";
            this.lblFechaDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblFechaDesc.Size = new System.Drawing.Size(142, 35);
            this.lblFechaDesc.TabIndex = 2;
            this.lblFechaDesc.Text = "Fecha de nacimiento:";
            this.lblFechaDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelFecha
            // 
            this.panelFecha.Controls.Add(this.dtpNacimiento);
            this.panelFecha.Controls.Add(this.chkTieneFechaNacimiento);
            this.panelFecha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFecha.Location = new System.Drawing.Point(164, 45);
            this.panelFecha.Margin = new System.Windows.Forms.Padding(0);
            this.panelFecha.Name = "panelFecha";
            this.panelFecha.Size = new System.Drawing.Size(244, 35);
            this.panelFecha.TabIndex = 3;
            // 
            // dtpNacimiento
            // 
            this.dtpNacimiento.Enabled = false;
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNacimiento.Location = new System.Drawing.Point(3, 3);
            this.dtpNacimiento.Name = "dtpNacimiento";
            this.dtpNacimiento.Size = new System.Drawing.Size(120, 24);
            this.dtpNacimiento.TabIndex = 0;
            // 
            // chkTieneFechaNacimiento
            // 
            this.chkTieneFechaNacimiento.AutoSize = true;
            this.chkTieneFechaNacimiento.Location = new System.Drawing.Point(132, 6);
            this.chkTieneFechaNacimiento.Margin = new System.Windows.Forms.Padding(6, 6, 0, 0);
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
            this.lblGeneroDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGeneroDesc.Location = new System.Drawing.Point(19, 80);
            this.lblGeneroDesc.Name = "lblGeneroDesc";
            this.lblGeneroDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblGeneroDesc.Size = new System.Drawing.Size(142, 35);
            this.lblGeneroDesc.TabIndex = 4;
            this.lblGeneroDesc.Text = "Género:";
            this.lblGeneroDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboGenero
            // 
            this.cboGenero.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGenero.FormattingEnabled = true;
            this.cboGenero.Items.AddRange(new object[] {
            "Masculino",
            "Femenino",
            "No especificado"});
            this.cboGenero.Location = new System.Drawing.Point(167, 83);
            this.cboGenero.Name = "cboGenero";
            this.cboGenero.Size = new System.Drawing.Size(238, 25);
            this.cboGenero.TabIndex = 5;
            // 
            // lblEncargadoDesc
            // 
            this.lblEncargadoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEncargadoDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEncargadoDesc.Location = new System.Drawing.Point(19, 115);
            this.lblEncargadoDesc.Name = "lblEncargadoDesc";
            this.lblEncargadoDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblEncargadoDesc.Size = new System.Drawing.Size(142, 35);
            this.lblEncargadoDesc.TabIndex = 6;
            this.lblEncargadoDesc.Text = "Nombre encargado:";
            this.lblEncargadoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEncargado
            // 
            this.txtEncargado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEncargado.Location = new System.Drawing.Point(167, 118);
            this.txtEncargado.MaxLength = 150;
            this.txtEncargado.Name = "txtEncargado";
            this.txtEncargado.Size = new System.Drawing.Size(238, 24);
            this.txtEncargado.TabIndex = 7;
            // 
            // lblTelefonoDesc
            // 
            this.lblTelefonoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTelefonoDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTelefonoDesc.Location = new System.Drawing.Point(19, 150);
            this.lblTelefonoDesc.Name = "lblTelefonoDesc";
            this.lblTelefonoDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblTelefonoDesc.Size = new System.Drawing.Size(142, 35);
            this.lblTelefonoDesc.TabIndex = 8;
            this.lblTelefonoDesc.Text = "Teléfono encargado:";
            this.lblTelefonoDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTelefono.Location = new System.Drawing.Point(167, 153);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(238, 24);
            this.txtTelefono.TabIndex = 9;
            // 
            // lblDireccionDesc
            // 
            this.lblDireccionDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDireccionDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDireccionDesc.Location = new System.Drawing.Point(19, 185);
            this.lblDireccionDesc.Name = "lblDireccionDesc";
            this.lblDireccionDesc.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblDireccionDesc.Size = new System.Drawing.Size(142, 35);
            this.lblDireccionDesc.TabIndex = 10;
            this.lblDireccionDesc.Text = "Dirección:";
            this.lblDireccionDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDireccion.Location = new System.Drawing.Point(167, 188);
            this.txtDireccion.MaxLength = 250;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(238, 24);
            this.txtDireccion.TabIndex = 11;
            // 
            // lblMensaje
            // 
            this.tabla.SetColumnSpan(this.lblMensaje, 2);
            this.lblMensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblMensaje.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMensaje.Location = new System.Drawing.Point(19, 220);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblMensaje.Size = new System.Drawing.Size(386, 30);
            this.lblMensaje.TabIndex = 12;
            // 
            // lblSeparador
            // 
            this.lblSeparador.Location = new System.Drawing.Point(19, 250);
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(142, 6);
            this.lblSeparador.TabIndex = 13;
            // 
            // panelBotones
            // 
            this.tabla.SetColumnSpan(this.panelBotones, 2);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelBotones.Location = new System.Drawing.Point(16, 260);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelBotones.Size = new System.Drawing.Size(392, 107);
            this.panelBotones.TabIndex = 14;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(302, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 32);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.AlHacerClickEnCancelar);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(184, 7);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 32);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "💾  Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.AlHacerClickEnGuardar);
            // 
            // FrmEdicionNino
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(424, 421);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.panelCabecera);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEdicionNino";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo Niño — La Casa de los Niños";
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
        private System.Windows.Forms.Label lblSeparador;
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
