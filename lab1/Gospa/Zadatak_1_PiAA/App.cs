namespace Zadatak_1_PiAA
{
    public class App
    {
        private List<string> m_asciiFiles = new List<string>();
        private List<string> m_hexFiles = new List<string>();

        // Paterni za KMP
        private string[] m_asciiPatterns =
        {
            "heard",
            "appearance",
            "extraordinary powers",
            "Then perhaps you will kindly explain how it is tha",
        };
        private string[] m_hexPatterns =
        {
            "10AE7",
            "F76BA5AAB7",
            "04806496FFDACD2A2 AB",
            "5DD2EE9A1FAC8103029 0220B 7997025B E2F416B0C7BD8 0",
        };

        private FileType m_currentFileType;

        public App() { }

        #region Public Methods

        public void StartLevenshtein(FileType fileType)
        {
            m_currentFileType = fileType;
            var files = m_currentFileType == FileType.ASCII ? m_asciiFiles : m_hexFiles;

            if (files.Count == 0)
            {
                Console.WriteLine("Nije moguce izvrsiti pretragu Levenshtein algoritmom.");
                return;
            }

            Console.WriteLine("Zapocinje pretraga Levenshtein algoritmom...");

            string result = string.Empty;

            foreach (var file in files)
            {
                string text = File.ReadAllText(file);

                result += $"Pretraga fajla: {Path.GetFileName(file)}\n";
                result += "-----------------------------------------------------------\n";

                // Poziv algoritma
                Levenshtein.Start(text, ref result, fileType);
            }

            try
            {
                string nameForFileSave = m_currentFileType == FileType.ASCII ? "ASCII_Levenshtein.txt" : "HEX_Levenshtein.txt";

                SaveToFile(Path.GetDirectoryName(files[0])!, nameForFileSave, result);

                Console.WriteLine("Pretraga Levenshtein algoritmom zavrsena.");
                Console.WriteLine($"\nRezultati sacuvani u fajl:\n  {Path.GetDirectoryName(files[0])!}\\{nameForFileSave}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom cuvanja rezultata u fajl.");
                Console.WriteLine(e.Message);
            }
        }

        public void StartKMP()
        {
            var files = m_currentFileType == FileType.ASCII ? m_asciiFiles : m_hexFiles;
            var patterns = m_currentFileType == FileType.ASCII ? m_asciiPatterns : m_hexPatterns;

            if (files.Count == 0)
            {
                Console.WriteLine("Nije moguce izvrsiti pretragu KMP algoritmom.");
                return;
            }

            string result = string.Empty;
            string performance = string.Empty;

            foreach (string pattern in patterns)
            {
                result += $"Pretraga stringa: \"{pattern}\" ({pattern.Length} karaktera)\n";
                result += "-----------------------------------------------------------\n";

                performance += $"Pretraga stringa: \"{pattern}\" ({pattern.Length} karaktera)\n";
                performance +=
                    "-------------------------------------------------------------------\n";

                foreach (string file in files)
                {
                    string text = File.ReadAllText(file);

                    result += $"Pretraga fajla: {Path.GetFileName(file)}\n";

                    performance += "  Fajl: " + Path.GetFileName(file).PadRight(10) + " ";

                    // Poziv algoritma
                    KMPMatcher.Start(text, pattern, ref result, ref performance);

                    result += "\n\n";
                }

                result += "\n\n";
                performance += "\n\n";
            }

            SaveResultsAndPerformace( result, performance,
                m_currentFileType == FileType.ASCII ? "ASCII_KMP_Results.txt" : "HEX_KMP_Results.txt",
                m_currentFileType == FileType.ASCII ? "ASCII_KMP_Performance.txt" : "HEX_KMP_Performance.txt"
            );
        }

        public void LoadASCIIFiles(string folderPath)
        {
            LoadFiles(folderPath, FileType.ASCII);
            m_currentFileType = FileType.ASCII;
        }

        public void LoadHexFiles(string folderPath)
        {
            LoadFiles(folderPath, FileType.HEX);
            m_currentFileType = FileType.HEX;
        }

        #endregion

        #region Private Methods

        private void LoadFiles(string folderPath, FileType fileType)
        {
            var files = fileType == FileType.ASCII ? m_asciiFiles : m_hexFiles;

            files.Clear();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Folder sa tekstovima za pretragu ne postoji.");
                return;
            }

            files.AddRange(Directory.GetFiles(folderPath, "1*.txt"));
            Console.WriteLine("Fajlovi sa tekstovima za pretragu ucitani.");

            foreach (string file in files)
                Console.WriteLine($"  {(file)}");
            Console.WriteLine();
        }

        private void SaveResultsAndPerformace( string result, string performance, string resultName, string performanceName)
        {
            // Sacuvaj rezultate u fajlove
            string currentFolder = Path.GetDirectoryName(
                m_currentFileType == FileType.ASCII ? m_asciiFiles[0] : m_hexFiles[0]
            )!;

            try
            {
                SaveToFile(currentFolder, resultName, result);
                SaveToFile(currentFolder, performanceName, performance);

                Console.WriteLine(
                    $"Rezultati sacuvani u fajlove:\n  {Path.Combine(currentFolder, resultName)}\n  {Path.Combine(currentFolder, performanceName)}\n"
                );
            }
            catch (Exception e)
            {
                Console.WriteLine("Greska prilikom cuvanja rezultata u fajlove.");
                Console.WriteLine(e.Message);
            }
        }

        private void SaveToFile(string folderPath, string fileName, string content)
        {
            string path = Path.Combine(folderPath, fileName);

            File.WriteAllText(path, content);
        }

        #endregion
    }

    public enum FileType
    {
        ASCII,
        HEX,
    }
}
