#include "BinomialHeap.h"
#include <vector>
#include <cstdlib>
#include <ctime>
#include <chrono>
#include <thread>

int GetRandomNumber(int a, int b);
void Test(int n, int k, int p, int a, int b);

int main()
{
	srand(static_cast<unsigned int>(time(nullptr)));
		
	int a, b, p;

	cout << "Unesite opseg brojeva [a, b]:" << endl;
	cout << "a = "; cin >> a;
	cout << "b = "; cin >> b;
	cout << "p (<= 100): "; cin >> p;

	// Testiranje za 1000 <= N <= 10.000.000 i 10 <= k <= 100
	int n = 1000;
	while (n <= 10'000'000)
	{
		int k = 100;
		while (k >= 10)
		{
			cout << "N: " << n << ", K: " << k << ", P: " << p << endl;
			cout << "---------------------------------" << endl;
			auto start = std::chrono::high_resolution_clock::now();

			Test(n, k, p, a, b);

			auto end = std::chrono::high_resolution_clock::now();
			std::chrono::duration<double> diff = end - start;
			cout << "Vreme izvrsavanja: " << diff.count() << "s" << endl;
			cout << "---------------------------------" << endl << endl;

			k /= 10;
		}

		n *= 10;
	}

	return 0;
}

void Test(int n, int k, int p, int a, int b)
{
	// Prvi deo zadatka

	BinomialHeap h;
	for (int i = 0; h.GetCount() < n; ++i)
	{
		int x = GetRandomNumber(a, b);
		h.Insert(x);

		if ((i + 1) % k == 0)
			h.ExtractMin();
	}

	// Nakon generisanja
	// Zadatak 5

	// Sada simuliramo MaxHeap tako sto ubacujemo negativne vrednosti
	// Kljuc nam je sada frekvencija, a dodatni atribut flag sada koristimo da znamo koji broj se javio odredjeni broj puta
	// Posle samo uzmemo prva p iz heap-a i ispisemo ih
	BinomialHeap freqHeap;

	int currentFreq = 1;
	int currentNum = h.ExtractMin();

	// Extract-ujemo brojeve i dobijamo ih u rastucem redosledu
	// Dokle god je sledeci broj isti kao trenutni, povecavamo frekvenciju
	// Kada se promeni broj, ubacujemo trenutni broj sa njegovom frekvencijom u heap
	while (h.GetCount() > 0)
	{
		int x = h.ExtractMin();
		if (x == currentNum)
			currentFreq++;
		else
		{
			// Simuliramo MaxHeap pomocu MinHeap-a tako sto cuvamo frekvenciju kao negativnu vrednost
			freqHeap.Insert(-currentFreq, currentNum);
			currentNum = x;
			currentFreq = 1;
		}
	}

	freqHeap.Insert(-currentFreq, currentNum);

	h.Destroy();

	//freqHeap.Print();

	for (int i = 0; i < p; ++i)
	{
		int x = freqHeap.ExtractMin();
		cout << x << endl;
	}

	freqHeap.Destroy();
}

int GetRandomNumber(int a, int b)
{
	return rand() % (b - a + 1) + a;
}