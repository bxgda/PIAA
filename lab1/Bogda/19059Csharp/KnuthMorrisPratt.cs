using System;
using System.Diagnostics;
using System.IO;

namespace _19059Csharp
{
    internal class KnuthMorrisPratt
    {
        public static void PretragaKMP(string tekst, string podstring, StreamWriter izlaz)
        {
            Stopwatch stoperica = new Stopwatch();
            stoperica.Start();

            int n = tekst.Length;
            int m = podstring.Length;
            izlaz.WriteLine($"Duzinu teksta: {n}, duzinu podstringa: {m}\npodstring: {podstring}\n");
            int[] prefiksTabela = Prefiksi(podstring);
            int br = 0, j = 0;

            for (int i = 0; i < n; i++)
            {
                while (j > 0 && tekst[i] != podstring[j])
                    j = prefiksTabela[j - 1];

                if (tekst[i] == podstring[j])
                    j++;

                if (j == m)
                {
                    br++;
                    izlaz.WriteLine($"nadjeno poklapanje na indeksu {i - m + 1}");
                    j = prefiksTabela[j - 1];
                }
            }
            stoperica.Stop();
            TimeSpan vreme = stoperica.Elapsed;
            izlaz.WriteLine($"\nnadjeno je {br} poklapanja\nvreme izvrsenja: {vreme.TotalMilliseconds} ms\n");
        }

        private static int[] Prefiksi(string podstring)
        {
            int duzina = podstring.Length;
            int[] pref = new int[duzina];
            pref[0] = 0;
            int j = 0;

            for (int i = 1; i < duzina; i++)
            {
                while (j > 0 && podstring[i] != podstring[j])
                    j = pref[j - 1];

                if (podstring[i] == podstring[j])
                    j++;

                pref[i] = j;
            }
            return pref;
        }
    }
}
