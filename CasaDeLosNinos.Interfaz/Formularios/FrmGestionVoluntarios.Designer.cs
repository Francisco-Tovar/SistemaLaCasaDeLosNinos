namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmGestionVoluntarios
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
            panelTools = new Panel();
            btnRefrescar = new FontAwesome.Sharp.IconButton();
            btnDesactivar = new FontAwesome.Sharp.IconButton();
            btnHoras = new FontAwesome.Sharp.IconButton();
            btnEditar = new FontAwesome.Sharp.IconButton();
            btnNuevo = new FontAwesome.Sharp.IconButton();
            chkInactivos = new CheckBox();
            txtBusqueda = new TextBox();
            panelGrid = new Panel();
            dgvVoluntarios = new DataGridView();
            panelStatus = new Panel();
            lblStatus = new Label();
            panelTools.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVoluntarios).BeginInit();
            panelStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panelTools
            // 
            panelTools.Controls.Add(btnRefrescar);
            panelTools.Controls.Add(btnDesactivar);
            panelTools.Controls.Add(btnHoras);
            panelTools.Controls.Add(btnEditar);
            panelTools.Controls.Add(btnNuevo);
            panelTools.Controls.Add(chkInactivos);
            panelTools.Controls.Add(txtBusqueda);
            panelTools.Dock = DockStyle.Top;
            panelTools.Location = new Point(1, 1);
            panelTools.Name = "panelTools";
            panelTools.Padding = new Padding(10);
            panelTools.Size = new Size(898, 48);
            panelTools.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Font = new Font("Segoe UI", 9.75F);
            btnRefrescar.IconChar = FontAwesome.Sharp.IconChar.Refresh;
            btnRefrescar.IconColor = Color.Black;
            btnRefrescar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefrescar.IconSize = 24;
            btnRefrescar.Location = new Point(742, 6);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(113, 32);
            btnRefrescar.TabIndex = 2;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Font = new Font("Segoe UI", 9.75F);
            btnDesactivar.IconChar = FontAwesome.Sharp.IconChar.UserSlash;
            btnDesactivar.IconColor = Color.Black;
            btnDesactivar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDesactivar.IconSize = 24;
            btnDesactivar.Location = new Point(510, 6);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(110, 32);
            btnDesactivar.TabIndex = 3;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnHoras
            // 
            btnHoras.Font = new Font("Segoe UI", 9.75F);
            btnHoras.IconChar = FontAwesome.Sharp.IconChar.ClockFour;
            btnHoras.IconColor = Color.Black;
            btnHoras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnHoras.IconSize = 24;
            btnHoras.Location = new Point(626, 7);
            btnHoras.Name = "btnHoras";
            btnHoras.Size = new Size(110, 32);
            btnHoras.TabIndex = 4;
            btnHoras.Text = "Horas";
            btnHoras.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnHoras.UseVisualStyleBackColor = true;
            btnHoras.Click += btnHoras_Click;
            // 
            // btnEditar
            // 
            btnEditar.Font = new Font("Segoe UI", 9.75F);
            btnEditar.IconChar = FontAwesome.Sharp.IconChar.UserPen;
            btnEditar.IconColor = Color.Black;
            btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditar.IconSize = 24;
            btnEditar.Location = new Point(404, 7);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 32);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Font = new Font("Segoe UI", 9.75F);
            btnNuevo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnNuevo.IconColor = Color.Black;
            btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNuevo.IconSize = 24;
            btnNuevo.Location = new Point(298, 7);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 32);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // chkInactivos
            // 
            chkInactivos.AutoSize = true;
            chkInactivos.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkInactivos.Location = new Point(200, 14);
            chkInactivos.Name = "chkInactivos";
            chkInactivos.Size = new Size(77, 21);
            chkInactivos.TabIndex = 1;
            chkInactivos.Text = "Inactivos";
            chkInactivos.UseVisualStyleBackColor = true;
            chkInactivos.CheckedChanged += chkInactivos_CheckedChanged;
            // 
            // txtBusqueda
            // 
            txtBusqueda.BorderStyle = BorderStyle.FixedSingle;
            txtBusqueda.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBusqueda.Location = new Point(10, 12);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "🔍  Buscar...";
            txtBusqueda.Size = new Size(175, 25);
            txtBusqueda.TabIndex = 0;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvVoluntarios);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(1, 49);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(898, 520);
            panelGrid.TabIndex = 2;
            // 
            // dgvVoluntarios
            // 
            dgvVoluntarios.AllowUserToAddRows = false;
            dgvVoluntarios.AllowUserToDeleteRows = false;
            dgvVoluntarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVoluntarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVoluntarios.Dock = DockStyle.Fill;
            dgvVoluntarios.Location = new Point(0, 0);
            dgvVoluntarios.MultiSelect = false;
            dgvVoluntarios.Name = "dgvVoluntarios";
            dgvVoluntarios.ReadOnly = true;
            dgvVoluntarios.RowHeadersVisible = false;
            dgvVoluntarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVoluntarios.Size = new Size(898, 520);
            dgvVoluntarios.TabIndex = 0;
            dgvVoluntarios.CellFormatting += dgvVoluntarios_CellFormatting;
            // 
            // panelStatus
            // 
            panelStatus.Controls.Add(lblStatus);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(1, 569);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(898, 30);
            panelStatus.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 8);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(67, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Registros: 0";
            // 
            // FrmGestionVoluntarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(panelGrid);
            Controls.Add(panelStatus);
            Controls.Add(panelTools);
            Name = "FrmGestionVoluntarios";
            Text = "Gestión de Voluntarios";
            Load += FrmGestionVoluntarios_Load;
            panelTools.ResumeLayout(false);
            panelTools.PerformLayout();
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVoluntarios).EndInit();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.CheckBox chkInactivos;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnHoras;
        private FontAwesome.Sharp.IconButton btnDesactivar;
        private FontAwesome.Sharp.IconButton btnRefrescar;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvVoluntarios;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}
