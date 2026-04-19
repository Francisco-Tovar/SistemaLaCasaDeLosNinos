using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmBitacoraVoluntario : FormBase
    {
        private readonly Voluntario _voluntario;
        private readonly IServicioRegistroHoras _servicioHoras;
        private readonly int _idUsuarioSesion;

        public FrmBitacoraVoluntario(Voluntario voluntario, int idUsuarioSesion, IServicioRegistroHoras servicioHoras, ThemeColors theme)
        {
            InitializeComponent();
            _voluntario = voluntario;
            _idUsuarioSesion = idUsuarioSesion;
            _servicioHoras = servicioHoras;
            _theme = theme;

            lblVoluntario.Text = _voluntario.NombreCompleto;
            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;
        }

        private async void FrmBitacoraVoluntario_Load(object sender, EventArgs e)
        {
            ThemeEngine.ApplyTheme(this, _theme);

            // Estilo para el título del voluntario
            lblVoluntario.ForeColor = _theme.AccentColor;
            
            // Forzar estilo verde "Nuevo" para el botón agregar
            btnAgregar.BackColor = _theme.StatusSuccess;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.IconColor = Color.White;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;

            await CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                var registros = await _servicioHoras.ObtenerPorVoluntarioAsync(_voluntario.Id);
                
                // Ordenar por fecha descendente y luego por ID descendente para los del mismo día
                var lista = registros.OrderByDescending(r => r.Fecha)
                                     .ThenByDescending(r => r.Id)
                                     .ToList();
                
                dgvHoras.DataSource = lista;

                ConfigurarColumnas();
                
                decimal total = await _servicioHoras.ObtenerTotalHorasVoluntarioAsync(_voluntario.Id);
                lblTotal.Text = $"Total Acumulado: {total:0.##} horas";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bitácora: {ex.Message}", "Error");
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvHoras.Columns["Id"] != null) dgvHoras.Columns["Id"].Visible = false;
            if (dgvHoras.Columns["IdVoluntario"] != null) dgvHoras.Columns["IdVoluntario"].Visible = false;
            if (dgvHoras.Columns["IdUsuario"] != null) dgvHoras.Columns["IdUsuario"].Visible = false;

            if (dgvHoras.Columns["Fecha"] != null)
            {
                dgvHoras.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvHoras.Columns["Fecha"].Width = 100;
            }

            if (dgvHoras.Columns["HorasAportadas"] != null)
            {
                dgvHoras.Columns["HorasAportadas"].HeaderText = "Horas";
                dgvHoras.Columns["HorasAportadas"].Width = 80;
            }

            if (dgvHoras.Columns["Descripcion"] != null)
            {
                dgvHoras.Columns["Descripcion"].HeaderText = "Actividad/Descripción";
                dgvHoras.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            using var frm = new FrmRegistroHoras(_voluntario.Id, _idUsuarioSesion, _servicioHoras, _theme);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await CargarDatos();
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvHoras.CurrentRow?.DataBoundItem is RegistroHoras reg)
            {
                var res = MessageBox.Show("¿Desea eliminar este registro de horas?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    await _servicioHoras.EliminarAsync(reg.Id);
                    await CargarDatos();
                }
            }
            else MessageBox.Show("Seleccione un registro para eliminar.", "Aviso");
        }

        private void AlHacerClickEnCerrar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            DragForm();
        }
    }
}
