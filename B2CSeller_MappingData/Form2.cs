using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace B2CSeller_MappingData
{
    public partial class Form2 : Form
    {
        private static readonly UpdateQueries up = new UpdateQueries();
        private static readonly Sql dbHelper = new Sql(ConfigurationManager.ConnectionStrings["DB_CON"].ConnectionString);

        private DataTable _data;
        private string[] _selectedColumns = Array.Empty<string>();
        private string _filteredColumn;

        public Form2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Tam ekran yap
            this.StartPosition = FormStartPosition.CenterScreen; // Ekranın ortasında başlat
        }

        public void LoadData(DataTable data, string[] selectedColumns, string filteredColumn)
        {
            _data = data;
            _selectedColumns = selectedColumns;
            _filteredColumn = filteredColumn;
            MainView.DataSource = _data;
        }

        public void LoadRowData(DataGridViewRow row)
        {
            var dataTable = new DataTable();

            foreach (var columnName in _selectedColumns)
            {
                if (row.Cells[columnName] != null)
                {
                    dataTable.Columns.Add(columnName);
                }
            }

            var dataRow = dataTable.NewRow();
            foreach (var columnName in _selectedColumns)
            {
                if (row.Cells[columnName] != null)
                {
                    dataRow[columnName] = row.Cells[columnName].Value;
                }
            }

            dataTable.Rows.Add(dataRow);
            HeaderView.DataSource = dataTable;
        }

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            if (_data == null) return;

            var filterText = SearchText.Text;
            if (string.IsNullOrEmpty(filterText))
            {
                MainView.DataSource = _data;
            }
            else
            {
                var dv = _data.DefaultView;
                dv.RowFilter = $"{_filteredColumn} LIKE '%{filterText}%'";
                MainView.DataSource = dv;
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (MainView.SelectedRows.Count == 0 || HeaderView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen her iki DataGridView'dan bir satır seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedRow2 = HeaderView.SelectedRows[0];
            var id = selectedRow2.Cells["ID"].Value.ToString();

            var selectedRow1 = MainView.SelectedRows[0];

            if (_filteredColumn == "ColorDescription")
            {
                UpdateColor(selectedRow1, selectedRow2, id);
            }
            else if (_filteredColumn == "Kategori")
            {
                UpdateProductHierarchy(selectedRow1, selectedRow2, id);

            }
            else if (_filteredColumn == "ItemDim1Code")
            {
                UpdateDim1(selectedRow1, selectedRow2, id);

            }
            else if (_filteredColumn == "ProductHierarchyLevel05")
            {
                UpdateAttributes(selectedRow1, selectedRow2, id);

            }


            MessageBox.Show("Satır güncellendi ve veritabanına kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void UpdateColor(DataGridViewRow selectedRow1, DataGridViewRow selectedRow2, string id)
        {
            var newColorCode = selectedRow1.Cells["ColorCode"].Value.ToString();
            var newColorDescription = selectedRow1.Cells["ColorDescription"].Value.ToString();

            selectedRow2.Cells["ErpCode"].Value = newColorCode;
            selectedRow2.Cells["ColorDescription"].Value = newColorDescription;

            var parameters = new Dictionary<string, object>
            {
                { "@ErpCode", newColorCode },
                { "@ID", id }
            };

            dbHelper.ExecuteNonQuery(up.ColorUpdate, parameters);
        }

        private void UpdateProductHierarchy(DataGridViewRow selectedRow1, DataGridViewRow selectedRow2, string id)
        {
            var newProductHierarchyId = selectedRow1.Cells["ProductHierarchyId"].Value.ToString();
            var newProductHierarchyLevel01 = selectedRow1.Cells["Cinsiyet Grubu"].Value.ToString();
            var newProductHierarchyLevel02 = selectedRow1.Cells["Yaş Grubu"].Value.ToString();
            var newProductHierarchyLevel03 = selectedRow1.Cells["Ürün Üst Grubu"].Value.ToString();
            var newProductHierarchyLevel04 = selectedRow1.Cells["Ürün Grubu"].Value.ToString();
            var newProductHierarchyLevel05 = selectedRow1.Cells["Kategori"].Value.ToString();

            selectedRow2.Cells["ProductHierarchyId"].Value = newProductHierarchyId;
            selectedRow2.Cells["Cinsiyet Grubu"].Value = newProductHierarchyLevel01;
            selectedRow2.Cells["Yaş Grubu"].Value = newProductHierarchyLevel02;
            selectedRow2.Cells["Ürün Üst Grubu"].Value = newProductHierarchyLevel03;
            selectedRow2.Cells["Ürün Grubu"].Value = newProductHierarchyLevel04;
            selectedRow2.Cells["Kategori"].Value = newProductHierarchyLevel05;

            var parameters = new Dictionary<string, object>
            {
                { "@ErpHierarchyId", newProductHierarchyId },
                { "@ID", id }
            };

            dbHelper.ExecuteNonQuery(up.HierarchyUpdate, parameters);
        }

        private void UpdateDim1(DataGridViewRow selectedRow1, DataGridViewRow selectedRow2, string id)
        {
            var newDim1Code = selectedRow1.Cells["ItemDim1Code"].Value.ToString();

            selectedRow2.Cells["ErpCode"].Value = newDim1Code;


            var parameters = new Dictionary<string, object>
            {
                { "@ErpCode", newDim1Code },
                { "@ID", id }
            };

            dbHelper.ExecuteNonQuery(up.Dim1Update, parameters);
        }

        private void UpdateAttributes(DataGridViewRow selectedRow1, DataGridViewRow selectedRow2, string id)
        {
            var newColorCode = selectedRow1.Cells["ColorCode"].Value.ToString();
            var newColorDescription = selectedRow1.Cells["ColorDescription"].Value.ToString();

            selectedRow2.Cells["ErpCode"].Value = newColorCode;
            selectedRow2.Cells["ColorDescription"].Value = newColorDescription;

            var parameters = new Dictionary<string, object>
            {
                { "@ErpCode", newColorCode },
                { "@ID", id }
            };

            dbHelper.ExecuteNonQuery(up.ColorUpdate, parameters);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
