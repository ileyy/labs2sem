#include <iostream>
#include <iomanip>

class ClassA {
public:
    int value;
};

class ClassB {
public:
    float value;
};

class Converter {
public:
    ClassB Convert(const ClassA& a) {
        ClassB b;
        b.value = static_cast<float>(a.value);
        return b;
    }

    ClassA Convert(const ClassB& b) {
        ClassA a;
        a.value = static_cast<int>(b.value);
        return a;
    }
};

int main() {
    ClassA objA;
    std::cout << "Enter number: ";
    std::cin >> objA.value;

    ClassB objB;
    std::cout << "Enter float number: ";
    std::cin >> objB.value;

    Converter converter;

    ClassB temp = objB;

    objB = converter.Convert(objA);
    std::cout << "Converted from ClassA to ClassB: " << std::fixed << std::setprecision(1) << objB.value << std::endl;

    objA = converter.Convert(temp);
    std::cout << "Converted from ClassB to ClassA: " << objA.value << std::endl;

    return 0;
}
