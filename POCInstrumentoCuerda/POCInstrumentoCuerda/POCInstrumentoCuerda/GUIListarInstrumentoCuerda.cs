using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POCInstrumentoCuerda.model;

namespace POCInstrumentoCuerda
{
    public partial class GUIListarInstrumentoCuerda : Form
    {
        private readonly TextBox filtroNombre = new TextBox();
        private readonly TextBox filtroPrecioMaximo = new TextBox();
        private List<Instrumento> instrumentos = new List<Instrumento>();

        public GUIListarInstrumentoCuerda()
        {
            InitializeComponent();
            Controls.Add(new Label { Text = "Nombre:", Location = new Point(77, 60), AutoSize = true });
            filtroNombre.SetBounds(125, 57, 120, 20);
            Controls.Add(filtroNombre);
            Controls.Add(new Label { Text = "Precio máximo:", Location = new Point(270, 60), AutoSize = true });
            filtroPrecioMaximo.SetBounds(365, 57, 100, 20);
            Controls.Add(filtroPrecioMaximo);
            btnListar.Click += async delegate { await ListarAsync(); };
        }

        private async Task ListarAsync()
        {
            try
            {
                instrumentos = await GraphQLService.ListarAsync();
                tableInstrumentosCuerda.Rows.Clear();
                double precioMaximo;
                bool filtrarPrecio = double.TryParse(filtroPrecioMaximo.Text, out precioMaximo);
                foreach (var instrumento in instrumentos.Where(item =>
                    (string.IsNullOrWhiteSpace(filtroNombre.Text) || item.nombre.IndexOf(filtroNombre.Text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (!filtrarPrecio || item.precio <= precioMaximo)))
                {
                    tableInstrumentosCuerda.Rows.Add(instrumento.id, instrumento.nombre, instrumento.precio,
                        instrumento.fechaVenta, instrumento.numeroTrastes, instrumento.numeroCuerdas);
                }
            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
