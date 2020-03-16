namespace AbstractIceCreamShopView
{
    partial class FormReportIceCreamtComponents
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column_component = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_IceCream = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_component,
            this.Column_IceCream,
            this.Column_count});
            this.dataGridView.Location = new System.Drawing.Point(21, 59);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(620, 308);
            this.dataGridView.TabIndex = 0;
            // 
            // Column_component
            // 
            this.Column_component.HeaderText = "Компонент";
            this.Column_component.Name = "Column_component";
            this.Column_component.Width = 200;
            // 
            // Column_IceCream
            // 
            this.Column_IceCream.HeaderText = "Мороженое";
            this.Column_IceCream.Name = "Column_IceCream";
            this.Column_IceCream.Width = 200;
            // 
            // Column_count
            // 
            this.Column_count.HeaderText = "Количество";
            this.Column_count.Name = "Column_count";
            this.Column_count.Width = 200;
            // 
            // buttonSaveExcel
            // 
            this.buttonSaveExcel.Location = new System.Drawing.Point(30, 12);
            this.buttonSaveExcel.Name = "buttonSaveExcel";
            this.buttonSaveExcel.Size = new System.Drawing.Size(156, 23);
            this.buttonSaveExcel.TabIndex = 1;
            this.buttonSaveExcel.Text = "Сохранить в Excel";
            this.buttonSaveExcel.UseVisualStyleBackColor = true;
            this.buttonSaveExcel.Click += new System.EventHandler(this.buttonSaveExcel_Click);
            // 
            // FormReportIceCreamtComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 379);
            this.Controls.Add(this.buttonSaveExcel);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormReportIceCreamtComponents";
            this.Text = "FormReportIceCreamComponents";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_component;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_IceCream;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_count;
        private System.Windows.Forms.Button buttonSaveExcel;
    }
}