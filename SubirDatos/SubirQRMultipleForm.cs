using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using ExcelDataReader;

namespace SubirDatos
{
    public partial class SubirQRMultipleForm : Form
    {
        private DataTable datosExcel;
        private string connectionString;

        public SubirQRMultipleForm(string connString)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // necesario para ExcelDataReader
            InitializeComponent();
            connectionString = connString;
        }

        private void btnSeleccionarExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Archivos Excel|*.xls;*.xlsx",
                Title = "Seleccionar archivo Excel"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                datosExcel = LeerExcelConEncabezado(ofd.FileName);
                dataGridViewDatos.DataSource = datosExcel;
                lblCantidadFilas.Text = $"Filas cargadas: {datosExcel.Rows.Count}";
            }
        }

        private DataTable LeerExcelConEncabezado(string archivo)
        {
            using (var stream = File.Open(archivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // 👈 IMPORTANTE: ahora sí usamos la cabecera
                        }
                    });

                    // Tomamos la primera hoja
                    var tabla = result.Tables[0];
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

            DataTable tablaRecortada = new DataTable();
            tablaRecortada.Columns.Add("FechaOperacion", typeof(DateTime));  // 1
            tablaRecortada.Columns.Add("IdQR", typeof(string));              // 7
            tablaRecortada.Columns.Add("NroTerminal", typeof(string));       // 3 (con procesamiento)
            tablaRecortada.Columns.Add("Importe", typeof(decimal));          // 19
            tablaRecortada.Columns.Add("Resultado", typeof(string));         // 32
            tablaRecortada.Columns.Add("Nombre", typeof(string));            // 0
            tablaRecortada.Columns.Add("MarcaTarjeta", typeof(string));      // 12
            tablaRecortada.Columns.Add("IdPago", typeof(string));            // 2

            foreach (DataRow row in datosExcel.Rows)
            {
                try
                {
                    // Solo incluir si el medio es "Medio: Transferencia"
                    if (row[12]?.ToString().Trim() != "Medio: Transferencia")
                        continue;

                    DataRow nuevaFila = tablaRecortada.NewRow();

                    // FechaOperacion (col 1)
                    if (DateTime.TryParse(row[1]?.ToString().Replace(" ART", ""), out var fecha))
                        nuevaFila["FechaOperacion"] = fecha;
                    else
                        nuevaFila["FechaOperacion"] = DBNull.Value;

                    // IdQR (col 7)
                    nuevaFila["IdQR"] = row[7]?.ToString().Trim();

                    // NroTerminal (col 3, con lógica de limpieza)
                    string terminalRaw = row[3]?.ToString().Trim();
                    string soloNumeros = new string(terminalRaw?.Where(char.IsDigit).ToArray());
                    string terminalLimpio = soloNumeros.Length >= 8 ? soloNumeros.Substring(0, 8) : soloNumeros;
                    nuevaFila["NroTerminal"] = terminalLimpio;

                    // Importe (col 19)
                    nuevaFila["Importe"] = decimal.TryParse(row[19]?.ToString(), out var monto) ? monto : 0;

                    // Resultado (col 32)
                    nuevaFila["Resultado"] = row[32]?.ToString().Trim();

                    // Nombre (col 0)
                    nuevaFila["Nombre"] = row[0]?.ToString().Trim();

                    // MarcaTarjeta (Medio) (col 12)
                    nuevaFila["MarcaTarjeta"] = row[12]?.ToString().Trim();

                    // IdPago (col 2)
                    nuevaFila["IdPago"] = row[2]?.ToString().Trim();

                    tablaRecortada.Rows.Add(nuevaFila);
                }
                catch
                {
                    continue;
                }
            }

            datosExcel = tablaRecortada;
            dataGridViewDatos.DataSource = datosExcel;
            lblCantidadFilas.Text = $"Filas recortadas: {datosExcel.Rows.Count}";
        }





        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (datosExcel == null || datosExcel.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                Title = "Guardar archivo Excel",
                FileName = "DatosRecortados.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(datosExcel, "Datos");
                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("✅ Archivo Excel exportado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("💥 Error al exportar: " + ex.Message);
                }
            }
        }
    }
}
