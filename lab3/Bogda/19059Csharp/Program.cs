namespace _19059Csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // OPET PAKAO KAKO SAM ZVAO FUNKCIJE HARD-CODED

            // svi potrebni folderi i fajlovi se nalaze u /bin/Release

            // 100

            Compressor.Kodiraj("100/loremLpsum/ulaz100.txt", "100/loremLpsum/izlaz100.bin", "100/loremLpsum/kod100.txt");
            Compressor.Dekodiraj("100/loremLpsum/izlaz100.bin", "100/loremLpsum/izlaz100provera.txt", "100/loremLpsum/kod100.txt");

            Compressor.Kodiraj("100/random/rndUlaz100.txt", "100/random/rndIzlaz100.bin", "100/random/rndKod100.txt");
            Compressor.Dekodiraj("100/random/rndIzlaz100.bin", "100/random/randIzlaz100provera.txt", "100/random/rndKod100.txt");

            // 1000

            Compressor.Kodiraj("1000/loremLpsum/ulaz1000.txt", "1000/loremLpsum/izlaz1000.bin", "1000/loremLpsum/kod1000.txt");
            Compressor.Dekodiraj("1000/loremLpsum/izlaz1000.bin", "1000/loremLpsum/izlaz1000provera.txt", "1000/loremLpsum/kod1000.txt");

            Compressor.Kodiraj("1000/random/rndUlaz1000.txt", "1000/random/rndIzlaz1000.bin", "1000/random/rndKod1000.txt");
            Compressor.Dekodiraj("1000/random/rndIzlaz1000.bin", "1000/random/randIzlaz1000provera.txt", "1000/random/rndKod1000.txt");

            // 10k

            Compressor.Kodiraj("10k/loremLpsum/ulaz10k.txt", "10k/loremLpsum/izlaz10k.bin", "10k/loremLpsum/kod10k.txt");
            Compressor.Dekodiraj("10k/loremLpsum/izlaz10k.bin", "10k/loremLpsum/izlaz10kprovera.txt", "10k/loremLpsum/kod10k.txt");

            Compressor.Kodiraj("10k/random/rndUlaz10k.txt", "10k/random/rndIzlaz10k.bin", "10k/random/rndKod10k.txt");
            Compressor.Dekodiraj("10k/random/rndIzlaz10k.bin", "10k/random/randIzlaz10kprovera.txt", "10k/random/rndKod10k.txt");

            // 100k

            Compressor.Kodiraj("100k/loremLpsum/ulaz100k.txt", "100k/loremLpsum/izlaz100k.bin", "100k/loremLpsum/kod100k.txt");
            Compressor.Dekodiraj("100k/loremLpsum/izlaz100k.bin", "100k/loremLpsum/izlaz100kprovera.txt", "100k/loremLpsum/kod100k.txt");

            Compressor.Kodiraj("100k/random/rndUlaz100k.txt", "100k/random/rndIzlaz100k.bin", "100k/random/rndKod100k.txt");
            Compressor.Dekodiraj("100k/random/rndIzlaz100k.bin", "100k/random/randIzlaz100kprovera.txt", "100k/random/rndKod100k.txt");

            // 1m

            Compressor.Kodiraj("1m/loremLpsum/ulaz1m.txt", "1m/loremLpsum/izlaz1m.bin", "1m/loremLpsum/kod1m.txt");
            Compressor.Dekodiraj("1m/loremLpsum/izlaz1m.bin", "1m/loremLpsum/izlaz1mprovera.txt", "1m/loremLpsum/kod1m.txt");

            Compressor.Kodiraj("1m/random/rndUlaz1m.txt", "1m/random/rndIzlaz1m.bin", "1m/random/rndKod1m.txt");
            Compressor.Dekodiraj("1m/random/rndIzlaz1m.bin", "1m/random/randIzlaz1mprovera.txt", "1m/random/rndKod1m.txt");

            // 10m

            Compressor.Kodiraj("10m/loremLpsum/ulaz10m.txt", "10m/loremLpsum/izlaz10m.bin", "10m/loremLpsum/kod10m.txt");
            Compressor.Dekodiraj("10m/loremLpsum/izlaz10m.bin", "10m/loremLpsum/izlaz10mprovera.txt", "10m/loremLpsum/kod10m.txt");

            Compressor.Kodiraj("10m/random/rndUlaz10m.txt", "10m/random/rndIzlaz10m.bin", "10m/random/rndKod10m.txt");
            Compressor.Dekodiraj("10m/random/rndIzlaz10m.bin", "10m/random/randIzlaz10mprovera.txt", "10m/random/rndKod10m.txt");
        }
    }
}
