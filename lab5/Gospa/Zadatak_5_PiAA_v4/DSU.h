#pragma once
#include <algorithm>

// Disjoint Set Union za Boruvku
class DSU
{
private:
	int* parent;
	int* rank;
	int n;

public:
	DSU(int n);
	~DSU();

	int Find(int u);
	void Union(int u, int v);
};

