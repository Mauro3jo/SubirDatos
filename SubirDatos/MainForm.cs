using System;
using System.Windows.Forms;

namespace SubirDatos
{
    public partial class MainForm : Form
    {
        private string connectionString;

        public MainForm(string connString)
        {
            InitializeComponent();
            connectionString = connString;
            lblBaseDatos.Text = "Conectado a: " + connectionString;
        }

        //private void btnSubirPendientes_Click(object sender, EventArgs e)
        //{
        //    SubirDatosPendientesForm pendientesForm = new SubirDatosPendientesForm(connectionString);
        //    pendientesForm.Show();
        //}

        private void btnSubirQR_Click(object sender, EventArgs e)
        {
            SubirQRForm qrForm = new SubirQRForm(connectionString);
            qrForm.Show();
        }

        //private void btnAcumularQR_Click(object sender, EventArgs e)
        //{
        //    SubirQRMultipleForm acumularQRForm = new SubirQRMultipleForm(connectionString);
        //    acumularQRForm.Show();
        //}

        private void btnJuntarCSV_Click(object sender, EventArgs e)
        {
            JuntarCSVForm juntarForm = new JuntarCSVForm(connectionString);
            juntarForm.Show();
        }
    }
}
