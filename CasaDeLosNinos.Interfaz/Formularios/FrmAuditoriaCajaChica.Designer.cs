namespace CasaDeLosNinos.Interfaz.Formularios;

partial class FrmAuditoriaCajaChica
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Panel panelCabecera;
    private System.Windows.Forms.Label lblTitulo;
    private FontAwesome.Sharp.IconButton btnClose;
    private System.Windows.Forms.DataGridView grdAuditoria;
    
    // Columnas del Grid
    private System.Windows.Forms.DataGridViewTextBoxColumn colId;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
    private System.Windows.Forms.DataGridViewTextBoxColumn colConcepto;
    private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle;
    private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;

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
        grdAuditoria = new DataGridView();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grdAuditoria).BeginInit();
        SuspendLayout();
        // 
        // panelCabecera
        // 
        panelCabecera.Controls.Add(btnClose);
        panelCabecera.Controls.Add(lblTitulo);
        panelCabecera.Dock = DockStyle.Top;
        panelCabecera.Location = new Point(1, 1);
        panelCabecera.Name = "panelCabecera";
        panelCabecera.Size = new Size(948, 75);
        panelCabecera.TabIndex = 2;
        panelCabecera.MouseDown += panelCabecera_MouseDown;
        // 
        // btnClose
        // 
        btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClose.Cursor = Cursors.Hand;
        btnClose.FlatAppearance.BorderSize = 0;
        btnClose.FlatStyle = FlatStyle.Flat;
        btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
        btnClose.IconColor = Color.Black;
        btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnClose.IconSize = 20;
        btnClose.Location = new Point(910, 3);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(35, 35);
        btnClose.TabIndex = 0;
        btnClose.Click += AlHacerClickEnCerrar;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Location = new Point(14, 18);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(122, 15);
        lblTitulo.TabIndex = 1;
        lblTitulo.Text = "📊  Auditoría Mensual";
        // 
        // grdAuditoria
        // 
        grdAuditoria.AllowUserToAddRows = false;
        grdAuditoria.AllowUserToDeleteRows = false;
        grdAuditoria.AllowUserToResizeColumns = false;
        grdAuditoria.AllowUserToResizeRows = false;
        grdAuditoria.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grdAuditoria.BackgroundColor = Color.White;
        grdAuditoria.BorderStyle = BorderStyle.None;
        grdAuditoria.ColumnHeadersHeight = 45;
        grdAuditoria.Dock = DockStyle.Fill;
        grdAuditoria.Location = new Point(1, 76);
        grdAuditoria.Name = "grdAuditoria";
        grdAuditoria.RowHeadersVisible = false;
        grdAuditoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grdAuditoria.Size = new Size(948, 523);
        grdAuditoria.TabIndex = 1;
        // 
        // FrmAuditoriaCajaChica
        // 
        ClientSize = new Size(950, 600);
        Controls.Add(grdAuditoria);
        Controls.Add(panelCabecera);
        Name = "FrmAuditoriaCajaChica";
        StartPosition = FormStartPosition.CenterParent;
        panelCabecera.ResumeLayout(false);
        panelCabecera.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grdAuditoria).EndInit();
        ResumeLayout(false);
    }
}
