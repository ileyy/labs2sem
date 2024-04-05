#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>

class Creature {
public:
    int health;
    int strength;
    Creature(int health, int strength) : health(health), strength(strength) {}

    float attack() {
        return rand() % (int)(1.2 * strength - 0.8 * strength) + 0.8 * strength;
    }
};

class Human : public Creature {
private:
    std::string name;
public:
    Human(std::string name, int health, int strength) : Creature(health, strength), name(name) {}
    std::string getName() { return name; }
};

class Monster : public Creature {
private:
    int level;
public:
    Monster(int level = 1, int health = 100, int strength = 100) : Creature(health, strength), level(level) {}
};

class Fight {
public:
    static std::string battle(Human& human, Monster& monster) {
        float humanDamage = human.attack();
        float monsterDamage = monster.attack();

        std::cout << human.getName() << "'s Health: " << human.health << ", Damage: " << humanDamage << std::endl;
        std::cout << "Monster's Health: " << monster.health << ", Damage: " << monsterDamage << std::endl;

        human.health -= static_cast<int>(monsterDamage);
        monster.health -= static_cast<int>(humanDamage);

        if (human.health <= 0 && monster.health <= 0) {
            return "Draw";
        }
        else if (human.health <= 0) {
            return "Monster wins";
        }
        else if (monster.health <= 0) {
            return human.getName() + " wins";
        }
        else {
            return "Continue fighting";
        }
    }
};

int main() {
    srand(static_cast<unsigned int>(time(0)));

    std::string humanName;
    int humanHealth, humanStrength;
    std::cout << "Enter human's name: ";
    std::cin >> humanName;
    std::cout << "Enter human's health: ";
    std::cin >> humanHealth;
    std::cout << "Enter human's strength: ";
    std::cin >> humanStrength;

    Human human(humanName, humanHealth, humanStrength);
    Monster monster;

    while (true) {
        std::cin.ignore();
        std::cout << "Press Enter to start next iteration of battle..." << std::endl;
        std::cin.get();
        std::string result = Fight::battle(human, monster);
        std::cout << result << std::endl;
        if (result != "Continue fighting") {
            break;
        }
    }

    return 0;
}
