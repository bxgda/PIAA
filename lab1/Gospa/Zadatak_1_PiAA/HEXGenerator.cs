using System.Text;

namespace Zadatak_1_PiAA
{
    public class HEXGenerator
    {
        private static readonly Random m_random = new Random();
        private static readonly string m_hexValues = "0123456789ABCDEF";

        public static void GenerateFiles(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // 100.txt, 1000.txt, 10000.txt, 100000.txt
            for (int i = 2; i <= 5; i++)
            {
                string fileName = $"{folder}/{(int)Math.Pow(10, i)}.txt";

                try
                {
                    // Popuni fajl sa random HEX vrednostima
                    GenerateFile(fileName, (int)Math.Pow(10, i));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Greska prilikom generisanja fajla {fileName}");
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void GenerateFile(string fileName, int numOfWords)
        {
            StringBuilder sb = new StringBuilder();
            int maxWordLength = 20;

            int currentNumOfWords = 0;
            while (currentNumOfWords < numOfWords)
            {
                int wordLength = m_random.Next(1, maxWordLength + 1);
                for (int i = 0; i < wordLength; i++)
                    sb.Append(m_hexValues[m_random.Next(m_hexValues.Length)]);

                sb.Append(' ');
                ++currentNumOfWords;
            }

            File.WriteAllText(fileName, sb.ToString());
        }
    }
}
