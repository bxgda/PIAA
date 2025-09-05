using System;
using System.IO;

namespace _19059Csharp
{
    internal class Program
    {
        public static void Pretrazi(string imeFajla, string podstring, string imeFajlaIzvestaj, bool funkcija) //1-kmp, 0-levenstein
        {
            try
            {
                string tekst;
                using (StreamReader sr = new StreamReader(imeFajla))
                {
                    tekst = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(imeFajlaIzvestaj, append: true))
                {
                    if (funkcija == true)
                    {
                        sw.WriteLine($"##### Pokrenut KMP algoritam za fajl {imeFajla}\n");
                        KnuthMorrisPratt.PretragaKMP(tekst, podstring, sw);
                    }
                    else
                    {
                        sw.WriteLine($"##### Pokretut Levenstein algoritam za fajl {imeFajla}\n");
                        Levenstein.PretragaLevenstein(tekst, podstring, sw);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + $"\n\nproblem sa otvaranjem fajla: {imeFajla} ili fajla: {imeFajlaIzvestaj}\n");
            }
        }

        static void Main(string[] args)
        {
            //Hex generisanje

            //HexGenerator.Generisi(100, "hex100.txt");
            //HexGenerator.Generisi(1000, "hex1k.txt");
            //HexGenerator.Generisi(10000, "hex10k.txt");
            //HexGenerator.Generisi(100000, "hex100k.txt");

            // OVO JE PAKAO KAKO SAM JA TO TAD ZVAO OVU FUNKCIJU PRETRAZI... BOLE ME OCI DOK GLEDAM OVO...

            // tekstovi i izvestaji funkcija se nalaze u /bin/Debug

            // realno sam trebao da pokrecem program u Release mod jer se onda brze izvrsava ali nisam to tad znao... u nardenim labovima sam to ispravio

            //KMP

            //Ascii

            Pretrazi("ascii100.txt", "green", "izvestajKMP.txt", true);                                                     //acsii tekst 100 reci - podstring duzine 5
            Pretrazi("ascii100.txt", "tubeshaped", "izvestajKMP.txt", true);                                                //acsii tekst 100 reci - podstring duzine 10
            Pretrazi("ascii100.txt", "Not a nasty, dirty, ", "izvestajKMP.txt", true);                                      //acsii tekst 100 reci - podstring duzine 20
            Pretrazi("ascii100.txt", "Not a nasty, dirty, wet hole, filled with the ends", "izvestajKMP.txt", true);        //acsii tekst 100 reci - podstring duzine 50

            Pretrazi("ascii1k.txt", "green", "izvestajKMP.txt", true);                                                      //ascii tekst 1000 reci - podstring duzine 5
            Pretrazi("ascii1k.txt", "tubeshaped", "izvestajKMP.txt", true);                                                 //ascii tekst 1000 reci - podstring duzine 10
            Pretrazi("ascii1k.txt", "Not a nasty, dirty, ", "izvestajKMP.txt", true);                                       //ascii tekst 1000 reci - podstring duzine 20
            Pretrazi("ascii1k.txt", "Not a nasty, dirty, wet hole, filled with the ends", "izvestajKMP.txt", true);         //ascii tekst 1000 reci - podstring duzine 50

            Pretrazi("ascii10k.txt", "Thorin", "izvestajKMP.txt", true);                                                    //acsii tekst 10000 reci - podstring duzine 5
            Pretrazi("ascii10k.txt", "determined", "izvestajKMP.txt", true);                                                //acsii tekst 10000 reci - podstring duzine 10
            Pretrazi("ascii10k.txt", "burglary—especially ", "izvestajKMP.txt", true);                                      //acsii tekst 10000 reci - podstring duzine 20
            Pretrazi("ascii10k.txt", "This news alters them much for the better. So far ", "izvestajKMP.txt", true);        //acsii tekst 10000 reci - podstring duzine 50

            Pretrazi("ascii100k.txt", "Bilbo", "izvestajKMP.txt", true);                                                    //acsii tekst 100000 reci - podstring duzine 5
            Pretrazi("ascii100k.txt", "determined", "izvestajKMP.txt", true);                                               //acsii tekst 100000 reci - podstring duzine 10
            Pretrazi("ascii100k.txt", "burglary—especially ", "izvestajKMP.txt", true);                                     //acsii tekst 100000 reci - podstring duzine 20
            Pretrazi("ascii100k.txt", "This news alters them much for the better. So far ", "izvestajKMP.txt", true);       //acsii tekst 100000 reci - podstring duzine 50

            //Hex

            Pretrazi("hex100.txt", "CBA25", "izvestajKMP.txt", true);                                                       //hex tekst 100 reci - podstring duzine 5
            Pretrazi("hex100.txt", "9B004F6AEA", "izvestajKMP.txt", true);                                                  //hex tekst 100 reci - podstring duzine 10
            Pretrazi("hex100.txt", "10D2678DF0EDA39F1789", "izvestajKMP.txt", true);                                        //hex tekst 100 reci - podstring duzine 20
            Pretrazi("hex100.txt", "688ACCC3B6A4DF326B 876529E4126 73564C2CAF0 4A2762C", "izvestajKMP.txt", true);          //hex tekst 100 reci - podstring duzine 

            Pretrazi("hex1k.txt", "59711", "izvestajKMP.txt", true);                                                        //hex tekst 1000 reci - podstring duzine 5
            Pretrazi("hex1k.txt", "4FAF97CCFE", "izvestajKMP.txt", true);                                                   //hex tekst 1000 reci - podstring duzine 10
            Pretrazi("hex1k.txt", "B83A2492050A9560DAFB", "izvestajKMP.txt", true);                                         //hex tekst 1000 reci - podstring duzine 20
            Pretrazi("hex1k.txt", "B64E624F2CE004C4770 CD661EBD7E578EBB30 FA26613E3 B", "izvestajKMP.txt", true);           //hex tekst 1000 reci - podstring duzine 50

            Pretrazi("hex10k.txt", "4F047", "izvestajKMP.txt", true);                                                       //hex tekst 10000 reci - podstring duzine 5
            Pretrazi("hex10k.txt", "E3D7F3EBF8", "izvestajKMP.txt", true);                                                  //hex tekst 10000 reci - podstring duzine 10
            Pretrazi("hex10k.txt", "B79080C1E6E5D84AB63F", "izvestajKMP.txt", true);                                        //hex tekst 10000 reci - podstring duzine 20
            Pretrazi("hex10k.txt", "2C508B55D70A9F4162 1EA01D7 86C5732CA427D296C42 B4A", "izvestajKMP.txt", true);          //hex tekst 10000 reci - podstring duzine 50

            Pretrazi("hex100k.txt", "C3C3F", "izvestajKMP.txt", true);                                                      //hex tekst 100000 reci - podstring duzine 5
            Pretrazi("hex100k.txt", "66943CB78E", "izvestajKMP.txt", true);                                                 //hex tekst 100000 reci - podstring duzine 10
            Pretrazi("hex100k.txt", "A3FDDF4B86A09D09E54F", "izvestajKMP.txt", true);                                       //hex tekst 100000 reci - podstring duzine 20
            Pretrazi("hex100k.txt", "F72C8096935793DDB7 127038DC 17F933B4721A2F78 83436", "izvestajKMP.txt", true);         //hex tekst 100000 reci - podstring duzine 50


            //Levenstein

            //Ascii

            Pretrazi("ascii100.txt", "round", "izvestajLevenstein.txt", false);                                             //acsii tekst 100 reci - podstring duzine 5
            Pretrazi("ascii100.txt", "tubeshaped", "izvestajLevenstein.txt", false);                                        //acsii tekst 100 reci - podstring duzine 10
            Pretrazi("ascii100.txt", "panelled-walls,", "izvestajLevenstein.txt", false);                                   //acsii tekst 100 reci - podstring duzine 15, napribliynije 20 sto ima

            Pretrazi("ascii1k.txt", "lived", "izvestajLevenstein.txt", false);                                              //ascii tekst 1000 reci - podstring duzine 5
            Pretrazi("ascii1k.txt", "hobbitlike", "izvestajLevenstein.txt", false);                                         //ascii tekst 1000 reci - podstring duzine 10
            Pretrazi("ascii1k.txt", "unsuspecting-Bilbo's", "izvestajLevenstein.txt", false);                               //ascii tekst 1000 reci - podstring duzine 19

            Pretrazi("ascii10k.txt", "Thorin", "izvestajLevenstein.txt", false);                                            //acsii tekst 10000 reci - podstring duzine 5
            Pretrazi("ascii10k.txt", "determined", "izvestajLevenstein.txt", false);                                        //acsii tekst 10000 reci - podstring duzine 10
            Pretrazi("ascii10k.txt", "burglary—especially,", "izvestajLevenstein.txt", false);                              //acsii tekst 10000 reci - podstring duzine 19

            Pretrazi("ascii100k.txt", "Bilbo", "izvestajLevenstein.txt", false);                                            //acsii tekst 100000 reci - podstring duzine 5
            Pretrazi("ascii100k.txt", "determined", "izvestajLevenstein.txt", false);                                       //acsii tekst 100000 reci - podstring duzine 10
            Pretrazi("ascii100k.txt", "burglary—especially,", "izvestajLevenstein.txt", false);                             //acsii tekst 100000 reci - podstring duzine 19

            //Hex

            Pretrazi("hex100.txt", "CBA25E", "izvestajLevenstein.txt", false);                                              //hex tekst 100 reci - podstring duzine 5
            Pretrazi("hex100.txt", "9B004F6AEA", "izvestajLevenstein.txt", false);                                          //hex tekst 100 reci - podstring duzine 10
            Pretrazi("hex100.txt", "10D2678DF0EDA39F1789", "izvestajLevenstein.txt", false);                                //hex tekst 100 reci - podstring duzine 20

            Pretrazi("hex1k.txt", "59711", "izvestajLevenstein.txt", false);                                                //hex tekst 1000 reci - podstring duzine 5
            Pretrazi("hex1k.txt", "4FAF97CCFE", "izvestajLevenstein.txt", false);                                           //hex tekst 1000 reci - podstring duzine 10
            Pretrazi("hex1k.txt", "B83A2492050A9560DAFB", "izvestajLevenstein.txt", false);                                 //hex tekst 1000 reci - podstring duzine 20

            Pretrazi("hex10k.txt", "4F047", "izvestajLevenstein.txt", false);                                               //hex tekst 10000 reci - podstring duzine 5
            Pretrazi("hex10k.txt", "E3D7F3EBF8", "izvestajLevenstein.txt", false);                                          //hex tekst 10000 reci - podstring duzine 10
            Pretrazi("hex10k.txt", "B79080C1E6E5D84AB63F", "izvestajLevenstein.txt", false);                                //hex tekst 10000 reci - podstring duzine 20

            Pretrazi("hex100k.txt", "C3C3F", "izvestajLevenstein.txt", false);                                              //hex tekst 100000 reci - podstring duzine 5
            Pretrazi("hex100k.txt", "66943CB78E", "izvestajLevenstein.txt", false);                                         //hex tekst 100000 reci - podstring duzine 10
            Pretrazi("hex100k.txt", "A3FDDF4B86A09D09E54F", "izvestajLevenstein.txt", false);                               //hex tekst 100000 reci - podstring duzine 20
        }
    }
}
