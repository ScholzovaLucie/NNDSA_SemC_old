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
            singleBufferManager = new BufferManager(fileManager);
            doubleBufferManager = new BufferManager(fileManager);
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

        private double TestBufferPerformance(BufferManager bufferManager)
        {
            for (int i = 0; i < 5; i++)
            {
                bufferManager.ProcessNextBlock(i);
            }

            Stopwatch stopwatch = new Stopwatch();
            long totalMilliseconds = 0;

            for (int i = 0; i < 50; i++) 
            {
                stopwatch.Restart();  
                bufferManager.ProcessNextBlock(i % 10);  
                stopwatch.Stop();
                totalMilliseconds += stopwatch.ElapsedMilliseconds;
            }

            double averageTime = totalMilliseconds / 50.0; 
            return averageTime;
        }
    }
}
