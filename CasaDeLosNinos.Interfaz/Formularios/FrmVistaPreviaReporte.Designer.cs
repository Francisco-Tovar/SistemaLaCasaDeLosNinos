namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmVistaPreviaReporte
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlHeader = new System.Windows.Forms.Panel();
            lblTitulo = new System.Windows.Forms.Label();
            dgvPreview = new System.Windows.Forms.DataGridView();
            pnlFooter = new System.Windows.Forms.Panel();
            lblRegistros = new System.Windows.Forms.Label();
            btnCerrar = new FontAwesome.Sharp.IconButton();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(900, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(232, 25);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Vista Previa del Reporte";
            // 
            // dgvPreview
            // 
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AllowUserToDeleteRows = false;
            dgvPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.BackgroundColor = System.Drawing.Color.White;
            dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvPreview.DefaultCellStyle = dataGridViewCellStyle1;
            dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvPreview.Location = new System.Drawing.Point(0, 60);
            dgvPreview.Name = "dgvPreview";
            dgvPreview.ReadOnly = true;
            dgvPreview.RowHeadersVisible = false;
            dgvPreview.Size = new System.Drawing.Size(900, 440);
            dgvPreview.TabIndex = 1;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(lblRegistros);
            pnlFooter.Controls.Add(btnCerrar);
            pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlFooter.Location = new System.Drawing.Point(0, 500);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new System.Drawing.Size(900, 60);
            pnlFooter.TabIndex = 2;
            // 
            // lblRegistros
            // 
            lblRegistros.AutoSize = true;
            lblRegistros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblRegistros.Location = new System.Drawing.Point(20, 22);
            lblRegistros.Name = "lblRegistros";
            lblRegistros.Size = new System.Drawing.Size(126, 15);
            lblRegistros.TabIndex = 1;
            lblRegistros.Text = "Registros encontrados: 0";
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Times;
            btnCerrar.IconColor = System.Drawing.Color.Black;
            btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCerrar.IconSize = 24;
            btnCerrar.Location = new System.Drawing.Point(760, 10);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(120, 40);
            btnCerrar.TabIndex = 0;
            btnCerrar.Text = "Cerrar";
            btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // FrmVistaPreviaReporte
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 560);
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
