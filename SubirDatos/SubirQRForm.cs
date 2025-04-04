using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExcelDataReader;

namespace SubirDatos
{
    public partial class SubirQRForm : Form
    {
        private DataTable datosQR;
        private string connectionString;

        public SubirQRForm(string connString)
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
                datosQR = LeerExcel(ofd.FileName);
                dataGridViewQR.DataSource = datosQR;
                lblCantidadFilas.Text = $"Filas cargadas: {datosQR.Rows.Count}";
            }
        }

        private DataTable LeerExcel(string rutaArchivo)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    return result.Tables[0];
                }
            }
        }

        private void btnSubirQR_Click(object sender, EventArgs e)
        {
            if (datosQR == null || datosQR.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para subir.");
                return;
            }

            btnSubirQR.Enabled = false;
            btnSeleccionarExcel.Enabled = false;
            progressBarSubida.Visible = true;
            progressBarSubida.Value = 0;
            progressBarSubida.Maximum = datosQR.Rows.Count;

            List<string> columnasDestino = new List<string>
    {
        "Fecha Operacion", "Fecha de Presentacion", "Fecha de Pago", "Nro# de Cupon", "Nro# de Comercio",
        "Nro# de Tarjeta", "Moneda", "Total Bruto", "Total Descuento", "Total Neto", "Entidad Pagadora",
        "Cuenta Bancaria", "Nro# Liquidacion", "Nro# de Lote", "Tipo de Liquidacion", "Estado", "Cuotas",
        "Nro# de Autorizacion", "Tarjeta", "Tipo de Operacion", "Comercio Participante", "Promocion Plan",
        "Tarjeta-Tipo", "Costo Financiero", "Costo Financiero en $", "Tipo de Financiacion", "Costo por anticipo",
        "% Comision con IVA", "Arancel", "IVA 21%", "IMPUESTO DEBITO/CREDITO", "CUIT", "Condicion Fiscal", "Provincia",
        "Retencion Provincial", "Retencion Ganacia", "Retencion IVA", "Total Con descuentos", "CBU/CVU", "Banco",
        "Tipo de cuenta", "Nro de cuenta", "Alias", "Nombre Comercio", "Retencion Municipal", "RETENCION",
        "Retencion impositiva", "Asesor ABM", "Rubro", "Fecha Alta comercio", "Provincia ABM", "Razon Social",
        "Legajo", "Cod. Actividad", "Codigo Postal", "Fecha OP", "Arancel Fiserv", "Pago Real", "dias habiles",
        "Formula anticipo", "TASA_PREFERENCIAL", "Fecha inicio", "ganancia", "fecha", "Debito/credito",
        "COD# ACTIVIDAD RENTAS", "anticipo%", "BONIFICACION", "FECHA2"
    };

            var columnasMoney = new HashSet<string>
    {
        "Total Descuento", "Total Neto", "Costo Financiero en $", "Costo por anticipo",
        "Arancel", "IVA 21%", "IMPUESTO DEBITO/CREDITO", "Total Con descuentos",
        "Retencion Provincial", "Retencion Ganacia", "Retencion IVA", "Retencion impositiva",
        "Formula anticipo", "Arancel Fiserv"
    };

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int insertados = 0;
                    int duplicados = 0;

                    for (int filaIndex = 0; filaIndex < datosQR.Rows.Count; filaIndex++)
                    {
                        DataRow fila = datosQR.Rows[filaIndex];

                        if (fila.ItemArray.Length < 61)
                            continue;

                        object codigoActividad = TryParseFloat(fila[59]);
                        object codigoPostal = TryParseFloat(fila[53]);
                        object arancelFiserv = TryParseMoney(fila[55]);
                        object pagoReal = fila[56]?.ToString();

                        List<object> valoresConvertidos = new List<object>();

                        for (int i = 0; i < columnasDestino.Count; i++)
                        {
                            string nombre = columnasDestino[i];
                            object valor;

                            if (nombre == "Cod. Actividad" || nombre == "COD# ACTIVIDAD RENTAS")
                                valor = codigoActividad;
                            else if (nombre == "Codigo Postal")
                                valor = codigoPostal;
                            else if (nombre == "Arancel Fiserv")
                                valor = arancelFiserv;
                            else if (nombre == "Pago Real")
                                valor = pagoReal;
                            else if (nombre == "Formula anticipo" && fila.ItemArray.Length > 58)
                            {
                                // ✅ Conversión especial para "Formula anticipo"
                                string raw = fila[58]?.ToString()?.Trim();
                                if (!string.IsNullOrEmpty(raw))
                                {
                                    raw = raw.Replace("$", "").Replace(".", "").Replace(",", ".");
                                    if (decimal.TryParse(raw, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal dec))
                                    {
                                        if (dec > 100000)
                                            valor = DBNull.Value;
                                        else
                                            valor = dec;
                                    }
                                    else
                                    {
                                        valor = DBNull.Value;
                                    }
                                }
                                else
                                {
                                    valor = DBNull.Value;
                                }
                            }
                            else
                            {
                                valor = i < fila.ItemArray.Length ? fila[i] : DBNull.Value;

                                if (nombre == "Alias")
                                    valor = valor?.ToString();
                                else if (nombre == "Fecha Alta comercio" && valor is DateTime dtAlta)
                                    valor = dtAlta.ToOADate();
                                else if (nombre == "Fecha inicio" || nombre == "fecha" || nombre == "FECHA2")
                                    valor = TryParseFecha(valor);
                                else if (nombre == "ganancia")
                                    valor = TryParseFloat(valor);
                                else if (nombre == "dias habiles")
                                    valor = TryParseInt(valor);
                                else if (columnasMoney.Contains(nombre))
                                    valor = TryParseMoney(valor);
                            }

                            valoresConvertidos.Add(valor ?? DBNull.Value);
                        }

                        string where = string.Join(" AND ",
                            columnasDestino.Select((col, idx) =>
                                valoresConvertidos[idx] == DBNull.Value ? $"[{col}] IS NULL" : $"[{col}] = @w{idx}"));

                        using (SqlCommand check = new SqlCommand($"SELECT COUNT(*) FROM base_dashboard WHERE {where}", conn))
                        {
                            for (int i = 0; i < columnasDestino.Count; i++)
                                if (valoresConvertidos[i] != DBNull.Value)
                                    check.Parameters.AddWithValue($"@w{i}", valoresConvertidos[i]);

                            int existe = (int)check.ExecuteScalar();
                            if (existe > 0)
                            {
                                duplicados++;
                                continue;
                            }
                        }

                        string columnasSQL = string.Join(",", columnasDestino.Select(c => $"[{c}]"));
                        string valoresSQL = string.Join(",", columnasDestino.Select((c, i) => $"@p{i}"));

                        using (SqlCommand insert = new SqlCommand($"INSERT INTO base_dashboard ({columnasSQL}) VALUES ({valoresSQL})", conn))
                        {
                            for (int i = 0; i < columnasDestino.Count; i++)
                                insert.Parameters.AddWithValue($"@p{i}", valoresConvertidos[i]);

                            insert.ExecuteNonQuery();
                            insertados++;
                        }

                        progressBarSubida.Value = Math.Min(progressBarSubida.Maximum, filaIndex + 1);
                        Application.DoEvents();
                    }

                    MessageBox.Show($"✔️ Carga completa.\n✅ Insertados: {insertados}\n🔁 Duplicados ignorados: {duplicados}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("💥 Error al subir QR: " + ex.Message);
            }
            finally
            {
                btnSubirQR.Enabled = true;
                btnSeleccionarExcel.Enabled = true;
                progressBarSubida.Visible = false;
            }
        }




        private object TryParseFecha(object valor)
        {
            if (valor == null) return DBNull.Value;
            if (DateTime.TryParse(valor.ToString(), out DateTime dt))
                return dt;
            return DBNull.Value;
        }

        private object TryParseFloat(object valor)
        {
            if (valor == null) return DBNull.Value;
            if (float.TryParse(valor.ToString(), out float f))
                return f;
            return DBNull.Value;
        }

        private object TryParseInt(object valor)
        {
            if (valor == null) return DBNull.Value;
            if (int.TryParse(valor.ToString(), out int i))
                return i;
            return DBNull.Value;
        }

        private object TryParseMoney(object valor)
        {
            if (valor == null || valor == DBNull.Value) return DBNull.Value;

            string val = valor.ToString().Trim();

            // Filtrar valores vacíos, símbolos, espacios u objetos vacíos
            if (string.IsNullOrWhiteSpace(val) || val == "{}" || val.ToLower().Contains("na") || val.Contains("--"))
                return DBNull.Value;

            // Quitar símbolos de miles (.) y reemplazar , por . (coma decimal)
            val = val.Replace(".", "").Replace(",", ".");

            // Intentar conversión a decimal
            if (decimal.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal d))
                return d;

            // Si no se puede convertir, devolver null
            return DBNull.Value;
        }




    }
}
