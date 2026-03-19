using DocumentCore;
using System;
using System.Windows;
using System.Windows.Forms;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Diagnostics;
namespace DocumentApp
{

    public partial class Form1 : Form
    {
        private DocumentBuilder _builder = new DocumentBuilder();

        private Button btnAddHeader;
        private Button btnAddText;
        private Button btnRemove;
        private Button btnBuild;
        private Button btnAddList;
        private Button btnAddTable;
        private ListBox listBlocks;
        private RichTextBox txtResult;
        private CheckBox chkDarkTheme;

        static Form1()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public Form1()
        {
            InitializeComponent();
            updateList();
            ThemeManager.LoadTheme();

            if (chkDarkTheme != null)
            {
                chkDarkTheme.Checked = ThemeManager.IsDarkTheme;
                ThemeManager.ApplyTheme(this);
            }
        }

        private void InitializeComponent()
        {
            chkDarkTheme = new CheckBox();
            btnAddHeader = new Button();
            btnAddText = new Button();
            btnRemove = new Button();
            btnBuild = new Button();
            btnAddList = new Button();
            btnAddTable = new Button();
            listBlocks = new ListBox();
            txtResult = new RichTextBox();
            SuspendLayout();
            // 
            // chkDarkTheme
            // 
            chkDarkTheme.AutoSize = true;
            chkDarkTheme.Font = new Font("Segoe UI", 10F);
            chkDarkTheme.Location = new Point(12, 526);
            chkDarkTheme.Name = "chkDarkTheme";
            chkDarkTheme.Size = new Size(131, 23);
            chkDarkTheme.TabIndex = 7;
            chkDarkTheme.Text = "🌙 Тёмная тема";
            chkDarkTheme.CheckedChanged += chkDarkTheme_CheckedChanged;
            // 
            // btnAddHeader
            // 
            btnAddHeader.Location = new Point(20, 20);
            btnAddHeader.Name = "btnAddHeader";
            btnAddHeader.Size = new Size(150, 30);
            btnAddHeader.TabIndex = 0;
            btnAddHeader.Text = "Добавить заголовок";
            btnAddHeader.Click += btnAddHeader_Click;
            // 
            // btnAddText
            // 
            btnAddText.Location = new Point(20, 60);
            btnAddText.Name = "btnAddText";
            btnAddText.Size = new Size(150, 30);
            btnAddText.TabIndex = 1;
            btnAddText.Text = "Добавить текст";
            btnAddText.Click += btnAddText_Click;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(20, 100);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(150, 30);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Удалить выбранный";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnBuild
            // 
            btnBuild.Location = new Point(20, 140);
            btnBuild.Name = "btnBuild";
            btnBuild.Size = new Size(150, 30);
            btnBuild.TabIndex = 3;
            btnBuild.Text = "Собрать отчет";
            btnBuild.Click += btnBuild_Click;
            // 
            // btnAddList
            // 
            btnAddList.Location = new Point(20, 180);
            btnAddList.Name = "btnAddList";
            btnAddList.Size = new Size(150, 30);
            btnAddList.TabIndex = 5;
            btnAddList.Text = "Добавить список";
            btnAddList.Click += btnAddList_Click;
            // 
            // btnAddTable
            // 
            btnAddTable.Location = new Point(20, 220);
            btnAddTable.Name = "btnAddTable";
            btnAddTable.Size = new Size(150, 30);
            btnAddTable.TabIndex = 6;
            btnAddTable.Text = "Добавить таблицу";
            btnAddTable.Click += btnAddTable_Click;
            //
            //
            //
            var btnExportExcel = new Button();
            btnExportExcel.Text = "📦Экспорт в Excel";
            btnExportExcel.Location = new Point(10, 415);
            btnExportExcel.Size = new Size(160, 35);
            btnExportExcel.BackColor = Color.FromArgb(21, 101, 192);
            btnExportExcel.ForeColor = Color.White;
            btnExportExcel.FlatStyle = FlatStyle.Flat;
            btnExportExcel.Click += btnExportExcel_Click;
            this.Controls.Add(btnExportExcel);
            // 
            // listBlocks
            // 
            listBlocks.ItemHeight = 15;
            listBlocks.Location = new Point(200, 20);
            listBlocks.Name = "listBlocks";
            listBlocks.Size = new Size(250, 394);
            listBlocks.TabIndex = 3;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(470, 20);
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(300, 530);
            txtResult.TabIndex = 4;
            txtResult.Text = "";
            // 
            // Form1
            // 
            ClientSize = new Size(784, 561);
            Controls.Add(btnAddHeader);
            Controls.Add(btnAddText);
            Controls.Add(btnRemove);
            Controls.Add(btnBuild);
            Controls.Add(listBlocks);
            Controls.Add(txtResult);
            Controls.Add(btnAddList);
            Controls.Add(btnAddTable);
            Controls.Add(chkDarkTheme);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Конструктор документов";
            ResumeLayout(false);
            PerformLayout();

        }

        private void btnAddHeader_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog("Введите текст заголовка:"))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    IBlock block = new HeaderBlock();
                    block.setContent(dialog.UserInput);
                    _builder.addBlock(block);
                    updateList();
                }
            }
        }
        private void btnAddText_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog("Введите текст:"))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    IBlock block = new TextBlock();
                    block.setContent(dialog.UserInput);
                    _builder.addBlock(block);
                    updateList();
                }
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBlocks.SelectedIndex != -1)
            {
                _builder.removeBlock(listBlocks.SelectedIndex);
                updateList();
            }
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string result = _builder.build();
            txtResult.Text = result;
        }

        private void updateList()
        {
            if (listBlocks == null || _builder == null)
                return;

            listBlocks.Items.Clear();
            foreach (var block in _builder.getBlocks())
            {
                listBlocks.Items.Add(block.getName());
            }
        }

        private void btnAddList_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog("Введите элементы списка (каждый с новой строки):"))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    IBlock block = new ListBlock();
                    block.setContent(dialog.UserInput);
                    _builder.addBlock(block);
                    updateList();
                }
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            using (var dialog = new InputDialog("Введите таблицу (ячейки через |, строки с новой строки):\nПример: Ячейка1|Ячейка2\nЯчейка3|Ячейка4"))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    IBlock block = new TableBlock();
                    block.setContent(dialog.UserInput);
                    _builder.addBlock(block);
                    updateList();
                }
            }
        }

        private void chkDarkTheme_CheckedChanged(object sender, EventArgs e)
        {
            ThemeManager.IsDarkTheme = chkDarkTheme.Checked;
            ThemeManager.ApplyTheme(this);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = $"Документ_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToExcel(saveFileDialog.FileName);
                        MessageBox.Show("Экспорт выполнен успешно!", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportToExcel(string FilePath)
        {
            using (var package = new ExcelPackage())
            {
                var infoSheet = package.Workbook.Worksheets.Add("Информация");
                infoSheet.Cells[1, 1].Value = "Конструктор документов";
                infoSheet.Cells[1, 1].Style.Font.Bold = true;
                infoSheet.Cells[1, 1].Style.Font.Size = 16;
                infoSheet.Cells[2, 1].Value = $"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}";
                infoSheet.Cells[3, 1].Value = $"Блоков: {_builder.getBlocks().Count}";

                var contentSheet = package.Workbook.Worksheets.Add("Содержание");
                int row = 1;
                var blocks = _builder.getBlocks();

                foreach (var block in blocks)
                {
                    if (block is HeaderBlock headerBlock)
                    {
                        contentSheet.Cells[row, 1].Value = headerBlock.getName().Replace("Заголовок: ", "");
                        contentSheet.Cells[row, 1].Style.Font.Bold = true;
                        contentSheet.Cells[row, 1].Style.Font.Size = 14;
                        contentSheet.Cells[row, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        contentSheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 120, 215));
                        contentSheet.Cells[row, 1].Style.Font.Color.SetColor(Color.White);
                        row++;
                    }
                    else if (block is TextBlock textBlock)
                    {
                        contentSheet.Cells[row, 1].Value = textBlock.render().Trim();
                        contentSheet.Cells[row, 1].Style.WrapText = true;
                        row++;
                    }
                    else if (block is ListBlock listBlock)
                    {
                        var items = listBlock.GetAllItems();
                        foreach (var item in items)
                        {
                            contentSheet.Cells[row, 1].Value += $"{item} ";
                        }
                        row++;
                    }
                    else if (block is TableBlock tableBlock)
                    {

                        string tableContent = tableBlock.GetItems()?.ToString() ?? "";


                        string[] rows = tableContent.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        int startRow = row;

                        for (int i = 0; i < rows.Length; i++)
                        {
                            string[] cells = rows[i].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int j = 0; j < cells.Length; j++)
                            {
                                contentSheet.Cells[startRow + i, j + 1].Value = cells[j].Trim();
                                contentSheet.Cells[startRow + i, j + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                        }

                        if (rows.Length > 0)
                        {
                            string[] firstRowCells = rows[0].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int c = 1; c <= firstRowCells.Length; c++)
                            {
                                contentSheet.Column(c).AutoFit();
                            }
                        }

                        row = startRow + rows.Length + 1;
                    }
                    else
                    {
                        contentSheet.Cells[row, 1].Value = block.render().Trim();
                        row++;
                    }
                }

                var fileInfo = new System.IO.FileInfo(FilePath);
                    package.SaveAs(fileInfo);
                }
            }
        }
    }
