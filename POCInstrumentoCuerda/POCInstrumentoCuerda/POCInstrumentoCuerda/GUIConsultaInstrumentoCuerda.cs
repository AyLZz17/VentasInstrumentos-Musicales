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
    public partial class GUIConsultaInstrumentoCuerda : Form
    {
        public GUIConsultaInstrumentoCuerda()
        {
            InitializeComponent();
            btnConsultar.Click += async delegate { await ConsultarAsync(); };
        }

        private async Task ConsultarAsync()
        {
            try
            {
                var instrumento = await GraphQLService.BuscarAsync(ParseId(txtBuscarId.Text));
                if (instrumento == null)
                {
                    ClearDetails();
                    MessageBox.Show("No se encontró un instrumento con ese ID.", "Consulta");
                    return;
                }
                ShowDetails(instrumento);
            }
            catch (Exception exception) { MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        internal static void ShowDetails(Instrumento instrumento, Label id, Label nombre, Label precio, Label fecha, Label trastes, Label cuerdas)
        {
            id.Text = instrumento.id.ToString();
            nombre.Text = instrumento.nombre;
            precio.Text = instrumento.precio.ToString("0.##");
            fecha.Text = instrumento.fechaVenta;
            trastes.Text = instrumento.numeroTrastes.ToString();
            cuerdas.Text = instrumento.numeroCuerdas.ToString();
        }

        private void ShowDetails(Instrumento instrumento)
        {
            ShowDetails(instrumento, txtId, txtNombre, txtPrecio, txtFechaVenta, txtNumeroTrastes, txtNumeroCuerdas);
        }

        private void ClearDetails()
        {
            txtId.Text = txtNombre.Text = txtPrecio.Text = txtFechaVenta.Text = txtNumeroTrastes.Text = txtNumeroCuerdas.Text = "No encontrado";
        }

        private static int ParseId(string value)
        {
            int id;
            if (!int.TryParse(value, out id) || id <= 0) throw new ArgumentException("El ID debe ser un entero positivo.");
            return id;
        }
    }
}
