using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCInstrumentoCuerda
{
    public partial class GUIPrincipal : Form
    {
        public GUIPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void instrumentoVientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adicionarInstrumentoVientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAdicionarInstrumentoCuerda gui = new GUIAdicionarInstrumentoCuerda();
            gui.Show();
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIEliminarInstrumentoCuerda gui = new GUIEliminarInstrumentoCuerda();
            gui.Show();
        }

        private void listarInstrumentoVientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIListarInstrumentoCuerda gui = new GUIListarInstrumentoCuerda();
            gui.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void actualizarInstrumentoVientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIActualizarInstrumentoCuerda gui = new GUIActualizarInstrumentoCuerda();
            gui.Show();
        }

        private void consultaInstrumentoVientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIConsultaInstrumentoCuerda gui = new GUIConsultaInstrumentoCuerda();
            gui.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void derechosDeAutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIDerechosAutor gui = new GUIDerechosAutor();
            gui.Show();
        }
    }
}
