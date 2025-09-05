using System;
using System.Diagnostics;

namespace _19059Csharp
{
    internal class AlgoritmiZaSortiranje
    {
        private static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        private static void StampajRezultate(TimeSpan vreme, long memorija)
        {
            Console.WriteLine($"- vreme: {vreme.TotalSeconds} s\n- memorija: {memorija} B");
        }

        public static void Selection(int[] niz)
        {
            Stopwatch stoperica = new Stopwatch();
            long memorijaNaPocetku = GC.GetTotalMemory(false);

            int n = niz.Length;

            stoperica.Start();

            for (int i = 0; i < n - 1; i++)
            {
                int min = i;

                for (int j = i; j < n; j++)
                    if (niz[j] < niz[min])
                        min = j;

                Swap(ref niz[i], ref niz[min]);
            }

            stoperica.Stop();
            TimeSpan vreme = stoperica.Elapsed;
            long memorijaNaKraju = GC.GetTotalMemory(false);

            Console.WriteLine("### Selection:");
            StampajRezultate(vreme, memorijaNaKraju - memorijaNaPocetku);
        }

        public static void Quick(int[] niz)
        {
            Stopwatch stoperica = new Stopwatch();
            long memorijaNaPocetku = GC.GetTotalMemory(false);
            stoperica.Start();

            QuickSort(niz, 0, niz.Length - 1);

            stoperica.Stop();
            TimeSpan vreme = stoperica.Elapsed;
            long memorijaNaKraju = GC.GetTotalMemory(false);

            Console.WriteLine("### Quick:");
            StampajRezultate(vreme, memorijaNaKraju - memorijaNaPocetku);
        }

        private static void QuickSort(int[] niz, int pocetak, int kraj)
        {
            if (pocetak < kraj)
            {
                int pivot = Partition(niz, pocetak, kraj);
                QuickSort(niz, pocetak, pivot - 1);
                QuickSort(niz, pivot + 1, kraj);
            }
        }

        private static int Partition(int[] niz, int pocetak, int kraj)
        {
            int pivot = niz[kraj];
            int indeks = pocetak - 1;

            for (int i = pocetak; i < kraj; i++) 
            {
                if (niz[i] < pivot)
                {
                    indeks++;
                    if (indeks != i)                   
                        Swap(ref niz[indeks], ref niz[i]);
                }
            }
            if (indeks + 1 != kraj)                   
                Swap(ref niz[indeks + 1], ref niz[kraj]);
            return indeks + 1;
        }

        public static void Counting(int[] nizA)             //trebalo bi da se i prosledi opseg brojeva ali mi podrazumevamo da je 10 000
        {
            Stopwatch stoperica = new Stopwatch();
            long memorijaNaPocetku = GC.GetTotalMemory(false);
            stoperica.Start();

            int[] nizB = new int[nizA.Length];
            int[] nizC = new int[10001];

            for (int i = 0; i < nizC.Length; i++)
                nizC[i] = 0;

            for (int i = 0; i < nizA.Length; i++)
                nizC[nizA[i]]++;

            for (int i = 1; i < nizC.Length; i++)
                nizC[i] += nizC[i - 1];

            for (int i = nizA.Length - 1; i >= 0; i--)
            {
                nizB[nizC[nizA[i]] - 1] = nizA[i];         
                nizC[nizA[i]]--;
            }
            for (int i = 0; i < nizA.Length; i++)
                nizA[i] = nizB[i];

            stoperica.Stop();
            TimeSpan vreme = stoperica.Elapsed;
            long memorijaNaKraju = GC.GetTotalMemory(false);

            Console.WriteLine("### Counting:");
            StampajRezultate(vreme, memorijaNaKraju - memorijaNaPocetku);
        }
    }
}
