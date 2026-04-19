using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionCajaChica : FormBase
    {
        private readonly IServicioCajaChica _servicioCajaChica;
        private readonly IServicioFoto _servicioFoto;
        private readonly int _idUsuarioSesion;
        private List<CajaChica> _movimientos = new();

        public FrmGestionCajaChica(IServicioCajaChica servicioCajaChica, IServicioFoto servicioFoto, int idUsuarioSesion, ThemeColors theme)
        {
            InitializeComponent();
            _servicioCajaChica = servicioCajaChica;
            _servicioFoto = servicioFoto;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;
            
            ThemeEngine.ApplyTheme(this, _theme);
            
            // Colores semánticos requeridos saltando el predeterminado del ThemeEngine
            btnNuevoIngreso.BackColor = _theme.StatusSuccess;
            btnNuevoIngreso.FlatAppearance.MouseOverBackColor = Color.FromArgb(Math.Min(255, _theme.StatusSuccess.R + 20), Math.Min(255, _theme.StatusSuccess.G + 20), Math.Min(255, _theme.StatusSuccess.B + 20));
            // Forzamos rojo indio puro, ignoramos el tema violeta para que resalte claramente.
            btnNuevoEgreso.BackColor = Color.IndianRed;
            btnNuevoEgreso.FlatAppearance.MouseOverBackColor = Color.LightCoral;
            
            ConfigurarListasFiltro();
            ConfigurarGrilla();
            VincularEventos();
        }

        private void VincularEventos()
        {
            this.Load += AlCargarFormulario;
            cboMes.SelectedIndexChanged += AlCambiarPeriodo;
            cboAnio.SelectedIndexChanged += AlCambiarPeriodo;
            btnNuevoIngreso.Click += AlHacerClickEnIngreso;
            btnNuevoEgreso.Click += AlHacerClickEnEgreso;
            btnAuditoria.Click += AlHacerClickEnAuditoria;
            grdMovimientos.CellDoubleClick += AlDobleClickEnCelda;
            grdMovimientos.SelectionChanged += AlCambiarSeleccion;
        }
        
        public override void RefreshTheme(Estilos.ThemeColors theme)
        {
            base.RefreshTheme(theme);
            
            // Reaplicar colores quemados para sobrevivir al motor central
            btnNuevoIngreso.BackColor = _theme.StatusSuccess;
            btnNuevoIngreso.FlatAppearance.MouseOverBackColor = Color.FromArgb(Math.Min(255, _theme.StatusSuccess.R + 20), Math.Min(255, _theme.StatusSuccess.G + 20), Math.Min(255, _theme.StatusSuccess.B + 20));
            // Forzamos rojo indio puro interdimensionalmente
            btnNuevoEgreso.BackColor = Color.IndianRed;
            btnNuevoEgreso.FlatAppearance.MouseOverBackColor = Color.LightCoral;
        }

        private void ConfigurarListasFiltro()
        {
            var meses = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            
            foreach (var m in meses)
                cboMes.Items.Add(m);
                
            int anioActual = DateTime.Today.Year;
            for (int i = anioActual - 5; i <= anioActual + 1; i++)
                cboAnio.Items.Add(i.ToString());

            cboMes.SelectedIndex = DateTime.Today.Month - 1;
            cboAnio.SelectedItem = anioActual.ToString();
        }

        private void ConfigurarGrilla()
        {
            grdMovimientos.AutoGenerateColumns = false;
            grdMovimientos.Columns.Clear();
            
            // Estándar premium
            grdMovimientos.ColumnHeadersHeight = 45;
            grdMovimientos.RowTemplate.Height = 35;
            grdMovimientos.AllowUserToResizeColumns = false;
            grdMovimientos.AllowUserToResizeRows = false;
            grdMovimientos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grdMovimientos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colId", HeaderText = "ID", DataPropertyName = "Id", Width = 50, Visible = false
            });
            
            grdMovimientos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colFecha", HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 100 
            });
            
            grdMovimientos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colConcepto", HeaderText = "Concepto/Detalle", DataPropertyName = "Concepto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill 
            });
            
            grdMovimientos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colIngreso", HeaderText = "Ingreso", Width = 120 
            });
            
            grdMovimientos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colEgreso", HeaderText = "Egreso", Width = 120 
            });

            grdMovimientos.CellFormatting += AlFormatearCelda;
        }

        private async void AlCargarFormulario(object? sender, EventArgs e)
        {
            await CargarDatosAsync();
        }

        private async void AlCambiarPeriodo(object? sender, EventArgs e)
        {
            if (cboMes.SelectedIndex == -1 || cboAnio.SelectedIndex == -1) return;
            await CargarDatosAsync();
        }

        private async Task CargarDatosAsync()
        {
            int mes = cboMes.SelectedIndex + 1;
            int anio = int.Parse(cboAnio.SelectedItem!.ToString()!);

            var registros = await _servicioCajaChica.ObtenerPorMesAsync(anio, mes);
            _movimientos.Clear();
            
            // Reemplazo de LINQ por foreach según política
            foreach (var r in registros)
            {
                _movimientos.Add(r);
            }

            grdMovimientos.DataSource = null;
            grdMovimientos.DataSource = _movimientos;

            var saldo = await _servicioCajaChica.ObtenerSaldoMensualAsync(anio, mes);
            ActualizarPanelSaldo(saldo);
        }

        private void ActualizarPanelSaldo(decimal saldo)
        {
            lblSaldoMonto.Text = "₡ " + saldo.ToString("N2");
            if (saldo < 0) 
                lblSaldoMonto.ForeColor = _theme.StatusError;
            else if (saldo > 0) 
                lblSaldoMonto.ForeColor = _theme.TextPrimary; // o Verde
            else 
                lblSaldoMonto.ForeColor = _theme.TextSecondary;
        }

        private void AlFormatearCelda(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || grdMovimientos.Rows.Count <= e.RowIndex) return;

            var fila = grdMovimientos.Rows[e.RowIndex];
            if (fila.DataBoundItem is CajaChica mov)
            {
                if (grdMovimientos.Columns[e.ColumnIndex].Name == "colFecha")
                {
                    e.Value = mov.Fecha.ToString("dd/MMM/yyyy");
                    e.FormattingApplied = true;
                }
                else if (grdMovimientos.Columns[e.ColumnIndex].Name == "colIngreso")
                {
                    if (mov.TipoMovimiento == "Ingreso")
                    {
                        e.Value = "₡ " + mov.Monto.ToString("N2");
                        e.CellStyle.ForeColor = Color.ForestGreen;
                        e.CellStyle.Font = new Font(grdMovimientos.Font, FontStyle.Bold);
                    }
                    else e.Value = "";
                    e.FormattingApplied = true;
                }
                else if (grdMovimientos.Columns[e.ColumnIndex].Name == "colEgreso")
                {
                    if (mov.TipoMovimiento == "Egreso")
                    {
                        e.Value = "₡ " + mov.Monto.ToString("N2");
                        e.CellStyle.ForeColor = Color.IndianRed;
                        e.CellStyle.Font = new Font(grdMovimientos.Font, FontStyle.Bold);
                    }
                    else e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        private async void AlHacerClickEnIngreso(object? sender, EventArgs e)
        {
            using var frm = new FrmEdicionCajaChica(_servicioCajaChica, _servicioFoto, _idUsuarioSesion, null, "Ingreso", _theme);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                await CargarDatosAsync();
            }
        }

        private async void AlHacerClickEnEgreso(object? sender, EventArgs e)
        {
            using var frm = new FrmEdicionCajaChica(_servicioCajaChica, _servicioFoto, _idUsuarioSesion, null, "Egreso", _theme);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                await CargarDatosAsync();
            }
        }

        private async void AlDobleClickEnCelda(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || grdMovimientos.Rows.Count <= e.RowIndex) return;

            var fila = grdMovimientos.Rows[e.RowIndex];
            if (fila.DataBoundItem is CajaChica movOriginal)
            {
                try 
                {
                    using var frm = new FrmEdicionCajaChica(_servicioCajaChica, _servicioFoto, _idUsuarioSesion, movOriginal, movOriginal.TipoMovimiento, _theme);
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        await CargarDatosAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo abrir el registro para edición: {ex.Message}", "Error de Interfaz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AlCambiarSeleccion(object? sender, EventArgs e)
        {
            // El botón de auditoría ahora es global para el mes
            btnAuditoria.Visible = true;
        }

        private void AlHacerClickEnAuditoria(object? sender, EventArgs e)
        {
            if (cboMes.SelectedIndex == -1 || cboAnio.SelectedIndex == -1) return;

            int mes = cboMes.SelectedIndex + 1;
            int anio = int.Parse(cboAnio.SelectedItem!.ToString()!);

            using var frm = new FrmAuditoriaCajaChica(anio, mes, _servicioCajaChica, _theme);
            frm.ShowDialog(this);
        }
    }
}
