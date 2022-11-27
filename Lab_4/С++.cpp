//Требования к языкам программирования : JAVA, C++, python, assembler
//Разработать программу на нескольких языках, реализующую следующую математическую функцию в цикле
//f(i + 1) = f(i) + от(n = i)до(100000000) Σ(b * 2 + c - i)
//и сравнить производительность в зависимости от языка


#include <iostream>
#include <ctime>

using namespace std;

unsigned int start_time = clock();
int main()
{
    cout << "Lab_4, Паршина А Д " << endl;
    int a = 0, b = 2, c = 3;

    for (int i = 0; i < 100000000; i++) {
        a += 2 * b + c - i;
    }

    cout << "Ответ:" << a << endl;
    unsigned int end_time = clock();
    unsigned int search_time = end_time - start_time;
    cout << "Время: " << search_time << " мс" << endl;
    return 0;
}

 
// ответ
// -187459712
// 223150 мс