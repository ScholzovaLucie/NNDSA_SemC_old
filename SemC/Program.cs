using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace SemC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "heapfile.bin";
            var heapFile = new HeapFile(filePath, 1024, 1000000);  // buffer size 1024 bytes, total 100 records

            Console.WriteLine("Spouštění s jedním bufferem:");
            heapFile.WriteRecords(false);  // Write records to the file with single buffer
            heapFile.ReadRecords(false);   // Read records from the file with single buffer

            Console.WriteLine("\nSpouštění se dvěma buffery:");
            heapFile.WriteRecords(true);  // Write records to the file with dual buffers
            heapFile.ReadRecords(true);
        }


    }
}
