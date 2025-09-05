using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace _19059Csharp
{
    internal class Compressor
    {
        public static void Kodiraj(string ulazniFajl, string izlazniFajl, string kodnaTablica)
        {
            string tekst = ProcitajTekst(ulazniFajl);                                   // citamo tekst iz .txt fajla i smestamo ga u string

            List<Simbol> simboli = IzracunavanjeUcestanosti(tekst);                     // racunamo ukupnu ucestanost svakog karaktera

            ShannonFano.GenerisanjeKoda(simboli, 0, simboli.Count - 1);                 // za svaki objekat karakter u listi generisemo kod

            UpisKodneTablice(kodnaTablica, simboli);                                    // upisujemo kodnu tablicu

            ShannonFano.KodiranjeTeksta(tekst, simboli, izlazniFajl);                   // kodiramo procitani tekst i upisemo ga u fajl
        }

        public static void Dekodiraj(string ulazniFajl, string izlazniFajl, string kodnaTablica)
        {
            List<Simbol> simboli = CitanjeKodneTablice(kodnaTablica);                   // citamo kodnu tablicu
            
            byte[] bajtovi = ProcitajBinarno(ulazniFajl);                               // procitamo niz bajtova iz .bin fajla

            ShannonFano.DekodiranjeBajtova(bajtovi, izlazniFajl, simboli );             // dekodiramo taj niz i upisujemo u fajl za proveru
        }

        public static List<Simbol> IzracunavanjeUcestanosti(string tekst)
        {
            Dictionary<char, int> ucestanost = new Dictionary<char, int>();             // koriscenje ugradjenog recnika kako bi izbrojali pojavljivanje svakog karaktera

            foreach (char karakter in tekst)
            {
                if (ucestanost.ContainsKey(karakter))                                   // vec se pojavio
                    ucestanost[karakter]++;
                else                                                                    // prvo pojavljivanje
                    ucestanost[karakter] = 1;
            }

            var simboli = new List<Simbol>();                                           // konvertovanje u uredjenu listu koju sortiramo po broju pojavljivanja

            foreach (var karakter in ucestanost)
                simboli.Add(new Simbol(karakter.Key, karakter.Value));

            simboli.Sort((a, b) => b.Ucestanost.CompareTo(a.Ucestanost));

            return simboli;
        }

        public static string ProcitajTekst(string ulazniFajl)
        {
            string tekst;

            using (StreamReader sr = new StreamReader(ulazniFajl))
                tekst = sr.ReadToEnd();
     
            return tekst;
        }

        public static byte[] ProcitajBinarno(string imeFajla)
        {
            using (FileStream fs = new FileStream(imeFajla, FileMode.Open, FileAccess.Read))
            {
                byte[] bajtovi = new byte[fs.Length];                                   // posto fs.Length vraca koliko je veliki fajl u bajtovima mi to koristimo da napravimo bas toliki niz
                fs.Read(bajtovi, 0, bajtovi.Length);                                    // smestamo u niz bajtovi, citamo od 0 indeksa, u duzini dvih bajtova

                return bajtovi;
            }
        }

        public static byte[] TekstBitovaUNizBajtova(string kodiraniTekst)               // pomocna funkcija koja string od 8 karaktera pretvara u 1 bajt
        {
            BitArray bitovi = new BitArray(kodiraniTekst.Length);                       // pravimo niz bitova od 8 elemenata jer je posle tako lakse da se kopira u bajt

            for (int i = 0; i < bitovi.Length; i++)
                bitovi[i] = kodiraniTekst[i] == '1';

            byte[] bajtovi = new byte[1];                                               // nisam znao kako da uradim a da nemam niz od 1 elementa

            bitovi.CopyTo(bajtovi, 0);                                                  // jer ova funkcija radi super i kopira ceo niz bitova u bajt a niz bitova nam je uvek velicine 8

            return bajtovi;
        }

        public static void UpisKodneTablice(string imeFajla, List<Simbol> simboli)  
        {
            using (StreamWriter writer = new StreamWriter(imeFajla))
            {
                foreach (var simbol in simboli)
                {
                    writer.WriteLine($"{(int)simbol.Karakter},{simbol.Ucestanost},{simbol.Kod}");
                }
            }                                                                           // upisujemo karakter kao int vresnost
        }

        public static List<Simbol> CitanjeKodneTablice(string imeFajla)
        {
            List<Simbol> simboli = new List<Simbol>();

            using (StreamReader reader = new StreamReader(imeFajla))
            {
                string line;
                while ((line = reader.ReadLine()) != null)                              // citamo celu liniju
                {
                    var parts = line.Split(',');                                        // razdvajamo delove
                    char karakter = (char)int.Parse(parts[0]);
                    int ucestanost = int.Parse(parts[1]);
                    string kod = parts[2];

                    Simbol simbol = new Simbol(karakter, ucestanost, kod);
                    simboli.Add(simbol);
                }
            }
            return simboli;
        }

        // svuda podrazumevam da ce fajl da se otvori lepo pa nemam try catch jer je aplikacija eksperimentalnog tipa i znam da ce sigurno svi fajlovi da se otvore
    }
}
