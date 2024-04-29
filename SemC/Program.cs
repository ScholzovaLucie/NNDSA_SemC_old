using SemC;

class Program
{
    private static HeapFileManager fileManager = new HeapFileManager("heapfile.dat");
    private static BufferManager bufferManager = new BufferManager(fileManager);
    private static PerformanceTester performanceTester = new PerformanceTester(fileManager);

    static void Main(string[] args)
    {
        InitializeApplication();
        DisplayMenu();
    }

    private static void InitializeApplication()
    {
        fileManager.InitializeHeapFile(5); // Příklad inicializace s 5 bloky
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Vítejte v aplikaci pro správu sekvenčního zápisu a čtení!");
        Console.WriteLine("-------------------------------------------------");
    }

    private static void DisplayMenu()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nZvolte si akci:");
            Console.WriteLine("1. Načíst blok dat do bufferu");
            Console.WriteLine("2. Zapsat blok dat z bufferu do souboru");
            Console.WriteLine("3. Zobrazit obsah aktuálního bufferu");
            Console.WriteLine("4. Test časové náročnosti při použití jednoho vs. dvou bufferů");
            Console.WriteLine("5. Ukončit aplikaci");
            Console.Write("\nVaše volba: ");

            string choice = Console.ReadLine();
            ProcessUserInput(choice, ref running);
        }
    }

    private static void ProcessUserInput(string choice, ref bool running)
    {
        switch (choice)
        {
            case "1":
                LoadDataBlock();
                break;
            case "2":
                WriteDataFromBuffer();
                break;
            case "3":
                DisplayBufferContents();
                break;
            case "4":
                PerformPerformanceTest();
                break;
            case "5":
                running = false;
                Console.WriteLine("Aplikace byla ukončena. Děkujeme za použití.");
                break;
            default:
                Console.WriteLine("Neplatná volba, zkuste to znovu.");
                break;
        }
    }

    private static void LoadDataBlock()
    {
        Console.Write("Zadejte index bloku, který chcete načíst: ");
        int index = int.Parse(Console.ReadLine());
        bufferManager.ProcessNextBlock(index);
    }

    private static void WriteDataFromBuffer()
    {
        Console.Write("Zadejte index bufferu pro zápis do souboru (1 nebo 2): ");
        int bufferIndex = int.Parse(Console.ReadLine());

        try
        {
            if (bufferIndex == 1 || bufferIndex == 2)
            {
                bufferManager.WriteBufferToFile(bufferIndex);
                Console.WriteLine("Data byla zapsána do souboru.");
            }
            else
            {
                Console.WriteLine("Neplatný index bufferu. Zadejte 1 nebo 2.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Došlo k chybě při zápisu do souboru: {ex.Message}");
        }
    }

    private static void DisplayBufferContents()
    {
        Console.WriteLine("Obsah bufferu:");
        bufferManager.DisplayCurrentBufferContents(); // Implementujte tuto metodu v BufferManager
    }

    private static void PerformPerformanceTest()
    {
        performanceTester.RunPerformanceTest();
    }
}
