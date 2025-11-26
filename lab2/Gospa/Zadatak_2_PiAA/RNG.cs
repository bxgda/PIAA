namespace Zadatak_2_PiAA
{
    public class RNG
    {
        // Generise i vraca listu random brojeva velicine size, izmedju minValue i maxValue
        public static List<int> GenerateNumbers(int minValue, int maxValue, int size)
        {
            List<int> arr = new(size);

            var random = new Random();

            for (int i = 0; i < size; ++i)
            {
                arr.Add(random.Next(minValue, maxValue));
            }

            return arr;
        }

        // Generise fajl sa random brojevima (koriscena samo za testiranje)
        public static void Generate(string filePath, int minValue, int maxValue, int size)
        {
            Console.WriteLine($"Generisanje fajla: {filePath}...");

            using (var writer = new StreamWriter(filePath))
            {
                var random = new Random();
                for (var i = 0; i < size; i++)
                {
                    writer.WriteLine(random.Next(minValue, maxValue));
                }
            }

            Console.WriteLine($"Fajl {filePath} je uspesno generisan.\n");
        }
    }
}
