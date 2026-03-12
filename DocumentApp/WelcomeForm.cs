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
    public partial class WelcomeForm : Form
    {
            private CheckBox chkDarkTheme;
            private Label lblTitle;
            private Label lblSubtitle;
            private Label lblDescription;
            private Label lblVersion;
            private Button btnStart;
            private Button btnExit;

            public WelcomeForm()
            {
                InitializeComponent();
                ThemeManager.LoadTheme();
                chkDarkTheme.Checked = ThemeManager.IsDarkTheme;
                ThemeManager.ApplyTheme(this);
            }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WelcomeForm));
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblDescription = new Label();
            lblVersion = new Label();
            btnStart = new Button();
            btnExit = new Button();
            chkDarkTheme = new CheckBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 126, 215);
            lblTitle.Location = new Point(80, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(466, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📋Конструктор документов";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.FromArgb(166, 166, 166);
            lblSubtitle.Location = new Point(105, 95);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(372, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Создавайте структурированные документы легко!";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 18F);
            lblDescription.ForeColor = Color.FromArgb(60, 60, 60);
            lblDescription.Location = new Point(80, 128);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(421, 351);
            lblDescription.TabIndex = 2;
            lblDescription.Text = resources.GetString("lblDescription.Text");
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F);
            lblVersion.ForeColor = Color.FromArgb(150, 150, 150);
            lblVersion.Location = new Point(170, 479);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(240, 15);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "Версия 2.0 | Лабораторная работа по ООП";
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(0, 120, 215);
            btnStart.Cursor = Cursors.Hand;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(192, 497);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(200, 50);
            btnStart.TabIndex = 4;
            btnStart.Text = "▶️Начать работу";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(220, 53, 69);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10F);
            btnExit.ForeColor = Color.Black;
            btnExit.Location = new Point(232, 553);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 35);
            btnExit.TabIndex = 5;
            btnExit.Text = "❌Выход";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // chkDarkTheme
            // 
            chkDarkTheme.AutoSize = true;
            chkDarkTheme.Font = new Font("Segoe UI", 10F);
            chkDarkTheme.Location = new Point(12, 560);
            chkDarkTheme.Name = "chkDarkTheme";
            chkDarkTheme.Size = new Size(131, 23);
            chkDarkTheme.TabIndex = 6;
            chkDarkTheme.Text = "🌙 Тёмная тема";
            chkDarkTheme.CheckedChanged += chkDarkTheme_CheckedChanged;
            // 
            // WelcomeForm
            // 
            BackColor = Color.FromArgb(246, 246, 246);
            ClientSize = new Size(600, 600);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblDescription);
            Controls.Add(lblVersion);
            Controls.Add(btnStart);
            Controls.Add(btnExit);
            Controls.Add(chkDarkTheme);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WelcomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Конструктор документов";
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnStart_Click(object sender, EventArgs e)
            {
                Form1 mainForm = new Form1();
                mainForm.Show();

                Hide();
            }

        private void btnExit_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

        private void chkDarkTheme_CheckedChanged(object sender, EventArgs e)
            {
            ThemeManager.IsDarkTheme = chkDarkTheme.Checked;
            ThemeManager.ApplyTheme(this);
            }

       
    }
}
