namespace CasaDeLosNinos.Interfaz.Formularios;

partial class FrmEdicionCajaChica
{
    private System.ComponentModel.IContainer components = null;
    
    // Header
    private System.Windows.Forms.Panel pnlCabecera;
    private System.Windows.Forms.Label lblTitulo;
    private FontAwesome.Sharp.IconButton btnClose;

    // Controles principales
    private System.Windows.Forms.Label lblConcepto;
    private System.Windows.Forms.TextBox txtConcepto;
    private System.Windows.Forms.Label lblMonto;
    private System.Windows.Forms.NumericUpDown numMonto;
    private System.Windows.Forms.Label lblFecha;
    private System.Windows.Forms.DateTimePicker dtpFecha;
    
    // Fotografía Recibo
    private System.Windows.Forms.GroupBox grpRecibo;
    private System.Windows.Forms.PictureBox picRecibo;
    private FontAwesome.Sharp.IconButton btnTomarFoto;
    private FontAwesome.Sharp.IconButton btnSubirFoto;
    private FontAwesome.Sharp.IconButton btnQuitarFoto;
    
    // Acciones
    private FontAwesome.Sharp.IconButton btnGuardar;
    private FontAwesome.Sharp.IconButton btnCancelar;

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
        pnlCabecera = new Panel();
        btnClose = new FontAwesome.Sharp.IconButton();
        lblTitulo = new Label();
        lblConcepto = new Label();
        txtConcepto = new TextBox();
        lblMonto = new Label();
        numMonto = new NumericUpDown();
        lblFecha = new Label();
        dtpFecha = new DateTimePicker();
        grpRecibo = new GroupBox();
        btnQuitarFoto = new FontAwesome.Sharp.IconButton();
        btnSubirFoto = new FontAwesome.Sharp.IconButton();
        btnTomarFoto = new FontAwesome.Sharp.IconButton();
        picRecibo = new PictureBox();
        btnGuardar = new FontAwesome.Sharp.IconButton();
        btnCancelar = new FontAwesome.Sharp.IconButton();
        pnlCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numMonto).BeginInit();
        grpRecibo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picRecibo).BeginInit();
        SuspendLayout();
        // 
        // pnlCabecera
        // 
        pnlCabecera.Controls.Add(btnClose);
        pnlCabecera.Controls.Add(lblTitulo);
        pnlCabecera.Dock = DockStyle.Top;
        pnlCabecera.Location = new Point(1, 1);
        pnlCabecera.Name = "pnlCabecera";
        pnlCabecera.Size = new Size(678, 60);
        pnlCabecera.TabIndex = 0;
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
        btnClose.Location = new Point(638, 5);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(35, 35);
        btnClose.TabIndex = 0;
        btnClose.Click += AlHacerClickEnCancelar;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
        lblTitulo.Location = new Point(17, 5);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(183, 29);
        lblTitulo.TabIndex = 1;
        lblTitulo.Text = "Nuevo Ingreso";
        // 
        // lblConcepto
        // 
        lblConcepto.AutoSize = true;
        lblConcepto.Location = new Point(30, 80);
        lblConcepto.Name = "lblConcepto";
        lblConcepto.Size = new Size(103, 15);
        lblConcepto.TabIndex = 9;
        lblConcepto.Text = "Concepto/Detalle:";
        // 
        // txtConcepto
        // 
        txtConcepto.Location = new Point(30, 105);
        txtConcepto.Multiline = true;
        txtConcepto.Name = "txtConcepto";
        txtConcepto.Size = new Size(320, 80);
        txtConcepto.TabIndex = 8;
        // 
        // lblMonto
        // 
        lblMonto.AutoSize = true;
        lblMonto.Location = new Point(30, 272);
        lblMonto.Name = "lblMonto";
        lblMonto.Size = new Size(121, 15);
        lblMonto.TabIndex = 7;
        lblMonto.Text = "Monto Monetario (₡):";
        // 
        // numMonto
        // 
        numMonto.DecimalPlaces = 2;
        numMonto.Location = new Point(30, 297);
        numMonto.Maximum = new decimal(new int[] { 100000000, 0, 0, 0 });
        numMonto.Name = "numMonto";
        numMonto.Size = new Size(150, 23);
        numMonto.TabIndex = 6;
        // 
        // lblFecha
        // 
        lblFecha.AutoSize = true;
        lblFecha.Location = new Point(30, 202);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(41, 15);
        lblFecha.TabIndex = 5;
        lblFecha.Text = "Fecha:";
        // 
        // dtpFecha
        // 
        dtpFecha.Format = DateTimePickerFormat.Short;
        dtpFecha.Location = new Point(30, 227);
        dtpFecha.Name = "dtpFecha";
        dtpFecha.Size = new Size(150, 23);
        dtpFecha.TabIndex = 4;
        // 
        // grpRecibo
        // 
        grpRecibo.Controls.Add(btnQuitarFoto);
        grpRecibo.Controls.Add(btnSubirFoto);
        grpRecibo.Controls.Add(btnTomarFoto);
        grpRecibo.Controls.Add(picRecibo);
        grpRecibo.Location = new Point(380, 80);
        grpRecibo.Name = "grpRecibo";
        grpRecibo.Size = new Size(270, 310);
        grpRecibo.TabIndex = 1;
        grpRecibo.TabStop = false;
        grpRecibo.Text = "Recibo o Comprobante";
        // 
        // btnQuitarFoto
        // 
        btnQuitarFoto.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
        btnQuitarFoto.IconColor = Color.Black;
        btnQuitarFoto.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnQuitarFoto.IconSize = 20;
        btnQuitarFoto.Location = new Point(205, 250);
        btnQuitarFoto.Name = "btnQuitarFoto";
        btnQuitarFoto.Size = new Size(50, 40);
        btnQuitarFoto.TabIndex = 0;
        // 
        // btnSubirFoto
        // 
        btnSubirFoto.IconChar = FontAwesome.Sharp.IconChar.Upload;
        btnSubirFoto.IconColor = Color.Black;
        btnSubirFoto.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnSubirFoto.IconSize = 20;
        btnSubirFoto.Location = new Point(140, 250);
        btnSubirFoto.Name = "btnSubirFoto";
        btnSubirFoto.Size = new Size(50, 40);
        btnSubirFoto.TabIndex = 1;
        // 
        // btnTomarFoto
        // 
        btnTomarFoto.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;
        btnTomarFoto.IconColor = Color.Black;
        btnTomarFoto.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnTomarFoto.IconSize = 20;
        btnTomarFoto.Location = new Point(15, 250);
        btnTomarFoto.Name = "btnTomarFoto";
        btnTomarFoto.Size = new Size(120, 40);
        btnTomarFoto.TabIndex = 2;
        btnTomarFoto.Text = "Cámara";
        btnTomarFoto.TextImageRelation = TextImageRelation.ImageBeforeText;
        // 
        // picRecibo
        // 
        picRecibo.BorderStyle = BorderStyle.FixedSingle;
        picRecibo.Location = new Point(15, 30);
        picRecibo.Name = "picRecibo";
        picRecibo.Size = new Size(240, 210);
        picRecibo.SizeMode = PictureBoxSizeMode.Zoom;
        picRecibo.TabIndex = 3;
        picRecibo.TabStop = false;
        // 
        // btnGuardar
        // 
        btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
        btnGuardar.IconColor = Color.Black;
        btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnGuardar.IconSize = 24;
        btnGuardar.Location = new Point(30, 345);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(130, 45);
        btnGuardar.TabIndex = 3;
        btnGuardar.Text = "Guardar";
        btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
        // 
        // btnCancelar
        // 
        btnCancelar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
        btnCancelar.IconColor = Color.Black;
        btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCancelar.IconSize = 24;
        btnCancelar.Location = new Point(170, 345);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(130, 45);
        btnCancelar.TabIndex = 2;
        btnCancelar.Text = "Cancelar";
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
        // 
        // FrmEdicionCajaChica
        // 
        ClientSize = new Size(680, 430);
        Controls.Add(grpRecibo);
        Controls.Add(btnCancelar);
        Controls.Add(btnGuardar);
        Controls.Add(dtpFecha);
        Controls.Add(lblFecha);
        Controls.Add(numMonto);
        Controls.Add(lblMonto);
        Controls.Add(txtConcepto);
        Controls.Add(lblConcepto);
        Controls.Add(pnlCabecera);
        Name = "FrmEdicionCajaChica";
        StartPosition = FormStartPosition.CenterParent;
        pnlCabecera.ResumeLayout(false);
        pnlCabecera.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numMonto).EndInit();
        grpRecibo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)picRecibo).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
