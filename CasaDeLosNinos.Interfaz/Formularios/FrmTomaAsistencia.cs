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
        private readonly int _idUsuarioSesion;
        private List<NinoAsistenciaDto> _detalles = new();

        public FrmTomaAsistencia(IServicioAsistencia servicioAsistencia, int idUsuarioSesion)
        {
            InitializeComponent();
            _servicioAsistencia = servicioAsistencia;
            _idUsuarioSesion = idUsuarioSesion;

            ConfigurarEstiloGrilla();
            ConfigurarColumnasGrilla();
        }

        private void ConfigurarEstiloGrilla()
        {
            // El estilo base es manejado por ThemeEngine.
            grdAsistencia.ColumnHeadersHeight = 35;
        }

        private void ConfigurarColumnasGrilla()
        {
            grdAsistencia.Columns.Clear();
            grdAsistencia.AutoGenerateColumns = false;

            grdAsistencia.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCompleto", HeaderText = "Niño", Width = 250, ReadOnly = true });
            grdAsistencia.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "Presente", HeaderText = "Presente", Width = 80 });
            grdAsistencia.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Observacion", HeaderText = "Observación", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            grdAsistencia.CellValueChanged += AlCambiarCelda;
            grdAsistencia.CurrentCellDirtyStateChanged += (_, _) => { if (grdAsistencia.IsCurrentCellDirty) grdAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit); };
        }

        private async void FrmTomaAsistencia_Load(object sender, EventArgs e) => await CargarDatosAsync();

        private async void AlHacerClickEnCargar(object sender, EventArgs e) => await CargarDatosAsync();

        private async Task CargarDatosAsync()
        {
            try
            {
                var fecha = dtpFecha.Value.Date;
                var lista = await _servicioAsistencia.ObtenerNinosParaAsistenciaAsync(fecha);
                _detalles = lista.OrderBy(n => n.NombreCompleto).ToList();

                grdAsistencia.DataSource = null;
                grdAsistencia.DataSource = _detalles;

                btnGuardar.Enabled = true;
                lblEstado.Text = "📝 Hoja de asistencia cargada.";
                ActualizarResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar:\n{ex.Message}", "Error");
            }
        }

        private void ActualizarResumen()
        {
            var presentes = _detalles.Count(d => d.Presente);
            lblResumen.Text = $"Presentes: {presentes} / Total: {_detalles.Count}";
        }

        private void AlCambiarCelda(object? sender, DataGridViewCellEventArgs e) => ActualizarResumen();

        private void AlHacerClickEnMarcarTodos(object sender, EventArgs e) => CambiarEstadoTodos(true);
        private void AlHacerClickEnDesmarcarTodos(object sender, EventArgs e) => CambiarEstadoTodos(false);

        private void CambiarEstadoTodos(bool estado)
        {
            foreach (var d in _detalles) d.Presente = estado;
            grdAsistencia.Refresh();
            ActualizarResumen();
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            try
            {
                var f = dtpFecha.Value.Date;
                var (exito, mensaje) = await _servicioAsistencia.GuardarAsistenciaAsync(f, _detalles, _idUsuarioSesion);
                
                if (exito)
                {
                    MessageBox.Show(mensaje, "Éxito");
                    await CargarDatosAsync();
                }
                else
                {
                    MessageBox.Show(mensaje, "Validación");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}", "Error");
            }
        }
    }
}
