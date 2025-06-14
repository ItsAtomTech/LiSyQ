//Serial class by Atomtech 2024
//This is adapted from LiSyQ Project files for use on general porpuse
//redistrubution is not prohibited.

const byte numChars = 1000;
char receivedChars[numChars];  // an array to store the received data

Communicator coms;
bool newData = false;
bool newDataSoft = false;


//Recieve Serial until new line
void recvWithEndMarker() {
  static byte ndx = 0;
  char endMarker = '\n';
  char rc;

	//Normal Serial Coms
  while (Serial.available() > 0 && newData == false) {
    rc = Serial.read();

    if (rc != endMarker) {
		receivedChars[ndx] = rc;	 
		ndx++;

		if (ndx >= numChars) {
		ndx = numChars - 1;
		}
	  
    } else {
      receivedChars[ndx] = '\0';  // terminate the string
      newData = true;
      ndx = 0;
      
    }
  }
  
  return;
  
};




//Process Recieved data from Serial if complete
void showNewData() {
  if (newData == true) {
	String SData = receivedChars; //Put the complete Data to the String target
	coms.processCommands(SData); //modify this to whatever function
	newData = false;
  }
  return;
  
};



