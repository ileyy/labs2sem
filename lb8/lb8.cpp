#include <iostream>
#include <windows.graphics.h>

double func(double x) {
    return x * x * x - 0.125 * x * x - x + 10;
}

void plot(double x1, double x2) {
    const int width = 100;
    const int height = 50;
    char graph[height][width];

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            graph[i][j] = ' ';
        }
    }

    double min_y = func(x1), max_y = func(x1);
    for (double x = x1; x <= x2; x += (x2 - x1) / width) {
        double y = func(x);
        if (y < min_y) min_y = y;
        if (y > max_y) max_y = y;
    }

    for (double x = x1; x <= x2; x += (x2 - x1) / width) {
        double y = func(x);
        int ix = (int)((x - x1) / (x2 - x1) * (width - 1));
        int iy = (int)((y - min_y) / (max_y - min_y) * (height - 1));
        graph[height - 1 - iy][ix] = '*';
    }

    if (min_y < 0 && max_y > 0) {
        int y_axis = height - (int)((0 - min_y) / (max_y - min_y) * height);
        for (int i = 0; i < width; ++i) if (graph[y_axis][i] != '*') graph[y_axis][i] = '-';
    }
    if (x1 < 0 && x2 > 0) {
        int x_axis = (int)((0 - x1) / (x2 - x1) * (width - 1));
        for (int i = 0; i < height; ++i) if (graph[i][x_axis] != '*') graph[i][x_axis] = '|';
    }

    for (int i = 0; i < height; ++i) {
        for (int j = 0; j < width; ++j) {
            std::cout << graph[i][j];
        }
        std::cout << std::endl;
    }
}

int main() {
    double x1, x2;
    std::cout << "Enter interval for x1 and x2: ";
    std::cin >> x1 >> x2;

    plot(x1, x2);

    return 0;
}
