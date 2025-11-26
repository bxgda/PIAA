#pragma once
#include <iostream>
#include <unordered_map>

using std::cout;
using std::cin;
using std::endl;

struct Node
{
    Node* parent;
    int key;
    int flag;
    int degree;
    Node* child;
    Node* sibling;

	Node(int key, int flag) : key(key), flag(flag)
    {
        parent = nullptr;
        child = nullptr;
        sibling = nullptr;
        degree = 0;
    }
};

class BinomialHeap
{
private:
    Node* m_head;
    int m_count;

    void Union(BinomialHeap* hprim);
    void Link(Node* y, Node* z);
    Node* Merge(BinomialHeap* h1, BinomialHeap* h2);
	void Destroy(Node* node);
	void PrintTree(Node* node, int indent);

public:
    BinomialHeap(Node* head = nullptr, int count = 0) : m_head(head), m_count(count) {}
	~BinomialHeap() = default;

	Node* Insert(int key, int flag = INT_MAX);
	int ExtractMin();
    Node* DecreaseKey(Node* node, int newKey);
	void DeleteNode(Node* node);
    Node* GetMinNode();

	inline void Print() { PrintTree(m_head, 0); }
    inline void Destroy() { Destroy(m_head); m_head = nullptr; }
    inline int GetCount() { return m_count; }
};

