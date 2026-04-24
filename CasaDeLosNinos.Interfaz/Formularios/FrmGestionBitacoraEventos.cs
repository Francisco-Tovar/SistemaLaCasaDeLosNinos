using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Aplicacion.Servicios;
using FontAwesome.Sharp;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionBitacoraEventos : FormBase
    {
        private readonly IServicioBitacoraEvento _servicioEvento;
        private readonly int _idUsuarioSesion;
        private List<BitacoraEvento> _todosLosEventos = new();

        public FrmGestionBitacoraEventos(
            IServicioBitacoraEvento servicioEvento,
            int idUsuarioSesion,
            ThemeColors theme)
        {
            InitializeComponent();
            _servicioEvento = servicioEvento;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;
            
            ThemeEngine.ApplyTheme(this, _theme);

            ConfigurarEstiloGrilla();
            ConfigurarColumnasGrilla();

            btnActualizar.Click += async (_, _) => await CargarEventosAsync();
        }

        private void ConfigurarEstiloGrilla()
        {
            grdEventos.ColumnHeadersHeight = 45;
            grdEventos.AllowUserToResizeColumns = false;
            grdEventos.AllowUserToResizeRows = false;
            grdEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void ConfigurarColumnasGrilla()
        {
            grdEventos.Columns.Clear();
            grdEventos.AutoGenerateColumns = false;

            grdEventos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colFecha", 
                HeaderText = "Fecha", 
                DataPropertyName = "Fecha", 
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            grdEventos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colTitulo", 
                HeaderText = "Título", 
                DataPropertyName = "Titulo", 
                FillWeight = 30 
            });

            grdEventos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colUsuario", 
                HeaderText = "Usuario", 
                DataPropertyName = "NombreUsuario", 
                FillWeight = 20 
            });

            grdEventos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colDescripcion", 
                HeaderText = "Descripción", 
                DataPropertyName = "Descripcion", 
                FillWeight = 35 
            });

            grdEventos.RowTemplate.Height = 35;
        }

        private async void FrmGestionBitacoraEventos_Load(object sender, EventArgs e)
        {
            await CargarEventosAsync();
        }

        private async Task CargarEventosAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _todosLosEventos = (await _servicioEvento.ObtenerTodosAsync()).ToList();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar eventos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AplicarFiltro()
        {
            var filtro = txtBuscar.Text.Trim().ToLowerInvariant();

            var lista = _todosLosEventos
                .Where(e => string.IsNullOrEmpty(filtro)
                         || e.Titulo.ToLowerInvariant().Contains(filtro)
                         || e.Descripcion.ToLowerInvariant().Contains(filtro)
                         || e.NombreUsuario.ToLowerInvariant().Contains(filtro))
                .ToList();

            grdEventos.DataSource = null;
            grdEventos.DataSource = lista;

            lblConteo.Text = $"Registros: {lista.Count}";
        }

        private void AlCambiarBusqueda(object sender, EventArgs e) => AplicarFiltro();

        private void AlDobleClickEnFila(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AlHacerClickEnEditar(sender, e);
        }

        private BitacoraEvento? ObtenerEventoSeleccionado()
        {
            if (grdEventos.CurrentRow?.DataBoundItem is BitacoraEvento evento)
                return evento;
            return null;
        }

        private async void AlHacerClickEnNuevo(object sender, EventArgs e)
        {
            using var frm = new FrmEdicionBitacoraEvento(null, _servicioEvento, _idUsuarioSesion, _theme);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarEventosAsync();
        }

        private async void AlHacerClickEnEditar(object sender, EventArgs e)
        {
            var evento = ObtenerEventoSeleccionado();
            if (evento == null) return;

            using var frm = new FrmEdicionBitacoraEvento(evento, _servicioEvento, _idUsuarioSesion, _theme);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarEventosAsync();
        }

        private async void AlHacerClickEnEliminar(object sender, EventArgs e)
        {
            var evento = ObtenerEventoSeleccionado();
            if (evento == null) return;

            if (MessageBox.Show($"¿Desea eliminar definitivamente el evento: {evento.Titulo}?", "Confirmar Eliminación", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    await _servicioEvento.EliminarEventoAsync(evento.Id);
                    await CargarEventosAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error");
                }
            }
        }
        
        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            btnNuevo.IconColor = Color.White;
            btnEditar.IconColor = theme.TextPrimary;
            btnEliminar.IconColor = Color.White;
            btnActualizar.IconColor = theme.TextPrimary;
        }
    }
}
