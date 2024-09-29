using HtmlAgilityPack;
using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;


namespace GetPrices
{
    public partial class Form1 : Form
    {


        public Form1()
        {

            InitializeComponent();
        }


        public DataTable ConvertHtmlTableToDataTable(string html )
        {
            // Load the HTML document using HtmlAgilityPack
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Find the table in the HTML
            var table = doc.DocumentNode.SelectSingleNode("//table");

            if (table == null)
            {
                throw new Exception("No table found in the provided HTML.");
            }

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Find the header columns
            var headers = table.SelectNodes(".//th");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    dataTable.Columns.Add(header.InnerText.Trim());
                }
            }

            // Find all rows and cells and add them to the DataTable
            var rows = table.SelectNodes(".//tr");
            foreach (var row in rows)
            {
                var cells = row.SelectNodes("td");
                if (cells != null && cells.Count > 0)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < cells.Count; i++)
                    {
                        dataRow[i] = cells[i].InnerText.Trim();
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable; // Ensure that DataTable is returned to resolve CS0161
        }
        private void SaveDataTableToExcel(DataTable dataTable)
        {
            // Prompt user to choose save location
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Create a new workbook
                        using (var workbook = new XLWorkbook())
                        {
                            // Add DataTable as a worksheet
                            workbook.Worksheets.Add(dataTable, "Data");

                            // Save the workbook
                            workbook.SaveAs(saveFileDialog.FileName);

                            MessageBox.Show("Data saved to Excel successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while saving to Excel: " + ex.Message);
                    }
                }
            }
        }
        private void SaveDataTableToCSV(DataTable dataTable)
        {
            // Prompt user to choose save location
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files|*.csv";
                saveFileDialog.Title = "Save as CSV File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Use UTF-8 encoding to handle Arabic characters
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                        {
                            // Write the header
                            for (int i = 0; i < dataTable.Columns.Count; i++)
                            {
                                writer.Write(dataTable.Columns[i].ColumnName);
                                if (i < dataTable.Columns.Count - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();

                            // Write the rows
                            foreach (DataRow row in dataTable.Rows)
                            {
                                for (int i = 0; i < dataTable.Columns.Count; i++)
                                {
                                    writer.Write(row[i].ToString());
                                    if (i < dataTable.Columns.Count - 1)
                                        writer.Write(",");
                                }
                                writer.WriteLine();
                            }
                        }

                        MessageBox.Show("Data saved to CSV successfully with Arabic support!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while saving to CSV: " + ex.Message);
                    }
                }
            }
        }


        public DataTable ConvertHtmlTableToDataTable_new(string html,bool mostwrad)
        {
            // Load the HTML document using HtmlAgilityPack
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Find the table in the HTML
            var table = doc.DocumentNode.SelectSingleNode("//table");

            if (table == null)
            {
                throw new Exception("No table found in the provided HTML.");
            }

            // Create a new DataTable with specific columns (including new columns you've mentioned)
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Handle", typeof(string));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Body (HTML)", typeof(string));
            dataTable.Columns.Add("Vendor", typeof(string));
            dataTable.Columns.Add("Product Category", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));  // This will have "veggie", "fruits", or "leafy"
            dataTable.Columns.Add("Tags", typeof(string));
            dataTable.Columns.Add("Published", typeof(bool));
            dataTable.Columns.Add("Variant Price", typeof(string));  // Assuming it is filled with the highest price
            dataTable.Columns.Add("Option1 Name", typeof(string));
            dataTable.Columns.Add("Option1 Value", typeof(string));
            dataTable.Columns.Add("Option2 Name", typeof(string));
            dataTable.Columns.Add("Option2 Value", typeof(string));
            dataTable.Columns.Add("Option3 Name", typeof(string));
            dataTable.Columns.Add("Option3 Value", typeof(string));
            dataTable.Columns.Add("Variant SKU", typeof(string));
            dataTable.Columns.Add("Variant Grams", typeof(string));
            dataTable.Columns.Add("Variant Inventory Tracker", typeof(string));
            dataTable.Columns.Add("Variant Inventory Qty", typeof(string));
            dataTable.Columns.Add("Variant Inventory Policy", typeof(string));
            dataTable.Columns.Add("Variant Fulfillment Service", typeof(string));
            dataTable.Columns.Add("Variant Compare At Price", typeof(string));
            dataTable.Columns.Add("Variant Requires Shipping", typeof(string));
            dataTable.Columns.Add("Variant Taxable", typeof(string));
            dataTable.Columns.Add("Variant Barcode", typeof(string));
            dataTable.Columns.Add("Image Src", typeof(string));
            dataTable.Columns.Add("Image Position", typeof(string));
            dataTable.Columns.Add("Image Alt Text", typeof(string));
            dataTable.Columns.Add("Gift Card", typeof(string));
            dataTable.Columns.Add("SEO Title", typeof(string));
            dataTable.Columns.Add("SEO Description", typeof(string));
            dataTable.Columns.Add("Google Shopping / Google Product Category", typeof(string));
            dataTable.Columns.Add("Google Shopping / Gender", typeof(string));
            dataTable.Columns.Add("Google Shopping / Age Group", typeof(string));
            dataTable.Columns.Add("Google Shopping / MPN", typeof(string));
            dataTable.Columns.Add("Google Shopping / Condition", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Product", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Label 0", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Label 1", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Label 2", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Label 3", typeof(string));
            dataTable.Columns.Add("Google Shopping / Custom Label 4", typeof(string));
            dataTable.Columns.Add("Variant Image", typeof(string));
            dataTable.Columns.Add("Variant Weight Unit", typeof(string));
            dataTable.Columns.Add("Variant Tax Code", typeof(string));
            dataTable.Columns.Add("Cost per item", typeof(string));
            dataTable.Columns.Add("Included / Jordan", typeof(string));
            dataTable.Columns.Add("Price / Jordan", typeof(string));
            dataTable.Columns.Add("Compare At Price / Jordan", typeof(string));
            dataTable.Columns.Add("Included / International", typeof(string));
            dataTable.Columns.Add("Price / International", typeof(string));
            dataTable.Columns.Add("Compare At Price / International", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));

            // Iterate through the HTML rows and populate the DataTable
            var rows = table.SelectNodes(".//tr");
            foreach (var row in rows)
            {
                var cells = row.SelectNodes("td");
                if (cells != null && cells.Count > 0)
                {
                    DataRow dataRow = dataTable.NewRow();

                    // Assuming the cell values correspond to columns you provided
                    string category = cells[6].InnerText.Trim(); // Category type (Œ÷«—, ›Ê«ﬂÂ, Ê—ﬁÌ« )
                    double Variant_Price = (Convert.ToDouble(cells[5].InnerText.Trim()) / 100);
                    string item_name = cells[0].InnerText.Trim();
                    if (mostwrad==true)
                    {
                        item_name = item_name + " " + "„” Ê—œ";
                    }
                    dataRow["Handle"] = item_name;  // ’‰›
                    dataRow["Title"] = item_name;   // ’‰›
                    dataRow["Body (HTML)"] = "<p>" + item_name + "</p>";
                    dataRow["Vendor"] = "Sooq2Door";  // Fixed value
                    dataRow["Product Category"] = "Uncategorized";  // Fixed value
                    dataRow["Type"] = GetProductType(category);  // Logic for "veggie", "fruits", or "leafy"
                    dataRow["Tags"] = "";
                    dataRow["Published"] = true;  // Assuming it's always true
                    dataRow["Variant Price"] = (Convert.ToDouble(cells[5].InnerText.Trim()) / 100).ToString();  // «·”⁄— «·«⁄·Ï - ﬁ—‘/ ﬂÌ·Ê

                    // Fill in the new columns with default or empty values
                    dataRow["Option1 Name"] = "Title";
                    dataRow["Option1 Value"] = "Default Title";
                    dataRow["Option2 Name"] = "";
                    dataRow["Option2 Value"] = "";
                    dataRow["Option3 Name"] = "";
                    dataRow["Option3 Value"] = "";
                    dataRow["Variant SKU"] = "1000";
                    dataRow["Variant Grams"] = "";
                    dataRow["Variant Inventory Tracker"] = "";
                    dataRow["Variant Inventory Qty"] = "20";
                    dataRow["Variant Inventory Policy"] = "deny";
                    dataRow["Variant Fulfillment Service"] = "manual";
                    dataRow["Variant Compare At Price"] = "";
                    dataRow["Variant Requires Shipping"] = "TRUE";
                    dataRow["Variant Taxable"] = "TRUE";
                    dataRow["Variant Barcode"] = "";
                    dataRow["Image Src"] = "";
                    dataRow["Image Position"] = "";
                    dataRow["Image Alt Text"] = "";
                    dataRow["Gift Card"] = "FALSE";
                    dataRow["SEO Title"] = "";
                    dataRow["SEO Description"] = "";
                    dataRow["Google Shopping / Google Product Category"] = "";
                    dataRow["Google Shopping / Gender"] = "";
                    dataRow["Google Shopping / Age Group"] = "";
                    dataRow["Google Shopping / MPN"] = "";
                    dataRow["Google Shopping / Condition"] = "";
                    dataRow["Google Shopping / Custom Product"] = "";
                    dataRow["Google Shopping / Custom Label 0"] = "";
                    dataRow["Google Shopping / Custom Label 1"] = "";
                    dataRow["Google Shopping / Custom Label 2"] = "";
                    dataRow["Google Shopping / Custom Label 3"] = "";
                    dataRow["Google Shopping / Custom Label 4"] = "";
                    dataRow["Variant Image"] = "";
                    dataRow["Variant Weight Unit"] = "kg";
                    dataRow["Variant Tax Code"] = "";
                    dataRow["Cost per item"] = "";
                    dataRow["Included / Jordan"] = "TRUE";
                    dataRow["Price / Jordan"] = "";
                    dataRow["Compare At Price / Jordan"] = "";
                    dataRow["Included / International"] = "TRUE";
                    dataRow["Price / International"] = "";
                    dataRow["Compare At Price / International"] = "";
                    dataRow["Status"] = "active";

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        // Helper function to determine product type
        private string GetProductType(string category)
        {
            switch (category)
            {
                case "Œ÷«—":
                    return "veggie";
                case "›Ê«ﬂÂ":
                    return "fruits";
                case "Ê—ﬁÌ« ":
                    return "leafy";
                default:
                    return "unknown";
            }
        }

        private void btn_gene_excek_Click(object sender, EventArgs e)
        {
            string html = txt_html.Text;

            // Create an instance of HtmlTableParser and parse the HTML table
            DataTable table = ConvertHtmlTableToDataTable_new(html,false);

            SaveDataTableToCSV(table);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string html = txt_html.Text;

            // Create an instance of HtmlTableParser and parse the HTML table
            DataTable table = ConvertHtmlTableToDataTable(html);

            SaveDataTableToCSV(table);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string html = txt_html.Text;

            // Create an instance of HtmlTableParser and parse the HTML table
            DataTable table = ConvertHtmlTableToDataTable_new(html,true );

            SaveDataTableToCSV(table);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string html = txt_html.Text;

            // Create an instance of HtmlTableParser and parse the HTML table
            DataTable table = ConvertHtmlTableToDataTable(html);

            SaveDataTableToCSV(table);
        }
    }
}
