using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;
using FontAwesome.Sharp;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmMantenimiento : FormBase
    {
        private readonly IInicializadorBaseDatos _inicializador;
        private readonly IServicioReporte _servicioReporte;

        public FrmMantenimiento(IInicializadorBaseDatos inicializador, IServicioReporte servicioReporte, ThemeColors theme)
        {
            InitializeComponent();
            _inicializador = inicializador;
            _servicioReporte = servicioReporte;
            this.Text = "Mantenimiento y Seguridad";
            this.EsRedimensionable = false;
            RefreshTheme(theme);
        }

        private void InitializeComponent()
        {
            var pnlMain = new Panel();
            var lblTitulo = new Label();
            
            // Sección Respaldo
            var grpRespaldo = new GroupBox();
            var btnRespaldo = new IconButton();
            var lblInfoRespaldo = new Label();

            // Sección Restauración
            var grpRestaurar = new GroupBox();
            var btnImportar = new IconButton();
            var lblInfoImportar = new Label();

            // Sección Peligro (Reinicio)
            var grpPeligro = new GroupBox();
            var btnReset = new IconButton();
            var lblInfoReset = new Label();

            // ────────────────────────────────────────────────────────
            // Layout General
            // ────────────────────────────────────────────────────────
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Padding = new Padding(30);

            lblTitulo.Text = "Mantenimiento y Seguridad del Sistema";
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.Goldenrod;
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 50;

            // ────────────────────────────────────────────────────────
            // Sección: RESPALDO
            // ────────────────────────────────────────────────────────
            grpRespaldo.Text = "Copia de Seguridad";
            grpRespaldo.Dock = DockStyle.Top;
            grpRespaldo.Height = 120;
            grpRespaldo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            btnRespaldo.Text = "Respaldar Base de Datos";
            btnRespaldo.IconChar = IconChar.Database;
            btnRespaldo.IconColor = Color.Black;
            btnRespaldo.IconSize = 28;
            btnRespaldo.FlatStyle = FlatStyle.Flat;
            btnRespaldo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRespaldo.Size = new Size(220, 50);
            btnRespaldo.Location = new Point(20, 40);
            btnRespaldo.Cursor = Cursors.Hand;
            btnRespaldo.Click += BtnRespaldo_Click;

            lblInfoRespaldo.Text = "Genera un archivo comprimido (.zip) con la base de datos y todos los archivos del sistema.";
            lblInfoRespaldo.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblInfoRespaldo.Location = new Point(250, 45);
            lblInfoRespaldo.Size = new Size(450, 60);

            grpRespaldo.Controls.Add(btnRespaldo);
            grpRespaldo.Controls.Add(lblInfoRespaldo);

            // ────────────────────────────────────────────────────────
            // Sección: RESTAURACIÓN
            // ────────────────────────────────────────────────────────
            grpRestaurar.Text = "Restauración de Datos";
            grpRestaurar.Dock = DockStyle.Top;
            grpRestaurar.Height = 120;
            grpRestaurar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            btnImportar.Text = "Importar Respaldo";
            btnImportar.IconChar = IconChar.FileUpload;
            btnImportar.IconColor = Color.Black;
            btnImportar.IconSize = 28;
            btnImportar.FlatStyle = FlatStyle.Flat;
            btnImportar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportar.Size = new Size(220, 50);
            btnImportar.Location = new Point(20, 40);
            btnImportar.Cursor = Cursors.Hand;
            btnImportar.Click += BtnImportar_Click;

            lblInfoImportar.Text = "IMPORTANTE: Sobreescribe los datos actuales. Use únicamente para recuperar el sistema ante una falla.";
            lblInfoImportar.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblInfoImportar.Location = new Point(250, 45);
            lblInfoImportar.Size = new Size(450, 60);

            grpRestaurar.Controls.Add(btnImportar);
            grpRestaurar.Controls.Add(lblInfoImportar);

            // ────────────────────────────────────────────────────────
            // Sección: PELIGRO (REINICIO)
            // ────────────────────────────────────────────────────────
            grpPeligro.Text = "Zona de Peligro";
            grpPeligro.Dock = DockStyle.Top;
            grpPeligro.Height = 120;
            grpPeligro.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpPeligro.ForeColor = Color.Firebrick;

            btnReset.Text = "REINICIAR TODO";
            btnReset.IconChar = IconChar.ExclamationTriangle;
            btnReset.IconColor = Color.White;
            btnReset.IconSize = 28;
            btnReset.BackColor = Color.Firebrick;
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReset.Size = new Size(220, 50);
            btnReset.Location = new Point(20, 40);
            btnReset.Cursor = Cursors.Hand;
            btnReset.Click += BtnReset_Click;

            lblInfoReset.Text = "Borra todos los registros (Niños, Asistencia, Caja Chica, etc.) excepto su cuenta de Administrador. Esta acción NO se puede deshacer.";
            lblInfoReset.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblInfoReset.ForeColor = Color.Firebrick;
            lblInfoReset.Location = new Point(250, 45);
            lblInfoReset.Size = new Size(450, 60);

            grpPeligro.Controls.Add(btnReset);
            grpPeligro.Controls.Add(lblInfoReset);

            // Armado
            pnlMain.Controls.Add(grpPeligro);
            pnlMain.Controls.Add(new Panel { Height = 10, Dock = DockStyle.Top }); // Espaciador
            pnlMain.Controls.Add(grpRestaurar);
            pnlMain.Controls.Add(new Panel { Height = 10, Dock = DockStyle.Top }); // Espaciador
            pnlMain.Controls.Add(grpRespaldo);
            pnlMain.Controls.Add(lblTitulo);

            this.Controls.Add(pnlMain);
            this.Size = new Size(928, 535);
        }

        private async void BtnRespaldo_Click(object? sender, EventArgs e)
        {
            await RealizarRespaldoAsync();
        }

        private async void BtnImportar_Click(object? sender, EventArgs e)
        {
            await RealizarRestauracionAsync();
        }

        private async Task RealizarRespaldoAsync()
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "Seleccione la carpeta donde desea guardar el respaldo.";

            if (fbd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                await _servicioReporte.RespaldarSistemaFullAsync(fbd.SelectedPath);
                Cursor = Cursors.Default;

                MessageBox.Show("Respaldo completado exitosamente.\n\nSe ha generado un archivo .zip con la base de datos y archivos críticos.", "Respaldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al realizar el respaldo: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RealizarRestauracionAsync()
        {
            var confirm = MessageBox.Show("¡ADVERTENCIA CRÍTICA!\n\nAl importar un respaldo se SOBREESCRIBIRÁN todos los datos actuales del sistema. Esta acción no se puede deshacer.\n\n¿Desea continuar?", "Advertencia de Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            using var ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar archivo de respaldo (.zip)";
            ofd.Filter = "Respaldo del Sistema (*.zip)|*.zip";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                await _servicioReporte.RestaurarSistemaFullAsync(ofd.FileName);
                Cursor = Cursors.Default;

                MessageBox.Show("Restauración completada.\n\nEl sistema se ha restablecido a partir del respaldo seleccionado. El sistema se cerrará para aplicar los cambios.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al restaurar el respaldo: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnReset_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¡ADVERTENCIA CRÍTICA!\n\nEsta acción borrará todos los registros de beneficiarios, actividad y economía.\n\n¿Desea continuar?",
                "CONFIRMACIÓN DE SEGURIDAD",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm == DialogResult.Yes)
            {
                var confirm2 = MessageBox.Show("¿Está COMPLETAMENTE seguro?", "ÚLTIMA ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (confirm2 == DialogResult.Yes)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        await _inicializador.ReiniciarBaseDatosAsync();
                        Cursor = Cursors.Default;

                        MessageBox.Show("La base de datos ha sido reiniciada.\nEl sistema se cerrará.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show($"Error: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            
            // Buscar controles y aplicar colores del tema
            foreach (Control c in this.Controls)
            {
                AplicarTemaRecursivo(c, theme);
            }
        }

        private void AplicarTemaRecursivo(Control parent, ThemeColors theme)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is GroupBox grp)
                {
                    grp.ForeColor = (grp.Text == "Zona de Peligro") ? Color.Firebrick : theme.TextPrimary;
                    AplicarTemaRecursivo(grp, theme);
                }
                else if (c is IconButton btn)
                {
                    if (btn.Text == "REINICIAR TODO")
                    {
                        btn.ForeColor = theme.AccentColor;
                        btn.IconColor = theme.AccentColor;
                    }
                    else
                    {
                        btn.IconColor = theme.AccentColor;
                        btn.ForeColor = theme.TextPrimary;
                    }
                }
                else if (c is Label lbl)
                {
                    if (lbl.ForeColor != Color.Firebrick && lbl.Font.Style != FontStyle.Italic)
                    {
                        lbl.ForeColor = theme.TextPrimary;
                    }
                }
                
                if (c.HasChildren) AplicarTemaRecursivo(c, theme);
            }
        }
    }
}
