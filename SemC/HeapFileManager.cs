using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class HeapFileManager
    {
        private List<Block> blocks;
        public string FilePath { get; set; }

        public HeapFileManager(string filePath)
        {
            FilePath = filePath;
            blocks = new List<Block>();
        }

        public void InitializeHeapFile(int numberOfBlocks)
        {
            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(new Block()); 
            }
        }

        public void WriteBlockToFile(Block block)
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (var record in block.Records)
                {
                    writer.Write(record.Id);
                    writer.Write(record.Data);
                }
            }
        }

        public Block ReadBlockFromFile(int blockIndex)
        {
            return blocks[blockIndex];
        }
    }
}
