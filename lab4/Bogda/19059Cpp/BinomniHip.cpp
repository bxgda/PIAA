#include "BinomniHip.h"

BinomniHip::BinomniHip()
{
    glava = nullptr;
    najmanji = nullptr;
    brEl = 0;
}

BinomniHip::~BinomniHip()
{
    Unisti(glava);
}

void BinomniHip::Unisti(Cvor* node)
{
    if (node == nullptr)
        return;

    Unisti(node->dete);
    Unisti(node->rodjak);
    delete node;
}

Cvor* BinomniHip::napraviCvor(int vrednost)
{
    return new Cvor(vrednost);
}

void BinomniHip::dodaj(Cvor* novi)
{
    this->glava = unija(this->glava, novi);
    ++brEl;
    najmanji = nadjiNajmanji();
}

void BinomniHip::povezi(Cvor* y, Cvor* z) 
{
    y->roditelj = z;
    y->rodjak = z->dete;
    z->dete = y;
    z->stepen = z->stepen + 1;
}

Cvor* BinomniHip::unija(Cvor* c1, Cvor* c2)
{
    Cvor *c = spoji(c1, c2);
    
    if (c == nullptr)
        return c;
    
    Cvor* prevX = nullptr;
    Cvor* x = c;
    Cvor* nextX = x->rodjak;

    while (nextX != nullptr) {

        if ((x->stepen != nextX->stepen) || (nextX->rodjak != nullptr && nextX->rodjak->stepen == x->stepen)) {

            prevX = x;
            x = nextX;
        }
        else if (x->vrednost <= nextX->vrednost) {

            x->rodjak = nextX->rodjak;
            povezi(nextX, x);
        }
        else if (prevX == nullptr) {
            
            c = nextX;
            povezi(x, nextX);
            x = nextX;
        }
        else {

            prevX->rodjak = nextX;
            povezi(x, nextX);
            x = nextX;
        }
        nextX = x->rodjak;
    }
    return c;
}

Cvor* BinomniHip::spoji(Cvor* c1, Cvor* c2)
{
    Cvor* c;
    Cvor* temp1 = c1; 
    Cvor* temp2 = c2;

    if (temp1 != nullptr && temp2 != nullptr) {

        if (temp1->stepen <= temp2->stepen) 
            c = temp1;

        else 
            c = temp2;
        
    }
    else if (temp1 == nullptr)
        c = temp2;
    else
        c = temp1;

    while (temp1 != nullptr && temp2 != nullptr) {

        if (temp1->stepen < temp2->stepen) 
            temp1 = temp1->rodjak;

        else if (temp1->stepen == temp2->stepen) {

             Cvor* tempZamena = temp1->rodjak;
             temp1->rodjak = temp2;
             temp1 = tempZamena;
        }
        else {
            
             Cvor* tempZamena = temp2->rodjak;
             temp2->rodjak = temp1;
             temp2 = tempZamena;
        }
    }
    return c;
}

Cvor* BinomniHip::izbaciNajmanji()
{
    if (glava == nullptr) {

        cout << "Nema vise elemenata izbaciNajmanji" << endl;
        return 0;
    }

    Cvor* prethodni = nullptr;
    Cvor* najmanjiC = glava;

    najmanjiC = nadjiNajmanjiSaPret(&prethodni);

    if (prethodni == nullptr && najmanjiC->rodjak == nullptr)
        glava = nullptr;

    else if (prethodni == nullptr)
        glava = najmanjiC->rodjak;

    else if (prethodni->rodjak == nullptr)
        prethodni = nullptr;

    else
        prethodni->rodjak = najmanjiC->rodjak;

    Cvor* drugi = nullptr;

    if (najmanjiC->dete != nullptr) {

        okreniListu(najmanjiC->dete, &drugi);
        najmanjiC->dete->rodjak = nullptr;
    }

    this->glava = unija(this->glava, drugi);

    brEl--;

    najmanji = nadjiNajmanji();

    Cvor * zaReturn = new Cvor(najmanjiC->vrednost);
    delete najmanjiC;
    return zaReturn;
}

void BinomniHip::okreniListu(Cvor* c, Cvor** drugi)
{
    if (c->rodjak != nullptr) {

        okreniListu(c->rodjak, drugi);
        c->rodjak->rodjak = c;
    }
    else
        *drugi = c;
    c->roditelj = nullptr;
}

Cvor* BinomniHip::nadjiNajmanji()
{
    if (glava == nullptr) {

        cout << "nema vise elemenata nadjiNajmanji" << endl;
        return nullptr;
    }

    int min = glava->vrednost;
    Cvor* najmanjitmp = glava;
    Cvor* temp = glava;

    while (temp != nullptr) {

        if (temp->vrednost < min) {

            najmanjitmp = temp;
            min = najmanjitmp->vrednost;
        }
        temp = temp->rodjak;
    }
    return najmanjitmp;
}

Cvor* BinomniHip::nadjiNajmanjiSaPret(Cvor** prethodni)
{
    if (glava == nullptr) {

        cout << "nema vise elemenata nadjiNajmanjiSaPret" << endl;
        return nullptr;
    }

    Cvor* temp = glava;
    Cvor* najmanjitmp = temp;

    int min = najmanjitmp->vrednost;

    while (temp->rodjak != nullptr) {

        if (temp->rodjak->vrednost < min) {

            min = temp->rodjak->vrednost;
            *prethodni = temp;
            najmanjitmp = temp->rodjak;
        }
        temp = temp->rodjak;
    }
    return najmanjitmp;
}

void BinomniHip::naEkran(Cvor* c, int uvuceno)
{
    while (c != nullptr) {

        for (int i = 0; i < uvuceno; ++i)
            cout << "  ";
        
        cout << c->vrednost << endl;
        naEkran(c->dete, uvuceno + 1);
        c = c->rodjak;
    }
}
