using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public class HeaderBlock : IBlock
    {
        private string _content = "";

        public string getName()
        {
            return "Заголовок";
        }

        public void setContent(string content)
        {
            _content = content;
        }

        public string render()
        {
            return $"=== {_content} ===\r\n";
        }
    }
}
