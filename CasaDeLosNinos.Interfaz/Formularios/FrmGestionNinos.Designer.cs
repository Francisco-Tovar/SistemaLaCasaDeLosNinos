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
            this.panelHerramientas = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.chkMostrarInactivos = new System.Windows.Forms.CheckBox();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.btnEditar = new FontAwesome.Sharp.IconButton();
            this.btnBitacora = new FontAwesome.Sharp.IconButton();
            this.btnEstado = new FontAwesome.Sharp.IconButton();
            this.btnActualizar = new FontAwesome.Sharp.IconButton();
            this.panelInferior = new System.Windows.Forms.Panel();
            this.lblConteo = new System.Windows.Forms.Label();
            this.grdNinos = new System.Windows.Forms.DataGridView();
            this.panelHerramientas.SuspendLayout();
            this.panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNinos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHerramientas
            // 
            this.panelHerramientas.Controls.Add(this.btnActualizar);
            this.panelHerramientas.Controls.Add(this.btnEstado);
            this.panelHerramientas.Controls.Add(this.btnBitacora);
            this.panelHerramientas.Controls.Add(this.btnEditar);
            this.panelHerramientas.Controls.Add(this.btnNuevo);
            this.panelHerramientas.Controls.Add(this.chkMostrarInactivos);
            this.panelHerramientas.Controls.Add(this.txtBuscar);
            this.panelHerramientas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHerramientas.Location = new System.Drawing.Point(0, 0);
            this.panelHerramientas.Name = "panelHerramientas";
            this.panelHerramientas.Padding = new System.Windows.Forms.Padding(10, 8, 10, 4);
            this.panelHerramientas.Size = new System.Drawing.Size(884, 50);
            this.panelHerramientas.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Location = new System.Drawing.Point(10, 12);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "🔍  Buscar...";
            this.txtBuscar.Size = new System.Drawing.Size(180, 24);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.AlCambiarBusqueda);
            // 
            // chkMostrarInactivos
            // 
            this.chkMostrarInactivos.AutoSize = true;
            this.chkMostrarInactivos.Location = new System.Drawing.Point(200, 14);
            this.chkMostrarInactivos.Name = "chkMostrarInactivos";
            this.chkMostrarInactivos.Size = new System.Drawing.Size(78, 21);
            this.chkMostrarInactivos.TabIndex = 1;
            this.chkMostrarInactivos.Text = "Inactivos";
            this.chkMostrarInactivos.UseVisualStyleBackColor = true;
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevo.IconSize = 24;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(310, 6);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 36);
            this.btnNuevo.TabIndex = 2;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.AlHacerClickEnNuevo);
            // 
            // btnEditar
            // 
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
            this.btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEditar.IconSize = 24;
            this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.Location = new System.Drawing.Point(415, 6);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 36);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.AlHacerClickEnEditar);
            // 
            // btnBitacora
            // 
            this.btnBitacora.FlatAppearance.BorderSize = 0;
            this.btnBitacora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBitacora.IconChar = FontAwesome.Sharp.IconChar.BookOpen;
            this.btnBitacora.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBitacora.IconSize = 24;
            this.btnBitacora.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBitacora.Location = new System.Drawing.Point(520, 6);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(110, 36);
            this.btnBitacora.TabIndex = 4;
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBitacora.UseVisualStyleBackColor = true;
            this.btnBitacora.Click += new System.EventHandler(this.AlHacerClickEnBitacora);
            // 
            // btnEstado
            // 
            this.btnEstado.FlatAppearance.BorderSize = 0;
            this.btnEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstado.IconChar = FontAwesome.Sharp.IconChar.Ban;
            this.btnEstado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEstado.IconSize = 24;
            this.btnEstado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstado.Location = new System.Drawing.Point(635, 6);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(120, 36);
            this.btnEstado.TabIndex = 5;
            this.btnEstado.Text = "Desactivar";
            this.btnEstado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.AlHacerClickEnCambiarEstado);
            // 
            // btnActualizar
            // 
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            this.btnActualizar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnActualizar.IconSize = 24;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(760, 6);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(115, 36);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Refrescar";
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // panelInferior
            // 
            this.panelInferior.Controls.Add(this.lblConteo);
            this.panelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInferior.Location = new System.Drawing.Point(0, 511);
            this.panelInferior.Name = "panelInferior";
            this.panelInferior.Padding = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.panelInferior.Size = new System.Drawing.Size(884, 30);
            this.panelInferior.TabIndex = 2;
            // 
            // lblConteo
            // 
            this.lblConteo.AutoSize = true;
            this.lblConteo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblConteo.Location = new System.Drawing.Point(10, 5);
            this.lblConteo.Name = "lblConteo";
            this.lblConteo.Size = new System.Drawing.Size(0, 15);
            this.lblConteo.TabIndex = 0;
            // 
            // grdNinos
            // 
            this.grdNinos.AllowUserToAddRows = false;
            this.grdNinos.AllowUserToDeleteRows = false;
            this.grdNinos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdNinos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdNinos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdNinos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdNinos.ColumnHeadersHeight = 30;
            this.grdNinos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNinos.Location = new System.Drawing.Point(0, 50);
            this.grdNinos.MultiSelect = false;
            this.grdNinos.Name = "grdNinos";
            this.grdNinos.ReadOnly = true;
            this.grdNinos.RowHeadersVisible = false;
            this.grdNinos.RowTemplate.Height = 35;
            this.grdNinos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdNinos.Size = new System.Drawing.Size(884, 461);
            this.grdNinos.TabIndex = 7;
            this.grdNinos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlDobleClickEnFila);
            // 
            // FrmGestionNinos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 541);
            this.Controls.Add(this.grdNinos);
            this.Controls.Add(this.panelInferior);
            this.Controls.Add(this.panelHerramientas);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmGestionNinos";
            this.Text = "Gestión de Niños";
            this.Load += new System.EventHandler(this.FrmGestionNinos_Load);
            this.panelHerramientas.ResumeLayout(false);
            this.panelHerramientas.PerformLayout();
            this.panelInferior.ResumeLayout(false);
            this.panelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNinos)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHerramientas;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkMostrarInactivos;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnBitacora;
        private FontAwesome.Sharp.IconButton btnEstado;
        private FontAwesome.Sharp.IconButton btnActualizar;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Label lblConteo;
        private System.Windows.Forms.DataGridView grdNinos;
    }
}
