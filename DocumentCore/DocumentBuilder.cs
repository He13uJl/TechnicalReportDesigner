using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCore
{
    public class DocumentBuilder
    {
        private List<IBlock> _blocks = new List<IBlock>();

        public void addBlock(IBlock block)
        {
            _blocks.Add(block);
        }

        public void removeBlock(int index)
        {
            if (index >= 0 && index < _blocks.Count)
            _blocks.RemoveAt(index);
        }

        public List<IBlock> getBlocks()
        {
            return _blocks; 
        }

        public string build()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var block in _blocks)
            {
                sb.Append(block.render());
            }
            return sb.ToString();
        }

    }
}
