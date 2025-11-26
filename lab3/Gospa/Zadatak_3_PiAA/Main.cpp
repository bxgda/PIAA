#include "App.h"

std::vector<std::vector<int>> ReadTestCases(const char* fileName) {

	std::vector<std::vector<int>> testCases;
	std::ifstream file(fileName);

	if (!file.is_open()) {
		std::cerr << "File not found!" << "\n";
		return testCases;
	}

	while (!file.eof()) {

		int m;
		file >> m;
		std::vector<int> testCase(m);

		for (int j = 0; j < m; j++) {
			int x;
			file >> x;
			if (file.fail()) break;
			testCase[j] = x;
		}

		testCases.push_back(testCase);
	}

	file.close();
	
	return testCases;
}

int main() {
	
	// Ucitavanje manjih test primera radi lakseg testiranja
	std::vector<std::vector<int>> testCases = ReadTestCases("test_cases.txt");

	App app;

	for (const auto& testCase : testCases) {
		
		cout << "Test case: ";
		for (int x : testCase)
			cout << x << " ";
		cout << "\n";
		
		int result = app.GetMinimumJumps(testCase);
		cout << "Result: " << result << "\n\n";
	}
	
	// Rucno testiranje
	//int result = app.GetMinimumJumps({ 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 });
	//cout << "Result: " << result << "\n";

	return 0;
}