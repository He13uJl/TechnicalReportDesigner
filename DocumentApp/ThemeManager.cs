using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentApp
{
    public static class ThemeManager
    {
        private static string _configPath = "theme.config";
        private static bool _isDarkTheme = false;

        // Цвета для светлой темы
        public static Color Light_Background = Color.FromArgb(240, 240, 240);
        public static Color Light_Panel = Color.White;
        public static Color Light_Text = Color.FromArgb(60, 60, 60);
        public static Color Light_Button = Color.FromArgb(0, 120, 215);
        public static Color Light_ButtonText = Color.White;

        // Цвета для темной темы
        public static Color Dark_Background = Color.FromArgb(38, 39, 38);
        public static Color Dark_Panel = Color.FromArgb(45, 45, 45);
        public static Color Dark_Text = Color.FromArgb(220, 220, 220);
        public static Color Dark_Button = Color.FromArgb(0, 158, 200);
        public static Color Dark_ButtonText = Color.White;

        public static bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                _isDarkTheme = value;
                SaveTheme();
            }
        }

        // Загрузка темы из файла
        public static void LoadTheme()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    string content = File.ReadAllText(_configPath);
                    _isDarkTheme = content == "dark";
                }
            }
            catch
            {
                _isDarkTheme = false;
            }
        }

        // Сохранение темы в файл
        public static void SaveTheme()
        {
            try
            {
                File.WriteAllText(_configPath, _isDarkTheme ? "dark" : "light");
            }
            catch { }
        }

        // Применение темы к форме
        public static void ApplyTheme(Form form)
        {
            Color bg = _isDarkTheme ? Dark_Background : Light_Background;
            Color panel = _isDarkTheme ? Dark_Panel : Light_Panel;
            Color text = _isDarkTheme ? Dark_Text : Light_Text;
            Color button = _isDarkTheme ? Dark_Button : Light_Button;
            Color buttonText = _isDarkTheme ? Dark_ButtonText : Light_ButtonText;

            form.BackColor = bg;
            form.ForeColor = text;

            // Применение ко всем контролам
            ApplyThemeToControls(form.Controls, panel, text, button, buttonText);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls,
            Color panel, Color text, Color button, Color buttonText)
        {
            foreach (Control ctrl in controls)
            {
                // Panel
                if (ctrl is Panel)
                {
                    ctrl.BackColor = panel;
                    ctrl.ForeColor = text;
                }

                // GroupBox
                else if (ctrl is GroupBox)
                {
                    ctrl.BackColor = panel;
                    ctrl.ForeColor = text;
                }

                // Button
                else if (ctrl is Button btn)
                {
                    btn.BackColor = button;
                    btn.ForeColor = buttonText;
                    btn.FlatStyle = FlatStyle.Flat;
                }

                // ListBox
                else if (ctrl is ListBox)
                {
                    ctrl.BackColor = panel;
                    ctrl.ForeColor = text;
                }

                // RichTextBox
                else if (ctrl is RichTextBox)
                {
                    ctrl.BackColor = _isDarkTheme ? Color.FromArgb(50, 50, 50) : Color.FromArgb(250, 250, 250);
                    ctrl.ForeColor = text;
                }

                // Label
                else if (ctrl is Label)
                {
                    ctrl.BackColor = Color.Transparent;
                    ctrl.ForeColor = text;
                }

                // Рекурсивное применение к дочерним контролам
                if (ctrl.HasChildren)
                {
                    ApplyThemeToControls(ctrl.Controls, panel, text, button, buttonText);
                }
            }
        }
    }
}
