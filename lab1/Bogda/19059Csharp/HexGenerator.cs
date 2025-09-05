using System;
using System.IO;

namespace _19059Csharp
{
    internal class HexGenerator
    {
        public static void Generisi(int brojReci, string imeFajla)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(imeFajla))
                {
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    for (int i = 0; i < brojReci; i++)
                    {
                        int duzinaReci = rnd.Next(5, 20);
                        string rec = GenerisiHexString(duzinaReci, rnd);
                        sw.Write(rec + " ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + $"\n\nproblem sa otvaranjem fajla {imeFajla}\n");
            }
        }

        private static string GenerisiHexString(int duzina, Random rnd)
        {
            string rec = "";

            for (int i = 0; i < duzina; i++)
            {
                rec += rnd.Next(16).ToString("X");
            }

            return rec;
        }
    }
}
