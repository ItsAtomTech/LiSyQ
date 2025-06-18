//Serial class by Atomtech 2024
//This is adapted from LiSyQ Project files for use on general porpuse
//redistrubution is not prohibited.

const byte numChars = 1000;
char receivedChars[numChars];  // an array to store the received data

Communicator coms;
bool newData = false;
bool isConfig = false;

#define MAX_CHANNELS_LS 3

int channel_interval = 0;
char channelMarker = ',';

int channels[MAX_CHANNELS_LS];
int currentChannel = 0;

bool isInChannels(int value) {
  for (int i = 0; i < MAX_CHANNELS_LS; i++) {
    if (channels[i] == value) return true;
  }
  return false;
}


void recvWithEndMarker() {
  static byte ndx = 0;
  char endMarker = '\n';
  char rc;

  while (Serial.available() > 0 && newData == false) {
    rc = Serial.read();

    if (rc != endMarker) {
      if (ndx == 1) {
        isConfig = (receivedChars[0] == 'D' && rc == 'X');
      }

      receivedChars[ndx] = rc;
      ndx++;
      if (ndx >= numChars) ndx = numChars - 1;

      if (!isConfig && rc == channelMarker) {
        receivedChars[ndx - 1] = '\0';  // remove comma
        if (isInChannels(currentChannel)) {
          String SData = receivedChars;
          coms.processCommands(SData);
        }
        ndx = 0;
        currentChannel++;
      }

    } else {
      receivedChars[ndx] = '\0';
      if (isConfig || isInChannels(currentChannel)) {
        String SData = receivedChars;
        coms.processCommands(SData);
      }

      newData = true;
      ndx = 0;
      currentChannel = 0;
      isConfig = false;
    }
  }
}

//Process Recieved data from Serial if complete
void showNewData() {
  if (newData == true) {
	String SData = receivedChars; //Put the complete Data to the String target
	//coms.processCommands(SData); //modify this to whatever function
	newData = false;
  }
  return;
  
};



