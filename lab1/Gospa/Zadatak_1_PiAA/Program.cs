using Zadatak_1_PiAA;

App app = new App();

Console.WriteLine("Pretraga tekstova sa ASCII karakterima:");
app.LoadASCIIFiles("ASCII");
app.StartKMP();
app.StartLevenshtein(FileType.ASCII);
Console.WriteLine("--------------------------------------------------------------\n");

Console.WriteLine("Pretraga tekstova sa HEX karakterima:");
// Odkomentarisati liniju ispod da bi se generisali novi fajlovi sa HEX vrednostima
//HEXGenerator.GenerateFiles("HEX");
app.LoadHexFiles("HEX");
app.StartKMP();
app.StartLevenshtein(FileType.HEX);
Console.WriteLine("--------------------------------------------------------------\n");
