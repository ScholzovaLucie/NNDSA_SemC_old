using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    internal class Record
    {
        public int Id { get; set; } 
        public string Data { get; set; } 

        public Record(int id, string data)
        {
            Id = id;
            Data = data;
        }
    }
}
