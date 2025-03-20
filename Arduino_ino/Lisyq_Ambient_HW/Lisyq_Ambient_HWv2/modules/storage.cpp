#include <AT24C256.h>

AT24C256 eeprom(0x50);

/*
Wrapper class for the EEPROM AT24C256 process.
*/
class localStorage{
  
  //get Item starting at index and length.
   public: String getItem(int index, int size){
    char data[size];
    eeprom.read(index, (uint8_t*) data, sizeof(data));
    return data;
  }

  //set Item starting at index upto the size length.
  public: void setItem(int index, int size, char data[]){
    eeprom.write(index, (uint8_t*)data, size);  
  }

  //set Item starting at index upto the size length with String
  public: void setItem(int index, unsigned int size, String data){
    char Sdata[size] = {}; 
    data.toCharArray(Sdata, size);
    eeprom.write(index, (uint8_t*)Sdata, size);  
  }



  
};
