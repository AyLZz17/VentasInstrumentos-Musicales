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
    public partial class GUIEliminarInstrumentoCuerda : Form
    {
        private Instrumento instrumentoEncontrado;

        public GUIEliminarInstrumentoCuerda()
        {
            InitializeComponent();
            btnConsultar.Click += async delegate { await BuscarAsync(); };
            btnEliminar.Click += async delegate { await EliminarAsync(); };
            btnEliminar.Enabled = false;
        }

        private async Task BuscarAsync()
        {
            try
            {
                instrumentoEncontrado = await GraphQLService.BuscarAsync(ParseId(txtBuscarId.Text));
                if (instrumentoEncontrado == null)
                {
                    btnEliminar.Enabled = false;
                    MessageBox.Show("No se encontró el instrumento.", "Consulta");
                    return;
                }
                GUIConsultaInstrumentoCuerda.ShowDetails(instrumentoEncontrado, txtId, txtNombre, txtPrecio, txtFechaVenta, txtNumeroTrastes, txtNumeroCuerdas);
                btnEliminar.Enabled = true;
            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async Task EliminarAsync()
        {
            if (MessageBox.Show("¿Confirma la eliminación del instrumento mostrado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                await GraphQLService.EliminarAsync(ParseId(instrumentoEncontrado.id));
                MessageBox.Show("Instrumento eliminado.", "Operación exitosa");
                Close();
            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static int ParseId(string value)
        {
            int id;
            if (!int.TryParse(value, out id) || id <= 0) throw new ArgumentException("El ID debe ser un entero positivo.");
            return id;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
