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
    public partial class GUIAdicionarInstrumentoCuerda : Form
    {
        public GUIAdicionarInstrumentoCuerda()
        {
            InitializeComponent();
            btnAdicionar.Click += async delegate { await AdicionarAsync(); };
        }

        private async Task AdicionarAsync()
        {
            try
            {
                var input = new InstrumentoInput
                {
                    id = ParseInt(txtId.Text, "ID"),
                    nombre = Required(txtNombre.Text, "Nombre"),
                    precio = ParseDouble(txtPrecio.Text, "Precio"),
                    fechaVenta = txtFechaVenta.Value.ToString("yyyy-MM-dd"),
                    numeroCuerdas = ParseInt(txtNumeroCuerdas.Text, "Número de cuerdas"),
                    numeroTrastes = ParseInt(txtNumeroTrastes.Text, "Número de trastes")
                };
                var created = await GraphQLService.AdicionarAsync(input);
                MessageBox.Show("Instrumento adicionado con ID " + created.id, "Operación exitosa");
                Close();
            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static int ParseInt(string value, string field)
        {
            int result;
            if (!int.TryParse(value, out result) || result <= 0) throw new ArgumentException(field + " debe ser un entero positivo.");
            return result;
        }

        private static double ParseDouble(string value, string field)
        {
            double result;
            if (!double.TryParse(value, out result) || result <= 0) throw new ArgumentException(field + " debe ser mayor que cero.");
            return result;
        }

        private static string Required(string value, string field)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(field + " es obligatorio.");
            return value.Trim();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
