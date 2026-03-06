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

        public Form1()
        {
            InitializeComponent();
            updateList();
        }

        private void InitializeComponent()
        {
            this.Text = "Конструктор документов";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;






            // 
            // btnAddHeader
            // 
            btnAddHeader = new Button();
            btnAddHeader.Location = new Point(20, 20);
            btnAddHeader.Name = "btnAddHeader";
            btnAddHeader.Size = new Size(150, 30);
            btnAddHeader.TabIndex = 0;
            btnAddHeader.Text = "Добавить заголовок";
            btnAddHeader.Click += btnAddHeader_Click;
            // 
            // btnAddText
            // 
            btnAddText = new Button();
            btnAddText.Location = new Point(20, 60);
            btnAddText.Name = "btnAddText";
            btnAddText.Size = new Size(150, 30);
            btnAddText.TabIndex = 1;
            btnAddText.Text = "Добавить текст";
            btnAddText.Click += btnAddText_Click;
            // 
            // btnRemove
            // 
            btnRemove = new Button();
            btnRemove.Location = new Point(20, 100);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(150, 30);
            btnRemove.TabIndex = 2;
            btnRemove.Text = "Удалить выбранный";
            btnRemove.Click += btnRemove_Click;
            //
            // btnBuild
            //
            btnBuild = new Button();
            btnBuild.Text = "Собрать отчет";
            btnBuild.Location = new Point(20, 140);
            btnBuild.Size = new Size(150, 30);
            btnBuild.Click += btnBuild_Click;

            btnAddList = new Button();
            btnAddList.Text = "Добавить список";
            btnAddList.Location = new Point(20, 180);
            btnAddList.Size = new Size(150, 30);
            btnAddList.Click += btnAddList_Click;


            btnAddTable = new Button();
            btnAddTable.Text = "Добавить таблицу";
            btnAddTable.Location = new Point(20, 220);
            btnAddTable.Size = new Size(150, 30);
            btnAddTable.Click += btnAddTable_Click;

            // 
            // listBlocks
            // 
            listBlocks = new ListBox();
            listBlocks.ItemHeight = 15;
            listBlocks.Location = new Point(200, 20);
            listBlocks.Name = "listBlocks";
            listBlocks.Size = new Size(250, 394);
            listBlocks.TabIndex = 3;
            // 
            // txtResult
            // 
            txtResult = new RichTextBox();
            txtResult.Location = new Point(470, 20);
            txtResult.Size = new Size(300, 530);
            txtResult.ReadOnly = true;

            this.Controls.Add(btnAddHeader);
            this.Controls.Add(btnAddText);
            this.Controls.Add(btnRemove);
            this.Controls.Add(btnBuild);
            this.Controls.Add(listBlocks);
            this.Controls.Add(txtResult);
            this.Controls.Add(btnAddList);
            this.Controls.Add(btnAddTable);

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

    }
}
