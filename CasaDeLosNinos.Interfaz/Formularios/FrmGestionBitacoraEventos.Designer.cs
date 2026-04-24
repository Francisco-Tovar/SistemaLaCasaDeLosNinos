namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmGestionBitacoraEventos
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
            btnEliminar = new FontAwesome.Sharp.IconButton();
            btnEditar = new FontAwesome.Sharp.IconButton();
            btnNuevo = new FontAwesome.Sharp.IconButton();
            txtBuscar = new TextBox();
            panelInferior = new Panel();
            lblConteo = new Label();
            grdEventos = new DataGridView();
            panelHerramientas.SuspendLayout();
            panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdEventos).BeginInit();
            SuspendLayout();
            // 
            // panelHerramientas
            // 
            panelHerramientas.Controls.Add(btnActualizar);
            panelHerramientas.Controls.Add(btnEliminar);
            panelHerramientas.Controls.Add(btnEditar);
            panelHerramientas.Controls.Add(btnNuevo);
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
            // btnEliminar
            // 
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnEliminar.IconColor = Color.Black;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 24;
            btnEliminar.Location = new Point(460, 7);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 32);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += AlHacerClickEnEliminar;
            // 
            // btnEditar
            // 
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.IconChar = FontAwesome.Sharp.IconChar.Edit;
            btnEditar.IconColor = Color.Black;
            btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditar.IconSize = 24;
            btnEditar.Location = new Point(354, 7);
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
            btnNuevo.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnNuevo.IconColor = Color.Black;
            btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNuevo.IconSize = 24;
            btnNuevo.Location = new Point(248, 7);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(100, 32);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nuevo";
            btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += AlHacerClickEnNuevo;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(10, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "🔍  Buscar eventos...";
            txtBuscar.Size = new Size(220, 25);
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
            // grdEventos
            // 
            grdEventos.AllowUserToAddRows = false;
            grdEventos.AllowUserToDeleteRows = false;
            grdEventos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdEventos.BorderStyle = BorderStyle.None;
            grdEventos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grdEventos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grdEventos.ColumnHeadersHeight = 30;
            grdEventos.Dock = DockStyle.Fill;
            grdEventos.Location = new Point(1, 49);
            grdEventos.MultiSelect = false;
            grdEventos.Name = "grdEventos";
            grdEventos.ReadOnly = true;
            grdEventos.RowHeadersVisible = false;
            grdEventos.RowTemplate.Height = 35;
            grdEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdEventos.Size = new Size(882, 461);
            grdEventos.TabIndex = 7;
            grdEventos.CellDoubleClick += AlDobleClickEnFila;
            // 
            // FrmGestionBitacoraEventos
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 541);
            Controls.Add(grdEventos);
            Controls.Add(panelInferior);
            Controls.Add(panelHerramientas);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmGestionBitacoraEventos";
            Text = "Bitácora de Eventos";
            Load += FrmGestionBitacoraEventos_Load;
            panelHerramientas.ResumeLayout(false);
            panelHerramientas.PerformLayout();
            panelInferior.ResumeLayout(false);
            panelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grdEventos).EndInit();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHerramientas;
        private System.Windows.Forms.TextBox txtBuscar;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnActualizar;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Label lblConteo;
        private System.Windows.Forms.DataGridView grdEventos;
    }
}
