using System;
using System.Collections.Generic;
using static System.Math;

namespace _19059Csharp
{
    public struct Tacka
    {
        public int id { get; }
        public double x { get; }
        public double y { get; }

        public Tacka(int id, double x, double y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }

    public struct Poteg
    {
        public int u { get; } 
        public int v { get; }
        public double duzina { get; }

        public Poteg(int u, int v, double duzina)
        {
            this.u = u;
            this.v = v;
            this.duzina = duzina;
        }
    }

    internal class Graf
    {
        public List<Tacka> Tacke { get; }                                   // liste koristimo za Boruvkin algoritam
        public List<Poteg> Potezi { get; }
        private HashSet<(double, double)> TackeSet;                         // hes mape koristimo za pretrage da li tacka ili poteg vec postoje da bi provera bila O(1)
        private HashSet<(int, int)> PoteziSet;

        public Graf()
        {
            Tacke = new List<Tacka>();
            Potezi = new List<Poteg>();
            PoteziSet = new HashSet<(int, int)>();
            TackeSet = new HashSet<(double, double)>();
        }

        public bool DodajTacku(int id, double x, double y)
        {
            if (!TackeSet.Contains((x, y)))                                 // ako vec postoji tacka sa istim koordinatama onda je ne dodajemo
            {
                Tacke.Add(new Tacka(id, x, y));                             // ako ne postoji dodajemo i u listu i u hes mapu
                TackeSet.Add((x, y));
                return true;
            }
            return false;
        }

        public bool DodajPoteg(int u, int v)
        {
            if (u == v)                                                     // necemo potege koji pocinju i zavrsavaju se u isti cvor
                return false;

            if (!PoteziSet.Contains((Math.Min(u, v), Math.Max(u, v))))      // ako postoji vec isti poteg onda ne dodajemo
            {
                Potezi.Add(new Poteg(u, v, EuklidovaRazdaljina(Tacke[u].x, Tacke[u].y, Tacke[v].x, Tacke[v].y)));
                PoteziSet.Add((Math.Min(u, v), Math.Max(u, v)));
                return true;                                                // dodajemo i racunamo odmah rastojanje izmedju te dve tacke u ravni
            }
            return false;
        }

        private double EuklidovaRazdaljina(double x1, double y1, double x2, double y2)
        {
            return Sqrt(Pow(x1 - x2, 2) + Pow(y1 - y2, 2));                 
        }

        public void IspisiPotege()
        {
            Console.WriteLine("Potezi:");
            for (int i = 0; i < Potezi.Count; i++)
            {
                var p = Potezi[i];
                Console.WriteLine($"{i}: {p.u} - {p.v} -> razdaljina -> {p.duzina}");
            }
        }

        public List<Poteg> BoruvkaMinSpeznoStablo()
        {
            int brojTacaka = Tacke.Count;
            UnionFind uf = new UnionFind(brojTacaka);                       // ovo je posebna klasa da bi se lakse pamtili rank stabla i roditelj bez prosledjivanja tih nizova u funkcije
            List<Poteg> mss = new List<Poteg>();                            // pravimo novu listu potega koja ce da nam predstavlja minimalno sprezno stablo
            int brojKomponenti = brojTacaka;

            while (brojKomponenti > 1)
            {
                Poteg[] najjeftiniji = new Poteg[brojTacaka];               // pravimo novu listu koja sadrzi najjeftinije potege

                for (int i = 0; i < brojTacaka; i++)
                    najjeftiniji[i] = default;                              // u pocetku stavimo da je sve 0

                foreach (var poteg in Potezi)                               // za svaki poteg u celom grafu 
                {
                    int krajU = uf.Find(poteg.u);                           // nalazimo roditelja za jedan i drugi kraj (u pocetku su oni sami svoj roditelj)
                    int krajV = uf.Find(poteg.v);

                    if (krajU == krajV)                                     // ako dva kraja vode ka istom roditelju onda nista ne radimo jer je ta komponenta vec spojena
                        continue;

                    if (najjeftiniji[krajU].duzina == 0 || poteg.duzina < najjeftiniji[krajU].duzina)
                        najjeftiniji[krajU] = poteg;                        // ako je duzina 0 ili duzina manja od najjeftinije duzine za taj poteg onda postavljamo da je najjeftinija duzina bas ta
                    
                    if (najjeftiniji[krajV].duzina == 0 || poteg.duzina < najjeftiniji[krajV].duzina)
                        najjeftiniji[krajV] = poteg;                        // isto tako i za drugi kraj
                }

                for (int i = 0; i < brojTacaka; i++)                        // za svaku tacku
                {
                    var poteg = najjeftiniji[i];                            // gledamo sada da spajamo komponente
                    
                    if (poteg.duzina == 0)                                  // ako je duzina tog potega 0 znaci da je iz prethodne petlje ostao kao najkraci pa ostaje
                        continue;

                    int krajU = uf.Find(poteg.u);                           // ako nije 0 onda trazimo roditelja za jedan i drugi kraj
                    int krajV = uf.Find(poteg.v);

                    if (krajU == krajV)                                     // ako je isti roditelj onda ne spajamo jer je vec ta komponenta spojena
                        continue;

                    mss.Add(poteg);                                         // dodajemo taj poteg u listu minimalnog spreznog stabla
                    uf.Union(krajU, krajV);                                 // radimo uniju te dve komponente 
                    brojKomponenti--;                                       // smanjujemo broj komponenti za 1 i nastavljamo dalje
                }
            }
            return mss;
        }
    }
}
