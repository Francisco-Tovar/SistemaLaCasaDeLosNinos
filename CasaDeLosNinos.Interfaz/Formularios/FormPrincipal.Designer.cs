namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FormPrincipal
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
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.mnuBeneficiarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGestionNinos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActividades = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAsistencia = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblOrg = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.menuPrincipal.SuspendLayout();
            this.panelCentral.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.menuPrincipal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuPrincipal.ForeColor = System.Drawing.Color.White;
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBeneficiarios,
            this.mnuActividades});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(984, 25);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // mnuBeneficiarios
            // 
            this.mnuBeneficiarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGestionNinos});
            this.mnuBeneficiarios.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mnuBeneficiarios.ForeColor = System.Drawing.Color.White;
            this.mnuBeneficiarios.Name = "mnuBeneficiarios";
            this.mnuBeneficiarios.Size = new System.Drawing.Size(126, 21);
            this.mnuBeneficiarios.Text = "👶  Beneficiarios";
            // 
            // mnuGestionNinos
            // 
            this.mnuGestionNinos.Name = "mnuGestionNinos";
            this.mnuGestionNinos.Size = new System.Drawing.Size(193, 22);
            this.mnuGestionNinos.Text = "Gestión de Niños...";
            this.mnuGestionNinos.Click += new System.EventHandler(this.AlAbrirGestionNinos);
            // 
            // mnuActividades
            // 
            this.mnuActividades.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAsistencia});
            this.mnuActividades.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mnuActividades.ForeColor = System.Drawing.Color.White;
            this.mnuActividades.Name = "mnuActividades";
            this.mnuActividades.Size = new System.Drawing.Size(117, 21);
            this.mnuActividades.Text = "📋  Actividades";
            // 
            // mnuAsistencia
            // 
            this.mnuAsistencia.Name = "mnuAsistencia";
            this.mnuAsistencia.Size = new System.Drawing.Size(210, 22);
            this.mnuAsistencia.Text = "Toma de Asistencia...";
            this.mnuAsistencia.Click += new System.EventHandler(this.AlAbrirTomaAsistencia);
            // 
            // panelCentral
            // 
            this.panelCentral.Controls.Add(this.lblBienvenida);
            this.panelCentral.Controls.Add(this.lblOrg);
            this.panelCentral.Controls.Add(this.lblHint);
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Location = new System.Drawing.Point(0, 25);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.panelCentral.Size = new System.Drawing.Size(984, 596);
            this.panelCentral.TabIndex = 1;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(160)))));
            this.lblBienvenida.Location = new System.Drawing.Point(415, 60);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(134, 31);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrg
            // 
            this.lblOrg.AutoSize = true;
            this.lblOrg.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblOrg.Location = new System.Drawing.Point(415, 107);
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Size = new System.Drawing.Size(117, 20);
            this.lblOrg.TabIndex = 1;
            this.lblOrg.Text = "La Casa de los Niños";
            this.lblOrg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(160)))));
            this.lblHint.Location = new System.Drawing.Point(415, 147);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(376, 15);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "Use el menú superior para navegar entre los módulos del sistema.";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(984, 621);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.menuPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menuPrincipal;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "La Casa de los Niños";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.Resize += new System.EventHandler(this.FormPrincipal_Resize);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.panelCentral.ResumeLayout(false);
            this.panelCentral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem mnuBeneficiarios;
        private System.Windows.Forms.ToolStripMenuItem mnuGestionNinos;
        private System.Windows.Forms.ToolStripMenuItem mnuActividades;
        private System.Windows.Forms.ToolStripMenuItem mnuAsistencia;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.Label lblHint;
    }
}
