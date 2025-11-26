#pragma once
#include <iostream>
#include <fstream>
#include <vector>

using std::cout;

class App
{
public:
	App() = default;
	~App() = default;

	int GetMinimumJumps(const std::vector<int>& testCase);
};

