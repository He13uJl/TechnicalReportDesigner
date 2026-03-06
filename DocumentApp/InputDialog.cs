using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentApp
{
    public partial class InputDialog : Form
    {
        private Label lblPrompt;
        private TextBox txtInput;
        private Button btnOk;
        private Button btnCancel;

        public string UserInput { get; private set; }
        public InputDialog(string promt)
        {
            InitializeComponent(promt);
        }

        private void InitializeComponent(string prompt)
        {
            this.Text = "Ввод данных";
            this.Size = new Size(400, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblPrompt = new Label();
            lblPrompt.Text = prompt;
            lblPrompt.Location = new Point(10, 10);
            lblPrompt.Size = new Size(360, 20);

            txtInput = new TextBox();
            txtInput.Location = new Point(10, 40);
            txtInput.Size = new Size(360, 25);
            txtInput.Multiline = true;
            this.AcceptButton = btnOk;

            btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Location = new Point(190, 80);
            btnOk.Size = new Size(80, 30);
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Location = new Point(280, 80);
            btnCancel.Size = new Size(80, 30);
            btnCancel.DialogResult = DialogResult.Cancel;

            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            UserInput = txtInput.Text;

            if (string.IsNullOrWhiteSpace(UserInput))
            {
                MessageBox.Show("Введите текст!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }











    }
}
