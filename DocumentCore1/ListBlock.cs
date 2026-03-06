using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public class ListBlock : IBlock
    {
        private List<string> _items = new List<string>();
        private string _title = "Список";

        public string getName()
        {
            return $"Список ({_items.Count} из.)";
        }

        public void setContent(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                // Разделяем по разным типам переносов строк
                string[] lines = content.Split(
                    new[] { "\r\n", "\n", "\r" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                foreach (var line in lines)
                {
                    string trimmed = line.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        _items.Add(trimmed);
                    }
                }
            }
        }

        public string render()
        {
            string result = "";
            foreach (var item in _items)
            {
                result += $"{item}\r\n";
            }
            return result + "\r\n";
        }

        // Метод для добавления элемента в список
        public void addItem(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                _items.Add(item.Trim());
            }
        }

        // Метод для получения всех элементов
        public List<string> GetAllItems()
        {
            return _items;
        }
    }
}
