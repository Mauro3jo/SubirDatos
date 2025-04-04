using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace SubirDatos
{
    public static class Config
    {
        private static JObject config;

        static Config()
        {
            try
            {
                // Asegura que el archivo se lea desde el mismo directorio del ejecutable
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

                if (!File.Exists(path))
                {
                    MessageBox.Show($"El archivo de configuración no fue encontrado en: {path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    config = new JObject();
                    return;
                }

                string json = File.ReadAllText(path);
                config = JObject.Parse(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo de configuración:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                config = new JObject();
            }
        }

        public static string GetConnectionString(string key)
        {
            return config["ConnectionStrings"]?[key]?.ToString();
        }
    }
}
