#include <ESP8266WiFi.h>
#include <EEPROM.h>
#include <ESPping.h>  // Include the ESP8266 Ping library "ESPping"

class WiFiConnector {
private:
    String inputBuffer; // Buffer to hold incoming data
   
    unsigned long connectionStartTime;

public: 
    bool connected;
	bool CONFIG_ACTIVE = false; //If configuration action is Active
    WiFiConnector() {
        // Serial.begin(115200); // Start the serial communication at 115200 baud
        WiFi.mode(WIFI_STA); // Set WiFi to station mode
        inputBuffer = ""; // Initialize the buffer
        connected = false;
        connectionStartTime = 0;
        EEPROM.begin(512); // Allocate enough space in EEPROM
    }
		//This will now load saved network
    void loadSavedNetwork() {
        delay(2000); // Delay to prevent error
        if (!loadCredentialsFromEEPROM()) {
            Serial.println("Enter 'connect' to start WiFi setup...");
        }
    }

    void connectWiFi(String ssid, String password) {
        Serial.println("Attempting WiFi...");
        WiFi.begin(ssid.c_str(), password.c_str());
        connectionStartTime = millis();
        while (WiFi.status() != WL_CONNECTED && millis() - connectionStartTime < 10000) {
            delay(100);
            Serial.print(".");
        }
        if (WiFi.status() == WL_CONNECTED) {
            Serial.println("\nConnected!");
            Serial.print("IP Address: ");
            Serial.println(WiFi.localIP());
            connected = true;
            saveCredentialsToEEPROM(ssid, password);
        } else {
            Serial.println("\nConnection failed. Check your credentials or setup.");
            connected = false;
        }
    }
	
	
    void disconnectWiFi() {
        if (connected) {
            WiFi.disconnect();
            Serial.println("Disconnected from WiFi.");
            connected = false;
        } else {
            Serial.println("Not currently connected to WiFi.");
        }
    }

	
    void processSerial() {
        while (Serial.available() > 0) {
            char incomingChar = Serial.read();
            if (incomingChar == '\n' || incomingChar == '\r') {
                if (inputBuffer.length() > 0) {
                    handleCommand(inputBuffer);
                    inputBuffer = ""; // Clear the buffer after handling the command
                }
            } else {
                inputBuffer += incomingChar; // Add the incoming char to the buffer
            }
        }
    }

    void handleCommand(const String& command) {
        static String ssid;
        static bool expectingSSID = false, expectingPassword = false;
        if (command.startsWith("ping ")) {
            pingAddress(command.substring(5)); // Call pingAddress function with the IP part of the command
            return;
        } else if (command == "disconnect") {
            disconnectWiFi(); // Call disconnectWiFi function
			CONFIG_ACTIVE = false;
            return;
        }
		
        if (!connected) {
            if (command == "connect" && !expectingSSID && !expectingPassword) {
                Serial.println("Enter SSID:");
                expectingSSID = true;
				CONFIG_ACTIVE = true;
            } else if (expectingSSID) {
                ssid = command;
                Serial.println("Enter Password:");
                expectingSSID = false;
                expectingPassword = true;
				CONFIG_ACTIVE = true;
            } else if (expectingPassword) {
                Serial.println("Connecting...");
                connectWiFi(ssid, command);
                expectingPassword = false;
				CONFIG_ACTIVE = false;
            }
        } else {
            //Serial.println("Already connected to WiFi.");
        }
    }

    void pingAddress(const String& address) {
        Serial.println("Pinging " + address);
        for (int i = 0; i < 4; i++) {
            bool result = Ping.ping(address.c_str());
            if (result) {
                Serial.println("Reply from " + address);
            } else {
                Serial.println("No reply from " + address);
            }
        }
    }


    void saveCredentialsToEEPROM(const String& ssid, const String& password) {
        int ssidLength = ssid.length();
        int passLength = password.length();

        EEPROM.write(wifiAddressStart, ssidLength); // store SSID length
        for (int i = 0; i < ssidLength; ++i) {
            EEPROM.write(wifiAddressStart + 1 + i, ssid[i]); // store SSID characters
        }

        EEPROM.write(wifiAddressStart + 1 + ssidLength, passLength); // store password length
        for (int i = 0; i < passLength; ++i) {
            EEPROM.write(wifiAddressStart + 2 + ssidLength + i, password[i]); // store password characters
        }

        EEPROM.commit();
    }


    bool loadCredentialsFromEEPROM() {
        int ssidLength = EEPROM.read(wifiAddressStart);
        if (ssidLength == 0 || ssidLength > 32) {
            return false;
        }

        String ssid = "";
        for (int i = 0; i < ssidLength; ++i) {
            ssid += char(EEPROM.read(wifiAddressStart + 1 + i));
        }

        int passLength = EEPROM.read(wifiAddressStart + 1 + ssidLength);
        if (passLength == 0 || passLength > 32) {
            return false;
        }

        String password = "";
        for (int i = 0; i < passLength; ++i) {
            password += char(EEPROM.read(wifiAddressStart + 2 + ssidLength + i));
        }

        Serial.println("\n" + ssid + " " + password);
        connectWiFi(ssid, password);
        return true;
    }

};
