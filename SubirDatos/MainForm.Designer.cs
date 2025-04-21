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
            this.btnSubirPendientes = new System.Windows.Forms.Button();
            this.btnSubirQR = new System.Windows.Forms.Button();
            this.btnAcumularQR = new System.Windows.Forms.Button();
            this.btnJuntarCSV = new System.Windows.Forms.Button();

            this.lblTotalBrutoHoy = new System.Windows.Forms.Label();
            this.lblProximoBruto = new System.Windows.Forms.Label();
            this.lblPagoHoy = new System.Windows.Forms.Label();
            this.lblProximoPago = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // lblTotalBrutoHoy
            this.lblTotalBrutoHoy.AutoSize = true;
            this.lblTotalBrutoHoy.Location = new System.Drawing.Point(50, 20);
            this.lblTotalBrutoHoy.Name = "lblTotalBrutoHoy";
            this.lblTotalBrutoHoy.Size = new System.Drawing.Size(140, 16);
            this.lblTotalBrutoHoy.Text = "Total Bruto Hoy: $0.00";

            // lblProximoBruto
            this.lblProximoBruto.AutoSize = true;
            this.lblProximoBruto.Location = new System.Drawing.Point(50, 45);
            this.lblProximoBruto.Name = "lblProximoBruto";
            this.lblProximoBruto.Size = new System.Drawing.Size(145, 16);
            this.lblProximoBruto.Text = "Próximo Bruto: $0.00";

            // lblPagoHoy
            this.lblPagoHoy.AutoSize = true;
            this.lblPagoHoy.Location = new System.Drawing.Point(50, 70);
            this.lblPagoHoy.Name = "lblPagoHoy";
            this.lblPagoHoy.Size = new System.Drawing.Size(116, 16);
            this.lblPagoHoy.Text = "Pago Hoy: $0.00";

            // lblProximoPago
            this.lblProximoPago.AutoSize = true;
            this.lblProximoPago.Location = new System.Drawing.Point(50, 95);
            this.lblProximoPago.Name = "lblProximoPago";
            this.lblProximoPago.Size = new System.Drawing.Size(131, 16);
            this.lblProximoPago.Text = "Próximo Pago: $0.00";

            //// btnSubirPendientes
            //this.btnSubirPendientes.Location = new System.Drawing.Point(50, 130);
            //this.btnSubirPendientes.Name = "btnSubirPendientes";
            //this.btnSubirPendientes.Size = new System.Drawing.Size(200, 35);
            //this.btnSubirPendientes.Text = "Subir Datos Pendientes";
            //this.btnSubirPendientes.UseVisualStyleBackColor = true;

            // btnSubirQR
            this.btnSubirQR.Location = new System.Drawing.Point(50, 180);
            this.btnSubirQR.Name = "btnSubirQR";
            this.btnSubirQR.Size = new System.Drawing.Size(200, 35);
            this.btnSubirQR.Text = "Subir QR";
            this.btnSubirQR.UseVisualStyleBackColor = true;
            this.btnSubirQR.Click += new System.EventHandler(this.btnSubirQR_Click);

            //// btnAcumularQR
            //this.btnAcumularQR.Location = new System.Drawing.Point(50, 230);
            //this.btnAcumularQR.Name = "btnAcumularQR";
            //this.btnAcumularQR.Size = new System.Drawing.Size(200, 35);
            //this.btnAcumularQR.Text = "Acumular QR (CSV)";
            //this.btnAcumularQR.UseVisualStyleBackColor = true;

            // btnJuntarCSV
            this.btnJuntarCSV.Location = new System.Drawing.Point(50, 280);
            this.btnJuntarCSV.Name = "btnJuntarCSV";
            this.btnJuntarCSV.Size = new System.Drawing.Size(200, 35);
            this.btnJuntarCSV.Text = "Juntar CSV";
            this.btnJuntarCSV.UseVisualStyleBackColor = true;
            this.btnJuntarCSV.Click += new System.EventHandler(this.btnJuntarCSV_Click);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblTotalBrutoHoy);
            this.Controls.Add(this.lblProximoBruto);
            this.Controls.Add(this.lblPagoHoy);
            this.Controls.Add(this.lblProximoPago);
            this.Controls.Add(this.btnJuntarCSV);
            this.Controls.Add(this.btnAcumularQR);
            this.Controls.Add(this.btnSubirQR);
            this.Controls.Add(this.btnSubirPendientes);
            this.Name = "MainForm";
            this.Text = "Menú Principal";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSubirPendientes;
        private System.Windows.Forms.Button btnSubirQR;
        private System.Windows.Forms.Button btnAcumularQR;
        private System.Windows.Forms.Button btnJuntarCSV;

        private System.Windows.Forms.Label lblTotalBrutoHoy;
        private System.Windows.Forms.Label lblProximoBruto;
        private System.Windows.Forms.Label lblPagoHoy;
        private System.Windows.Forms.Label lblProximoPago;
    }
}
