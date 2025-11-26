#pragma once
#include <iostream>
#include <vector>
#include <queue>
#include <unordered_map>

#include "DSU.h"

using namespace std;

struct Edge
{
	int u, v, weight;
	Edge(int u, int v, int weight) : u(u), v(v), weight(weight) {}
};

class Graph
{
private:
	int V;
	unordered_map<int, vector<pair<int, int>>> adjList; // {vertex, {vertex, weight}}
	

public:
	Graph(int V);
	~Graph() = default;

	void AddEdge(int u, int v, int weight);
	bool DoesEdgeExist(int u, int v);
	void PrintGraph();


	Graph BoruvkaMST();
	// Ova funkcija prvo kreira MST pomocu Boruvke
	// A zatim brise redom sve cvorove pocev od listova (posto je stablo, u svakom trenutku mora da ima bar 2 lista)
	// U red listova se stavljaju cvorovi ciji je stepen 1
	// Kada se obrise cvor njegovom susedu se smanji stepen i ako je sused postao list, stavlja se u red
	// Ovaj postupak se ponavlja dok se ne obrise ceo graf
	void DeleteNodesWithBoruvka();


private:
	void DeleteNode(int u);
};