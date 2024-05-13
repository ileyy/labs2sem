#include <SFML/Graphics.hpp>
#include <iostream>

void drawHouse(sf::RenderWindow& window) {
    sf::RectangleShape house(sf::Vector2f(200.0f, 200.0f));
    house.setPosition(100, 300);
    house.setFillColor(sf::Color::Yellow);

    sf::RectangleShape door(sf::Vector2f(50.0f, 100.0f));
    door.setPosition(175, 400);
    door.setFillColor(sf::Color::Blue);

    sf::RectangleShape windowShape(sf::Vector2f(40.0f, 40.0f));
    windowShape.setPosition(125, 350);
    windowShape.setFillColor(sf::Color::White);

    sf::RectangleShape windowShape2(sf::Vector2f(40.0f, 40.0f));
    windowShape2.setPosition(235, 350);
    windowShape2.setFillColor(sf::Color::White);

    sf::RectangleShape chimney(sf::Vector2f(30.0f, 80.0f));
    chimney.setPosition(250, 220);
    chimney.setFillColor(sf::Color::Red);

    window.draw(house);
    window.draw(door);
    window.draw(windowShape);
    window.draw(windowShape2);
    window.draw(chimney);
}

void drawClock(sf::RenderWindow& window) {
    sf::ConvexShape rhombus;
    rhombus.setPointCount(4);
    rhombus.setPoint(0, sf::Vector2f(500, 100));
    rhombus.setPoint(1, sf::Vector2f(600, 200));
    rhombus.setPoint(2, sf::Vector2f(500, 300));
    rhombus.setPoint(3, sf::Vector2f(400, 200));
    rhombus.setFillColor(sf::Color::White);
    rhombus.setOutlineColor(sf::Color::Black);
    rhombus.setOutlineThickness(5);

    sf::Font font;
    if (!font.loadFromFile("comic.ttf")) std::cerr << "Error loading font\n";
    sf::Text text;
    text.setFont(font);
    text.setCharacterSize(20);
    text.setFillColor(sf::Color::Black);

    std::string numbers[4] = { "12", "3", "6", "9" };
    sf::Vector2f positions[4] = {
        sf::Vector2f(490, 110),  // 12
        sf::Vector2f(575, 187), // 3
        sf::Vector2f(493, 270), // 6
        sf::Vector2f(415, 187)  // 9
    };

    sf::RectangleShape hourHand(sf::Vector2f(50.0f, 5.0f));
    hourHand.setFillColor(sf::Color::Black);
    hourHand.setPosition(500, 200);
    hourHand.setRotation(45);

    sf::RectangleShape minuteHand(sf::Vector2f(70.0f, 3.0f));
    minuteHand.setFillColor(sf::Color::Black);
    minuteHand.setPosition(500, 200);
    minuteHand.setRotation(120);

    window.draw(rhombus);
    window.draw(hourHand);
    window.draw(minuteHand);
    for (int i = 0; i < 4; i++) {
        text.setString(numbers[i]);
        text.setPosition(positions[i]);
        window.draw(text);
    }
}

int main() {
    sf::RenderWindow window(sf::VideoMode(800, 600), "house with clock");

    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear(sf::Color(128, 128, 128));
        drawHouse(window);
        drawClock(window);
        window.display();
    }

    return 0;
}
