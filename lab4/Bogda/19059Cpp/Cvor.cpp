#include "Cvor.h"

Cvor::Cvor(int vrednost)
{
	this->vrednost = vrednost;
	stepen = 0;
	roditelj = nullptr;
	dete = nullptr;
	rodjak = nullptr;
}

void Cvor::promeniZnak()
{
	vrednost = vrednost * (-1);
}

void Cvor::naEkran()
{
	cout << "vrednost: " << vrednost << " stepen: " << stepen << endl;
}
