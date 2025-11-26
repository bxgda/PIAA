#include "Graph.h"
#include <time.h>
#include <chrono>

Graph* Case1Graph(int N, int k)
{
	srand(static_cast<unsigned int>(time(nullptr)));

	// Graf sa N cvorova
	Graph* graph = new Graph(N);

	// Spajanje jednog slucajno odabranog cvora sa svim ostalima
	int rndNode = rand() % N;
	for (int i = 0; i < N; ++i)
	{
		if (i == rndNode)
			continue;

		graph->AddEdge(rndNode, i, rand() % 100 + 1);
	}

	// Generisati jos k potega
	for (int i = 0; i < k; ++i)
	{
        int u, v;
        do
        {
			u = rand() % N;
			v = rand() % N;
		} while (u == v || graph->DoesEdgeExist(u, v));

        graph->AddEdge(u, v, rand() % 100 + 1);
	}

	return graph;
}

Graph* Case2Graph(int N, int k)
{
	srand(static_cast<unsigned int>(time(nullptr)));

	Graph* graph = new Graph(N);

	// Spojiti cvorove u daisy chain
	for (int i = 0; i < N - 1; ++i)
		graph->AddEdge(i, i + 1, rand() % 100 + 1);
	graph->AddEdge(N - 1, 0, rand() % 100 + 1);

	// Generisati jos k potega
	for (int i = 0; i < k; ++i)
	{
        int u, v;
        do
        {
			u = rand() % N;
			v = rand() % N;
        } while (u == v || graph->DoesEdgeExist(u, v));

		graph->AddEdge(u, v, rand() % 100 + 1);
	}

	return graph;
}

int main()
{
	vector<int> valuesK = { 2, 5, 10, 20, 33 }; // 50*N potega je previse (ne moze se ubaciti bez duplih grana)
	int N = 100;

	while (N <= 100000)
	{
		for (int k : valuesK)
		{
			cout << "N = " << N << ", k = " << k << endl;

			// Case 1
			Graph* graph = Case1Graph(N, N * k);

			auto start = chrono::high_resolution_clock::now();
			graph->DeleteNodesWithBoruvka();
			auto end = chrono::high_resolution_clock::now();
			chrono::duration<double> duration = end - start;
			cout << "Case 1: " << duration.count() << " seconds" << endl;
			delete graph;

			// Case 2
			graph = Case2Graph(N, N * k);

			start = chrono::high_resolution_clock::now();
			graph->DeleteNodesWithBoruvka();
			end = chrono::high_resolution_clock::now();
			duration = end - start;
			cout << "Case 2: " << duration.count() << " seconds" << endl;
			delete graph;

			cout << "--------------------------------\n" << endl;
		}

		N *= 10;
	}

	return 0;
}