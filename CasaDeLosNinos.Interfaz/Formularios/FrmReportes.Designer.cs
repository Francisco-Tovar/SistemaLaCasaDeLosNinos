namespace CasaDeLosNinos.Interfaz.Formularios;

partial class FrmReportes
{
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel pnlReportes;
    private System.Windows.Forms.Label lblTituloReporte;
    private System.Windows.Forms.ComboBox cboTipoReporte;
    private System.Windows.Forms.Panel pnlFiltros;
    private System.Windows.Forms.ComboBox cboMes;
    private System.Windows.Forms.ComboBox cboAnio;
    private System.Windows.Forms.DateTimePicker dtpInicio;
    private System.Windows.Forms.DateTimePicker dtpFin;
    private System.Windows.Forms.Label lblFiltro1;
    private System.Windows.Forms.Label lblFiltro2;
    private FontAwesome.Sharp.IconButton btnGenerarPdf;
    private FontAwesome.Sharp.IconButton btnGenerarCsv;
    private FontAwesome.Sharp.IconButton btnVistaPrevia;
    private System.Windows.Forms.Label lblPersona;
    private System.Windows.Forms.ComboBox cboPersona;
    private System.Windows.Forms.CheckBox chkIncluirFotos;

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
        pnlReportes = new Panel();
        btnVistaPrevia = new FontAwesome.Sharp.IconButton();
        btnGenerarCsv = new FontAwesome.Sharp.IconButton();
        btnGenerarPdf = new FontAwesome.Sharp.IconButton();
        pnlFiltros = new Panel();
        chkIncluirFotos = new CheckBox();
        cboPersona = new ComboBox();
        lblPersona = new Label();
        dtpFin = new DateTimePicker();
        lblFiltro2 = new Label();
        dtpInicio = new DateTimePicker();
        lblFiltro1 = new Label();
        cboAnio = new ComboBox();
        cboMes = new ComboBox();
        cboTipoReporte = new ComboBox();
        lblTituloReporte = new Label();
        pnlReportes.SuspendLayout();
        pnlFiltros.SuspendLayout();
        SuspendLayout();
        // 
        // pnlReportes
        // 
        pnlReportes.Controls.Add(btnVistaPrevia);
        pnlReportes.Controls.Add(btnGenerarCsv);
        pnlReportes.Controls.Add(btnGenerarPdf);
        pnlReportes.Controls.Add(pnlFiltros);
        pnlReportes.Controls.Add(cboTipoReporte);
        pnlReportes.Controls.Add(lblTituloReporte);
        pnlReportes.Location = new Point(30, 20);
        pnlReportes.Name = "pnlReportes";
        pnlReportes.Size = new Size(740, 319);
        pnlReportes.TabIndex = 0;
        // 
        // btnVistaPrevia
        // 
        btnVistaPrevia.FlatAppearance.BorderSize = 0;
        btnVistaPrevia.FlatStyle = FlatStyle.Flat;
        btnVistaPrevia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnVistaPrevia.IconChar = FontAwesome.Sharp.IconChar.Search;
        btnVistaPrevia.IconColor = Color.Black;
        btnVistaPrevia.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnVistaPrevia.IconSize = 28;
        btnVistaPrevia.Location = new Point(20, 226);
        btnVistaPrevia.Name = "btnVistaPrevia";
        btnVistaPrevia.Size = new Size(170, 45);
        btnVistaPrevia.TabIndex = 5;
        btnVistaPrevia.Text = "Vista Previa";
        btnVistaPrevia.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnVistaPrevia.UseVisualStyleBackColor = true;
        // 
        // btnGenerarCsv
        // 
        btnGenerarCsv.FlatAppearance.BorderSize = 0;
        btnGenerarCsv.FlatStyle = FlatStyle.Flat;
        btnGenerarCsv.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnGenerarCsv.IconChar = FontAwesome.Sharp.IconChar.FileCsv;
        btnGenerarCsv.IconColor = Color.Black;
        btnGenerarCsv.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnGenerarCsv.IconSize = 28;
        btnGenerarCsv.Location = new Point(372, 226);
        btnGenerarCsv.Name = "btnGenerarCsv";
        btnGenerarCsv.Size = new Size(170, 45);
        btnGenerarCsv.TabIndex = 4;
        btnGenerarCsv.Text = "Exportar CSV";
        btnGenerarCsv.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnGenerarCsv.UseVisualStyleBackColor = true;
        // 
        // btnGenerarPdf
        // 
        btnGenerarPdf.FlatAppearance.BorderSize = 0;
        btnGenerarPdf.FlatStyle = FlatStyle.Flat;
        btnGenerarPdf.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnGenerarPdf.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
        btnGenerarPdf.IconColor = Color.Black;
        btnGenerarPdf.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnGenerarPdf.IconSize = 28;
        btnGenerarPdf.Location = new Point(196, 226);
        btnGenerarPdf.Name = "btnGenerarPdf";
        btnGenerarPdf.Size = new Size(170, 45);
        btnGenerarPdf.TabIndex = 3;
        btnGenerarPdf.Text = "Generar PDF";
        btnGenerarPdf.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnGenerarPdf.UseVisualStyleBackColor = true;
        // 
        // pnlFiltros
        // 
        pnlFiltros.Controls.Add(chkIncluirFotos);
        pnlFiltros.Controls.Add(cboPersona);
        pnlFiltros.Controls.Add(lblPersona);
        pnlFiltros.Controls.Add(dtpFin);
        pnlFiltros.Controls.Add(lblFiltro2);
        pnlFiltros.Controls.Add(dtpInicio);
        pnlFiltros.Controls.Add(lblFiltro1);
        pnlFiltros.Controls.Add(cboAnio);
        pnlFiltros.Controls.Add(cboMes);
        pnlFiltros.Location = new Point(20, 105);
        pnlFiltros.Name = "pnlFiltros";
        pnlFiltros.Size = new Size(548, 101);
        pnlFiltros.TabIndex = 2;
        // 
        // chkIncluirFotos
        // 
        chkIncluirFotos.AutoSize = true;
        chkIncluirFotos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        chkIncluirFotos.Location = new Point(11, 72);
        chkIncluirFotos.Name = "chkIncluirFotos";
        chkIncluirFotos.Size = new Size(131, 19);
        chkIncluirFotos.TabIndex = 6;
        chkIncluirFotos.Text = "Adjuntar Imágenes";
        chkIncluirFotos.UseVisualStyleBackColor = true;
        chkIncluirFotos.Visible = false;
        // 
        // cboPersona
        // 
        cboPersona.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPersona.Location = new Point(11, 35);
        cboPersona.Name = "cboPersona";
        cboPersona.Size = new Size(234, 23);
        cboPersona.TabIndex = 7;
        cboPersona.Visible = false;
        // 
        // lblPersona
        // 
        lblPersona.AutoSize = true;
        lblPersona.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPersona.Location = new Point(11, 12);
        lblPersona.Name = "lblPersona";
        lblPersona.Size = new Size(70, 15);
        lblPersona.TabIndex = 6;
        lblPersona.Text = "Seleccione:";
        lblPersona.Visible = false;
        // 
        // dtpFin
        // 
        dtpFin.Format = DateTimePickerFormat.Short;
        dtpFin.Location = new Point(387, 35);
        dtpFin.Name = "dtpFin";
        dtpFin.Size = new Size(120, 23);
        dtpFin.TabIndex = 5;
        dtpFin.Visible = false;
        // 
        // lblFiltro2
        // 
        lblFiltro2.AutoSize = true;
        lblFiltro2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFiltro2.Location = new Point(387, 12);
        lblFiltro2.Name = "lblFiltro2";
        lblFiltro2.Size = new Size(41, 15);
        lblFiltro2.TabIndex = 4;
        lblFiltro2.Text = "Hasta:";
        lblFiltro2.Visible = false;
        // 
        // dtpInicio
        // 
        dtpInicio.Format = DateTimePickerFormat.Short;
        dtpInicio.Location = new Point(261, 35);
        dtpInicio.Name = "dtpInicio";
        dtpInicio.Size = new Size(120, 23);
        dtpInicio.TabIndex = 3;
        dtpInicio.Visible = false;
        // 
        // lblFiltro1
        // 
        lblFiltro1.AutoSize = true;
        lblFiltro1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFiltro1.Location = new Point(11, 12);
        lblFiltro1.Name = "lblFiltro1";
        lblFiltro1.Size = new Size(53, 15);
        lblFiltro1.TabIndex = 2;
        lblFiltro1.Text = "Periodo:";
        // 
        // cboAnio
        // 
        cboAnio.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAnio.Location = new Point(160, 35);
        cboAnio.Name = "cboAnio";
        cboAnio.Size = new Size(85, 23);
        cboAnio.TabIndex = 1;
        // 
        // cboMes
        // 
        cboMes.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMes.Location = new Point(10, 35);
        cboMes.Name = "cboMes";
        cboMes.Size = new Size(130, 23);
        cboMes.TabIndex = 0;
        // 
        // cboTipoReporte
        // 
        cboTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTipoReporte.Font = new Font("Segoe UI", 11F);
        cboTipoReporte.Location = new Point(20, 60);
        cboTipoReporte.Name = "cboTipoReporte";
        cboTipoReporte.Size = new Size(400, 28);
        cboTipoReporte.TabIndex = 1;
        // 
        // lblTituloReporte
        // 
        lblTituloReporte.AutoSize = true;
        lblTituloReporte.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTituloReporte.ForeColor = Color.Goldenrod;
        lblTituloReporte.Location = new Point(10, 0);
        lblTituloReporte.Name = "lblTituloReporte";
        lblTituloReporte.Size = new Size(230, 30);
        lblTituloReporte.TabIndex = 0;
        lblTituloReporte.Text = "Seleccione el reporte";
        // 
        // FrmReportes
        // 
        ClientSize = new Size(800, 393);
        Controls.Add(pnlReportes);
        Name = "FrmReportes";
        Text = "Reportes y Mantenimiento";
        pnlReportes.ResumeLayout(false);
        pnlReportes.PerformLayout();
        pnlFiltros.ResumeLayout(false);
        pnlFiltros.PerformLayout();
        ResumeLayout(false);
    }
}
