namespace Zadatak_2_PiAA
{
    public class Heap
    {
        public static void HeapSort(List<int> arr)
        {
            if (arr == null || arr.Count == 0)
                return;

            int n = arr.Count;
            BuildHeap(arr, n);

            for (int i = n - 1; i >= 1; i--)
            {
                Swap(arr, 0, i);
                n--;
                Heapify(arr, n, 0);
            }
        }

        private static void BuildHeap(List<int> arr, int n)
        {
            for (int i = arr.Count / 2; i >= 0; i--)
                Heapify(arr, n, i);
        }

        private static void Heapify(List<int> arr, int n, int i)
        {
            int left = Left(i);
            int right = Right(i);
            int largest = i;

            if (left < n && arr[left] > arr[i])
                largest = left;
            else
                largest = i;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, n, largest);
            }
        }

        private static int Left(int i) => 2 * i + 1;

        private static int Right(int i) => 2 * i + 2;

        private static int Parent(int i) => ((i - 1) / 2);

        private static void Swap(List<int> arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
