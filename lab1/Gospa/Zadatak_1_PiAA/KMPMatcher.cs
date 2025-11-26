using System.Diagnostics;

namespace Zadatak_1_PiAA
{
    public class KMPMatcher
    {
        public static void Start(string T, string P, ref string result, ref string performance)
        {
            var stopWatch = Stopwatch.StartNew();

            // Preprocesiranje
            int[] lps = new int[P.Length];
            LPSGenerator(lps, P, P.Length);

            // Pretraga
            int cnt = Search(T, P, lps, ref result);

            stopWatch.Stop();
            double elapsedTime = stopWatch.Elapsed.TotalMilliseconds;

            performance +=
                $"{String.Format("{0, -27}", $"(Ukupno pojavljivanja: {cnt})")} - trajanje: {elapsedTime}ms\n";
        }

        private static int Search(string T, string P, int[] lps, ref string results)
        {
            int n = T.Length;
            int m = P.Length;

            int cnt = 0;

            int q = 0;
            for (int i = 0; i < n; ++i)
            {
                while (q > 0 && P[q] != T[i])
                    q = lps[q - 1];

                if (P[q] == T[i])
                    ++q;

                if (q == m)
                {
                    results += $"  Pronalazak na poziciji {i - m + 1}\n";
                    ++cnt;
                    q = lps[q - 1];
                }
            }

            results += $"Ukupno pojavljivanja: {cnt}\n";

            return cnt;
        }

        private static void LPSGenerator(int[] lps, string pattern, int m)
        {
            lps[0] = 0;

            int k = 0;
            for (int q = 1; q < m; q++)
            {
                while (k > 0 && pattern[k] != pattern[q])
                    k = lps[k - 1];

                if (pattern[k] == pattern[q])
                    ++k;

                lps[q] = k;
            }
        }
    }
}
