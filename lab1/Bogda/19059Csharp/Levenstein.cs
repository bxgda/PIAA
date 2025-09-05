using System;
using System.Diagnostics;
using System.IO;

namespace _19059Csharp
{
    internal class Levenstein
    {
        public static void PretragaLevenstein(string tekst, string podstring, StreamWriter izlaz)
        {
            if (!string.IsNullOrWhiteSpace(tekst))
            {
                izlaz.WriteLine($"Duzina teksta: {tekst.Length}, duzina podstringa: {podstring.Length}\npodstring: {podstring}\n");       //takodje nije bitno vreme za koje se odredi duzina teksta
                Stopwatch stoperica = new Stopwatch();
                int br = 0;
                string[] reci = tekst.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                stoperica.Start(); //ovde pokrecem stopericu jer smatram da nije bitno vreme za koje podelim string na reci a posto je ugradjena fukcija ne znam ni kako radi u pozadini

                foreach (var rec in reci)
                {
                    int udaljenost = LevensteinDistance(rec, podstring);

                    if (udaljenost <= podstring.Length * 0.2)
                    {
                        br++;
                        izlaz.WriteLine($"nadjeno poklapanje do 20% reci: {rec} i prosledjenog podstringa");
                    }
                }
                stoperica.Stop();
                TimeSpan vreme = stoperica.Elapsed;
                izlaz.WriteLine($"\nnadjeno je {br} poklapanja\nvreme izvrsenja: {vreme.TotalMilliseconds} ms\n");
            }
        }

        public static int LevensteinDistance(string tekst, string uporedniTekst)
        {
            int n = tekst.Length;
            int m = uporedniTekst.Length;
            int[,] mat = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                mat[i, 0] = i;
            for (int i = 0; i <= m; i++)
                mat[0, i] = i;

            int cena;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (tekst[i - 1] == uporedniTekst[j - 1])
                        cena = 0;
                    else
                        cena = 1;

                    mat[i, j] = Math.Min(Math.Min(mat[i - 1, j] + 1, mat[i, j - 1] + 1), mat[i - 1, j - 1] + cena);
                }
            }
            return mat[n, m];
        }
    }
}

