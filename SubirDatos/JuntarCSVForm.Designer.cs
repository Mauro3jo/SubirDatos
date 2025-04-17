namespace SubirDatos
{
    partial class JuntarCSVForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnSeleccionarCSV = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnRecortarCSV = new System.Windows.Forms.Button();
            this.btnExportarRecortadoExcel = new System.Windows.Forms.Button();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.lblCantidadFilas = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            this.SuspendLayout();

            this.btnSeleccionarCSV.Location = new System.Drawing.Point(12, 12);
            this.btnSeleccionarCSV.Size = new System.Drawing.Size(180, 30);
            this.btnSeleccionarCSV.Text = "Seleccionar CSVs";
            this.btnSeleccionarCSV.Click += new System.EventHandler(this.btnSeleccionarCSV_Click);

            this.btnExportarExcel.Location = new System.Drawing.Point(198, 12);
            this.btnExportarExcel.Size = new System.Drawing.Size(180, 30);
            this.btnExportarExcel.Text = "Exportar Excel Completo";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);

            this.btnRecortarCSV.Location = new System.Drawing.Point(384, 12);
            this.btnRecortarCSV.Size = new System.Drawing.Size(180, 30);
            this.btnRecortarCSV.Text = "Recortar Excel";
            this.btnRecortarCSV.Click += new System.EventHandler(this.btnRecortarCSV_Click);

            this.btnExportarRecortadoExcel.Location = new System.Drawing.Point(570, 12);
            this.btnExportarRecortadoExcel.Size = new System.Drawing.Size(200, 30);
            this.btnExportarRecortadoExcel.Text = "Exportar Excel Recortado";
            this.btnExportarRecortadoExcel.Click += new System.EventHandler(this.btnExportarRecortadoExcel_Click);

            this.dataGridViewDatos.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewDatos.Size = new System.Drawing.Size(960, 380);
            this.dataGridViewDatos.ReadOnly = true;
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;

            this.lblCantidadFilas.Location = new System.Drawing.Point(780, 20);
            this.lblCantidadFilas.AutoSize = true;
            this.lblCantidadFilas.Text = "Filas: 0";

            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.btnSeleccionarCSV);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnRecortarCSV);
            this.Controls.Add(this.btnExportarRecortadoExcel);
            this.Controls.Add(this.dataGridViewDatos);
            this.Controls.Add(this.lblCantidadFilas);
            this.Text = "Juntar Archivos CSV";

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionarCSV;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnRecortarCSV;
        private System.Windows.Forms.Button btnExportarRecortadoExcel;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.Label lblCantidadFilas;
    }
}
