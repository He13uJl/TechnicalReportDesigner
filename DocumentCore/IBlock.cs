using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public interface IBlock
    {
        string getName();
        void setContent(string content);
        string render();
    }
}
