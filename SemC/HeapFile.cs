using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class HeapFile
    {
        private string filePath;
        private int bufferSize;
        private int totalRecords;

        public HeapFile(string filePath, int bufferSize, int totalRecords)
        {
            this.filePath = filePath;
            this.bufferSize = bufferSize;
            this.totalRecords = totalRecords;
        }

        public void WriteRecords(bool useDualBuffer)
        {
            using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            var primaryBuffer = new Buffer<byte>(bufferSize);
            var secondaryBuffer = useDualBuffer ? new Buffer<byte>(bufferSize) : null;
            int totalDataSize = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < totalRecords; i++)
            {
                var data = Encoding.UTF8.GetBytes($"Record {i}\n");
                var currentBuffer = useDualBuffer && (i % 2 == 0) ? secondaryBuffer : primaryBuffer;
                currentBuffer.Add((byte)i);

                if (currentBuffer.Count + data.Length > bufferSize)
                {
                    var toWrite = currentBuffer.ToArray();
                    fs.Write(toWrite, 0, toWrite.Length);
                    totalDataSize += toWrite.Length;
                    currentBuffer.Clear();
                }

                currentBuffer.BlockCopy(data, 0, currentBuffer.Count, data.Length);
                
            }

            stopwatch.Stop();
            string bufferUsage = useDualBuffer ? "dvojitého" : "jednoduchého";
            Console.WriteLine($"Zapsáno {totalDataSize} bytů, trvání: {stopwatch.ElapsedMilliseconds} ms, použití {bufferUsage} bufferu.");

            // Write remaining data in both buffers
            FlushBuffer(fs, primaryBuffer, ref totalDataSize);
            if (useDualBuffer)
            {
                FlushBuffer(fs, secondaryBuffer, ref totalDataSize);
            }
 
        }

        private void FlushBuffer(FileStream fs, Buffer<byte> buffer, ref int totalDataSize)
        {
            if (buffer.Count > 0)
            {
                var remaining = buffer.ToArray();
                fs.Write(remaining, 0, remaining.Length);
                totalDataSize += remaining.Length;
                buffer.Clear();
            }
        }

        public void ReadRecords(bool useDualBuffer)
        {
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var primaryBuffer = new Buffer<byte>(bufferSize);
            var secondaryBuffer = useDualBuffer ? new Buffer<byte>(bufferSize) : null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int bytesRead, totalReadSize = 0;
            byte[] data = new byte[bufferSize];

            while ((bytesRead = fs.Read(data, 0, bufferSize)) > 0)
            {
                var currentBuffer = useDualBuffer && (totalReadSize / bufferSize % 2 == 0) ? secondaryBuffer : primaryBuffer;
                currentBuffer.Clear();

           
                    currentBuffer.BlockCopy(data, 0, 0, bytesRead);
                    var readString = Encoding.UTF8.GetString(currentBuffer.ToArray());
                    totalReadSize += bytesRead;
                

            }

            stopwatch.Stop();
            string bufferUsage = useDualBuffer ? "dvojitého" : "jednoduchého";
            Console.WriteLine($"Načteno {totalReadSize} bytů, trvání: {stopwatch.ElapsedMilliseconds} ms, použití {bufferUsage} bufferu.");
        }
    }

}

