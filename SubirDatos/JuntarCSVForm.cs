using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace SubirDatos
{
    public partial class JuntarCSVForm : Form
    {
        private DataTable datosCSV = new DataTable();
        private DataTable datosRecortados = new DataTable();
        private string connectionString;

        public JuntarCSVForm(string connString)
        {
            InitializeComponent();
            connectionString = connString;
        }

        private void btnSeleccionarCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Archivos CSV|*.csv",
                Title = "Seleccionar archivos CSV",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                datosCSV = new DataTable();
                bool primeraTabla = true;
                string[] encabezados = null;

                foreach (var archivo in ofd.FileNames)
                {
                    var lineas = File.ReadAllLines(archivo);
                    if (lineas.Length == 0) continue;

                    char separador = DetectarSeparador(lineas[0]);
                    var columnas = lineas[0].Split(separador);

                    if (primeraTabla)
                    {
                        encabezados = columnas;
                        foreach (var col in columnas)
                            datosCSV.Columns.Add(col.Trim());
                        for (int k = 1; k <= 10; k++)
                            datosCSV.Columns.Add($"Extra{k}");
                        primeraTabla = false;
                    }

                    for (int i = 1; i < lineas.Length; i++)
                    {
                        var valores = lineas[i].Split(separador);
                        var fila = datosCSV.NewRow();

                        for (int j = 0; j < encabezados.Length && j < valores.Length; j++)
                            if (j < datosCSV.Columns.Count)
                                fila[j] = valores[j].Trim();

                        for (int j = encabezados.Length; j < valores.Length; j++)
                        {
                            int indexExtra = j;
                            if (indexExtra < datosCSV.Columns.Count)
                                fila[indexExtra] = valores[j].Trim();
                            else break;
                        }

                        datosCSV.Rows.Add(fila);
                    }
                }

                dataGridViewDatos.DataSource = datosCSV;
                lblCantidadFilas.Text = $"Filas cargadas: {datosCSV.Rows.Count}";
            }
        }

        private char DetectarSeparador(string linea)
        {
            int countComa = linea.Count(c => c == ',');
            int countPuntoComa = linea.Count(c => c == ';');
            return countPuntoComa > countComa ? ';' : ',';
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel(datosCSV, "JuntadoCSV.xlsx", "DatosCSV");
        }

        private void btnRecortarCSV_Click(object sender, EventArgs e)
        {
            if (datosCSV == null || datosCSV.Rows.Count == 0)
            {
                MessageBox.Show("Primero cargá los CSV.");
                return;
            }

            var columnasDeseadas = new List<string>
            {
                "Fecha Operación", "Fecha de Presentación", "Fecha de Pago", "D", "D_1", "Nro,", "D_2",
                "Comercio", "Terminal", "Moneda", "Total Bruto", "Total Descuento", "Total Neto",
                "Entidad Pagadora", "Cuenta Bancaria", "Nro, Liquidación", "Nro, de Lote",
                "Tipo de Liquidacion", "Estado", "Cuotas", "Nro, de Autorizacion", "Tarjeta"
            };

            datosRecortados = new DataTable();
            columnasDeseadas.ForEach(col => datosRecortados.Columns.Add(col));

            foreach (DataRow filaOriginal in datosCSV.Rows)
            {
                var columnaMoneda = datosCSV.Columns.Cast<DataColumn>()
                    .FirstOrDefault(c => c.ColumnName.Trim().ToLower().Contains("moneda"));

                if (columnaMoneda == null || !filaOriginal[columnaMoneda].ToString().Trim()
                        .Equals("Medio: Transferencia", StringComparison.OrdinalIgnoreCase))
                    continue;

                DataRow nuevaFila = datosRecortados.NewRow();

                for (int i = 0; i < columnasDeseadas.Count; i++)
                {
                    string colDestino = columnasDeseadas[i];

                    if (colDestino.StartsWith("Fecha"))
                    {
                        var colFecha = datosCSV.Columns.Cast<DataColumn>()
                            .FirstOrDefault(c => c.ColumnName.Trim().ToLower().Contains("fecha del pago"));

                        if (colFecha != null)
                        {
                            string valorCrudo = filaOriginal[colFecha]?.ToString().Replace("ART", "").Trim();
                            if (DateTime.TryParse(valorCrudo, out DateTime fechaBase))
                            {
                                if (colDestino == "Fecha Operación")
                                    nuevaFila[i] = fechaBase.ToString("d/M/yyyy");
                                else if (colDestino == "Fecha de Pago")
                                    nuevaFila[i] = fechaBase.AddDays(1).ToString("d/M/yyyy");
                                else
                                    nuevaFila[i] = "";
                            }
                        }
                        continue;
                    }
                    else if (colDestino == "Comercio" || colDestino == "Terminal")
                    {
                        var colID = datosCSV.Columns.Cast<DataColumn>()
                            .FirstOrDefault(c => c.ColumnName.ToLower().Contains("id de pago externo"));

                        string val = colID != null ? filaOriginal[colID]?.ToString().Trim() : "";

                        if (colDestino == "Comercio") nuevaFila[i] = val;
                        if (colDestino == "Terminal") nuevaFila[i] = val.Length >= 8 ? val.Substring(0, 8) : val;
                        continue;
                    }
                    else if (colDestino == "Total Bruto")
                    {
                        int indexTotalBruto = 18; // ← Núm. de comercio
                        nuevaFila[i] = indexTotalBruto < filaOriginal.ItemArray.Length
                            ? filaOriginal[indexTotalBruto]?.ToString().Trim()
                            : "";
                        continue;
                    }

                    else if (colDestino == "Moneda" || colDestino.StartsWith("D") || colDestino == "Nro,")
                    {
                        nuevaFila[i] = "";
                        continue;
                    }
                    else if (colDestino == "Estado")
                    {
                        var extra3 = datosCSV.Columns.Cast<DataColumn>()
                            .FirstOrDefault(c => c.ColumnName.ToLower().Contains("extra3"));

                        nuevaFila[i] = extra3 != null && filaOriginal[extra3]?.ToString().ToUpper() == "SUCCESS"
                            ? "PRESENTADO"
                            : filaOriginal[extra3]?.ToString() ?? "";
                        continue;
                    }
                    else if (colDestino == "Tarjeta")
                    {
                        nuevaFila[i] = "QR";
                        continue;
                    }

                    var columna = datosCSV.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName == colDestino);

                    nuevaFila[i] = columna != null ? filaOriginal[columna] : "";
                }

                datosRecortados.Rows.Add(nuevaFila);
            }

            dataGridViewDatos.DataSource = datosRecortados;
            lblCantidadFilas.Text = $"Filas recortadas: {datosRecortados.Rows.Count}";
        }

        private void btnExportarRecortadoExcel_Click(object sender, EventArgs e)
        {
            if (datosRecortados == null || datosRecortados.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos recortados para exportar.");
                return;
            }

            ExportarExcel(datosRecortados, "RecorteTransferencias.xlsx", "Recorte");
        }

        private void ExportarExcel(DataTable tabla, string defaultName, string hoja)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Archivo Excel (*.xlsx)|*.xlsx",
                Title = "Guardar Excel",
                FileName = defaultName
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        if (hoja == "Recorte")
                        {
                            var ws = wb.Worksheets.Add(hoja);

                            // Escribir headers
                            for (int i = 0; i < tabla.Columns.Count; i++)
                                ws.Cell(1, i + 1).Value = tabla.Columns[i].ColumnName;

                            // Escribir filas
                            for (int i = 0; i < tabla.Rows.Count; i++)
                            {
                                for (int j = 0; j < tabla.Columns.Count; j++)
                                {
                                    string header = tabla.Columns[j].ColumnName;
                                    string valorCelda = tabla.Rows[i][j]?.ToString();

                                    // Reemplazamos punto por coma antes de convertir
                                    if (header == "Total Bruto")
                                    {
                                        valorCelda = valorCelda.Replace(".", ","); // 👈 clave
                                        if (decimal.TryParse(valorCelda, out var numero))
                                        {
                                            ws.Cell(i + 2, j + 1).Value = numero;
                                            ws.Cell(i + 2, j + 1).Style.NumberFormat.Format = "0.00";
                                        }
                                        else
                                        {
                                            ws.Cell(i + 2, j + 1).Value = valorCelda; // si falla, lo dejamos como está
                                        }
                                    }
                                    else
                                    {
                                        ws.Cell(i + 2, j + 1).Value = valorCelda;
                                    }
                                }
                            }
                        }



                        else
                        {
                            wb.Worksheets.Add(tabla, hoja); // con formato para el archivo completo
                        }

                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("✅ Archivo exportado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("💥 Error al exportar: " + ex.Message);
                }
            }
        }
    }
}
