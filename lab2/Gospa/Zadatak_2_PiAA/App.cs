using System.Diagnostics;

namespace Zadatak_2_PiAA
{
    public class App
    {
        private List<Action<List<int>>> m_sortFunctions = new List<Action<List<int>>>
        {
            Insertion.InsertionSort,
            Heap.HeapSort,
            Bucket.BucketSort,
        };

        private int[] m_sizes = { 100, 1000, 10000, 100000, 1000000, 10000000 };

        public void Start()
        {
            // Za svaku velicinu niza se poziva funkcija SolveProblem koja resava problem pomocu svake od tri metode sortiranja
            foreach (var size in m_sizes)
            {
                Console.WriteLine($"Niz od {size} elemenata...");

                var numbers = RNG.GenerateNumbers(0, 10000, size);
                SolveProblem(numbers);

                Console.WriteLine("------------------------------------------------");
            }
        }

        private void SolveProblem(List<int> numbers)
        {
            int n = numbers.Count;
            int k = (int)(n * 0.2f);

            Console.WriteLine($"  N: {n}, K: {k}\n");

            // Resavanje problema koristeci sve tri metode sortiranja
            foreach (var sortFunction in m_sortFunctions)
            {
                var copy = new List<int>(numbers);

                var watch = Stopwatch.StartNew();
                long memoryBefore = GC.GetTotalMemory(false);

                // ako je N >= 1000000, i ako je u pitanju insertion sort, preskace se jer sortiranje traje predugo
                if (n >= 1000000 && sortFunction == Insertion.InsertionSort)
                {
                    PrintResult(-1, -1, sortFunction.Method.Name, -1);
                    watch.Stop();
                    continue;
                }

                // Resavanje samog problema
                sortFunction(copy);
                int minimumCost = CalculateMinimumCost(copy, k);

                long memoryAfter = GC.GetTotalMemory(false);
                watch.Stop();

                PrintResult(
                    minimumCost,
                    memoryAfter - memoryBefore,
                    sortFunction.Method.Name,
                    watch.Elapsed.TotalSeconds
                );

                copy.Clear();
            }

            Console.WriteLine();
        }

        private int CalculateMinimumCost(List<int> arr, int k)
        {
            int minimumCost = 0;
            int n = arr.Count;
            int i = 0;

            while (i < n)
            {
                minimumCost += arr[i];
                i += (k + 1); // Za svaki kupljeni slatkis, dobije se jos k slatkisa besplatno
            }

            return minimumCost;
        }

        private void PrintResult(int minimumCost, long memory, string sortFunctionName, double time)
        {
            string minimumCostDisplay = minimumCost != -1 ? minimumCost.ToString() : "N/A";
            string memoryDisplay = memory != -1 ? memory.ToString() : "N/A";
            string timeDisplay = time != -1 ? time.ToString() : "N/A";

            Console.WriteLine($"  {"Minimalna cena", -24}: {minimumCostDisplay}");
            Console.WriteLine($"  {"Utrosak memorije", -24}: {memoryDisplay} bytes");
            Console.WriteLine($"  Koriscen {sortFunctionName, -15}: {timeDisplay} s\n");
        }
    }
}
