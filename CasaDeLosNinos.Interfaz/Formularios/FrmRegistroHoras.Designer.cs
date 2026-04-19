using System.Windows.Forms;
using System.Drawing;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmRegistroHoras
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
            lblFechaDesc = new Label();
            dtpFecha = new DateTimePicker();
            lblHorasDesc = new Label();
            numHoras = new NumericUpDown();
            lblActividadDesc = new Label();
            txtActividad = new TextBox();
            lblMensaje = new Label();
            pnlAcciones = new FlowLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            panelCabecera.SuspendLayout();
            tabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numHoras).BeginInit();
            pnlAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(398, 60);
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
            btnClose.IconSize = 18;
            btnClose.Location = new Point(368, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += AlHacerClickEnCancelar;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(12, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(130, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "⏰  Registrar Horas";
            // 
            // tabla
            // 
            tabla.AutoSize = true;
            tabla.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tabla.ColumnCount = 2;
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tabla.Controls.Add(lblFechaDesc, 0, 0);
            tabla.Controls.Add(dtpFecha, 1, 0);
            tabla.Controls.Add(lblHorasDesc, 0, 1);
            tabla.Controls.Add(numHoras, 1, 1);
            tabla.Controls.Add(lblActividadDesc, 0, 2);
            tabla.Controls.Add(txtActividad, 1, 2);
            tabla.Controls.Add(lblMensaje, 0, 3);
            tabla.Controls.Add(pnlAcciones, 0, 4);
            tabla.Dock = DockStyle.Fill;
            tabla.Location = new Point(1, 61);
            tabla.Name = "tabla";
            tabla.Padding = new Padding(20, 15, 20, 10);
            tabla.RowCount = 5;
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tabla.Size = new Size(398, 260);
            tabla.TabIndex = 1;
            // 
            // lblFechaDesc
            // 
            lblFechaDesc.Dock = DockStyle.Fill;
            lblFechaDesc.Location = new Point(23, 15);
            lblFechaDesc.Name = "lblFechaDesc";
            lblFechaDesc.Size = new Size(114, 40);
            lblFechaDesc.TabIndex = 0;
            lblFechaDesc.Text = "Fecha:";
            lblFechaDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(143, 23);
            dtpFecha.Margin = new Padding(3, 8, 3, 3);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 24);
            dtpFecha.TabIndex = 1;
            // 
            // lblHorasDesc
            // 
            lblHorasDesc.Dock = DockStyle.Fill;
            lblHorasDesc.Location = new Point(23, 55);
            lblHorasDesc.Name = "lblHorasDesc";
            lblHorasDesc.Size = new Size(114, 40);
            lblHorasDesc.TabIndex = 2;
            lblHorasDesc.Text = "Horas:";
            lblHorasDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numHoras
            // 
            numHoras.DecimalPlaces = 1;
            numHoras.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numHoras.Location = new Point(143, 63);
            numHoras.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numHoras.Name = "numHoras";
            numHoras.Size = new Size(80, 24);
            numHoras.TabIndex = 3;
            numHoras.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblActividadDesc
            // 
            lblActividadDesc.Dock = DockStyle.Fill;
            lblActividadDesc.Location = new Point(23, 95);
            lblActividadDesc.Name = "lblActividadDesc";
            lblActividadDesc.Size = new Size(114, 80);
            lblActividadDesc.TabIndex = 4;
            lblActividadDesc.Text = "Actividad:";
            lblActividadDesc.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtActividad
            // 
            txtActividad.BorderStyle = BorderStyle.None;
            txtActividad.Dock = DockStyle.Fill;
            txtActividad.Location = new Point(143, 103);
            txtActividad.Multiline = true;
            txtActividad.Name = "txtActividad";
            txtActividad.PlaceholderText = "Descripción breve del aporte...";
            txtActividad.Size = new Size(232, 74);
            txtActividad.TabIndex = 5;
            // 
            // lblMensaje
            // 
            tabla.SetColumnSpan(lblMensaje, 2);
            lblMensaje.Dock = DockStyle.Fill;
            lblMensaje.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblMensaje.Location = new Point(23, 175);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(352, 30);
            lblMensaje.TabIndex = 6;
            lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlAcciones
            // 
            tabla.SetColumnSpan(pnlAcciones, 2);
            pnlAcciones.Controls.Add(btnCancelar);
            pnlAcciones.Controls.Add(btnGuardar);
            pnlAcciones.Dock = DockStyle.Fill;
            pnlAcciones.FlowDirection = FlowDirection.RightToLeft;
            pnlAcciones.Location = new Point(20, 205);
            pnlAcciones.Margin = new Padding(0);
            pnlAcciones.Name = "pnlAcciones";
            pnlAcciones.Size = new Size(358, 45);
            pnlAcciones.TabIndex = 7;
            // 
            // btnCancelar
            // 
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 20;
            btnCancelar.Location = new Point(258, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(97, 35);
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
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 20;
            btnGuardar.Location = new Point(152, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 35);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Aceptar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // FrmRegistroHoras
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 320);
            Controls.Add(tabla);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmRegistroHoras";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registro de Horas — La Casa de los Niños";
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            tabla.ResumeLayout(false);
            tabla.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numHoras).EndInit();
            pnlAcciones.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TableLayoutPanel tabla;
        private System.Windows.Forms.Label lblFechaDesc;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblHorasDesc;
        private System.Windows.Forms.NumericUpDown numHoras;
        private System.Windows.Forms.Label lblActividadDesc;
        private System.Windows.Forms.TextBox txtActividad;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
