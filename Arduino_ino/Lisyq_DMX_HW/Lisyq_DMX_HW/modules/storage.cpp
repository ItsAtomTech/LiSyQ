#include <EEPROM.h>

/*
Wrapper class for the built-in EEPROM of Arduino.
*/
class localStorage {
  
  // Get Item starting at index and length.
  public:
  String getItem(int index, int size) {
    char data[size];
    for (int i = 0; i < size; i++) {
      data[i] = EEPROM.read(index + i);
    }
    return String(data);
  }
  
  //Get Item and end that ends with \0 null TS.
  public:
    String getItem(int index) {
    String result = "";
    char ch;
    int i = 0;
    while (true) {
      ch = EEPROM.read(index + i);
      if (ch == '\0' || i >= EEPROM.length()) break;
      result += ch;
      i++;
    }
    return result;
  }

  // Set Item starting at index up to the size length.
  public:
  void setItem(int index, int size, char data[]) {
    for (int i = 0; i < size; i++) {
      EEPROM.write(index + i, data[i]);
    }
  }

  // Set Item starting at index up to the size length with String.
  public:
  void setItem(int index, unsigned int size, String data) {
    for (unsigned int i = 0; i < size; i++) {
      if (i < data.length()) {
        EEPROM.write(index + i, data[i]);
      } else {
        EEPROM.write(index + i, '\0'); // Null-terminate the string
      }
    }
  }
};
