namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmGestionNinos
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
            panelHerramientas = new Panel();
            btnActualizar = new FontAwesome.Sharp.IconButton();
            btnDesactivar = new FontAwesome.Sharp.IconButton();
            btnBitacora = new FontAwesome.Sharp.IconButton();
            btnEditar = new FontAwesome.Sharp.IconButton();
            btnNuevo = new FontAwesome.Sharp.IconButton();
            chkMostrarInactivos = new CheckBox();
            txtBuscar = new TextBox();
            panelInferior = new Panel();
            lblConteo = new Label();
            grdNinos = new DataGridView();
            panelHerramientas.SuspendLayout();
            panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdNinos).BeginInit();
            SuspendLayout();
            // 
            // panelHerramientas
            // 
            panelHerramientas.Controls.Add(btnActualizar);
            panelHerramientas.Controls.Add(btnDesactivar);
            panelHerramientas.Controls.Add(btnBitacora);
            panelHerramientas.Controls.Add(btnEditar);
            panelHerramientas.Controls.Add(btnNuevo);
            panelHerramientas.Controls.Add(chkMostrarInactivos);
            panelHerramientas.Controls.Add(txtBuscar);
            panelHerramientas.Dock = DockStyle.Top;
            panelHerramientas.Location = new Point(1, 1);
            panelHerramientas.Name = "panelHerramientas";
            panelHerramientas.Padding = new Padding(10, 8, 10, 4);
            panelHerramientas.Size = new Size(882, 48);
            panelHerramientas.TabIndex = 1;
            // 
            // btnActualizar
            // 
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            btnActualizar.IconColor = Color.Black;
            btnActualizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnActualizar.IconSize = 24;
            btnActualizar.Location = new Point(752, 7);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(115, 32);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Refrescar";
            btnActualizar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnDesactivar
            // 
            btnDesactivar.FlatAppearance.BorderSize = 0;
            btnDesactivar.IconChar = FontAwesome.Sharp.IconChar.UserSlash;
            btnDesactivar.IconColor = Color.Black;
            btnDesactivar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDesactivar.IconSize = 24;
            btnDesactivar.Location = new Point(510, 7);
            btnDesactivar.Name = "btnDesactivar";
            btnDesactivar.Size = new Size(120, 32);
            btnDesactivar.TabIndex = 5;
            btnDesactivar.Text = "Desactivar";
            btnDesactivar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDesactivar.UseVisualStyleBackColor = true;
            btnDesactivar.Click += AlHacerClickEnCambiarEstado;
            // 
            // btnBitacora
            // 
            btnBitacora.FlatAppearance.BorderSize = 0;
            btnBitacora.IconChar = FontAwesome.Sharp.IconChar.BookOpen;
            btnBitacora.IconColor = Color.Black;
            btnBitacora.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBitacora.IconSize = 24;
            btnBitacora.Location = new Point(636, 7);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(110, 32);
            btnBitacora.TabIndex = 4;
            btnBitacora.Text = "Bitácora";
            btnBitacora.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBitacora.UseVisualStyleBackColor = true;
            btnBitacora.Click += AlHacerClickEnBitacora;
            // 
            // btnEditar
            // 
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.IconChar = FontAwesome.Sharp.IconChar.UserPen;
            btnEditar.IconColor = Color.Black;
            btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditar.IconSize = 24;
            btnEditar.Location = new Point(404, 7);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 32);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += AlHacerClickEnEditar;
            // 
            // btnNuevo
            // 
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            btnNuevo.IconColor = Color.Black;
            btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNuevo.IconSize = 24;
            btnNuevo.Location = new Point(298, 7);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 32);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += AlHacerClickEnNuevo;
            // 
            // chkMostrarInactivos
            // 
            chkMostrarInactivos.AutoSize = true;
            chkMostrarInactivos.Location = new Point(200, 14);
            chkMostrarInactivos.Name = "chkMostrarInactivos";
            chkMostrarInactivos.Size = new Size(77, 21);
            chkMostrarInactivos.TabIndex = 1;
            chkMostrarInactivos.Text = "Inactivos";
            chkMostrarInactivos.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(10, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "🔍  Buscar...";
            txtBuscar.Size = new Size(175, 25);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += AlCambiarBusqueda;
            // 
            // panelInferior
            // 
            panelInferior.Controls.Add(lblConteo);
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Location = new Point(1, 510);
            panelInferior.Name = "panelInferior";
            panelInferior.Padding = new Padding(10, 5, 10, 0);
            panelInferior.Size = new Size(882, 30);
            panelInferior.TabIndex = 2;
            // 
            // lblConteo
            // 
            lblConteo.AutoSize = true;
            lblConteo.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblConteo.Location = new Point(10, 5);
            lblConteo.Name = "lblConteo";
            lblConteo.Size = new Size(0, 15);
            lblConteo.TabIndex = 0;
            // 
            // grdNinos
            // 
            grdNinos.AllowUserToAddRows = false;
            grdNinos.AllowUserToDeleteRows = false;
            grdNinos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdNinos.BorderStyle = BorderStyle.None;
            grdNinos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grdNinos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grdNinos.ColumnHeadersHeight = 30;
            grdNinos.Dock = DockStyle.Fill;
            grdNinos.Location = new Point(1, 49);
            grdNinos.MultiSelect = false;
            grdNinos.Name = "grdNinos";
            grdNinos.ReadOnly = true;
            grdNinos.RowHeadersVisible = false;
            grdNinos.RowTemplate.Height = 35;
            grdNinos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdNinos.Size = new Size(882, 461);
            grdNinos.TabIndex = 7;
            grdNinos.CellDoubleClick += AlDobleClickEnFila;
            // 
            // FrmGestionNinos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 541);
            Controls.Add(grdNinos);
            Controls.Add(panelInferior);
            Controls.Add(panelHerramientas);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmGestionNinos";
            Text = "Gestión de Niños";
            Load += FrmGestionNinos_Load;
            panelHerramientas.ResumeLayout(false);
            panelHerramientas.PerformLayout();
            panelInferior.ResumeLayout(false);
            panelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grdNinos).EndInit();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHerramientas;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnBitacora;
        private FontAwesome.Sharp.IconButton btnDesactivar;
        private FontAwesome.Sharp.IconButton btnActualizar;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Label lblConteo;
        private System.Windows.Forms.DataGridView grdNinos;
    }
}
