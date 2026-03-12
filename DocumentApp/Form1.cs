using DocumentCore;
using System;
using System.Windows;
using System.Windows.Forms;


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
        public Form1()
        {
            InitializeComponent();
            updateList();
            ThemeManager.LoadTheme();
            chkDarkTheme.Checked = ThemeManager.IsDarkTheme;
            ThemeManager.ApplyTheme(this);
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

    }
}
