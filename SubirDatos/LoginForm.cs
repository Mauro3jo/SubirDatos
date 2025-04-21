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

            // 👇 ComboBox ya no se usa, así que esto se elimina o se comenta
            //cmbBaseDatos.Items.Add("DemoDB");
            //cmbBaseDatos.Items.Add("ProdDB");
            //cmbBaseDatos.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtUsuario.Text.Trim();
            string contraseñaIngresada = txtContraseña.Text.Trim();

            // Siempre usar ProdDB directamente
            string selectedDB = "ProdDB";

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

                    string query = @"SELECT Usuario, EsAdmin 
                             FROM UsuariosSubirBase 
                             WHERE LOWER(Usuario) = LOWER(@Usuario) 
                             AND RTRIM(password) = RTRIM(@Password)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuarioIngresado.ToLower());
                        cmd.Parameters.AddWithValue("@Password", contraseñaIngresada);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nombreUsuario = reader["Usuario"].ToString();
                                bool esAdmin = Convert.ToBoolean(reader["EsAdmin"]);

                                MessageBox.Show($"✅ Login exitoso. Bienvenido {nombreUsuario}.\nConectado a: {selectedDB}");
                                this.Hide();

                                // Le pasamos también el flag de admin al MainForm
                                MainForm mainForm = new MainForm(connectionString, esAdmin);
                                mainForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("❌ Usuario o contraseña incorrectos.");
                            }
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
