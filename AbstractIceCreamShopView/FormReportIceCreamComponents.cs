﻿using AbstractIceCreamShopBusinessLogic.BindingModels;
using AbstractIceCreamShopBusinessLogic.BusinessLogics;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractIceCreamShopView
{
    public partial class FormReportIceCreamComponents : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;

        public FormReportIceCreamComponents(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSaveToPDF_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveProductComponentsToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                        });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
        private void reportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetIceCreamComponent();
                ReportDataSource source = new ReportDataSource("DataSetIceCreamComponent", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
