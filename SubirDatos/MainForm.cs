using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SubirDatos
{
    public partial class MainForm : Form
    {
        private string connectionString;
        private bool esAdmin;

        public MainForm(string connString, bool esAdminFlag)
        {
            InitializeComponent();
            connectionString = connString;
            esAdmin = esAdminFlag;

            if (esAdmin)
            {
                CargarTotalesDashboard();
            }
            else
            {
                // Ocultamos los labels pero dejamos el espacio
                lblTotalBrutoHoy.Visible = false;
                lblPagoHoy.Visible = false;
                lblProximoBruto.Visible = false;
                lblProximoPago.Visible = false;
                btnSubirQR.Visible = false;

            }
        }

        private void CargarTotalesDashboard()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DateTime hoy = DateTime.Today;

                    // 🔍 Buscar la fecha del próximo pago válido con "Si Paga"
                    DateTime? proximoFecha = null;
                    string queryFecha = @"
                        SELECT MIN([Fecha de Pago])
                        FROM base_dashboard
                        WHERE [Fecha de Pago] > @Hoy AND [Pago Real] = 'Si Paga';
                    ";

                    using (SqlCommand cmdFecha = new SqlCommand(queryFecha, conn))
                    {
                        cmdFecha.Parameters.AddWithValue("@Hoy", hoy);
                        var result = cmdFecha.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                            proximoFecha = Convert.ToDateTime(result);
                    }

                    // 🔍 Consultar totales para hoy y la próxima fecha
                    string queryTotales = @"
                        SELECT
                            ISNULL(SUM(CASE WHEN [Fecha de Pago] = @Hoy AND [Pago Real] = 'Si Paga' THEN [Total Bruto] ELSE 0 END), 0) AS TotalBrutoHoy,
                            ISNULL(SUM(CASE WHEN [Fecha de Pago] = @Hoy AND [Pago Real] = 'Si Paga' THEN [Total Con descuentos] ELSE 0 END), 0) AS PagoHoy,
                            ISNULL(SUM(CASE WHEN [Fecha de Pago] = @Prox AND [Pago Real] = 'Si Paga' THEN [Total Bruto] ELSE 0 END), 0) AS ProximoBruto,
                            ISNULL(SUM(CASE WHEN [Fecha de Pago] = @Prox AND [Pago Real] = 'Si Paga' THEN [Total Con descuentos] ELSE 0 END), 0) AS ProximoPago
                        FROM base_dashboard;
                    ";

                    using (SqlCommand cmdTotales = new SqlCommand(queryTotales, conn))
                    {
                        cmdTotales.Parameters.AddWithValue("@Hoy", hoy);
                        cmdTotales.Parameters.AddWithValue("@Prox", (object)proximoFecha ?? DBNull.Value);

                        using (SqlDataReader reader = cmdTotales.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTotalBrutoHoy.Text = "Total Bruto Hoy: $" + Convert.ToDecimal(reader["TotalBrutoHoy"]).ToString("N2");
                                lblPagoHoy.Text = "Pago Hoy: $" + Convert.ToDecimal(reader["PagoHoy"]).ToString("N2");
                                lblProximoBruto.Text = "Próximo Bruto: $" + Convert.ToDecimal(reader["ProximoBruto"]).ToString("N2");
                                lblProximoPago.Text = "Próximo Pago: $" + Convert.ToDecimal(reader["ProximoPago"]).ToString("N2");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar totales: " + ex.Message);
            }
        }

        private void btnSubirQR_Click(object sender, EventArgs e)
        {
            SubirQRForm qrForm = new SubirQRForm(connectionString);
            qrForm.Show();
        }

        private void btnJuntarCSV_Click(object sender, EventArgs e)
        {
            JuntarCSVForm juntarForm = new JuntarCSVForm(connectionString);
            juntarForm.Show();
        }
    }
}
