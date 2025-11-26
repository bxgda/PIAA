namespace Zadatak_2_PiAA
{
    public class Bucket
    {
        public static void BucketSort(List<int> arr)
        {
            if (arr == null || arr.Count == 0)
                return;

            int maxValue = arr.Max();
            int n = arr.Count;

            List<int>[] buckets = new List<int>[n];

            for (int i = 0; i < n; i++)
                buckets[i] = new List<int>();

            foreach (var item in arr)
            {
                // delim sa maxValue da bih dobio vrednost izmedju 0 i 1
                int bucketIndex = (int)((item / (float)maxValue) * (n - 1));

                buckets[bucketIndex].Add(item);
            }

            //PrintBucketsHelper(buckets);

            for (int i = 0; i < n; i++)
                Insertion.InsertionSort(buckets[i]);

            int j = 0;
            for (int i = 0; i < n; i++)
                foreach (var item in buckets[i])
                    arr[j++] = item;
        }

        // Pomocna funkcija koja prikazuje stanje bucket-a (za debagovanje)
        private static void PrintBucketsHelper(List<int>[] buckets)
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                Console.Write($"Bucket {i}: ");
                foreach (var item in buckets[i])
                    Console.Write(item + " ");
                Console.WriteLine();
            }
        }
    }
}
