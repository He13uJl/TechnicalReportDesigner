using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public class TableBlock : IBlock
    {
        private List<List<string>> _rows = new List<List<string>>();
        private int _columns = 2;

        public string getName()
        {
            return $"Таблица ({_rows.Count} стр.)";
        }

        public void setContent(string content)
        {
            _rows.Clear();

            if (!string.IsNullOrEmpty(content))
            {
                // Разделяем строки таблицы
                string[] lines = content.Split(
                    new[] { "\r\n", "\n", "\r" },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in lines)
                {
                    // Разделяем ячейки по символу | или табуляции
                    string[] cells = line.Split(new[] { '|', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> row = new List<string>();

                    foreach (var cell in cells)
                    {
                        row.Add(cell.Trim());
                    }

                    if (row.Count > 0)
                    {
                        _rows.Add(row);
                        if (row.Count > _columns)
                            _columns = row.Count;
                    }
                }
            }
        }

        public string render()
        {
            string result = "";
            // Определяем максимальную ширину для каждого столбца
            int[] colWidths = new int[_columns];
            foreach (var row in _rows)
            {
                for (int i = 0; i < row.Count && i < _columns; i++)
                {
                    if (row[i].Length > colWidths[i])
                        colWidths[i] = row[i].Length;
                }
            }

            // Формируем таблицу
            foreach (var row in _rows)
            {
                result += "| ";
                for (int i = 0; i < _columns; i++)
                {
                    string cell = i < row.Count ? row[i] : "";
                    result += cell.PadRight(colWidths[i]) + " | ";
                }
                result += "\r\n";
            }

            return result + "\r\n";
        }

        // Метод для добавления строки
        public void addRow(List<string> cells)
        {
            _rows.Add(cells);
            if (cells.Count > _columns)
                _columns = cells.Count;
        }
        public string GetItems()
        {
            return $"Таблица ({_rows.Count} стр.)";
        }
    }
}
