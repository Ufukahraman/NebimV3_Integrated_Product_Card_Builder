
using System.Configuration;
using System;
using System.Data;
using System.Windows.Forms;


namespace B2CSeller_MappingData
{
    public partial class Form1 : Form
    {

        private static readonly SelectQueries select = new SelectQueries();
        private static readonly Sql dbHelper = new Sql(ConfigurationManager.ConnectionStrings["DB_CON"].ConnectionString);

        public Form1()
        {
            InitializeComponent();
            LoadData();
            this.WindowState = FormWindowState.Maximized; // Tam ekran yap
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranın ortasında başlat
        }

        private void LoadData()
        {
            var colorMapping = dbHelper.ExecuteQuery(select.ColorMapping, null);
            var hierarchyMapping = dbHelper.ExecuteQuery(select.HierarchyMapping, null);
            var productMapping = dbHelper.ExecuteQuery(select.ProductMapping, null);
            var dim1Mapping = dbHelper.ExecuteQuery(select.Dim1Mapping, null);

            SetupDataGridView(dataGridView1, colorMapping);
            SetupDataGridView(dataGridView2, hierarchyMapping);
            SetupDataGridView(dataGridView3, productMapping);
            SetupDataGridView(dataGridView4, dim1Mapping);
        }

        private void SetupDataGridView(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.DataSource = dataTable;
            dataGridView.AutoGenerateColumns = true;

            if (dataGridView.Columns["UpdateButton"] == null)
            {
                AddCustomButtonColumn(dataGridView);
            }


            dataGridView.CellContentClick -= DataGridView_CellContentClick;
            dataGridView.CellContentClick += DataGridView_CellContentClick;
        }

        private void AddCustomButtonColumn(DataGridView dataGridView)
        {
            var updateButtonColumn = new DataGridViewButtonColumn
            {
                Name = "UpdateButton",
                HeaderText = "Güncelle",
                Text = "Güncelle",
                UseColumnTextForButtonValue = true
            };

            dataGridView.Columns.Add(updateButtonColumn);
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var dataGridView = sender as DataGridView;
            if (dataGridView?.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                HandleUpdateButtonClick(dataGridView, e.RowIndex);
            }
        }

        private void HandleUpdateButtonClick(DataGridView dataGridView, int rowIndex)
        {
            DataTable dataToLoad = null;
            string[] selectedColumns = null;
            string filteredColumn = null;

            if (dataGridView == dataGridView1)
            {
                dataToLoad = dbHelper.ExecuteQuery(select.ColorData, null);
                selectedColumns = new[] { "ID", "Color", "ColorCode", "ErpCode", "ColorDescription" };
                filteredColumn = "ColorDescription";
            }
            else if (dataGridView == dataGridView2)
            {
                dataToLoad = dbHelper.ExecuteQuery(select.HierarchyData, null);
                selectedColumns = new[]
                {
                    "ID", "MainCategory", "SubCategory", "ErpHierarchyId",
                    "ProductHierarchyId", "Cinsiyet Grubu",
                    "Yaş Grubu", "Ürün Üst Grubu",
                    "Ürün Grubu", "Kategori"
                };
                filteredColumn = "Kategori";
            }
            else if (dataGridView == dataGridView4)
            {
                dataToLoad = dbHelper.ExecuteQuery(select.Dim1Data, null);
                selectedColumns = new[]
                {
                    "ID", "ItemDim1Code","ErpCode"
                };
                filteredColumn = "ItemDim1Code";
            }
            else if (dataGridView == dataGridView3)
            {

                selectedColumns = new[] { "ID", "ItemCode", "ItemDescription", "MainCategory", "SubCategory", "Brand" };
                var form3 = new Form3();
                form3.LoadData( selectedColumns);

                var selectedRow = dataGridView.Rows[rowIndex];
                form3.LoadRowData(selectedRow);

                form3.FormClosed += (s, args) => LoadData();
                form3.ShowDialog();
            }

            if (dataToLoad != null)
            {
                var form2 = new Form2();
                form2.LoadData(dataToLoad, selectedColumns, filteredColumn);

                var selectedRow = dataGridView.Rows[rowIndex];
                form2.LoadRowData(selectedRow);

                form2.FormClosed += (s, args) => LoadData();
                form2.ShowDialog();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
