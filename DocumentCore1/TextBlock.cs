using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public class TextBlock : IBlock
    {
        private string _content = "";

        public string getName()
        {
            string preview = string.IsNullOrEmpty(_content) ? "Текст" : _content;

            if (preview.Length > 30)
                preview = preview.Substring(0, 30) + "...";
            return preview;
        }

        public void setContent(string content)
        {
            _content = content;
        }

        public string render()
        {
            return $"{_content}\r\n";
        }
    }
}
