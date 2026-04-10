using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmTomaAsistencia : Form
    {
        private readonly IServicioAsistencia _servicioAsistencia;
        private readonly int                 _idUsuarioActual;

        // Datos en memoria
        private List<NinoAsistenciaDto> _lista = new();

        public FrmTomaAsistencia(IServicioAsistencia servicioAsistencia, int idUsuarioActual)
        {
            InitializeComponent();
            _servicioAsistencia = servicioAsistencia;
            _idUsuarioActual    = idUsuarioActual;

            // Configuraciones visuales customizadas para la grilla
            grdAsistencia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 120, 100);
            grdAsistencia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grdAsistencia.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            grdAsistencia.EnableHeadersVisualStyles = false;
            grdAsistencia.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 248, 244);
            
            ConfigurarColumnasGrilla();
        }

        private void ConfigurarColumnasGrilla()
        {
            var colPresente = new DataGridViewCheckBoxColumn
            {
                Name             = "colPresente",
                HeaderText       = "✔ Presente",
                DataPropertyName = "Presente",
                Width            = 80,
                FillWeight       = 15
            };

            var colNombre = new DataGridViewTextBoxColumn
            {
                Name             = "colNombre",
                HeaderText       = "Nombre del Niño / Niña",
                DataPropertyName = "NombreCompleto",
                ReadOnly         = true,
                FillWeight       = 85
            };

            grdAsistencia.Columns.AddRange(colPresente, colNombre);
        }

        private async void FrmTomaAsistencia_Load(object sender, EventArgs e)
        {
            dtpFecha.MaxDate = DateTime.Today;
            dtpFecha.Value   = DateTime.Today;
            await CargarAsistenciaAsync();
        }

        // ══════════════════════════════════════════════════════════════
        // LÓGICA DE DATOS
        // ══════════════════════════════════════════════════════════════

        private async Task CargarAsistenciaAsync()
        {
            btnGuardar.Enabled = false;
            lblEstado.Text     = "Cargando...";
            lblResumen.Text    = string.Empty;

            try
            {
                var fecha = dtpFecha.Value.Date;
                _lista = (await _servicioAsistencia.ObtenerNinosParaAsistenciaAsync(fecha)).ToList();

                grdAsistencia.DataSource = null;
                grdAsistencia.DataSource = _lista;

                btnGuardar.Enabled = _lista.Count > 0;
                ActualizarResumen();
                lblEstado.Text = _lista.Count > 0
                    ? $"Cargados {_lista.Count} niños activos para el {fecha:dd/MM/yyyy}"
                    : "No hay niños activos registrados en el sistema.";
            }
            catch (Exception ex)
            {
                lblEstado.Text = $"Error al cargar: {ex.Message}";
                MessageBox.Show($"Error al cargar la asistencia:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen()
        {
            int total    = _lista.Count;
            int presentes = _lista.Count(d => d.Presente);
            lblResumen.Text = $"Presentes: {presentes} / {total}   |   Ausentes: {total - presentes}";
        }

        private void CambiarTodos(bool estado)
        {
            foreach (var dto in _lista)
                dto.Presente = estado;

            // Refrescar grilla
            grdAsistencia.DataSource = null;
            grdAsistencia.DataSource = _lista;
            ActualizarResumen();
        }

        // ══════════════════════════════════════════════════════════════
        // MANEJADORES DE EVENTOS
        // ══════════════════════════════════════════════════════════════

        private async void AlHacerClickEnCargar(object sender, EventArgs e)
            => await CargarAsistenciaAsync();

        private void AlHacerClickEnMarcarTodos(object sender, EventArgs e)
            => CambiarTodos(true);

        private void AlHacerClickEnDesmarcarTodos(object sender, EventArgs e)
            => CambiarTodos(false);

        private void grdAsistencia_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdAsistencia.IsCurrentCellDirty)
                grdAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void AlCambiarCelda(object sender, DataGridViewCellEventArgs e)
        {
            // Sincronizar el cambio de checkbox con el DTO subyacente
            if (e.ColumnIndex == grdAsistencia.Columns["colPresente"]?.Index && e.RowIndex >= 0)
            {
                var celda = grdAsistencia.Rows[e.RowIndex].Cells["colPresente"];
                if (e.RowIndex < _lista.Count)
                    _lista[e.RowIndex].Presente = (bool)(celda.Value ?? false);

                ActualizarResumen();
            }
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            lblEstado.Text     = "Guardando asistencia...";

            try
            {
                var fecha = dtpFecha.Value.Date;
                var (exito, mensaje) = await _servicioAsistencia.GuardarAsistenciaAsync(
                    fecha, _lista, _idUsuarioActual);

                if (exito)
                {
                    lblEstado.ForeColor = Color.FromArgb(22, 120, 60);
                    lblEstado.Text      = $"✔  {mensaje}";
                    MessageBox.Show(mensaje, "Asistencia Guardada",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblEstado.ForeColor = Color.Firebrick;
                    lblEstado.Text      = $"✘  {mensaje}";
                }
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = Color.Firebrick;
                lblEstado.Text      = $"Error: {ex.Message}";
                MessageBox.Show($"Error al guardar la asistencia:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = _lista.Count > 0;
            }
        }
    }
}
