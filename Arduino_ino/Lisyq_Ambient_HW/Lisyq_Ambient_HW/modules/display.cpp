#include "TM1637.h"
#define CLK 12//pins definitions for TM1637 and can be changed to other ports
#define DIO 11

TM1637 tm1637(CLK,DIO);



unsigned long activeMS;

unsigned long idledDelay;
//Display AutoOff or Idle
bool isOff;
void autoOffOSD(long miliSeconds){
	
	if(millis() - idledDelay > 200){	
		idledDelay = millis();
		tm1637.point(1);
		tm1637.set(4);
	}else{
		return;
	}

  if(millis() - activeMS >= miliSeconds && isOff == false){
    tm1637.clearDisplay();
    tm1637.point(1);
	
	isOff = true;
    
  }else if(millis() - activeMS >= miliSeconds && millis() - activeMS < miliSeconds){
    //tm1637.point(0); 
    //tm1637.start();
    //tm1637.set(0);
	isOff = false;
  }else{
    tm1637.set(4);
    tm1637.point(0);   

  }  

  
}


//Helper function to return hex char into int
int hexToInt(char ch){  
    int toret = 0;
    String str = String(ch);
      str.toLowerCase();

    if(str == "a"){
      return 10;
    }else if(str == "b"){
      return 11;
    }else if(str == "c"){
      return 12;
    }else if(str == "d"){
      return 13;
    }else if(str == "e"){
      return 14;
    }else if(str == "f"){
      return 15;
    }else if(str.toInt() <= 9 && (str != "" && str != "*")){
      return str.toInt();
    }else{
        return "";       
    }    
      


    return ""; 
}


//Add Zeros at the front of a number and return a String;
String zeros(int num){     
  if(num < 10){
    return "00"+ String(num);
  }else if(num < 100){
    return "0"+ String(num);
  }else{
  return String(num);
  }   
}

unsigned long blinkMs;
bool isBlinking;
int blinkInt;
//Display will Blink on every loop
void osdBlink(bool blink){
  isBlinking = blink;
  
}


/* Wrapper function that accepts hex string for 4 digit display, 
String data: the 4 digit string, zeroFill: should empty parts be zero, colon: show colon
*/
void displayDigit(String data, bool zeroFill, bool colon){
		
		
    if(isBlinking && millis() - blinkMs >= 120){
      blinkInt++;
      blinkMs = millis();
      // Serial.println(blinkInt);  
      tm1637.set(1);   

    }
    if(blinkInt > 1){
      blinkInt = 0;  
    }else{
      if(blinkInt <= 0 && isBlinking){
        tm1637.clearDisplay();      
        return;
      }
    }
         
    
  
  char ListDisp[5] = {0,0,0,0,0};

  tm1637.point(colon);        
  data.toCharArray(ListDisp, 5); 
  //Loop through the chars in the string 
      for(int d = 0; d <= 3; d++){
      tm1637.display(3-d,hexToInt(ListDisp[3-d])); 
        if(d >= data.length() && zeroFill){
          tm1637.display(3-d,0); 
        }else if(d >= data.length()){
          tm1637.display(3-d,"");        
        }                  
      }
    activeMS = millis();


	isOff = false;
  return;
}

