#include <SFML/Graphics.hpp>
#include <iostream>

void drawHouseAndSmoke(sf::RenderWindow& window) {
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

    sf::CircleShape smoke(20);
    smoke.setFillColor(sf::Color(128, 128, 128));
    smoke.setPosition(255, 210);

    window.draw(house);
    window.draw(door);
    window.draw(windowShape);
    window.draw(windowShape2);
    window.draw(chimney);
    int smokeY = 210;

    for (int i = 0; i < 5; ++i) {
        smoke.setRadius(smoke.getRadius() + 2);
        smoke.setPosition(255, smokeY);
        smokeY -= 20;
        window.draw(smoke);
        window.display();
        sf::sleep(sf::milliseconds(500));
    }
}

int main() {
    sf::RenderWindow window(sf::VideoMode(800, 600), "house with smoke");

    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear(sf::Color(128, 128, 128));
        drawHouseAndSmoke(window);
        window.display();
    }

    return 0;
}
