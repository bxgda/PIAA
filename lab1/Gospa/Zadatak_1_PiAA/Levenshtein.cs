namespace Zadatak_1_PiAA
{
    public class Levenshtein
    {
        private static string[] m_patternsASCII = { "lover", "disturbing", "moustached-evidently" };
        private static string[] m_patternsHEX = { "B2B6F", "64347F4A5A", "065B827AF60B85B964AB" };

        public static void Start(string text, ref string result, FileType fileType)
        {
            if (fileType == FileType.ASCII)
                SearchPatterns(text, m_patternsASCII, ref result);
            else
                SearchPatterns(text, m_patternsHEX, ref result);
        }

        private static void SearchPatterns(string text, string[] patterns, ref string result)
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string pattern in patterns)
            {
                result += $"Za rec: \"{pattern}\" ({pattern.Length} karaktera)\n";
                int maxDist = (int)(pattern.Length * 0.2f);

                foreach (string word in words)
                {
                    // Prosledjujemo i maxDist da bi se prekinula pretraga cim distanca predje maxDist
                    int distance = LevenshteinDistance(pattern, word, maxDist);

                    // Ako se vrati -1 znaci da je prekinuta pretraga i rec je 'dalja' od 20%
                    if (distance != -1 && distance <= maxDist)
                    {
                        result += $"  Rec: \"{word}\" - Udaljenost: {distance}\n";
                    }
                }

                result += "\n";
            }
        }

        private static int LevenshteinDistance(string s, string t, int maxDistance)
        {
            int n = s.Length;
            int m = t.Length;

            int[,] dp = new int[n + 1, m + 1];

            // Popuni prvi red i prvu kolonu
            for (int i = 0; i <= n; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= m; j++)
                dp[0, j] = j;

            for (int i = 1; i <= n; ++i)
            {
                int minDist = dp[i - 1, 0];

                for (int j = 1; j <= m; ++j)
                {
                    if (s[i - 1] == t[j - 1])
                        dp[i, j] = dp[i - 1, j - 1];
                    else
                    {
                        int insert = dp[i, j - 1] + 1;
                        int delete = dp[i - 1, j] + 1;
                        int replace = dp[i - 1, j - 1] + 1;

                        dp[i, j] = Math.Min(insert, Math.Min(delete, replace));
                    }

                    minDist = Math.Min(minDist, dp[i, j]);
                }

                // ovo je dodatna optimizacija jer se u zadatku traze sve reci na
                // udaljenosti manjoj od 20% od duzine reci (maxDistance je 20% od duzine reci)
                if (minDist > maxDistance)
                    return -1;
            }

            return dp[n, m];
        }
    }
}
