namespace SubirDatos
{
    partial class SubirQRMultipleForm
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
            this.btnSeleccionarExcel = new System.Windows.Forms.Button();
            this.btnRecortarExcel = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.lblCantidadFilas = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            this.SuspendLayout();

            // 
            // btnSeleccionarExcel
            // 
            this.btnSeleccionarExcel.Location = new System.Drawing.Point(12, 12);
            this.btnSeleccionarExcel.Name = "btnSeleccionarExcel";
            this.btnSeleccionarExcel.Size = new System.Drawing.Size(160, 30);
            this.btnSeleccionarExcel.TabIndex = 0;
            this.btnSeleccionarExcel.Text = "Seleccionar Excel";
            this.btnSeleccionarExcel.UseVisualStyleBackColor = true;
            this.btnSeleccionarExcel.Click += new System.EventHandler(this.btnSeleccionarExcel_Click);

            // 
            // btnRecortarExcel
            // 
            this.btnRecortarExcel.Location = new System.Drawing.Point(178, 12);
            this.btnRecortarExcel.Name = "btnRecortarExcel";
            this.btnRecortarExcel.Size = new System.Drawing.Size(160, 30);
            this.btnRecortarExcel.TabIndex = 1;
            this.btnRecortarExcel.Text = "Recortar Datos";
            this.btnRecortarExcel.UseVisualStyleBackColor = true;
            this.btnRecortarExcel.Click += new System.EventHandler(this.btnRecortarExcel_Click);

            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Location = new System.Drawing.Point(344, 12);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(160, 30);
            this.btnExportarExcel.TabIndex = 2;
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = true;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);

            // 
            // lblCantidadFilas
            // 
            this.lblCantidadFilas.AutoSize = true;
            this.lblCantidadFilas.Location = new System.Drawing.Point(510, 20);
            this.lblCantidadFilas.Name = "lblCantidadFilas";
            this.lblCantidadFilas.Size = new System.Drawing.Size(85, 16);
            this.lblCantidadFilas.TabIndex = 3;
            this.lblCantidadFilas.Text = "Filas: 0";

            // 
            // dataGridViewDatos
            // 
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;
            this.dataGridViewDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatos.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewDatos.Name = "dataGridViewDatos";
            this.dataGridViewDatos.ReadOnly = true;
            this.dataGridViewDatos.Size = new System.Drawing.Size(960, 360);
            this.dataGridViewDatos.TabIndex = 4;

            // 
            // SubirQRMultipleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.dataGridViewDatos);
            this.Controls.Add(this.lblCantidadFilas);
            this.Controls.Add(this.btnExportarExcel);
            this.Controls.Add(this.btnRecortarExcel);
            this.Controls.Add(this.btnSeleccionarExcel);
            this.Name = "SubirQRMultipleForm";
            this.Text = "Acumular QR desde Excel";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionarExcel;
        private System.Windows.Forms.Button btnRecortarExcel;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.Label lblCantidadFilas;
    }
}
