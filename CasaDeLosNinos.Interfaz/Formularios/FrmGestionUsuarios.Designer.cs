namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmGestionUsuarios
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
            panelHeader = new Panel();
            btnDesactivar = new FontAwesome.Sharp.IconButton();
            btnEditar = new FontAwesome.Sharp.IconButton();
            label1 = new Label();
            txtBusqueda = new TextBox();
            btnNuevo = new FontAwesome.Sharp.IconButton();
            dgvUsuarios = new DataGridView();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(btnDesactivar);
            panelHeader.Controls.Add(btnEditar);
            panelHeader.Controls.Add(label1);
            panelHeader.Controls.Add(txtBusqueda);
            panelHeader.Controls.Add(btnNuevo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(1, 1);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(882, 53);
            panelHeader.TabIndex = 0;
            // 
            // btnDesactivar
            // 
            btnDesactivar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDesactivar.IconChar = FontAwesome.Sharp.IconChar.UserSlash;

            btnDesactivar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDesactivar.IconSize = 24;
            btnDesactivar.Location = new Point(478, 11);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(130, 32);
            btnDesactivar.TabIndex = 4;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += btnDesactivar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditar.IconChar = FontAwesome.Sharp.IconChar.UserPen;

            btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditar.IconSize = 24;
            btnEditar.Location = new Point(613, 11);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 32);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 17);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 2;
            label1.Text = "Buscar por nombre:";
            // 
            // txtBusqueda
            // 
            txtBusqueda.BorderStyle = BorderStyle.FixedSingle;
            txtBusqueda.Location = new Point(185, 17);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = " Nombre o usuario...";
            txtBusqueda.Size = new Size(250, 23);
            txtBusqueda.TabIndex = 1;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // btnNuevo
            // 
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;

            btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNuevo.IconSize = 24;
            btnNuevo.Location = new Point(718, 11);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(150, 32);
            btnNuevo.TabIndex = 0;
            btnNuevo.Text = "Nuevo Usuario";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ColumnHeadersHeight = 35;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(1, 54);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowTemplate.Height = 30;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(882, 440);
            dgvUsuarios.TabIndex = 1;
            dgvUsuarios.CellDoubleClick += dgvUsuarios_CellDoubleClick;
            // 
            // FrmGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 495);
            Controls.Add(dgvUsuarios);
            Controls.Add(panelHeader);
            Name = "FrmGestionUsuarios";
            Text = "Gestión de Usuarios";
            Load += FrmGestionUsuarios_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }

        private Panel panelHeader;
        private DataGridView dgvUsuarios;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnDesactivar;
        private Label label1;
        private TextBox txtBusqueda;
    }
}
