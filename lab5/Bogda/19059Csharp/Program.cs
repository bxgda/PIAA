using System;
using System.Collections.Generic;

namespace _19059Csharp
{
    internal class Program
    {
        static int[] VrednostiZaN = { 100, 1000, 10000, 100000 };
        static int[] VrednostiZaK = { 0, 2, 5, 10, 20, 33, 50 };
        static Random Rand = new Random();

        static void GenerisiNTacaka(int n, Graf g)
        {
            int brojac = 0;                                             // generisemo tacke u 1. kvadrantu koorditatnog sistema do x <= 1000 i y <= 1000 
            while (brojac < n)                                          
                if (g.DodajTacku(brojac, Rand.NextDouble() * 1000, Rand.NextDouble() * 1000))
                    brojac++;                                           // ako se desi da generisemo tacku sa istim koordinatama funkcija DodajTacku vraca false i mi generisemo novu
        }

        static void IzaberiTackuISpojiSaOstalima(Graf g)
        {
            int randomTacka = Rand.Next(g.Tacke.Count);                 // biramo random tacku i spajamo sa svima ostalima
            for (int i = 0; i < g.Tacke.Count; i++)
                if (i != randomTacka)
                    g.DodajPoteg(randomTacka, i);                       // ne proveravamo da li vec postoji poteg jer smo sigurno da inicijalno ne postoji po prvom pozivu ove funkcije
        }

        static void NapraviLanac(Graf g)
        {
            for (int i = 0; i < g.Tacke.Count - 1; i++)                 // pravimo lanac od prve tacke do poslednje
                g.DodajPoteg(i, i + 1);
        }

        static void GenerisiJosKPotega(Graf g)
        {
            int brojTacaka = g.Tacke.Count;
        
            for (int i = 1; i < 7; i++)                                 // prolazimo kroz listu VrednostiZaK i dodajemo toliko potega s tim sto kada dodam odredjen broj samo dodajemo na postojeci graf dalje
            {
                if (brojTacaka == 100 && VrednostiZaK[i] == 50)         // vrednosti za N i k koje su nemoguce pa ih preskacemo
                {
                    Console.WriteLine($"Nemoguce za N = {brojTacaka} i za k = {VrednostiZaK[i]}");
                    break;
                }
                int brojDodatih = 0;
                int brojPokusaja = 0;
                int ukupnoNovihPotega = (VrednostiZaK[i] * brojTacaka) - (brojTacaka * VrednostiZaK[i - 1]);        
                                                                        // novih potega koliko jos treba da se doda za sledece k
                while (brojDodatih < ukupnoNovihPotega)                 // dok nismo dodali koliko nam treba radimo sledece
                {
                    if (g.DodajPoteg(Rand.Next(brojTacaka), Rand.Next(brojTacaka)))
                        brojDodatih++;                                  // ako smo generisali granu koja ne postoji onda dodajemo
                    else
                        brojPokusaja++;                                 // ako ne onda povecavamo brojac koji sluzi cisto za statistiku
                }
                Console.WriteLine($"Tacke: {g.Tacke.Count}, k: {VrednostiZaK[i]}, broj potega: {g.Potezi.Count}, broj pokusaja: {brojPokusaja}");
            }                                                           // ispis na ekranu za proveru
        }

        static void PlasticanPrimerBoruvka()
        {
            Graf g = new Graf();                                        // primer koji moze na papiru u koordinatnom sistemu da se proveri

            g.DodajTacku(0, 1, 1);
            g.DodajTacku(1, 1, 4);
            g.DodajTacku(2, 2, 4);
            g.DodajTacku(3, 2, 3);
            g.DodajTacku(4, 4, 4);
            g.DodajTacku(5, 3, 1);

            g.DodajPoteg(0, 1);
            g.DodajPoteg(1, 3);
            g.DodajPoteg(3, 2);
            g.DodajPoteg(3, 5);
            g.DodajPoteg(1, 5);
            g.DodajPoteg(5, 4);
            g.DodajPoteg(3, 4);

            g.IspisiPotege();

            Console.WriteLine("-------------------------------");

            List<Poteg> p = g.BoruvkaMinSpeznoStablo();

            foreach (var po in p)
                Console.WriteLine($"{po.u} - {po.v}: {po.duzina}");
        }

        static void ProveraBoruvka(Graf g)
        {                                                               // provera za inicijalno stablo zvezde ili lanca
            List<Poteg> minSpreznoStablo = g.BoruvkaMinSpeznoStablo();
            foreach (var p in minSpreznoStablo)
                Console.WriteLine($"{p.u} - {p.v}: {p.duzina}");
        }

        static void Slucaj1()
        {
            for (int i = 0; i < 4; i++)                                 // za svako N iz liste VrednostiZaN
            {
                Graf graf = new Graf();
                GenerisiNTacaka(VrednostiZaN[i], graf);                 // generisemo N tacaka 
                IzaberiTackuISpojiSaOstalima(graf);                     // pravimo zvezdu
                ProveraBoruvka(graf);                                   // proveravamo Boruvkin algoritam za stablo jer bi prebao da iste te sve grane da kao rezultat MSS
                GenerisiJosKPotega(graf);                               // generisemo jos kpotega po zahtevima iz prvog dela zadatka
                ProveraBoruvka(graf);                                   // proveravamo Boruvkin algoritam za ceo generisani graf (trebalo bi za svako N i k) ali svakako radi lepo
                // graf.IspisiPotege();
            }
        }

        static void Slucaj2()
        {
            for (int i = 0; i < 4; i++)                                 // za svako N iz liste VrednostiZaN
            {
                Graf graf = new Graf();
                GenerisiNTacaka(VrednostiZaN[i], graf);                 // generisemo N tacaka
                NapraviLanac(graf);                                     // pravimo lanac
                ProveraBoruvka(graf);                                   // proveravamo Boruvkin algoritam
                GenerisiJosKPotega(graf);                               // generisemo jos k potega
                ProveraBoruvka(graf);                                   // proveravamo Boruvkin algoritam za ceo generisani graf (trebalo bi za svako N i k) ali svakako radi lepo
                // graf.IspisiPotege();
            }
        }

        static void Main(string[] args)
        {
            Slucaj1();                                                  // prvi slucaj u prvom delu zadatka gde se inicijalno pravi zvezda od cvorova i potega pa se dodaju novi potezi
            Slucaj2();                                                  // drugi slucaj u prvom delu zadatka gde se inicijalno pravi lanac od cvorova i potega pa se dodaju novi potezi
            PlasticanPrimerBoruvka();                                   // plastican primer koji moze lako da se nacrta u koordinatno sistemu i da se vidi da algoritam radi dobro
        }
    }       // najbolje testirati funkciju po funkciju i to jedno po jedno N i k
}
