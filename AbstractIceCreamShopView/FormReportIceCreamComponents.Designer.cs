namespace AbstractIceCreamShopView
{
    partial class FormReportIceCreamComponents
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ButtonSaveToPDF = new System.Windows.Forms.Button();
            this.ReportIceCreamComponentViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportIceCreamComponentViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSetIceCreamComponent";
            reportDataSource1.Value = this.ReportIceCreamComponentViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbstractIceCreamShopView.ReportIceCreamComponent.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(44, 36);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(2);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(712, 409);
            this.reportViewer.TabIndex = 7;
            // 
            // ButtonSaveToPDF
            // 
            this.ButtonSaveToPDF.Location = new System.Drawing.Point(51, 5);
            this.ButtonSaveToPDF.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSaveToPDF.Name = "ButtonSaveToPDF";
            this.ButtonSaveToPDF.Size = new System.Drawing.Size(146, 24);
            this.ButtonSaveToPDF.TabIndex = 8;
            this.ButtonSaveToPDF.Text = "Сохранить  в PDF";
            this.ButtonSaveToPDF.UseVisualStyleBackColor = true;
            this.ButtonSaveToPDF.Click += new System.EventHandler(this.ButtonSaveToPDF_Click);
            // 
            // ReportIceCreamComponentViewModelBindingSource
            // 
            this.ReportIceCreamComponentViewModelBindingSource.DataSource = typeof(AbstractIceCreamShopBusinessLogic.ViewModels.ReportIceCreamComponentViewModel);
            // 
            // FormReportIceCreamComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.ButtonSaveToPDF);
            this.Name = "FormReportIceCreamComponents";
            this.Text = "Мороженое и компоненты";
            this.Load += new System.EventHandler(this.reportViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportIceCreamComponentViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button ButtonSaveToPDF;
        private System.Windows.Forms.BindingSource ReportIceCreamComponentViewModelBindingSource;
    }
}