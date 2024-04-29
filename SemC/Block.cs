using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class Block
    {
        public List<Record> Records { get; set; }
        public static int Capacity { get; } = 103;

        public Block()
        {
            Records = new List<Record>();
        }

        public void AddRecord(Record record)
        {
            if (Records.Count < Capacity)
                Records.Add(record);
            else
                throw new InvalidOperationException("Block is full.");
        }
    }
}
