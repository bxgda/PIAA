#include "Graph.h"

Graph::Graph(int V) : V(V)
{
	adjList.reserve(V);
}

void Graph::AddEdge(int u, int v, int weight)
{
	adjList[u].push_back({v, weight});
	adjList[v].push_back({u, weight});
}

bool Graph::DoesEdgeExist(int u, int v)
{
	for (const auto& edge : adjList[u])
	{
		if (edge.first == v)
			return true;
	}

	return false;
}

void Graph::PrintGraph()
{
	for (int u = 0; u < V; ++u)
	{
		cout << u << ": ";
		for (const auto& edge : adjList[u])
			cout << edge.first << "(" << edge.second << ") ";
		cout << endl;
	}
}

Graph Graph::BoruvkaMST()
{
	DSU dsu(V);
		
	// Potezi koji cine MST
	vector<Edge> mstEdges;
	
	// Inicijalno svaki cvor je jedna komponenta
	int numComponents = V;
	int prevNumComponents = V;

	while (numComponents > 1)
	{
		vector<Edge> cheapest(V, { -1, -1, INT_MAX });

		// Pronalazak najjeftinijeg potega za svaku komponentu
		for (int u = 0; u < V; ++u)
		{
			for (const auto& edge : adjList[u])
			{
				int v = edge.first;
				int weight = edge.second;

				int set1 = dsu.Find(u);
				int set2 = dsu.Find(v);

				if (set1 != set2)
				{
					if (weight < cheapest[set1].weight)
						cheapest[set1] = { u, v, weight };

					if (weight < cheapest[set2].weight)
						cheapest[set2] = { u, v, weight };
				}
			}
		}

		// Spajanje komponenti (unija)
		for (int i = 0; i < V; ++i)
		{
			if (cheapest[i].u != -1 && cheapest[i].v != -1)
			{
				int set1 = dsu.Find(cheapest[i].u);
				int set2 = dsu.Find(cheapest[i].v);

				if (set1 != set2)
				{
					mstEdges.push_back(cheapest[i]);
					dsu.Union(set1, set2);
					--numComponents;
				}
			}
		}

		// Ako nijedna komponenta nije spojena u iteraciji, graf nije povezan
		if (numComponents == prevNumComponents)
		{
			// cerr << "Graf nije povezan." << endl;
			return Graph(0);
		}

		prevNumComponents = numComponents;
	}

	// Kreiranje MST grafa
	Graph mstGraph(V);
	for (const auto& edge : mstEdges)
		mstGraph.AddEdge(edge.u, edge.v, edge.weight);

	return mstGraph;
}

void Graph::DeleteNodesWithBoruvka()
{
	// Kreiranje MST-a pomocu Boruvke
	Graph mstGraph = BoruvkaMST();
	
	// Kreiranje vektora stepeni cvorova
	vector<int> degree(V, 0);
	for (int u = 0; u < V; ++u)
		degree[u] = mstGraph.adjList[u].size();

	// Red listova
	queue<int> leaves;
	for (int u = 0; u < V; ++u)
		if (degree[u] == 1)
			leaves.push(u);
	
	while (!leaves.empty())
	{
		int u = leaves.front();
		leaves.pop();
	
		// Ako je degree[u] == 0, cvor je vec obrisan
		if (degree[u] == 0)
			continue;

		// Obrisi cvor iz strukture
		DeleteNode(u);
		
		// Smanji stepen suseda cvora u
		for (const auto& edge : mstGraph.adjList[u])
		{
			int v = edge.first;
			--degree[v];

			if (degree[v] == 1) // Novi list
				leaves.push(v);
		}

		degree[u] = 0; // Obrisan cvor
	}

	cout << "Uspesno obrisani svi cvorovi." << endl;
}

void Graph::DeleteNode(int u)
{	
	// Obrisi cvor iz svih suseda
	for (const auto& edge : adjList[u])
	{
		int v = edge.first;

		adjList[v].erase(
			remove_if(adjList[v].begin(), adjList[v].end(), [u](const pair<int, int>& edge) {
				return edge.first == u;
			}), adjList[v].end());
	}

	// Obrisi cvor iz grafa
	adjList.erase(u);
}