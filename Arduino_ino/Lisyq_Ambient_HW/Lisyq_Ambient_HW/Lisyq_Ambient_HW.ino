#include "SoftPWM.h"
#include "modules/display.cpp"
#include "modules/storage.cpp"
#include "modules/controls.cpp"

controlPanel controls;
localStorage ls;

int pwm_leds_pins[][3] = { { 2, 3, 4 }, { 5, 6, 7 }, { 8, 9, 10 } };

int pin_count = 0;
const byte numChars = 1000;
char receivedChars[numChars];  // an array to store the received data

int channel = 0;       //Zero Based channel System
int channel_size = 3;  //This code is designed to control 3 channel of rgb sets

int channel_interval = 0;
char channelMarker = ',';

boolean newData = false;

// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 115200 bits per second:
  Serial.begin(115200);

  // Initialize 4Dig. Display
  tm1637.init();
  tm1637.set(6);//BRIGHT_TYPICAL = 2,BRIGHT_DARKEST = 0,BRIGHTEST = 7;

  channel = ls.getItem(0, 5).toInt();
  controls.number = channel;
  
  pin_count = sizeof(pwm_leds_pins) / sizeof(int);
  for (int pin = 0; pin < pin_count; pin++) {
    pinMode(pwm_leds_pins[pin], OUTPUT);
  }

  displayDigit("C"+zeros(channel), true, false);
  
  controls.init();

  SoftPWMBegin();
  SoftPWMSetFadeTime(ALL, 0, 0);
}

bool pressed;

// the loop routine runs over and over again forever:
void loop() { 

  recvWithEndMarker();
  showNewData();
  autoOffOSD(5000);
  //process_current();
  
  controls.currentMs = millis();
  controls.onKeyPress();
  processModes();


}

unsigned long execDelay;
bool executed;
bool isStaticDisplayed = false;//If the display have been updated once into static
void processModes(){
 pressed = controls.isPressed();
  if(pressed){
      isStaticDisplayed = false;     
  };

  
	if(millis() - execDelay < 100){	
    return;
	}else{
    execDelay = millis();
    
	}
  

  int mode = controls.currentMode;

  if(mode == 1){
      osdBlink(true);     
      displayDigit("C"+zeros(controls.number), true, false);
      channel = controls.number;
      isStaticDisplayed = false;   

      if(controls.number > 512){
        channel = 512;
        controls.number = 512;
      }else if(controls.number < 0){
        channel = 0;
        controls.number = 0;
      }


      
  }else if(isStaticDisplayed == false){
      isStaticDisplayed = true;     
      osdBlink(false); 
      channel = controls.number;
      saveChannel(String(channel));      
      displayDigit("C"+zeros(channel), true, false);         
  }




};


//Save Channel to EEPROM
void saveChannel(String ch){
  String current = ls.getItem(0, 5);
    
    if(current == ch){
      //Serial.println("Same value!");
      return;
    }
    
  //Serial.println("Value saved");
  ls.setItem(0,5,ch);
  return;
}





void recvWithEndMarker() {
  static byte ndx = 0;
  char endMarker = '\n';
  char rc;

  while (Serial.available() > 0 && newData == false) {
    rc = Serial.read();

    //This should send out as soon as it arrives for daisy chaining
    Serial.print(rc);

    if (rc != endMarker) {

     //count "," recieved

      if(rc == channelMarker){
        channel_interval++;
      }

    
          
 
      /* 
      If count of "," is less than target start, dont update receivedChars
         
      If count of "," exceds limit, stop updating received chars  
      */
      
      //Get the channel values in range of channel and channel_size
      if(channel_interval >= channel && channel_interval < (channel + channel_size)){

        receivedChars[ndx] = rc;
             
        ndx++;

        //removes extra "," at the start of the data array if channel is > 0
        if((channel >= 1 && rc == channelMarker) &&  (channel_interval == channel)){
         ndx--; 
        }        

       
        
      }

      if (ndx >= numChars) {
        ndx = numChars - 1;
      }
    } else {
      receivedChars[ndx] = '\0';  // terminate the string
      newData = true;
      ndx = 0;
      channel_interval = 0;
      
    }
  }
}

void showNewData() {
  if (newData == true) {
    //        Serial.print("This just in ... ");
    //Serial.println(receivedChars);
    process_current();
    newData = false;
  }
}




void process_current() {

  for (int ix = 0; ix < channel_size; ix++) {

    toggle_pins(ix);
  }
  
}



void toggle_pins(int p_num) {
  String data = getValue(receivedChars, ',' , p_num);
  
  
  set_leds(data, p_num);

  delay(0);
  //delayMicroseconds(500);
}


void set_leds(String value, int chanel) {
  //Serial.println("");

  for (int cls = 0; cls < channel_size; cls++) {
    char hex_color[2] = { value.charAt(cls * 2), value.charAt((cls * 2) + 1) };
    int color_num = strtoul(hex_color, NULL, 16);
    //analogWrite(pwm_leds_pins[cls], color_num);
    SoftPWMSet(pwm_leds_pins[chanel][cls], color_num);
  }
}




//String Functions

//Split and get the String at Sepcific Location of index
String getValue(String data, char separator, int index) {
  int found = 0;
  int strIndex[] = { 0, -1 };
  int maxIndex = data.length() - 1;

  for (int i = 0; i <= maxIndex && found <= index; i++) {
    if (data.charAt(i) == separator || i == maxIndex) {
      found++;
      strIndex[0] = strIndex[1] + 1;
      strIndex[1] = (i == maxIndex) ? i + 1 : i;
    }
  }
  return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}