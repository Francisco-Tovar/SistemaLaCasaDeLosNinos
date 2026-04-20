namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmVistaPreviaReporte
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMetadata;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Panel pnlFooter;
        private FontAwesome.Sharp.IconButton btnCerrar;
        private System.Windows.Forms.Label lblRegistros;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblMetadata = new Label();
            lblTitulo = new Label();
            btnCerrar = new FontAwesome.Sharp.IconButton();
            dgvPreview = new DataGridView();
            pnlFooter = new Panel();
            lblRegistros = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblMetadata);
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(btnCerrar);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(1, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(898, 85);
            pnlHeader.TabIndex = 0;
            // 
            // lblMetadata
            // 
            lblMetadata.AutoSize = true;
            lblMetadata.Font = new Font("Segoe UI", 9F);
            lblMetadata.ForeColor = Color.Gray;
            lblMetadata.Location = new Point(20, 52);
            lblMetadata.Name = "lblMetadata";
            lblMetadata.Size = new Size(107, 15);
            lblMetadata.TabIndex = 1;
            lblMetadata.Text = "Filtros aplicados: ...";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(224, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Vista Previa del Reporte";
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnCerrar.IconColor = Color.Black;
            btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCerrar.IconSize = 24;
            btnCerrar.Location = new Point(844, 10);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(41, 40);
            btnCerrar.TabIndex = 0;
            btnCerrar.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // dgvPreview
            // 
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AllowUserToDeleteRows = false;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.BackgroundColor = Color.White;
            dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvPreview.DefaultCellStyle = dataGridViewCellStyle1;
            dgvPreview.Dock = DockStyle.Fill;
            dgvPreview.Location = new Point(1, 86);
            dgvPreview.Name = "dgvPreview";
            dgvPreview.ReadOnly = true;
            dgvPreview.RowHeadersVisible = false;
            dgvPreview.Size = new Size(898, 413);
            dgvPreview.TabIndex = 1;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(lblRegistros);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(1, 499);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(898, 60);
            pnlFooter.TabIndex = 2;
            // 
            // lblRegistros
            // 
            lblRegistros.AutoSize = true;
            lblRegistros.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRegistros.Location = new Point(20, 22);
            lblRegistros.Name = "lblRegistros";
            lblRegistros.Size = new Size(133, 15);
            lblRegistros.TabIndex = 1;
            lblRegistros.Text = "Registros encontrados: 0";
            // 
            // FrmVistaPreviaReporte
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 560);
            Controls.Add(dgvPreview);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Name = "FrmVistaPreviaReporte";
            Text = "Vista Previa del Reporte";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).EndInit();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }
    }
}
