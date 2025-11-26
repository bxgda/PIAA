#include "App.h"

// Slozenost ovog resenja je O(n)

int App::GetMinimumJumps(const std::vector<int>& testCase)
{
	int n = testCase.size();

	if (testCase[0] == 0) return -1;
	if (n == 1) return 0;

	int jumps = 0;			// Broj skokova
	int maxReach = 0;		// Najdalja pozicija koja se trenutno moze dostici
	int currentEnd = 0;		// Krajnji domet trenutnog skoka

	for (int i = 0; i < n; i++) {

		// Pohlepno uzimamo najdalju poziciju koju mozemo dostici
		maxReach = std::max(maxReach, i + testCase[i]);

		// Ako smo dosli do kraja trenutnog skoka azuriramo promenljive
		if (i == currentEnd) {
			jumps++;
			currentEnd = maxReach;

			// Ako je novi current end veci ili jednak kraju niza, zavrsavamo
			if (currentEnd >= n - 1)
				return jumps;
		}
	}
	
	// U ovoj tacki znaci da nismo uspeli da stignemo do kraja niza
	return -1;
}