#include "DSU.h"

DSU::DSU(int n) : n(n)
{
	parent = new int[n];
	rank = new int[n];

	for (int i = 0; i < n; i++)
	{
		parent[i] = i;
		rank[i] = 0;
	}
}

DSU::~DSU()
{
	delete[] parent;
	delete[] rank;
}

int DSU::Find(int u)
{
	if (parent[u] == u)
		return u;

	return parent[u] = Find(parent[u]); // Path compression
}

void DSU::Union(int u, int v)
{
	u = Find(u);
	v = Find(v);

	if (u != v)
	{
		if (rank[u] < rank[v])
			std::swap(u, v);
		parent[v] = u;
		if (rank[u] == rank[v])
			rank[u]++;
	}
}