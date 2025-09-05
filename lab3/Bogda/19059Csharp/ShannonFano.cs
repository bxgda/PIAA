using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace _19059Csharp
{
    internal class ShannonFano
    {
        public static void GenerisanjeKoda(List<Simbol> simboli, int pocetak, int kraj) 
        {
            if (pocetak == kraj)                                    // prvi uslov izlaska iz rekurzije (kada se prosledni niz sa 1 elementom)
            {
                simboli[pocetak].Kod += "0";
                return;
            }

            if (pocetak + 1 == kraj)                                // drugi uslov izlaska iz rekurzije (kada se prosledi niz sa 2 elemenata
            {
                simboli[pocetak].Kod += "0";
                simboli[kraj].Kod += "1";
                return;
            }

            int ukupnaUcestanost = 0;                               // racunamo ukupnu ucestanost u podnizu (celom nizu u prvom prolasku)
            for (int i = pocetak; i <= kraj; i++)
                ukupnaUcestanost += simboli[i].Ucestanost;

            int polaUcestanosti = ukupnaUcestanost / 2;
            int suma = 0;
            int indeksPolovine = pocetak;
            
            for (int i = pocetak; i <= kraj; i++)                   // trazimo ideks koji deli niz na 2 dela sa podjednakim ucestanostima
            {
                suma += simboli[i].Ucestanost;
                if (suma >= polaUcestanosti)
                {
                    indeksPolovine = i;
                    break;
                }
            }

            for (int i = pocetak; i <= indeksPolovine; i++)         // kodiramo gornji deo niza sa 0 a donji sa 1
                simboli[i].Kod += "0";

            for (int i = indeksPolovine + 1; i <= kraj; i++)
                simboli[i].Kod += "1";

            GenerisanjeKoda(simboli, pocetak, indeksPolovine);      // rekruzivni pozivi za donji i gornji deo niza
            GenerisanjeKoda(simboli, indeksPolovine + 1, kraj);
        }

        public static void KodiranjeTeksta(string tekst, List<Simbol> simboli, string imeFajla)
        {
            Dictionary<char, string> kodovi = new Dictionary<char, string>();                                   // pravimo recnik kako bi efikasnije i lakse proveravali kodove karaktera

            foreach (var simbol in simboli)
                kodovi[simbol.Karakter] = simbol.Kod;

            using (BinaryWriter bw = new BinaryWriter(File.Open(imeFajla, FileMode.Create)))
            {
                string trenutniKod = string.Empty;

                foreach (char karakter in tekst)
                {
                    if (kodovi.ContainsKey(karakter))                                                           // realno svaki karakter ce da postoji u recniku ako imamo dobar kljuc
                    {
                        foreach (char bit in kodovi[karakter])                                                  // svaki bit u kodu posebno dodajemo u string 
                        {
                            trenutniKod += bit;

                            if (trenutniKod.Length == 8)                                                        // cim duzina stringa dostigne 8 odmah to pretvaramo u bajt i stampamo
                            {
                                byte[] bajt = Compressor.TekstBitovaUNizBajtova(trenutniKod.ToString());        // pretvaramo string u bajt sa pomocnom funkcijom (da bi ovde kod bio pregledniji)
                                
                                trenutniKod = string.Empty;                                                     // praznimo string i sve iz pocetka

                                bw.Write(bajt);                                     
                            }
                        }                                                                                       // u zavisnosti od broja karaktera moguce je da se poslednjih par karaktera ne dekodiraju dobro jer ih i ne upisujemo
                    }                                                                                           // upisujemo 8 po 8 radi efikasnosti 
                }
            }
        }

        public static void DekodiranjeBajtova(byte[] bajtovi, string imeFajla, List<Simbol> simboli)
        {
            Dictionary<string, char> mapa = new Dictionary<string, char>();                                     // pravimo recnik kako bi efikasnije i lakse proveravali kodove karaktera

            foreach (Simbol s in simboli)
                mapa[s.Kod] = s.Karakter;

            BitArray bitovi = new BitArray(bajtovi);                                                            // lako kopiramo sve iz bajtova u niz bitova iz kod lako proveramo 0 i 1

            using (StreamWriter sw = new StreamWriter(new FileStream(imeFajla, FileMode.Create)))
            {
                string trenutniKod = string.Empty; 

                for (int i = 0; i < bitovi.Length; i++)
                {
                    if (bitovi[i])                                                                              // popunjavao trenutni kod sa 0 ili 1
                        trenutniKod += '1';
                    else
                        trenutniKod += '0';

                    if (mapa.ContainsKey(trenutniKod))                                                          // ako taj kod postoji u tablici (uvek ce da postoji ako imamo dobar kluc) pisemo taj kod odmah
                    {
                        sw.Write(mapa[trenutniKod]);

                        trenutniKod = string.Empty;                                                             // brisemo trenutni kod i iz pocetka za ceo niz bitova
                    }
                }
            }
        }
    }
}
