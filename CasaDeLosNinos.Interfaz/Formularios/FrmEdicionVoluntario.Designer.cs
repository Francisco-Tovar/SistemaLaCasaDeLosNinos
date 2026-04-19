using System.Windows.Forms;
using System.Drawing;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmEdicionVoluntario
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
            components = new System.ComponentModel.Container();
            panelCabecera = new Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new Label();
            tabla = new TableLayoutPanel();
            lblNombreDesc = new Label();
            txtNombre = new TextBox();
            lblEspecialidadDesc = new Label();
            txtEspecialidad = new TextBox();
            lblCedulaDesc = new Label();
            panelCedula = new FlowLayoutPanel();
            cboTipoCedula = new ComboBox();
            txtCedula = new MaskedTextBox();
            lblTelefonoDesc = new Label();
            txtTelefono = new MaskedTextBox();
            lblCorreoDesc = new Label();
            txtCorreo = new TextBox();
            lblInstitucionDesc = new Label();
            txtInstitucion = new TextBox();
            lblSupervisorDesc = new Label();
            txtSupervisor = new TextBox();
            lblMensaje = new Label();
            pnlAcciones = new FlowLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            errorProvider = new ErrorProvider(components);
            panelCabecera.SuspendLayout();
            tabla.SuspendLayout();
            panelCedula.SuspendLayout();
            pnlAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(438, 75);
            panelCabecera.TabIndex = 0;
            panelCabecera.MouseDown += panelCabecera_MouseDown;
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
            btnClose.Location = new Point(405, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += AlHacerClickEnCancelar;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(14, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(132, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "＋  Nuevo Voluntario";
            // 
            // tabla
            // 
            tabla.AutoSize = true;
            tabla.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tabla.ColumnCount = 2;
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tabla.Controls.Add(lblNombreDesc, 0, 0);
            tabla.Controls.Add(txtNombre, 1, 0);
            tabla.Controls.Add(lblEspecialidadDesc, 0, 1);
            tabla.Controls.Add(txtEspecialidad, 1, 1);
            tabla.Controls.Add(lblCedulaDesc, 0, 2);
            tabla.Controls.Add(panelCedula, 1, 2);
            tabla.Controls.Add(lblTelefonoDesc, 0, 3);
            tabla.Controls.Add(txtTelefono, 1, 3);
            tabla.Controls.Add(lblCorreoDesc, 0, 4);
            tabla.Controls.Add(txtCorreo, 1, 4);
            tabla.Controls.Add(lblInstitucionDesc, 0, 5);
            tabla.Controls.Add(txtInstitucion, 1, 5);
            tabla.Controls.Add(lblSupervisorDesc, 0, 6);
            tabla.Controls.Add(txtSupervisor, 1, 6);
            tabla.Controls.Add(lblMensaje, 0, 7);
            tabla.Controls.Add(pnlAcciones, 0, 8);
            tabla.Dock = DockStyle.Fill;
            tabla.Location = new Point(1, 76);
            tabla.Name = "tabla";
            tabla.Padding = new Padding(20, 15, 20, 10);
            tabla.RowCount = 9;
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tabla.Size = new Size(438, 463);
            tabla.TabIndex = 1;
            // 
            // lblNombreDesc
            // 
            lblNombreDesc.Dock = DockStyle.Fill;
            lblNombreDesc.Location = new Point(23, 15);
            lblNombreDesc.Name = "lblNombreDesc";
            lblNombreDesc.Size = new Size(144, 40);
            lblNombreDesc.TabIndex = 0;
            lblNombreDesc.Text = "Nombre completo:";
            lblNombreDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Dock = DockStyle.Fill;
            txtNombre.Location = new Point(173, 23);
            txtNombre.Margin = new Padding(3, 8, 3, 3);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(242, 17);
            txtNombre.TabIndex = 1;
            // 
            // lblEspecialidadDesc
            // 
            lblEspecialidadDesc.Dock = DockStyle.Fill;
            lblEspecialidadDesc.Location = new Point(23, 55);
            lblEspecialidadDesc.Name = "lblEspecialidadDesc";
            lblEspecialidadDesc.Size = new Size(144, 40);
            lblEspecialidadDesc.TabIndex = 2;
            lblEspecialidadDesc.Text = "Especialidad:";
            lblEspecialidadDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtEspecialidad
            // 
            txtEspecialidad.BorderStyle = BorderStyle.None;
            txtEspecialidad.Dock = DockStyle.Fill;
            txtEspecialidad.Location = new Point(173, 63);
            txtEspecialidad.Margin = new Padding(3, 8, 3, 3);
            txtEspecialidad.Name = "txtEspecialidad";
            txtEspecialidad.Size = new Size(242, 17);
            txtEspecialidad.TabIndex = 3;
            // 
            // lblCedulaDesc
            // 
            lblCedulaDesc.Dock = DockStyle.Fill;
            lblCedulaDesc.Location = new Point(23, 95);
            lblCedulaDesc.Name = "lblCedulaDesc";
            lblCedulaDesc.Size = new Size(144, 40);
            lblCedulaDesc.TabIndex = 4;
            lblCedulaDesc.Text = "Cédula:";
            lblCedulaDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelCedula
            // 
            panelCedula.Controls.Add(cboTipoCedula);
            panelCedula.Controls.Add(txtCedula);
            panelCedula.Dock = DockStyle.Fill;
            panelCedula.Location = new Point(170, 95);
            panelCedula.Margin = new Padding(0);
            panelCedula.Name = "panelCedula";
            panelCedula.Size = new Size(248, 40);
            panelCedula.TabIndex = 5;
            // 
            // cboTipoCedula
            // 
            cboTipoCedula.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipoCedula.FlatStyle = FlatStyle.Flat;
            cboTipoCedula.FormattingEnabled = true;
            cboTipoCedula.Items.AddRange(new object[] { "CR", "DIM" });
            cboTipoCedula.Location = new Point(3, 8);
            cboTipoCedula.Margin = new Padding(3, 8, 3, 3);
            cboTipoCedula.Name = "cboTipoCedula";
            cboTipoCedula.Size = new Size(55, 25);
            cboTipoCedula.TabIndex = 0;
            cboTipoCedula.SelectedIndexChanged += AlCambiarTipoCedula;
            // 
            // txtCedula
            // 
            txtCedula.BorderStyle = BorderStyle.None;
            txtCedula.Location = new Point(64, 11);
            txtCedula.Margin = new Padding(3, 11, 3, 3);
            txtCedula.Name = "txtCedula";
            txtCedula.Size = new Size(160, 17);
            txtCedula.TabIndex = 1;
            txtCedula.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblTelefonoDesc
            // 
            lblTelefonoDesc.Dock = DockStyle.Fill;
            lblTelefonoDesc.Location = new Point(23, 135);
            lblTelefonoDesc.Name = "lblTelefonoDesc";
            lblTelefonoDesc.Size = new Size(144, 40);
            lblTelefonoDesc.TabIndex = 6;
            lblTelefonoDesc.Text = "Teléfono:";
            lblTelefonoDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTelefono
            // 
            txtTelefono.BorderStyle = BorderStyle.None;
            txtTelefono.Dock = DockStyle.Fill;
            txtTelefono.Location = new Point(173, 143);
            txtTelefono.Margin = new Padding(3, 8, 3, 3);
            txtTelefono.Mask = "0000-0000";
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(242, 17);
            txtTelefono.TabIndex = 7;
            txtTelefono.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // lblCorreoDesc
            // 
            lblCorreoDesc.Dock = DockStyle.Fill;
            lblCorreoDesc.Location = new Point(23, 175);
            lblCorreoDesc.Name = "lblCorreoDesc";
            lblCorreoDesc.Size = new Size(144, 40);
            lblCorreoDesc.TabIndex = 8;
            lblCorreoDesc.Text = "Correo:";
            lblCorreoDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtCorreo
            // 
            txtCorreo.BorderStyle = BorderStyle.None;
            txtCorreo.Dock = DockStyle.Fill;
            txtCorreo.Location = new Point(173, 183);
            txtCorreo.Margin = new Padding(3, 8, 3, 3);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(242, 17);
            txtCorreo.TabIndex = 9;
            // 
            // lblInstitucionDesc
            // 
            lblInstitucionDesc.Dock = DockStyle.Fill;
            lblInstitucionDesc.Location = new Point(23, 215);
            lblInstitucionDesc.Name = "lblInstitucionDesc";
            lblInstitucionDesc.Size = new Size(144, 40);
            lblInstitucionDesc.TabIndex = 10;
            lblInstitucionDesc.Text = "Institución:";
            lblInstitucionDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtInstitucion
            // 
            txtInstitucion.BorderStyle = BorderStyle.None;
            txtInstitucion.Dock = DockStyle.Fill;
            txtInstitucion.Location = new Point(173, 223);
            txtInstitucion.Margin = new Padding(3, 8, 3, 3);
            txtInstitucion.Name = "txtInstitucion";
            txtInstitucion.Size = new Size(242, 17);
            txtInstitucion.TabIndex = 11;
            // 
            // lblSupervisorDesc
            // 
            lblSupervisorDesc.Dock = DockStyle.Fill;
            lblSupervisorDesc.Location = new Point(23, 255);
            lblSupervisorDesc.Name = "lblSupervisorDesc";
            lblSupervisorDesc.Size = new Size(144, 40);
            lblSupervisorDesc.TabIndex = 12;
            lblSupervisorDesc.Text = "Supervisor:";
            lblSupervisorDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSupervisor
            // 
            txtSupervisor.BorderStyle = BorderStyle.None;
            txtSupervisor.Dock = DockStyle.Fill;
            txtSupervisor.Location = new Point(173, 263);
            txtSupervisor.Margin = new Padding(3, 8, 3, 3);
            txtSupervisor.Name = "txtSupervisor";
            txtSupervisor.Size = new Size(242, 17);
            txtSupervisor.TabIndex = 13;
            // 
            // lblMensaje
            // 
            tabla.SetColumnSpan(lblMensaje, 2);
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblMensaje.Location = new Point(23, 295);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(392, 30);
            lblMensaje.TabIndex = 14;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlAcciones
            // 
            tabla.SetColumnSpan(pnlAcciones, 2);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Fill;
            pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            pnlAcciones.Location = new Point(20, 325);
            pnlAcciones.Margin = new Padding(0);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(398, 128);
            pnlAcciones.TabIndex = 15;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 24;
            btnCancelar.Location = new Point(288, 3);
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
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(172, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(110, 40);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // FrmEdicionVoluntario
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 540);
            Controls.Add(tabla);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmEdicionVoluntario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edición — La Casa de los Niños";
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            tabla.ResumeLayout(false);
            tabla.PerformLayout();
            panelCedula.ResumeLayout(false);
            panelCedula.PerformLayout();
            pnlAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel tabla;
        private System.Windows.Forms.Label lblNombreDesc;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblEspecialidadDesc;
        private System.Windows.Forms.TextBox txtEspecialidad;
        private System.Windows.Forms.Label lblCedulaDesc;
        private System.Windows.Forms.FlowLayoutPanel panelCedula;
        private System.Windows.Forms.ComboBox cboTipoCedula;
        private System.Windows.Forms.MaskedTextBox txtCedula;
        private System.Windows.Forms.Label lblTelefonoDesc;
        private System.Windows.Forms.MaskedTextBox txtTelefono;
        private System.Windows.Forms.Label lblCorreoDesc;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblInstitucionDesc;
        private System.Windows.Forms.TextBox txtInstitucion;
        private System.Windows.Forms.Label lblSupervisorDesc;
        private System.Windows.Forms.TextBox txtSupervisor;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
