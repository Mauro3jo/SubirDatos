using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SubirDatos
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Agregar opciones al ComboBox
            cmbBaseDatos.Items.Add("DemoDB");
            cmbBaseDatos.Items.Add("ProdDB");
            cmbBaseDatos.SelectedIndex = 0; // Por defecto, apunta a la base local
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string contraseñaIngresada = txtContraseña.Text.Trim();
            string selectedDB = cmbBaseDatos.SelectedItem.ToString();

            // Obtener connection string desde Config
            string connectionString = Config.GetConnectionString(selectedDB);

            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show($"❌ No se encontró la cadena de conexión para '{selectedDB}'.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT Usuario 
                                     FROM UsuariosSubirBase 
                                     WHERE LOWER(Usuario) = LOWER(@Usuario) 
                                     AND RTRIM(password) = RTRIM(@Password)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuarioIngresado.ToLower());
                        cmd.Parameters.AddWithValue("@Password", contraseñaIngresada);

                        var usuarioEncontrado = cmd.ExecuteScalar();

                        if (usuarioEncontrado != null)
                        {
                            MessageBox.Show($"✅ Login exitoso. Bienvenido {usuarioEncontrado}.\nConectado a: {selectedDB}");
                            this.Hide();

                            MainForm mainForm = new MainForm(connectionString);
                            mainForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("❌ Usuario o contraseña incorrectos.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error en la conexión: " + ex.Message);
                }
            }
        }
    }
}
