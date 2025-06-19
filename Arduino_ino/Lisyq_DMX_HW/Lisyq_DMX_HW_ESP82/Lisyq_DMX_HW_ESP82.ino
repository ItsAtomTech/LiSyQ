// IMPORTANT!: ONLY USE FOR ESP8266 Boards, 
// MIGHT not work on Avr boards etc
// ===================================
#include "modules/storage.cpp"
#include "modules/communication.cpp"
#include "modules/serialRecv.cpp"
#include <ArduinoJson.h>

#include <ESPDMX.h>
// #include "modules/ESPDMX.h" //A modefied ESP8266 DMX Lib
DMXESPSerial dmx;

localStorage storage;

const int jsonStartIndex = 0;        // EEPROM start position
const int jsonMaxLength = 675;       // Max bytes to reserve (for writing only)


//DMX Channel Configs
String DMX_CONFIG[MAX_CHANNELS_LS];

//Count of Channel configured for Translation
int CONFIGURED_CHANNEL = 0;

void setup() {
  Serial.begin(115200);
  coms.setResponder(processCommands);
  dmx.init(512);  
  storage.init(jsonMaxLength+2);

  delay(500);//wait for initialization
  dmx.write(1, 255); 
  // DmxSimple.write(0, 0);
  Serial.println("I: Initilaizing");
  loadCondigData();

  
}


unsigned long prevUpdateTime = 0;
int updateInterval = 5;


void loop() {
  recvWithEndMarker();  // Hardware Serial
  showNewData();

  if(millis() - prevUpdateTime >= updateInterval){
    dmx.update(true);
    prevUpdateTime = millis(); 
  }
  

}


//parsing Config from Serial which starts with "DX" 
//Format: DX-channels:configs:counts
//Sample: DX-0,1,2:c12|f15|p16,...:3

//save Data from Config
bool saveFromConfig(String data){
  
  String channelsData =  getValue(data, ':', 0);
  String configsData =  getValue(data, ':', 1);
  String countsData =  getValue(data, ':', 2);


  //Parse Config from DATA String

  if(channelsData.length() <= 0 
  || configsData.length() <= 1 
  || countsData.length() <= 0){
      Serial.println("ER-Config err");  
      return false;
  }  

  // Dummy Create config JSON
  StaticJsonDocument<jsonMaxLength> config;
  // config["channels"] = "0,1,2";
  // config["configs"] = "c12|f15|p16";
  // config["counts"] = 4;  
  
  config["channels"] = channelsData;
  config["configs"] = configsData;
  config["counts"] = countsData;

  //Serialize to String
  String jsonString;
  serializeJson(config, jsonString);
  Serial.println("Saving:");
  Serial.println(jsonString);

  //Store in EEPROM
  storage.setItem(jsonStartIndex, jsonMaxLength, jsonString);
  
  delay(500); // Pause before doing a thing after save process
  return true;
}



//Loads saved Data from eeprom
void loadCondigData(){
  // 📤 Load from EEPROM (no size needed with improved getItem)
  String loadedData = storage.getItem(jsonStartIndex);
  // Serial.println("\nLoaded:");
  // Serial.println(loadedData);

  // 🧪 Deserialize
  StaticJsonDocument<jsonMaxLength> loadedConfig;
  DeserializationError error = deserializeJson(loadedConfig, loadedData);

  if (error) {
    Serial.print("Failed to parse: ");
    Serial.println(error.c_str());
  } else {
    // Serial.println("Ch: " + String(loadedConfig["channels"].as<const char*>()));
    putToChannels(String(loadedConfig["channels"].as<const char*>()));
    //Serial.println("Cn: " + String(loadedConfig["configs"].as<const char*>()));
    putToDMXConfig(String(loadedConfig["configs"].as<const char*>()));

    CONFIGURED_CHANNEL = loadedConfig["counts"].as<int>();
    Serial.println("Ct: " + String(CONFIGURED_CHANNEL)); 
    Serial.println("Cc: " + String(channels[0])); 

  }
  
  loadedData = ""; 
  loadedConfig.clear();
}


int channelCount = 0;

void putToChannels(String data) {
  channelCount = 0;

  int startIdx = 0;
  int commaIdx = data.indexOf(',');

  while (commaIdx != -1 && channelCount < MAX_CHANNELS_LS) {
    channels[channelCount++] = data.substring(startIdx, commaIdx).toInt();
    startIdx = commaIdx + 1;
    commaIdx = data.indexOf(',', startIdx);
  }

  if (startIdx < data.length() && channelCount < MAX_CHANNELS_LS) {
    channels[channelCount++] = data.substring(startIdx).toInt();
  }

  // 🌌 Clear the rest of the array
  for (int i = channelCount; i < MAX_CHANNELS_LS; i++) {
    channels[i] = -1;
  }
}


int dmxConfigCount = 0;
void putToDMXConfig(String data) {
  dmxConfigCount = 0; // reset count

  int startIdx = 0;
  int commaIdx = data.indexOf(',');

  while (commaIdx != -1 && dmxConfigCount < MAX_CHANNELS_LS) {
    DMX_CONFIG[dmxConfigCount++] = data.substring(startIdx, commaIdx);
    startIdx = commaIdx + 1;
    commaIdx = data.indexOf(',', startIdx);
  }

  // Last one
  if (startIdx < data.length() && dmxConfigCount < MAX_CHANNELS_LS) {
    DMX_CONFIG[dmxConfigCount++] = data.substring(startIdx);
  }
}


//Function helper to recieve Data from Serial with no blocking routine
void processCommands(String cmd){
  String type = getValue(cmd, '-', 0);
  String RAW_DATA = getValue(cmd, '-', 1);
  Serial.println(RAW_DATA);  
  if(type == "DX"){
      if(saveFromConfig(RAW_DATA)){
          loadCondigData();
      };      

  }else{
      
      processLSData(cmd);
         
  }  
  // 
}

void processData(String data, String config, int faderValue = 255) {
  int sep1 = data.indexOf(':');
  int sep2 = data.indexOf(':', sep1 + 1);

  String colorHex = data.substring(0, sep1);
  String pos1Hex = (sep2 != -1) ? data.substring(sep1 + 1, sep2) : "";
  String pos2Hex = (sep2 != -1) ? data.substring(sep2 + 1) : "";

  int colorR = strtol(colorHex.substring(0, 2).c_str(), NULL, 16);
  int colorG = strtol(colorHex.substring(2, 4).c_str(), NULL, 16);
  int colorB = strtol(colorHex.substring(4, 6).c_str(), NULL, 16);

  // Defaults
  int colorAddr = -1, faderAddr = -1, posAddr = -1;

  // Parse config parts
  int cIndex = config.indexOf("c");
  int fIndex = config.indexOf("f");
  int pIndex = config.indexOf("p");

  if (cIndex != -1) {
    int cEnd = config.indexOf('|', cIndex);
    colorAddr = config.substring(cIndex + 1, cEnd != -1 ? cEnd : config.length()).toInt();
  }

  if (fIndex != -1) {
    int fEnd = config.indexOf('|', fIndex);
    faderAddr = config.substring(fIndex + 1, fEnd != -1 ? fEnd : config.length()).toInt();
  }

  if (pIndex != -1) {
    posAddr = config.substring(pIndex + 1).toInt();
  }

  // Write color
  if (colorAddr >= 0) {
    writeToAddress(colorAddr + 0, colorR);
    writeToAddress(colorAddr + 1, colorG);
    writeToAddress(colorAddr + 2, colorB);
  }

  // Write fader
  if (faderAddr >= 0) {
    writeToAddress(faderAddr, faderValue);
  }

  // Only write positions if both config and values exist
  if (posAddr >= 0 && pos1Hex.length() > 0 && pos2Hex.length() > 0) {
    int pos1 = strtol(pos1Hex.c_str(), NULL, 16);
    int pos2 = strtol(pos2Hex.c_str(), NULL, 16);
    writeToAddress(posAddr + 0, pos1);
    writeToAddress(posAddr + 1, pos2);
  }
}


//function helper for writing
void writeToAddress(int address, int value) {
  // Serial.print("Writing "); Serial.print(value);
  // Serial.print(" to address "); Serial.println(address);

  dmx.write(address, value); 
  dmx.update(true);
}

//To-Do: Process The Data for each channel
void processLSData(String dt){

  int channelIndex = isInChannels(currentChannel);

  // Serial.print("Dat: "+ dt); 
  if(channelIndex >= 0){
    Serial.print(" <- "+ String(currentChannel) + " "); 
    String CONFIG_DATA = DMX_CONFIG[channelIndex];
    Serial.println(CONFIG_DATA);
    processData(dt,CONFIG_DATA,250);

  }
  Serial.println("->");
}




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
