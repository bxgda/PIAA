namespace Zadatak_2_PiAA
{
    public class Insertion
    {
        public static void InsertionSort(List<int> arr)
        {
            if (arr == null || arr.Count == 0)
                return;

            int n = arr.Count;

            if (n == 0)
                return;

            for (int i = 1; i < n; i++)
            {
                int j = i - 1;
                int key = arr[i];

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }
        }
    }
}
