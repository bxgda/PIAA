using System;

namespace _19059Csharp
{
    [Serializable]
    public class Simbol
    {
        public char Karakter { get; set; }
        public int Ucestanost { get; set; }
        public string Kod { get; set; }

        public Simbol(char karakter, int ucestanost)
        {
            Karakter = karakter;
            Ucestanost = ucestanost;
        }

        public Simbol(char karakter, int ucestanost, string kod)
        {
            Karakter = karakter;
            Ucestanost = ucestanost;
            Kod = kod;
        }

        public Simbol() { }
    }
}
