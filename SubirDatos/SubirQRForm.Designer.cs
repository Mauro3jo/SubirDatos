namespace SubirDatos
{
    partial class SubirQRForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSeleccionarExcel = new System.Windows.Forms.Button();
            this.btnSubirQR = new System.Windows.Forms.Button();
            this.dataGridViewQR = new System.Windows.Forms.DataGridView();
            this.lblCantidadFilas = new System.Windows.Forms.Label();
            this.progressBarSubida = new System.Windows.Forms.ProgressBar();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQR)).BeginInit();
            this.SuspendLayout();

            // 
            // btnSeleccionarExcel
            // 
            this.btnSeleccionarExcel.Location = new System.Drawing.Point(12, 12);
            this.btnSeleccionarExcel.Size = new System.Drawing.Size(150, 30);
            this.btnSeleccionarExcel.Text = "Seleccionar Excel";
            this.btnSeleccionarExcel.UseVisualStyleBackColor = true;
            this.btnSeleccionarExcel.Click += new System.EventHandler(this.btnSeleccionarExcel_Click);

            // 
            // btnSubirQR
            // 
            this.btnSubirQR.Location = new System.Drawing.Point(168, 12);
            this.btnSubirQR.Size = new System.Drawing.Size(150, 30);
            this.btnSubirQR.Text = "Subir QR a BD";
            this.btnSubirQR.UseVisualStyleBackColor = true;
            this.btnSubirQR.Click += new System.EventHandler(this.btnSubirQR_Click);

            // 
            // lblCantidadFilas
            // 
            this.lblCantidadFilas.Location = new System.Drawing.Point(330, 18);
            this.lblCantidadFilas.Size = new System.Drawing.Size(200, 20);
            this.lblCantidadFilas.Text = "Filas: 0";

            // 
            // progressBarSubida
            // 
            this.progressBarSubida.Location = new System.Drawing.Point(540, 18);
            this.progressBarSubida.Size = new System.Drawing.Size(200, 20);
            this.progressBarSubida.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarSubida.Visible = false;

            // 
            // dataGridViewQR
            // 
            this.dataGridViewQR.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewQR.Size = new System.Drawing.Size(960, 400);
            this.dataGridViewQR.ReadOnly = true;
            this.dataGridViewQR.AllowUserToAddRows = false;
            this.dataGridViewQR.AllowUserToDeleteRows = false;
            this.dataGridViewQR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // 
            // SubirQRForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.btnSeleccionarExcel);
            this.Controls.Add(this.btnSubirQR);
            this.Controls.Add(this.lblCantidadFilas);
            this.Controls.Add(this.progressBarSubida);
            this.Controls.Add(this.dataGridViewQR);
            this.Name = "SubirQRForm";
            this.Text = "Subir QR";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQR)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnSeleccionarExcel;
        private System.Windows.Forms.Button btnSubirQR;
        private System.Windows.Forms.DataGridView dataGridViewQR;
        private System.Windows.Forms.Label lblCantidadFilas;
        private System.Windows.Forms.ProgressBar progressBarSubida;
    }
}
