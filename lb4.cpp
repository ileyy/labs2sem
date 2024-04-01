#include <iostream>
#include <utility>

struct Node {
    int weight;
    int cost;
    Node* prev;
    Node* next;

    Node(int w, int c) : weight(w), cost(c), prev(nullptr), next(nullptr) {}
};

class DoublyLinkedList {
private:
    Node* head;

public:
    DoublyLinkedList() : head(nullptr) {}

    void push(int weight, int cost) {
        Node* newNode = new Node(weight, cost);
        if (!head) {
            head = newNode;
        } else {
            newNode->next = head;
            head->prev = newNode;
            head = newNode;
        }
    }

    std::pair<int, int> pop() {
        if (!head) {
            return std::make_pair(-1, -1);
        }
        Node* popped = head;
        head = head->next;
        if (head) {
            head->prev = nullptr;
        }
        std::pair<int, int> data = std::make_pair(popped->weight, popped->cost);
        delete popped;
        return data;
    }

    void insertAtPosition(int position, int weight, int cost) {
        Node* newNode = new Node(weight, cost);
        Node* current = head;
        int count = 0;
        while (current && count < position) {
            current = current->next;
            count++;
        }
        if (!current) {
            return;
        }
        newNode->next = current;
        newNode->prev = current->prev;
        if (current->prev) {
            current->prev->next = newNode;
        }
        current->prev = newNode;
        if (count == 0) {
            head = newNode;
        }
    }

    std::pair<int, int> deleteAtPosition(int position) {
        Node* current = head;
        int count = 0;
        while (current && count < position) {
            current = current->next;
            count++;
        }
        if (!current) {
            return std::make_pair(-1, -1);
        }
        if (current->prev) {
            current->prev->next = current->next;
        }
        if (current->next) {
            current->next->prev = current->prev;
        }
        if (count == 0) {
            head = current->next;
        }
        std::pair<int, int> data = std::make_pair(current->weight, current->cost);
        delete current;
        return data;
    }

    void display() {
        Node* current = head;
        while (current) {
            std::cout << "Weight: " << current->weight << ", Cost: " << current->cost << std::endl;
            current = current->next;
        }
    }

    ~DoublyLinkedList() {
        Node* current = head;
        while (current) {
            Node* temp = current;
            current = current->next;
            delete temp;
        }
    }
};

int main() {
    int counter;
    DoublyLinkedList dll;

    std::cout << "Enter number of additions: ";
    std::cin >> counter;
    if (counter < 1) {
        std::cout << "\nNot enough numbers." << std::endl;
        return 0;
    }
    std::cout << "\nAdd elements (format - weight cost):" << std::endl;
    for (int i = 0; i < counter; i++) {
        int w, c;
        std::cin >> w >> c;
        dll.push(w, c);
    }
    dll.display();

    char var;
    std::cout << "\nType Y if you want to insert elements, else type N: ";
    std::cin >> var;
    if (var == 'Y') {
        int count;
        std::cout << "\nHow many times do you want to insert: ";
        std::cin >> count;
        std::cout << "\nEnter the position where you want to insert element(format - position weight cost):" << std::endl;

        for (int i = 0; i < count; i++) {
            int ipos, iw, ic;
            std::cin >> ipos >> iw >> ic;
            dll.insertAtPosition(ipos, iw, ic);
        }

        dll.display();
    } else dll.display();

    std::cout << "\nType Y if you want to delete elements, else type N: ";
    std::cin >> var;
    if (var == 'Y') {
        int count;
        std::cout << "\nHow many times do you want to delete: ";
        std::cin >> count;
        std::cout << "\nEnter the position where you want to delete element:" << std::endl;

        for (int i = 0; i < count; i++) {
            int pos;
            std::cin >> pos;
            dll.deleteAtPosition(pos);
        }

        dll.display();
    } else dll.display();

    std::cout << "\nFinal result:";
    dll.display();
    return 0;
}
