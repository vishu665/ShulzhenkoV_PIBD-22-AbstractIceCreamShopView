using AbstractIceCreamShopBusinessLogic.ViewModels;
using AbstractIceCreamShopBusinessLogic.Interfaces;
using AbstractIceCreamShopBusinessLogic.BindingModels;
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
    public partial class FormIceCream : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IIceCreamLogic logic;
        private int? id;
        private List<IceCreamComponentViewModel> iceCreamComponents;
        public FormIceCream(IIceCreamLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormIceCream_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    IceCreamViewModel view = logic.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.IceCreamName;
                        textBoxPrice.Text = view.Price.ToString();
                        iceCreamComponents = view.IceCreamComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                iceCreamComponents = new List<IceCreamComponentViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (iceCreamComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = iceCreamComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                   DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIceCreamComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.ModelView != null)
                {
                    if (id.HasValue)
                    {
                        form.ModelView.IceCreamId = id.Value;
                    }
                    iceCreamComponents.Add(form.ModelView);
                }
                LoadData();
            }

        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormIceCreamComponent>();
                form.ModelView =
               iceCreamComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    iceCreamComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.ModelView;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        iceCreamComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }

        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (iceCreamComponents == null || iceCreamComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<IceCreamComponentBindingModel> icecreamComponentBM = new List<IceCreamComponentBindingModel>();
                for (int i = 0; i < iceCreamComponents.Count; ++i)
                {
                    icecreamComponentBM.Add(new IceCreamComponentBindingModel
                    {
                        Id = iceCreamComponents[i].Id,
                        IceCreamId = iceCreamComponents[i].IceCreamId,
                        ComponentId = iceCreamComponents[i].ComponentId,
                        Count = iceCreamComponents[i].Count
                    });
                }
                if (id.HasValue)
                {
                    logic.UpdElement(new IceCreamBindingModel
                    {
                        Id = id.Value,
                        IceCreamName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        iceCreamComponents = icecreamComponentBM
                    });
                }
                else
                {
                    logic.AddElement(new IceCreamBindingModel
                    {
                        IceCreamName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        iceCreamComponents = icecreamComponentBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
