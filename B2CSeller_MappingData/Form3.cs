using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace B2CSeller_MappingData
{
    public partial class Form3 : Form
    {
        private static readonly SelectQueries select = new SelectQueries();
        private static readonly Sql dbHelper = new Sql(ConfigurationManager.ConnectionStrings["DB_CON"].ConnectionString);
        private static string idValue = null;
        private static string selectedDbName = null;
        private static string selectedAttributeName = null;
       
        private string[] _selectedColumns = Array.Empty<string>();
        private DataTable _data;

        public Form3()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void LoadData(string[] selectedColumns)
        {
            _selectedColumns = selectedColumns;
        }

        public void LoadRowData(DataGridViewRow row)
        {
            var dataTable = new DataTable();

            foreach (var columnName in _selectedColumns)
            {
                if (row.Cells[columnName] != null)
                {
                    dataTable.Columns.Add(columnName);

                    if (columnName == "ID")
                    {
                        idValue = row.Cells[columnName]?.Value?.ToString();
                    }
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
            MainView.DataSource = dataTable;
            AddDynamicControls();
        }

        // Sınıf seviyesinde bir panel tanımlayın
        private Panel scrollablePanel;

        private void AddDynamicControls()
        {
            // Eğer panel daha önce tanımlanmadıysa, yeni bir panel oluştur
            if (scrollablePanel == null)
            {
                scrollablePanel = new Panel
                {
                    AutoScroll = true,
                    Dock = DockStyle.Fill
                };
                this.Controls.Add(scrollablePanel);
            }

            // Panelin içindeki tüm kontrolleri temizle
            scrollablePanel.Controls.Clear();

            var parameters = new Dictionary<string, object>
    {
        { "@no", idValue }
    };

            DataTable permapping = dbHelper.ExecuteQuery(select.PerProductMapping, parameters);

            for (int i = 1; i <= 50; i++)
            {
                int currentIndex = i; // Capture the current value of i
                DataTable attname = dbHelper.ExecuteQuery(select.attName,null);
                
                string attributeName = (attname.Rows.Count > 0 && attname.Rows[0]["AttributeTypeDescription"] != DBNull.Value)
                        ? attname.Rows[i-1]["AttributeTypeDescription"].ToString()
                        : "BOŞ";
                string dbName = $"Attribute{currentIndex:D2}";

                string attributeCode = (permapping.Rows.Count > 0 && permapping.Columns.Contains(dbName) && permapping.Rows[0][dbName] != DBNull.Value)
                                        ? permapping.Rows[0][dbName].ToString()
                                        : "BOŞ";
                var parameters2 = new Dictionary<string, object>
        {
            { "@no", i },
            { "@code", attributeCode }
        };

                DataTable perproduct = dbHelper.ExecuteQuery(select.PerProductData, parameters2);

                string attributeDescription = (perproduct.Rows.Count > 0 && perproduct.Rows[0]["AttributeDescription"] != DBNull.Value)
                                        ? perproduct.Rows[0]["AttributeDescription"].ToString()
                                        : "BOŞ";

                TextBox lbl = new TextBox
                {
                    Text = attributeName,
                    Location = new System.Drawing.Point(10, 30 * currentIndex),
                    Width = 200,
                    ReadOnly = true,
                    BackColor = System.Drawing.Color.LightYellow
                };

                TextBox code = new TextBox
                {
                    Name = $"code{currentIndex:D2}",
                    Width = 40,
                    Location = new System.Drawing.Point(220, 30 * currentIndex),
                    Text = attributeCode,
                    ReadOnly = true
                };

                TextBox desc = new TextBox
                {
                    Name = $"desc{currentIndex:D2}",
                    Width = 200,
                    Location = new System.Drawing.Point(270, 30 * currentIndex),
                    Text = attributeDescription,
                    ReadOnly = true
                };
                
                code.Click += (sender, e) => LoadDataGridByAttribute(currentIndex.ToString(), attributeName,dbName);
                desc.Click += (sender, e) => LoadDataGridByAttribute(currentIndex.ToString(), attributeName, dbName);

                scrollablePanel.Controls.Add(lbl);
                scrollablePanel.Controls.Add(code);
                scrollablePanel.Controls.Add(desc);
            }
        }

        private void LoadDataGridByAttribute(string index,string attributeName,string dbName)
        {
            var parameters = new Dictionary<string, object>
            {
                { "@no", index }
            };

            _data = dbHelper.ExecuteQuery(select.ProductData, parameters);
            dg.DataSource = _data;
            label2.Text =  "UYARI: " + attributeName + " için işlem yapılmaktadır.";
            selectedDbName = dbName;
            selectedAttributeName = attributeName;
            


        }



        private void SearchText_TextChanged(object sender, EventArgs e)
        {

            if (_data == null) return;
            
            var filterText = SearchText.Text;
            if (string.IsNullOrEmpty(filterText))
            {
                dg.DataSource = _data;
            }
            else
            {
                var dv = _data.DefaultView;
                dv.RowFilter = $"AttributeDescription LIKE '%{filterText}%'";
                dg.DataSource = dv;
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dg.SelectedRows[0];
                string attributeCode = selectedRow.Cells["AttributeCode"]?.Value?.ToString();

                if (!string.IsNullOrEmpty(attributeCode))
                {
                    string query = $@"
                        UPDATE ProductAttributeMapping
                        SET {selectedDbName} = @AttributeCode
                        WHERE ID = @ID";

                    // Parametreleri ekleyin
                    var parameters = new Dictionary<string, object>
                        {
                            { "@AttributeCode", attributeCode },
                            { "@ID", idValue }
                        };

                    dbHelper.ExecuteNonQuery(query, parameters);
                    MessageBox.Show(selectedAttributeName + " Güncellendi");
                    AddDynamicControls();
                    label2.Text = selectedAttributeName + " Güncellendi";

                }
                

            }

            else
            {
                MessageBox.Show("Lütfen güncellenecek bir satır seçin.");
            }
        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            TextBox code02TextBox = scrollablePanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "code02");
            TextBox code19TextBox = scrollablePanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "code19");

            // TextBox'lar varsa, içeriklerini kontrol et
            bool isCode02Empty = code02TextBox == null || string.IsNullOrWhiteSpace(code02TextBox.Text) || code02TextBox.Text.Equals("BOŞ", StringComparison.OrdinalIgnoreCase);
            bool isCode19Empty = code19TextBox == null || string.IsNullOrWhiteSpace(code19TextBox.Text) || code19TextBox.Text.Equals("BOŞ", StringComparison.OrdinalIgnoreCase);

            // Eğer herhangi bir TextBox boşsa veya "BOŞ" içeriyorsa, formun kapanmasını engelle
            if (isCode02Empty || isCode19Empty)
            {
                // Kullanıcıyı bilgilendirin
                MessageBox.Show("Lütfen 'code02' ve 'code19' alanlarını doldurunuz ve boş bırakmayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Formun kapanmasını engelle
                e.Cancel = true;
            }
        }
    }
}
