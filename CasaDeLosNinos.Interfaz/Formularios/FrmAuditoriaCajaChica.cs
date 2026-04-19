using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmAuditoriaCajaChica : FormBase
    {
        private readonly int _anio;
        private readonly int _mes;
        private readonly IServicioCajaChica _servicioCajaChica;

        public FrmAuditoriaCajaChica(int anio, int mes, IServicioCajaChica servicioCajaChica, ThemeColors theme)
        {
            InitializeComponent();
            _anio = anio;
            _mes = mes;
            _servicioCajaChica = servicioCajaChica;
            _theme = theme;

            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            // PROCEDIMIENTO ESPEJO (Igual a FrmEdicionVoluntario)
            ConfigurarGrilla();
            ConfigurarAreaCabecera();

            ThemeEngine.ApplyTheme(this, _theme);
            VincularEventos();
        }

        private void ConfigurarGrilla()
        {
            grdAuditoria.AutoGenerateColumns = false;
            grdAuditoria.Columns.Clear();

            // Estética Premium
            grdAuditoria.AllowUserToResizeColumns = false;
            grdAuditoria.AllowUserToResizeRows = false;
            grdAuditoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            grdAuditoria.ColumnHeadersHeight = 45;
            grdAuditoria.RowTemplate.Height = 45; // Más alto para detalles multilínea
            grdAuditoria.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            grdAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFecha",
                HeaderText = "Fecha/Hora",
                DataPropertyName = "FechaHoraCambio",
                Width = 120,
                DefaultCellStyle = { Format = "dd/MM HH:mm" }
            });

            grdAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUsuario",
                HeaderText = "Usuario",
                DataPropertyName = "Usuario",
                Width = 100
            });

            grdAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colConcepto",
                HeaderText = "Concepto",
                DataPropertyName = "ConceptoOriginal",
                Width = 150
            });

            grdAuditoria.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDetalles",
                HeaderText = "Cambios Realizados",
                DataPropertyName = "DetallesDelCambio",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void ConfigurarAreaCabecera()
        {
            // Panel Decorativo debajo de la cabecera
            var pnlInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = _theme.HeaderBackground,
                Padding = new Padding(15, 10, 15, 10),
                Name = "pnlHeaderInfo"
            };
            this.Controls.Add(pnlInfo);

            // Garantizar orden de apilado Dock:
            // 1. Cabecera (Top)
            // 2. Info (Top)
            // 3. Grid (Fill - al frente de los otros para que herede el resto)
            panelCabecera.SendToBack();
            pnlInfo.BringToFront();
            grdAuditoria.BringToFront();

            var icnStats = new FontAwesome.Sharp.IconPictureBox
            {
                IconChar = FontAwesome.Sharp.IconChar.ChartLine,
                IconColor = _theme.AccentColor,
                IconSize = 40,
                Size = new Size(40, 40),
                Location = new Point(20, 10),
                BackColor = Color.Transparent
            };
            pnlInfo.Controls.Add(icnStats);

            var lblSubtitle = new Label
            {
                Text = $"FISCALIZACIÓN DEL PERÍODO: {_mes:D2}/{_anio}",
                ForeColor = _theme.TextSecondary,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(70, 18),
                AutoSize = true
            };
            pnlInfo.Controls.Add(lblSubtitle);
        }

        private void VincularEventos()
        {
            this.Load += AlCargarFormulario;
        }

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            DragForm();
        }

        private async void AlCargarFormulario(object? sender, EventArgs e)
        {
            try
            {
                grdAuditoria.AutoGenerateColumns = false;
                var historial = await _servicioCajaChica.ObtenerAuditoriaMensualAsync(_anio, _mes);
                grdAuditoria.DataSource = historial.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlHacerClickEnCerrar(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
