#include <iostream>
#include <vector>

class Point {
public:
    int x, y;
    Point(int x, int y) : x(x), y(y) {}
};

class Line {
public:
    Point start, end;
    char symbol;
    Line(Point start, Point end, char symbol) : start(start), end(end), symbol(symbol) {}
};

class Rectangle {
public:
    Point topleft, bottomright;
    char symbol;
    Rectangle(Point topleft, Point bottomright, char symbol) : topleft(topleft), bottomright(bottomright), symbol(symbol) {}
};

class Shape {
public:
    virtual void draw(std::vector<std::vector<char>>& canvas) const = 0;
};

class Canvas {
private:
    std::vector<std::vector<char>> canvas;
public:
    Canvas(int width, int height) : canvas(height, std::vector<char>(width, ' ')) {}

    void draw() const {
        for (const auto& row : canvas) {
            std::cout << "|";
            for (char ch : row) {
                std::cout << ch;
            }
            std::cout << "|" << std::endl;
        }
    }

    void clear() {
        for (auto& row : canvas) {
            std::fill(row.begin(), row.end(), ' ');
        }
    }

    void drawShape(const Shape& shape) {
        shape.draw(canvas);
    }
};

class PointShape : public Shape {
private:
    Point point;
    char symbol;
public:
    PointShape(Point point, char symbol) : point(point), symbol(symbol) {}

    void draw(std::vector<std::vector<char>>& canvas) const override {
        canvas[point.y][point.x] = symbol;
    }
};

class LineShape : public Shape {
private:
    Line line;
public:
    LineShape(Line line) : line(line) {}

    void draw(std::vector<std::vector<char>>& canvas) const override {
        int dx = line.end.x - line.start.x;
        int dy = line.end.y - line.start.y;
        int steps = std::max(abs(dx), abs(dy));

        float xIncrement = dx / (float)steps;
        float yIncrement = dy / (float)steps;
        float x = line.start.x;
        float y = line.start.y;

        for (int i = 0; i <= steps; i++) {
            canvas[(int)y][(int)x] = line.symbol;
            x += xIncrement;
            y += yIncrement;
        }
    }
};

class RectangleShape : public Shape {
private:
    Rectangle rectangle;
public:
    RectangleShape(Rectangle rectangle) : rectangle(rectangle) {}

    void draw(std::vector<std::vector<char>>& canvas) const override {
        for (int y = rectangle.topleft.y; y <= rectangle.bottomright.y; y++) {
            for (int x = rectangle.topleft.x; x <= rectangle.bottomright.x; x++) {
                if (y == rectangle.topleft.y || y == rectangle.bottomright.y ||
                    x == rectangle.topleft.x || x == rectangle.bottomright.x) {
                    canvas[y][x] = rectangle.symbol;
                }
            }
        }
    }
};

int main() {
    Canvas canvas(40, 20);

    std::vector<Shape*> shapes;

    int choice;
    do {
        canvas.clear();
        for (auto& shape : shapes) {
            canvas.drawShape(*shape);
        }
        canvas.draw();

        std::cout << "Select the type of shape:" << std::endl;
        std::cout << "1. Point" << std::endl;
        std::cout << "2. Line" << std::endl;
        std::cout << "3. Rectangle" << std::endl;
        std::cout << "0. Exit" << std::endl;
        std::cout << "Your choice: ";
        std::cin >> choice;

        switch (choice) {
        case 1: {
            int x, y;
            char symbol;
            std::cout << "Enter coordinates for point (x y): ";
            std::cin >> x >> y;
            std::cout << "Enter symbol: ";
            std::cin >> symbol;
            shapes.push_back(new PointShape(Point(x, y), symbol));
            break;
        }
        case 2: {
            int x1, y1, x2, y2;
            char symbol;
            std::cout << "Enter coordinates for the first point of the line (x1 y1): ";
            std::cin >> x1 >> y1;
            std::cout << "Enter coordinates for the last point of the line (x2 y2): ";
            std::cin >> x2 >> y2;
            std::cout << "Enter symbol: ";
            std::cin >> symbol;
            shapes.push_back(new LineShape(Line(Point(x1, y1), Point(x2, y2), symbol)));
            break;
        }
        case 3: {
            int x1, y1, x2, y2;
            char symbol;
            std::cout << "Enter coordinates for the top left angle of the rectangle (x1 y1): ";
            std::cin >> x1 >> y1;
            std::cout << "Enter coordinates for the bottom right angle of the rectangle (x2 y2): ";
            std::cin >> x2 >> y2;
            std::cout << "Enter symbol: ";
            std::cin >> symbol;
            shapes.push_back(new RectangleShape(Rectangle(Point(x1, y1), Point(x2, y2), symbol)));
            break;
        }
        case 0:
            std::cout << "Exit." << std::endl;
            break;
        default:
            std::cout << "Error, try again." << std::endl;
        }

    } while (choice != 0);

    for (auto& shape : shapes) {
        delete shape;
    }

    return 0;
}
