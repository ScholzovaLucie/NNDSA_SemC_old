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
                    Block block = new Block(); 
                }
        
        }

        public void WriteBlockToFile(Block block)
        {
            using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var record in block.Records)
            {
                writer.Write(record.Id);
                writer.Write(record.Data);
            }

            int emptySpaces = Block.MaxCapacity - block.Records.Count;
            for (int i = 0; i < emptySpaces; i++)
            {
                writer.Write(0);
                writer.Write(string.Empty);
            }
            }
        }

        public Block ReadBlockFromFile(int blockIndex)
        {
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                int blockPosition = blockIndex * Block.MaxCapacity * (sizeof(int) + 100);
                fs.Seek(blockPosition, SeekOrigin.Begin);

                Block block = new Block();
                for (int i = 0; i < Block.MaxCapacity; i++)
                {
                    int id = br.ReadInt32();
                    string data = br.ReadString();
                    if (id != 0)
                    {
                        block.AddRecord(new Record(id, data));
                    }
                }
                return block;
            }
        }


    }

}

