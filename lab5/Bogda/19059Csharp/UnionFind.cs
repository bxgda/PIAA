namespace _19059Csharp
{
    internal class UnionFind
    {
        private int[] roditelj;                                         // roditelj sluzi da bi znali kako da spajamo komponente
        private int[] rank;                                             // rank je tu da se optimizuju stvari da kad spajamo komponente za ovu pretragu spajamo da stablo uvek bude sto plice

        public UnionFind(int velicina)
        {
            roditelj = new int[velicina];
            rank = new int[velicina];                                   // svi rangovi su inicijalno 0
            for (int i = 0; i < velicina; i++)
                roditelj[i] = i;
        }

        public int Find(int x)                                          // rekurzivna funkcija koja pronalazi roditelja
        {
            if (roditelj[x] != x)                                       // ako roditelj nije taj vec element onda 
                roditelj[x] = Find(roditelj[x]);                        // pozivamo funkciju za svog roditelja
            return roditelj[x];                                         // kad nadjemo onda izlazimo iz rekurzije i vracamo tog roditelja
        }

        public void Union(int x, int y)
        {
            int roditeljX = Find(x);                                    // trazimo roditelja za jedan i drugi kraj
            int roditeljY = Find(y);

            if (roditeljX == roditeljY)                                 // ako je isti roditelj ne spajamo jer su vec spojeni nekako
                return;

            if (rank[roditeljX] < rank[roditeljY])                      // ovde radimo optimizaciju da plice stablo spajamo na dublje kako bi ostalo stablo sto plice
                roditelj[roditeljX] = roditeljY;
            else if (rank[roditeljX] > rank[roditeljX])
                roditelj[roditeljY] = roditeljX;
            else
            {
                roditelj[roditeljY] = roditeljX;
                rank[roditeljX]++;
            }
        }
    }
}
