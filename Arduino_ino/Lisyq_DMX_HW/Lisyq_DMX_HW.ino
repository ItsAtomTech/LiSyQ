#include "modules/storage.cpp"
#include "modules/communication.cpp"
#include "modules/serialRecv.cpp"
#include <ArduinoJson.h>

localStorage storage;

const int jsonStartIndex = 0;        // EEPROM start position
const int jsonMaxLength = 675;       // Max bytes to reserve (for writing only)

#define MAX_CHANNELS_LS 20
//LiSyQ Based channels
int channels[MAX_CHANNELS_LS];

//DMX Channel Configs
String DMX_CONFIG[MAX_CHANNELS_LS];

//Count of Channel configured for Translation
int CONFIGURED_CHANNEL = 0;

void setup() {
  Serial.begin(115200);
  coms.setResponder(processCommands);
  delay(500);//wait for initialization
  
  loadCondigData();
}




void loop() {
  recvWithEndMarker();  // Hardware Serial
  showNewData();


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


// To-Do: Load saved Config into Data Variables

//Loads saved Data from eeprom
void loadCondigData(){
  // 📤 Load from EEPROM (no size needed with improved getItem)
  String loadedData = storage.getItem(jsonStartIndex);
  Serial.println("\nLoaded:");
  Serial.println(loadedData);

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

  }
  
  loadedData = ""; 
  loadedConfig.clear();
}


int channelCount = 0;
//Put to channel arrays
void putToChannels(String data) {
  channelCount = 0; // Reset count

  int startIdx = 0;
  int commaIdx = data.indexOf(',');

  while (commaIdx != -1 && channelCount < MAX_CHANNELS_LS) {
    channels[channelCount++] = data.substring(startIdx, commaIdx).toInt();
    startIdx = commaIdx + 1;
    commaIdx = data.indexOf(',', startIdx);
  }

  // Add the last number (or only one if no commas)
  if (startIdx < data.length() && channelCount < MAX_CHANNELS_LS) {
    channels[channelCount++] = data.substring(startIdx).toInt();
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

  if(type == "DX"){
      if(saveFromConfig(RAW_DATA)){
          loadCondigData();
      };      

  }  
  Serial.println(RAW_DATA);  
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
