#include <SoftwareSerial.h>

#define sRX D2
#define sTX D1



int channel_interval = 0;
char channelMarker = ',';

const byte numChars = 1000;
char receivedChars[numChars];  // an array to store the received data

int channel = 0;       //Zero Based channel System
int channel_size = 3;  //This code is designed to control 3 channel sets

boolean newData = false;

//Initialize SoftSerial Coms BAUD Rate
void initsSerial(){
	
	//--
	
}


void recvWithEndMarker() {
  static byte ndx = 0;
  char endMarker = '\n';
  char rc;

	//Normal Serial Coms
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
  

 
  return;
  
};


//Process Recieved data from Channel
void showNewData() {
  if (newData == true) {
    //        Serial.print("This just in ... ");
    //Serial.println(receivedChars);
    
	//process_current();
		
		
		fxbin = receivedChars;
	
	
	
    
	newData = false;
  }
  
  return;
  
};

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

