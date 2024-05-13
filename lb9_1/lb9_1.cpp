#include <iostream>
#include <string>

void draw_clock() {
    std::cout << "    /\\    \n";
    std::cout << "   /  \\   \n";
    std::cout << "  / 12 \\  \n";
    std::cout << " /      \\ \n";
    std::cout << "/3      9\\\n";
    std::cout << "\\        /\n";
    std::cout << " \\      /\n";
    std::cout << "  \\ 11 / \n";
    std::cout << "   \\  /  \n";
    std::cout << "    \\/    \n";
}

void draw_house() {
    std::cout << "    _______ \n";
    std::cout << "   /       \\\n";
    std::cout << "  /_________\\\n";
    std::cout << "  |  _ _ _  |\n";
    std::cout << "  | | | | | |\n";
    std::cout << "  | |-+-| | |\n";
    std::cout << "  | |_|_| | |\n";
    std::cout << "  |    _    |\n";
    std::cout << "  |   | |   |\n";
    std::cout << "  |___|_|___|\n";
}

int main() {
    std::cout << "Clock:\n";
    draw_clock();

    std::cout << "\nHouse:\n";
    draw_house();

    return 0;
}
