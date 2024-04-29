using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    internal class Buffer
    {
        public Block CurrentBlock { get; private set; }

        public Buffer()
        {
            CurrentBlock = new Block();
        }

        public void LoadBlock(Block block)
        {
            CurrentBlock = block;
        }

        public void DisplayContents()
        {
            foreach (var record in CurrentBlock.Records)
            {
                Console.WriteLine($"Record ID: {record.Id}, Data: {record.Data}");
            }
        }
    }
}
