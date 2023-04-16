int channel_interval = 0;
char channelMarker = ',';

const byte numChars = 1000;
char receivedChars[numChars];  // an array to store the received data

int channel = 0;       //Zero Based channel System
int channel_size = 3;  //This code is designed to control 3 channel of rgb sets

boolean newData = false;

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
  
  return;
  
};



void showNewData() {
  if (newData == true) {
    //        Serial.print("This just in ... ");
    //Serial.println(receivedChars);
    
	//process_current();
    
	newData = false;
  }
  
  return;
  
};



