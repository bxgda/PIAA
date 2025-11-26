#include "BinomialHeap.h"

void BinomialHeap::Union(BinomialHeap* hprim)
{
    m_head = Merge(this, hprim);

    if (m_head == nullptr)
        return;
    m_count += hprim->m_count;
    delete hprim; // brise objekat ali ne i cvorove na koje je ukazivao

    Node* prevX = nullptr;
    Node* x = m_head;
    Node* nextX = x->sibling;

    while (nextX != nullptr)
    {
        // ako x i nextX NISU JEDNAKI idi dalje
        // ili ako su x, nextX i nextX->sibling JEDNAKI samo idi dalje
        if ((x->degree != nextX->degree) || (nextX->sibling != nullptr && nextX->sibling->degree == x->degree))
        {
            prevX = x;
            x = nextX;
        }
        else if (x->key <= nextX->key)
        {
            x->sibling = nextX->sibling;
            Link(nextX, x); // zakaci nextX na x
        }
        else
        {
            if (prevX == nullptr)
                m_head = nextX;
            else
                prevX->sibling = nextX;
            Link(x, nextX); // zakaci x na nextX
            x = nextX;
        }
        nextX = x->sibling;
    }
}

void BinomialHeap::Link(Node* y, Node* z)
{
    y->parent = z;
    y->sibling = z->child;
    z->child = y;
    z->degree = z->degree + 1;
}

Node* BinomialHeap::Merge(BinomialHeap* h1, BinomialHeap* h2)
{
    if (!h1->m_head)
        return h2->m_head;

    if (!h2->m_head)
        return h1->m_head;

    Node* newHead = nullptr;
    Node* tail = nullptr;
    Node* a = h1->m_head;
    Node* b = h2->m_head;

    if (a->degree <= b->degree)
    {
        newHead = a;
        a = a->sibling;
    }
    else
    {
        newHead = b;
        b = b->sibling;
    }

    tail = newHead;

    while (a != nullptr && b != nullptr)
    {
        if (a->degree <= b->degree)
        {
            tail->sibling = a;
            a = a->sibling;
        }
        else
        {
            tail->sibling = b;
            b = b->sibling;
        }
        tail = tail->sibling;
    }

    if (a != nullptr)
        tail->sibling = a;
    else
        tail->sibling = b;

    return newHead;
}

void BinomialHeap::Destroy(Node* node)
{
    if (node == nullptr)
        return;
    Destroy(node->child);
    Destroy(node->sibling);
    delete node;
}

void BinomialHeap::PrintTree(Node* node, int indent)
{
    while (node != nullptr)
    {
        for (int i = 0; i < indent; ++i)
            cout << "  ";
        
        node->flag == INT_MAX ? 
            cout << node->key << endl : 
            cout << node->flag << " (" << -node->key << ")" << endl;

        PrintTree(node->child, indent + 1);
        node = node->sibling;
    }
}

// Public metode
Node* BinomialHeap::Insert(int key, int flag)
{
    Node* x = new Node(key, flag);
    BinomialHeap* hprim = new BinomialHeap(x, 1);
    Union(hprim);
    return x;
}

int BinomialHeap::ExtractMin()
{
    Node* minNode = GetMinNode();
    if (minNode == nullptr)
        return INT_MAX;

    // Obrisi minNode iz root liste
    Node* prev = nullptr;
    Node* curr = m_head;

    while (curr != nullptr && curr != minNode)
    {
        prev = curr;
        curr = curr->sibling;
    }

    if (prev == nullptr)
        m_head = minNode->sibling;
    else
        prev->sibling = minNode->sibling;

    // Okreni redosled dece obrisanog cvora (reverse linked list)
    Node* child = minNode->child;
    Node* prevChild = nullptr;
    while (child != nullptr)
    {
        Node* nextChild = child->sibling;
        child->sibling = prevChild;
        child->parent = nullptr;
        prevChild = child;
        child = nextChild;
    }

    // Bice obrisan u Union
    BinomialHeap* newHeap = new BinomialHeap(prevChild);
    Union(newHeap);

	int minKey = minNode->flag == INT_MAX ? minNode->key : minNode->flag;
    delete minNode;
    m_count--;

    return minKey;
}

// Nije potrebna za konkretni zadatak (koristio sam je u prethodnoj ideji resenja, pa kad sam vec implementirao da je ne brisem)
Node* BinomialHeap::DecreaseKey(Node* node, int newKey)
{
    if (newKey > node->key)
    {
        cout << "Nije moguce odraditi DecreaseKey: Novi kljuc je veci od trenutnog..." << endl;
        return nullptr;
    }

    node->key = newKey;
    Node* y = node;
    Node* z = y->parent;
    while (z != nullptr && y->key < z->key)
    {
        // Zamena kljuceva i frekvencija
        int tempKey = y->key;
        y->key = z->key;
        z->key = tempKey;

        y = z;
        z = y->parent;
    }
    
    return y;
}

// Nije potrebna za konkretni zadatak
void BinomialHeap::DeleteNode(Node* node)
{
    DecreaseKey(node, INT_MIN);
    ExtractMin();
}

// Prolazi kroz sve korene i trazi najmanji (ne cuvam posebno pointer na minNode)
Node* BinomialHeap::GetMinNode()
{
    if (m_head == nullptr)
        return nullptr;

    Node* minNode = m_head;
    Node* x = m_head->sibling;

    while (x != nullptr)
    {
        if (x->key < minNode->key)
            minNode = x;
        x = x->sibling;
    }

    return minNode;
}