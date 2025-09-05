using System;

namespace _19059Csharp
{
    internal class Program
    {
        public static void StampajNiz(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
                Console.WriteLine(niz[i]);
        }

        public static void Sortiraj(int[] niz, string algoritam)
        {
            switch (algoritam)
            {
                case "Selection":
                    AlgoritmiZaSortiranje.Selection(niz);
                    Console.WriteLine($"- Min. cena je: {IzracunajMinCenu(niz)}\n");
                    break;

                case "Quick":
                    AlgoritmiZaSortiranje.Quick(niz);
                    Console.WriteLine($"- Min. cena je: {IzracunajMinCenu(niz)}\n");
                    break;

                case "Counting":
                    AlgoritmiZaSortiranje.Counting(niz);
                    Console.WriteLine($"- Min. cena je: {IzracunajMinCenu(niz)}\n");
                    break;

                default:
                    break;
            }
        }

        public static int[] GenerisiNiz(int duzinaNiza)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int[] niz = new int[duzinaNiza];

            for (int i = 0; i < duzinaNiza; i++)
                niz[i] = rnd.Next(0, 10001);

            return niz;
        }

        // resenje 2. problema
        public static int IzracunajMinCenu(int[] niz)           //trebalo bi i k da se prosledi ali mi podrazumevamo da je 0.2
        {
            double k = niz.Length * 0.2, brojSlatkisa = 0;
            int cena = 0, i = 0;

            while (brojSlatkisa <= niz.Length)
            {
                cena += niz[i++];
                brojSlatkisa += k;
            }

            return cena;
        }

        public static void PokreniSve()
        {
            int[] velicineNizova = { 100, 1000, 10000, 100000, 1000000, 10000000 };

            for (int i = 0; i < velicineNizova.Length; i++)
            {
                Console.WriteLine($"*** Velicina niza: {velicineNizova[i]}\n");

                int[] niz1 = GenerisiNiz(velicineNizova[i]);            
                int[] niz2 = (int[])niz1.Clone();
                int[] niz3 = (int[])niz1.Clone();

                // ne pokrecem selection sort za nizove vece od 1 milion jer je presporo
                if (i < 5)                          
                    Sortiraj(niz1, "Selection");

                Sortiraj(niz2, "Quick");

                Sortiraj(niz3, "Counting");

                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            }
        }
        
        static void Main(string[] args)
        {
            PokreniSve();
        }
    }
}
