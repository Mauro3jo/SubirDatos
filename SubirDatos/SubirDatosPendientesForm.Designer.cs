namespace SubirDatos
{
    partial class SubirDatosPendientesForm
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
            this.btnSubirDatos = new System.Windows.Forms.Button();
            this.lblCantidadFilas = new System.Windows.Forms.Label();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar(); // ← barra de progreso

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            this.SuspendLayout();

            // btnSeleccionarExcel
            this.btnSeleccionarExcel.Location = new System.Drawing.Point(12, 12);
            this.btnSeleccionarExcel.Name = "btnSeleccionarExcel";
            this.btnSeleccionarExcel.Size = new System.Drawing.Size(150, 30);
            this.btnSeleccionarExcel.TabIndex = 0;
            this.btnSeleccionarExcel.Text = "Seleccionar Excel";
            this.btnSeleccionarExcel.UseVisualStyleBackColor = true;
            this.btnSeleccionarExcel.Click += new System.EventHandler(this.btnSeleccionarExcel_Click);

            // btnRecortarExcel
            this.btnRecortarExcel.Location = new System.Drawing.Point(168, 12);
            this.btnRecortarExcel.Name = "btnRecortarExcel";
            this.btnRecortarExcel.Size = new System.Drawing.Size(150, 30);
            this.btnRecortarExcel.TabIndex = 1;
            this.btnRecortarExcel.Text = "Recortar Excel";
            this.btnRecortarExcel.UseVisualStyleBackColor = true;
            this.btnRecortarExcel.Click += new System.EventHandler(this.btnRecortarExcel_Click);

            // btnSubirDatos
            this.btnSubirDatos.Location = new System.Drawing.Point(324, 12);
            this.btnSubirDatos.Name = "btnSubirDatos";
            this.btnSubirDatos.Size = new System.Drawing.Size(150, 30);
            this.btnSubirDatos.TabIndex = 2;
            this.btnSubirDatos.Text = "Subir a Base de Datos";
            this.btnSubirDatos.UseVisualStyleBackColor = true;
            this.btnSubirDatos.Click += new System.EventHandler(this.btnSubirDatos_Click);

            // lblCantidadFilas
            this.lblCantidadFilas.Location = new System.Drawing.Point(480, 18);
            this.lblCantidadFilas.Name = "lblCantidadFilas";
            this.lblCantidadFilas.Size = new System.Drawing.Size(200, 20);
            this.lblCantidadFilas.TabIndex = 3;
            this.lblCantidadFilas.Text = "Filas: 0";

            // dataGridViewDatos
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;
            this.dataGridViewDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatos.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewDatos.Name = "dataGridViewDatos";
            this.dataGridViewDatos.ReadOnly = true;
            this.dataGridViewDatos.Size = new System.Drawing.Size(960, 360);
            this.dataGridViewDatos.TabIndex = 4;

            // progressBar
            this.progressBar.Location = new System.Drawing.Point(12, 420);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(960, 23);
            this.progressBar.TabIndex = 5;
            this.progressBar.Visible = false;

            // SubirDatosPendientesForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblCantidadFilas);
            this.Controls.Add(this.dataGridViewDatos);
            this.Controls.Add(this.btnSubirDatos);
            this.Controls.Add(this.btnRecortarExcel);
            this.Controls.Add(this.btnSeleccionarExcel);
            this.Name = "SubirDatosPendientesForm";
            this.Text = "Subir Datos Pendientes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionarExcel;
        private System.Windows.Forms.Button btnRecortarExcel;
        private System.Windows.Forms.Button btnSubirDatos;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.Label lblCantidadFilas;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
