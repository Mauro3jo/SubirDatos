namespace SubirDatos
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblBaseDatos = new System.Windows.Forms.Label();
            this.btnSubirPendientes = new System.Windows.Forms.Button();
            this.btnSubirQR = new System.Windows.Forms.Button();
            this.btnAcumularQR = new System.Windows.Forms.Button(); // Nuevo botón

            this.SuspendLayout();

            // lblBaseDatos
            this.lblBaseDatos.AutoSize = true;
            this.lblBaseDatos.Location = new System.Drawing.Point(50, 30);
            this.lblBaseDatos.Name = "lblBaseDatos";
            this.lblBaseDatos.Size = new System.Drawing.Size(140, 16);
            this.lblBaseDatos.TabIndex = 0;
            this.lblBaseDatos.Text = "Conectado a: Ninguno";

            // btnSubirPendientes
            this.btnSubirPendientes.Location = new System.Drawing.Point(50, 70);
            this.btnSubirPendientes.Name = "btnSubirPendientes";
            this.btnSubirPendientes.Size = new System.Drawing.Size(200, 35);
            this.btnSubirPendientes.TabIndex = 1;
            this.btnSubirPendientes.Text = "Subir Datos Pendientes";
            this.btnSubirPendientes.UseVisualStyleBackColor = true;
            this.btnSubirPendientes.Click += new System.EventHandler(this.btnSubirPendientes_Click);

            // btnSubirQR
            this.btnSubirQR.Location = new System.Drawing.Point(50, 120);
            this.btnSubirQR.Name = "btnSubirQR";
            this.btnSubirQR.Size = new System.Drawing.Size(200, 35);
            this.btnSubirQR.TabIndex = 2;
            this.btnSubirQR.Text = "Subir QR";
            this.btnSubirQR.UseVisualStyleBackColor = true;
            this.btnSubirQR.Click += new System.EventHandler(this.btnSubirQR_Click);

            // btnAcumularQR (nuevo botón)
            this.btnAcumularQR.Location = new System.Drawing.Point(50, 170);
            this.btnAcumularQR.Name = "btnAcumularQR";
            this.btnAcumularQR.Size = new System.Drawing.Size(200, 35);
            this.btnAcumularQR.TabIndex = 3;
            this.btnAcumularQR.Text = "Acumular QR (CSV)";
            this.btnAcumularQR.UseVisualStyleBackColor = true;
            this.btnAcumularQR.Click += new System.EventHandler(this.btnAcumularQR_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 240);
            this.Controls.Add(this.btnAcumularQR);
            this.Controls.Add(this.btnSubirQR);
            this.Controls.Add(this.btnSubirPendientes);
            this.Controls.Add(this.lblBaseDatos);
            this.Name = "MainForm";
            this.Text = "Menú Principal";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblBaseDatos;
        private System.Windows.Forms.Button btnSubirPendientes;
        private System.Windows.Forms.Button btnSubirQR;
        private System.Windows.Forms.Button btnAcumularQR; // Declaración del nuevo botón
    }
}
