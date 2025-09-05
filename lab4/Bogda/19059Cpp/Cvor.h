#pragma once
#include <iostream>

using namespace std;

class Cvor
{
public:
	int vrednost;
	int stepen;
	Cvor* roditelj;
	Cvor* dete;
	Cvor* rodjak;

	Cvor(int vrednost);
	void promeniZnak();
	void naEkran();
};

