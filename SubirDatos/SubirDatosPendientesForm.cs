using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using System.Data.SqlClient;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SubirDatos
{
    public partial class SubirDatosPendientesForm : Form
    {
        private DataTable datosExcel;
        private string connectionString;

        public SubirDatosPendientesForm(string connString)
        {
            InitializeComponent();
            connectionString = connString;
        }

        private void btnSeleccionarExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Archivos Excel|*.xls;*.xlsx;",
                Title = "Seleccione archivo Excel"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                datosExcel = LeerExcel(ofd.FileName);
                dataGridViewDatos.DataSource = datosExcel;
                lblCantidadFilas.Text = $"Filas cargadas: {datosExcel.Rows.Count}";
            }
        }

        private DataTable LeerExcel(string rutaArchivo)
        {
            using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });

                    DataTable tabla = result.Tables[0];

                    if (tabla.Rows.Count > 0)
                        tabla.Rows.RemoveAt(0); // quitar cabecera

                    return tabla;
                }
            }
        }

        private void btnRecortarExcel_Click(object sender, EventArgs e)
        {
            if (datosExcel == null)
            {
                MessageBox.Show("Primero carga un Excel.");
                return;
            }

            DataTable tablaRecortadaFinal = new DataTable();
            tablaRecortadaFinal.Columns.Add("FechaOperacion", typeof(DateTime));
            tablaRecortadaFinal.Columns.Add("MarcaTarjeta", typeof(string));
            tablaRecortadaFinal.Columns.Add("Importe", typeof(decimal));
            tablaRecortadaFinal.Columns.Add("NroTerminal", typeof(string));
            tablaRecortadaFinal.Columns.Add("Resultado", typeof(string));
            tablaRecortadaFinal.Columns.Add("Cuota", typeof(string));

            foreach (DataRow row in datosExcel.Rows)
            {
                bool columnaCTieneDatos = !string.IsNullOrWhiteSpace(row[2]?.ToString());

                if (!columnaCTieneDatos)
                {
                    try
                    {
                        DataRow nuevaFila = tablaRecortadaFinal.NewRow();
                        if (DateTime.TryParse(row[0]?.ToString().Replace(" ART", ""), out var fecha))
                            nuevaFila["FechaOperacion"] = fecha;
                        else
                            nuevaFila["FechaOperacion"] = DBNull.Value;
                        nuevaFila["MarcaTarjeta"] = row[8]?.ToString().Trim();
                        nuevaFila["Importe"] = decimal.TryParse(row[12]?.ToString(), out var monto) ? monto : 0;
                        nuevaFila["NroTerminal"] = row[19]?.ToString().Trim();
                        nuevaFila["Resultado"] = row[30]?.ToString().Trim();
                        nuevaFila["Cuota"] = row[17]?.ToString().Trim();
                        tablaRecortadaFinal.Rows.Add(nuevaFila);
                    }
                    catch { continue; }
                }
                else
                {
                    if (row[11]?.ToString().Trim() != "Medio: Transferencia")
                    {
                        try
                        {
                            DataRow nuevaFila = tablaRecortadaFinal.NewRow();
                            if (DateTime.TryParse(row[0]?.ToString().Replace(" ART", ""), out var fecha))
                                nuevaFila["FechaOperacion"] = fecha;
                            else
                                nuevaFila["FechaOperacion"] = DBNull.Value;
                            nuevaFila["MarcaTarjeta"] = row[11]?.ToString().Trim();
                            nuevaFila["Importe"] = decimal.TryParse(row[19]?.ToString(), out var monto) ? monto : 0;

                            string terminalRaw = row[2]?.ToString().Trim();
                            string soloNumeros = new string(terminalRaw?.Where(char.IsDigit).ToArray());
                            string terminalLimpio = soloNumeros.Length >= 8 ? soloNumeros.Substring(0, 8) : soloNumeros;
                            nuevaFila["NroTerminal"] = terminalLimpio;

                            nuevaFila["Resultado"] = row[37]?.ToString().Trim();
                            nuevaFila["Cuota"] = row[12]?.ToString().Trim();
                            tablaRecortadaFinal.Rows.Add(nuevaFila);
                        }
                        catch { continue; }
                    }
                }
            }

            dataGridViewDatos.DataSource = tablaRecortadaFinal;
            datosExcel = tablaRecortadaFinal;
            lblCantidadFilas.Text = $"Filas recortadas: {datosExcel.Rows.Count}";
        }

        private void btnSubirDatos_Click(object sender, EventArgs e)
        {
            if (datosExcel == null || datosExcel.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para subir.");
                return;
            }

            btnSeleccionarExcel.Enabled = false;
            btnRecortarExcel.Enabled = false;
            btnSubirDatos.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Maximum = datosExcel.Rows.Count;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int insertados = 0;

                    foreach (DataRow row in datosExcel.Rows)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            @"INSERT INTO [dbo].[PagosPendientes] 
                            (FechaOperacion, MarcaTarjeta, Importe, NroTerminal, Resultado, Cuota)
                            VALUES (@FechaOperacion, @MarcaTarjeta, @Importe, @NroTerminal, @Resultado, @Cuota)", conn))
                        {
                            cmd.Parameters.AddWithValue("@FechaOperacion", row["FechaOperacion"] ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@MarcaTarjeta", row["MarcaTarjeta"] ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Importe", row["Importe"] ?? 0);
                            cmd.Parameters.AddWithValue("@NroTerminal", row["NroTerminal"] ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Resultado", row["Resultado"] ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Cuota", row["Cuota"] ?? (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                            insertados++;
                        }

                        progressBar.Value = Math.Min(progressBar.Maximum, insertados);
                        Application.DoEvents();
                    }
                }

                MessageBox.Show("✅ Datos subidos exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"💥 Error al subir datos: {ex.Message}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnSeleccionarExcel.Enabled = true;
                btnRecortarExcel.Enabled = true;
                btnSubirDatos.Enabled = true;
                progressBar.Visible = false;
            }
        }
    }
}
