using iTextSharp.text;
using iTextSharp.text.pdf;
using MakeYourRestaurant___Order.Controller;
using MakeYourRestaurant___Received.Model;
using MakeYourRestaurant___Received.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MakeYourRestaurant___Received.Controller
{
    public class ControllerReceived
    {
        Form1 f;
        Repository r;

        Button buttonAll = new Button();
        List<Button> listaButtons = new List<Button>();
        int? restId;

        public ControllerReceived(int? idrest)
        {
            restId = idrest;
            f = new Form1();
            r = new Repository();
            InitListeners();
            LoadData();
            f.ShowDialog();
        }
        public void LoadData()
        {
            List<int?> tables = r.GetTables();

            LoadUpperButtons();
            LoadOrdersData();
            InitScrollbar(tables);
            LoadSidepanelData(tables);
        }

        public void InitListeners()
        {
            f.buttonSignOut.Click += ButtonSignOut_Click;
            f.vScrollBar.Scroll += VScrollBar_Scroll;
            f.textBoxFilter.Enter += TextBoxFilter_Enter;
            f.textBoxFilter.Leave += TextBoxFilter_Leave;

            f.buttonRefresh.Click += ButtonRefresh_Click;
            f.buttonReport.Click += ButtonReport_Click;
            f.buttonStatus.Click += ButtonStatus_Click;
            f.buttonOrderlines.Click += ButtonOrderlines_Click;
            f.buttonDelete.Click += ButtonDelete_Click;

            f.textBoxFilter.TextChanged += TextBoxFilter_TextChanged;

        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (f.dataGridViewOrders.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = f.dataGridViewOrders.SelectedRows[0];
                OrderDTO selectedOrder = selectedRow.DataBoundItem as OrderDTO;

                if (selectedOrder != null)
                {
                    int orderId = selectedOrder.ID;
                    r.DeleteOrder(orderId);

                }
            }
        }

        private void TextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = f.textBoxFilter.Text;
            List<int?> tables = r.GetTables();

            if (filter != "Enter order ID  to search a order.")
            {
                if (!string.IsNullOrEmpty(filter) && !IsPositiveInteger(filter))
                {
                    MessageBox.Show("El valor ingresado no es un número entero positivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    f.textBoxFilter.Text = "";
                    return;
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    f.dataGridViewOrders.DataSource = r.FilterOrders(1, int.Parse(filter));
                    tables = r.GetTablesId(1, int.Parse(filter));
                    LoadSidepanelData(tables);
                }

                if (filter.Equals(""))
                {
                    f.dataGridViewOrders.DataSource = r.GetReceivedOrders(1);
                    LoadSidepanelData(tables);
                }
            }
        }

        private bool IsPositiveInteger(string value)
        {
            int number;
            return int.TryParse(value, out number) && number > 0;
        }

        private void ButtonOrderlines_Click(object sender, EventArgs e)
        {
            f.Hide();
            new ControllerOrderlines(restId);
        }
        private void ButtonStatus_Click(object sender, EventArgs e)
        {
            List<int?> tables = r.GetTables();
            if (f.buttonStatus.Text.Equals("Online"))
            {
                f.buttonStatus.Text = "Offline";

                f.textBoxFilter.Enabled = false;
                f.dataGridViewOrders.DataSource = null;
                f.panel3.Controls.Clear();

                f.buttonReport.Enabled = false;
                f.buttonRefresh.Enabled = false;
            }
            else
            {
                f.buttonStatus.Text = "Online";

                f.textBoxFilter.Enabled = true;
                LoadOrdersData();
                LoadSidepanelData(tables);

                f.buttonReport.Enabled = true;
                f.buttonRefresh.Enabled = true;
            }
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            double? total = 0;
            save.FileName = "MakeYourRestaurant_Received_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            save.ShowDialog();

            List<OrderDTO> orders = f.dataGridViewOrders.DataSource as List<OrderDTO>;

            if (save.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(save.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18);

                    iTextSharp.text.Font smallFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);


                    pdfDoc.Open();

                    string relativeImagePath = @"Assets\logoInvoice.png";
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Path.Combine(basePath, relativeImagePath);

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                    img.ScaleToFit(100f, 100f);
                    img.Alignment = Element.ALIGN_LEFT;

                    pdfDoc.Add(img);
                    pdfDoc.Add(new Phrase("\n"));

                    Paragraph title = new Paragraph("Received Orders", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };

                    pdfDoc.Add(new Phrase(title));
                    pdfDoc.Add(new Phrase("\n"));

                    PdfPTable table = new PdfPTable(5);

                    table.WidthPercentage = 100;

                    float[] widths = new float[] { 20f, 20f, 20f, 20f, 20f };
                    table.SetWidths(widths);

                    string[] headers = { "ID", "Table Number", "Date", "Total Price", "Client Name" };
                    foreach (string header in headers)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(header));
                        headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        table.AddCell(headerCell);
                    }

                    foreach (OrderDTO o in orders)
                    {
                        total += o.TotalPrice;

                        string idText = o.ID.ToString();
                        string tableNumberText = o.TableNumber.ToString();
                        string dateText = o.Date.ToString();
                        string totalPriceText = o.TotalPrice + "€";
                        string clientNameText = o.ClientName;

                        PdfPCell idCell = new PdfPCell(new Phrase(idText, smallFont));
                        PdfPCell tableNumberCell = new PdfPCell(new Phrase(tableNumberText, smallFont));
                        PdfPCell dateCell = new PdfPCell(new Phrase(dateText, smallFont));
                        PdfPCell totalPriceCell = new PdfPCell(new Phrase(totalPriceText, smallFont));
                        PdfPCell clientNameCell = new PdfPCell(new Phrase(clientNameText, smallFont));

                        table.AddCell(idCell);
                        table.AddCell(tableNumberCell);
                        table.AddCell(dateCell);
                        table.AddCell(totalPriceCell);
                        table.AddCell(clientNameCell);
                    }

                    pdfDoc.Add(table);

                    pdfDoc.Add(new Phrase("\n"));

                    pdfDoc.Add(new Phrase("Total: " + total + "€"));

                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            f.panel3.Controls.Clear();
            f.dataGridViewOrders.DataSource = null;
            LoadData();
        }

        private void TextBoxFilter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(f.textBoxFilter.Text))
            {
                f.textBoxFilter.Text = "Enter order ID  to search a order.";
            }
        }

        private void TextBoxFilter_Enter(object sender, EventArgs e)
        {
            if (f.textBoxFilter.Text.Equals("Enter order ID  to search a order."))
            {
                f.textBoxFilter.Text = "";
            }
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            f.panel3.VerticalScroll.Value = e.NewValue;
        }

        private void ButtonSignOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LoadUpperButtons()
        {
            f.buttonReceived.Text = string.Concat("Received (", r.GetNumReceivedOrders(1), ")");
            f.buttonBilled.Text = string.Concat("Billed (", r.GetNumBilledOrders(1), ")");
        }

        public void LoadOrdersData()
        {
            f.dataGridViewOrders.Dock = DockStyle.Fill;
            f.dataGridViewOrders.DataSource = r.GetReceivedOrders(1);
            f.dataGridViewOrders.Columns["ID"].HeaderText = "Bill No.";
            f.dataGridViewOrders.Columns["TableNumber"].HeaderText = "Table No.";
            f.dataGridViewOrders.Columns["Date"].HeaderText = "Ordered At";
            f.dataGridViewOrders.Columns["TotalPrice"].HeaderText = "Amount";
            f.dataGridViewOrders.Columns["ClientName"].HeaderText = "Ordered By";
            AutoSizeColumns(f.dataGridViewOrders);
        }

        public void InitScrollbar(List<int?> tables)
        {
            if (tables.Count == 0 || tables == null)
            {
                f.vScrollBar.Visible = false;
            }
            else
            {
                int totalButtonHeight = (tables.Count + 1) * 50;
                int panelHeight = f.panel3.Height;

                f.panel3.AutoScroll = true;

                if (totalButtonHeight > panelHeight)
                {
                    f.vScrollBar.Visible = true;
                }
                else
                {
                    f.vScrollBar.Visible = false;
                }
            }
        }

        public void LoadSidepanelData(List<int?> tables)
        {
            f.panel3.Controls.Clear();

            Color darkBlack = Color.FromArgb(20, 20, 20);
            Color lightBlack = Color.FromArgb(47, 47, 47);

            foreach (int? t in tables)
            {
                Button button = new Button();
                button.Name = "buttonTable" + t.ToString();
                button.Text = "Table No. " + t.ToString();
                button.Dock = DockStyle.Top;
                button.Height = 50;
                button.BackColor = darkBlack;
                button.ForeColor = Color.White;
                button.Click += button_Click;
                button.Font = new System.Drawing.Font("Poppins", 12, FontStyle.Regular);
                button.FlatStyle = FlatStyle.Flat;
                f.panel3.Controls.Add(button);
                listaButtons.Add(button);
            }

            buttonAll.Name = "buttonAllTables";
            buttonAll.Text = "All Tables";
            buttonAll.Dock = DockStyle.Top;
            buttonAll.Height = 50;
            buttonAll.BackColor = darkBlack;
            buttonAll.ForeColor = Color.White;
            buttonAll.FlatStyle = FlatStyle.Flat;
            buttonAll.Click += button_Click;
            buttonAll.Font = new System.Drawing.Font("Poppins", 12, FontStyle.Regular);
            f.panel3.Controls.Add(buttonAll);

            PrepareTableButtons();
        }

        public void PrepareTableButtons()
        {
            buttonAll.Enabled = false;
            foreach (Button b in listaButtons)
            {
                b.Enabled = true;
            }
        }

        public void AutoSizeColumns(DataGridView dgv)
        {
            dgv.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["TableNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["TotalPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["ClientName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int numeroBoton = GetTableNumber(b.Text);

            List<OrderDTO> listOrders = r.GetOrdersByTable(numeroBoton, 1);
            f.dataGridViewOrders.DataSource = listOrders;

            if (numeroBoton == -1)
            {
                LoadOrdersData();
            }

            foreach (Button b2 in listaButtons)
            {
                b2.Enabled = true;
            }
            buttonAll.Enabled = true;

            b.Enabled = false;
        }

        public int GetTableNumber(string input)
        {
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string numeroStr = match.Value;
                return int.Parse(numeroStr);
            }
            else
            {
                return -1;
            }
        }
    }
}
