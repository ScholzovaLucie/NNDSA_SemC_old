using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemC
{
    public class PerformanceTester
    {
        private HeapFileManager fileManager;
        private BufferManager singleBufferManager;
        private BufferManager doubleBufferManager;

        public PerformanceTester(HeapFileManager fileManager)
        {
            this.fileManager = fileManager;
            singleBufferManager = new BufferManager(fileManager, 1);
            doubleBufferManager = new BufferManager(fileManager, 2);
        }

        public void RunPerformanceTest()
        {
            Console.WriteLine("Zahajuje se test časové náročnosti...");

            var singleBufferTime = TestBufferPerformance(singleBufferManager);
            var doubleBufferTime = TestBufferPerformance(doubleBufferManager);

            Console.WriteLine("Testování časové náročnosti dokončeno:");
            Console.WriteLine($"Průměrný čas s jedním bufferem: {singleBufferTime} ms");
            Console.WriteLine($"Průměrný čas s dvěma buffery: {doubleBufferTime} ms");
        }

        private long TestBufferPerformance(BufferManager bufferManager)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10; i++) 
            {
                bufferManager.ProcessNextBlock(i);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds / 10; 
        }
    }
}
