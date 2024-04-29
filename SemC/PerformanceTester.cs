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
            singleBufferManager = new BufferManager(fileManager, 1); // Předpokládáme konstruktor s parametrem počtu bufferů
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

            // Zde předpokládáme, že BufferManager má metodu pro zpracování bloků dat
            for (int i = 0; i < 10; i++) // Testujeme na 10 bloků
            {
                bufferManager.ProcessNextBlock(i);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds / 10; // Vracíme průměrný čas na blok
        }
    }
}
