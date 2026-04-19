namespace CasaDeLosNinos.Interfaz.Formularios;

partial class FrmGestionCajaChica
{
    private System.ComponentModel.IContainer components = null;
    
    // Controles
    private System.Windows.Forms.Panel pnlHerramientas;
    private FontAwesome.Sharp.IconButton btnNuevoIngreso;
    private FontAwesome.Sharp.IconButton btnNuevoEgreso;
    private FontAwesome.Sharp.IconButton btnAuditoria;
    
    private System.Windows.Forms.Label lblMes;
    private System.Windows.Forms.ComboBox cboMes;
    private System.Windows.Forms.ComboBox cboAnio;
    
    private System.Windows.Forms.Panel pnlSaldo;
    private System.Windows.Forms.Label lblTextoSaldo;
    private System.Windows.Forms.Label lblSaldoMonto;
    
    private System.Windows.Forms.DataGridView grdMovimientos;

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
        pnlHerramientas = new Panel();
        btnAuditoria = new FontAwesome.Sharp.IconButton();
        btnNuevoEgreso = new FontAwesome.Sharp.IconButton();
        btnNuevoIngreso = new FontAwesome.Sharp.IconButton();
        cboAnio = new ComboBox();
        cboMes = new ComboBox();
        lblMes = new Label();
        pnlSaldo = new Panel();
        lblSaldoMonto = new Label();
        lblTextoSaldo = new Label();
        grdMovimientos = new DataGridView();
        pnlHerramientas.SuspendLayout();
        pnlSaldo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grdMovimientos).BeginInit();
        SuspendLayout();
        // 
        // pnlHerramientas
        // 
        pnlHerramientas.Controls.Add(btnAuditoria);
        pnlHerramientas.Controls.Add(btnNuevoEgreso);
        pnlHerramientas.Controls.Add(btnNuevoIngreso);
        pnlHerramientas.Controls.Add(cboAnio);
        pnlHerramientas.Controls.Add(cboMes);
        pnlHerramientas.Controls.Add(lblMes);
        pnlHerramientas.Controls.Add(pnlSaldo);
        pnlHerramientas.Dock = DockStyle.Top;
        pnlHerramientas.Location = new Point(1, 1);
        pnlHerramientas.Name = "pnlHerramientas";
        pnlHerramientas.Size = new Size(998, 70);
        pnlHerramientas.TabIndex = 2;
        // 
        // btnAuditoria
        // 
        btnAuditoria.IconChar = FontAwesome.Sharp.IconChar.Eye;
        btnAuditoria.IconColor = Color.Black;
        btnAuditoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnAuditoria.IconSize = 22;
        btnAuditoria.Location = new Point(568, 15);
        btnAuditoria.Name = "btnAuditoria";
        btnAuditoria.Size = new Size(120, 40);
        btnAuditoria.TabIndex = 0;
        btnAuditoria.Text = "Auditoría";
        btnAuditoria.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnAuditoria.Visible = true;
        // 
        // btnNuevoEgreso
        // 
        btnNuevoEgreso.IconChar = FontAwesome.Sharp.IconChar.MinusCircle;
        btnNuevoEgreso.IconColor = Color.White;
        btnNuevoEgreso.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnNuevoEgreso.IconSize = 22;
        btnNuevoEgreso.Location = new Point(442, 15);
        btnNuevoEgreso.Name = "btnNuevoEgreso";
        btnNuevoEgreso.Size = new Size(120, 40);
        btnNuevoEgreso.TabIndex = 1;
        btnNuevoEgreso.Text = "Egreso";
        btnNuevoEgreso.TextImageRelation = TextImageRelation.ImageBeforeText;
        // 
        // btnNuevoIngreso
        // 
        btnNuevoIngreso.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
        btnNuevoIngreso.IconColor = Color.White;
        btnNuevoIngreso.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnNuevoIngreso.IconSize = 22;
        btnNuevoIngreso.Location = new Point(316, 15);
        btnNuevoIngreso.Name = "btnNuevoIngreso";
        btnNuevoIngreso.Size = new Size(120, 40);
        btnNuevoIngreso.TabIndex = 2;
        btnNuevoIngreso.Text = "Ingreso";
        btnNuevoIngreso.TextImageRelation = TextImageRelation.ImageBeforeText;
        // 
        // cboAnio
        // 
        cboAnio.DropDownStyle = ComboBoxStyle.DropDownList;
        cboAnio.Location = new Point(230, 23);
        cboAnio.Name = "cboAnio";
        cboAnio.Size = new Size(80, 23);
        cboAnio.TabIndex = 3;
        // 
        // cboMes
        // 
        cboMes.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMes.Location = new Point(100, 23);
        cboMes.Name = "cboMes";
        cboMes.Size = new Size(120, 23);
        cboMes.TabIndex = 4;
        // 
        // lblMes
        // 
        lblMes.AutoSize = true;
        lblMes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblMes.Location = new Point(18, 23);
        lblMes.Name = "lblMes";
        lblMes.Size = new Size(74, 21);
        lblMes.TabIndex = 5;
        lblMes.Text = "Período:";
        // 
        // pnlSaldo
        // 
        pnlSaldo.Controls.Add(lblSaldoMonto);
        pnlSaldo.Controls.Add(lblTextoSaldo);
        pnlSaldo.Dock = DockStyle.Right;
        pnlSaldo.Location = new Point(748, 0);
        pnlSaldo.Name = "pnlSaldo";
        pnlSaldo.Size = new Size(250, 70);
        pnlSaldo.TabIndex = 6;
        // 
        // lblSaldoMonto
        // 
        lblSaldoMonto.AutoSize = true;
        lblSaldoMonto.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblSaldoMonto.Location = new Point(18, 30);
        lblSaldoMonto.Name = "lblSaldoMonto";
        lblSaldoMonto.Size = new Size(76, 30);
        lblSaldoMonto.TabIndex = 0;
        lblSaldoMonto.Text = "₡ 0.00";
        // 
        // lblTextoSaldo
        // 
        lblTextoSaldo.AutoSize = true;
        lblTextoSaldo.Location = new Point(20, 10);
        lblTextoSaldo.Name = "lblTextoSaldo";
        lblTextoSaldo.Size = new Size(64, 15);
        lblTextoSaldo.TabIndex = 1;
        lblTextoSaldo.Text = "Saldo Mes:";
        // 
        // grdMovimientos
        // 
        grdMovimientos.ColumnHeadersHeight = 45;
        grdMovimientos.Dock = DockStyle.Fill;
        grdMovimientos.Location = new Point(1, 71);
        grdMovimientos.MultiSelect = false;
        grdMovimientos.Name = "grdMovimientos";
        grdMovimientos.ReadOnly = true;
        grdMovimientos.RowHeadersVisible = false;
        grdMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grdMovimientos.AllowUserToResizeColumns = false;
        grdMovimientos.AllowUserToResizeRows = false;
        grdMovimientos.Size = new Size(998, 528);
        grdMovimientos.TabIndex = 1;
        // 
        // FrmGestionCajaChica
        // 
        ClientSize = new Size(1000, 600);
        Controls.Add(grdMovimientos);
        Controls.Add(pnlHerramientas);
        Name = "FrmGestionCajaChica";
        Text = "Caja Chica";
        pnlHerramientas.ResumeLayout(false);
        pnlHerramientas.PerformLayout();
        pnlSaldo.ResumeLayout(false);
        pnlSaldo.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grdMovimientos).EndInit();
        ResumeLayout(false);
    }
}
