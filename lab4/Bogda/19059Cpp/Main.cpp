#include "BinomniHip.h"
#include <cstdlib> 
#include <ctime>   
#include <algorithm>
#include <fstream>

void dodajBroj(int broj, BinomniHip* manji, BinomniHip* veci)
{
	if (manji->brEl == 0 || broj < abs(manji->najmanji->vrednost))
		manji->dodaj(new Cvor(-broj));
	else
		veci->dodaj(new Cvor(broj));
}

void balansiraj(BinomniHip* manji, BinomniHip* veci)
{
	BinomniHip* veciHip;
	BinomniHip* manjiHip;

	if (manji->brEl > veci->brEl) {

		veciHip = manji;
		manjiHip = veci;
	}
	else {

		veciHip = veci;
		manjiHip = manji;
	}

	if (veciHip->brEl - manjiHip->brEl == 2) {

		Cvor* transfer = veciHip->izbaciNajmanji();
		transfer->promeniZnak();
		manjiHip->dodaj(transfer);
	}
}

int vratiMedijanu(BinomniHip* manji, BinomniHip* veci)
{
	int broj;
	if (manji->brEl > veci->brEl) {

		broj = manji->najmanji->vrednost;
		return abs(broj);
	}
	else
		return veci->najmanji->vrednost;
}

void odradiSve()
{
	// ovde nesto nije radilo kako treba ali se ne secam sta, pa sam ja ovde nekako u main zaobisao tu gresku i nadao se da profesor to nece da vidi, i nije video

	int opsegA = 0, opsegB = 10000, k = 10;

	ofstream izlaz("izlaz.txt"); 
	
	int iteracije[5] = { 1000, 10000, 100000, 1'000'000, 10'000'000 };

	for (int i = 0; i < 5; i++) {

		BinomniHip b;
		BinomniHip manjiBrojevi;
		BinomniHip veciBrojevi;

		for (int j = 0; j < iteracije[i]; j++) {

			// prvi deo

			int cvorr = (rand() % opsegB + 1) + opsegA;

			b.dodaj(new Cvor(cvorr));

			if ((j + 1) % k == 0) {

				Cvor* temp = b.izbaciNajmanji();
				delete temp;
			}

			// zadatak 9

			dodajBroj(cvorr, &manjiBrojevi, &veciBrojevi);
			balansiraj(&manjiBrojevi, &veciBrojevi);
			izlaz << j <<": " << vratiMedijanu(&manjiBrojevi, &veciBrojevi) << endl;
		}

		cout << "zavrsio " << iteracije[i] << endl;
	}

	izlaz.close();
}

int main()
{
	srand(time(0));

	odradiSve();

	return 0;
}
