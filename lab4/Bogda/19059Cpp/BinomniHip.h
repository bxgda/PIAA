#pragma once
#include "Cvor.h"

class BinomniHip
{
public:
	Cvor* glava;
	Cvor* najmanji;
	int brEl;

	BinomniHip();
	~BinomniHip();
	void Unisti(Cvor* node);
	Cvor* napraviCvor(int vrednost);
	void dodaj(Cvor* novi);
	void povezi(Cvor* y, Cvor* z);
	Cvor* unija(Cvor* c1, Cvor* c2);
	Cvor* spoji(Cvor* c1, Cvor* c2);
	Cvor* izbaciNajmanji();
	void okreniListu(Cvor* c, Cvor** drugi);
	Cvor* nadjiNajmanji();
	Cvor* nadjiNajmanjiSaPret(Cvor** prethodni);

	void naEkran(Cvor* node, int indent);
};

