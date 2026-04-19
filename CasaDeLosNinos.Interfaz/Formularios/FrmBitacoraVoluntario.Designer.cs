using System.Windows.Forms;
using System.Drawing;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmBitacoraVoluntario
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
            panelCabecera = new Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new Label();
            panelTools = new Panel();
            btnAgregar = new FontAwesome.Sharp.IconButton();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            lblVoluntario = new Label();
            panelGrid = new Panel();
            dgvHoras = new DataGridView();
            panelStatus = new Panel();
            lblTotal = new Label();
            panelCabecera.SuspendLayout();
            panelTools.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoras).BeginInit();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.Controls.Add(btnClose);
            panelCabecera.Controls.Add(lblTitulo);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(598, 75);
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
            btnClose.Location = new Point(568, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += AlHacerClickEnCerrar;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(14, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(139, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "📖  Bitácora de Horas";
            // 
            // panelTools
            // 
            panelTools.Controls.Add(btnAgregar);
            panelTools.Controls.Add(btnEliminar);
            panelTools.Controls.Add(lblVoluntario);
            panelTools.Dock = DockStyle.Top;
            panelTools.Location = new Point(1, 76);
            panelTools.Name = "panelTools";
            panelTools.Size = new Size(598, 65);
            panelTools.TabIndex = 1;
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregar.IconChar = FontAwesome.Sharp.IconChar.Add;
            btnAgregar.IconColor = Color.Black;
            btnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAgregar.IconSize = 20;
            btnAgregar.Location = new Point(376, 10);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(103, 30);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Nuevo";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnEliminar.IconColor = Color.Black;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 20;
            btnEliminar.Location = new Point(485, 10);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(103, 30);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // lblVoluntario
            // 
            lblVoluntario.AutoSize = true;
            lblVoluntario.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold);
            lblVoluntario.Location = new Point(12, 10);
            lblVoluntario.Name = "lblVoluntario";
            lblVoluntario.Size = new Size(158, 37);
            lblVoluntario.TabIndex = 2;
            lblVoluntario.Text = "[Nombre]";
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvHoras);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(1, 141);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(10);
            panelGrid.Size = new Size(598, 228);
            panelGrid.TabIndex = 2;
            // 
            // dgvHoras
            // 
            dgvHoras.AllowUserToAddRows = false;
            dgvHoras.AllowUserToDeleteRows = false;
            dgvHoras.AllowUserToResizeColumns = false;
            dgvHoras.AllowUserToResizeRows = false;
            dgvHoras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoras.Dock = DockStyle.Fill;
            dgvHoras.Location = new Point(10, 10);
            dgvHoras.MultiSelect = false;
            dgvHoras.Name = "dgvHoras";
            dgvHoras.ReadOnly = true;
            dgvHoras.RowHeadersVisible = false;
            dgvHoras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoras.Size = new Size(578, 208);
            dgvHoras.TabIndex = 0;
            // 
            // panelStatus
            // 
            panelStatus.Controls.Add(lblTotal);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(1, 369);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(598, 30);
            panelStatus.TabIndex = 3;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.Location = new Point(12, 8);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(112, 15);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Total Acumulado: 0";
            // 
            // FrmBitacoraVoluntario
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(panelGrid);
            Controls.Add(panelStatus);
            Controls.Add(panelTools);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmBitacoraVoluntario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bitácora — La Casa de los Niños";
            Load += FrmBitacoraVoluntario_Load;
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            panelTools.ResumeLayout(false);
            panelTools.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoras).EndInit();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelCabecera;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton btnClose;
        private Panel panelTools;
        private Label lblVoluntario;
        private FontAwesome.Sharp.IconButton btnAgregar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private Panel panelGrid;
        private DataGridView dgvHoras;
        private Panel panelStatus;
        private Label lblTotal;
    }
}
