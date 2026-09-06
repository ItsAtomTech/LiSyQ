#include <WiFi.h>
#include <Preferences.h>
#include <ESPping.h>

class WiFiConnector {
private:
    String inputBuffer;
    unsigned long connectionStartTime;
    Preferences preferences; // Use Preferences for storage

public:
    bool connected;
	bool connectedFromSaves;
	String IPAddress;
	
    bool CONFIG_ACTIVE = false; //If configuration action is Active
    
    WiFiConnector() {
        // WiFi.mode(WIFI_STA); This is causing a RTCWDT_RTC_RESET, use with care
        inputBuffer = "";
        connected = false;
        connectionStartTime = 0;
  
    }

    void loadSavedNetwork() {
        delay(2000);
        if (!loadCredentialsFromPreferences()) {
            Serial.println("Enter 'connect' to start WiFi setup...");
        }
    }
	
	
    void closePrefs() {
        preferences.end(); // Ensure to close preferences when done
    }

	
    void connectWiFi(String ssid, String password) {
        Serial.println("Attempting WiFi...");
		// WiFi.mode(WIFI_STA);
        WiFi.begin(ssid.c_str(), password.c_str());
        connectionStartTime = millis();

        while (WiFi.status() != WL_CONNECTED && millis() - connectionStartTime < 10000) {
            delay(150);
            Serial.print(".");
        }

        if (WiFi.status() == WL_CONNECTED) {
            Serial.println("\nConnected!");
            Serial.print("IP Address: ");
            Serial.println(WiFi.localIP());
			IPAddress = WiFi.localIP().toString();
            connected = true;
			
			if(connectedFromSaves ==  false){
				saveCredentialsToPreferences(ssid, password);
			}
			
        } else {
            Serial.println("\nConnection failed. Check credentials.");
            connected = false;
        }
    }

    void disconnectWiFi() {
        if (connected) {
            WiFi.disconnect();
            Serial.println("Disconnected from WiFi.");
            connected = false;
			preferences.begin("WiFiCreds", false);  
			preferences.remove("ssid");
			preferences.remove("password");
			preferences.end(); 
			
			
        } else {
			preferences.begin("WiFiCreds", false);  
			preferences.remove("ssid");
			preferences.remove("password");
			preferences.end(); 
            Serial.println("No WiFI Connection");
        }
    }

	//returns true if a command is executed
    bool handleCommand(const String& command) {
        static String ssid;
        static bool expectingSSID = false, expectingPassword = false;

        if (command.startsWith("ping ")) {
			// Serial.println("Ping command is not supported...");
			if(!connected){
				return false;
			}
            pingAddress(command.substring(5));
            return true;
        } else if (command == "disconnect") {
            disconnectWiFi();
            CONFIG_ACTIVE = false;
            return true;
        }

        if (!connected) {
            if (command == "connect" && !expectingSSID && !expectingPassword) {
                Serial.println("Enter SSID:");
                expectingSSID = true;
                CONFIG_ACTIVE = true;
				return true;
            } else if (expectingSSID) {
                ssid = command;
                Serial.println("Enter Password:");
                expectingSSID = false;
                expectingPassword = true;
                CONFIG_ACTIVE = true;
				return true;
            } else if (expectingPassword) {
                Serial.println("Connecting...");
                connectWiFi(ssid, command);
                expectingPassword = false;
				connectedFromSaves = false;
                CONFIG_ACTIVE = false;
				return true;
            }
        }
		return false;
    }

    void pingAddress(const String& address) {
        Serial.println("Pinging " + address);
        for (int i = 0; i < 4; i++) {
            bool result = Ping.ping(address.c_str());
            if (result) {
                Serial.println("Reply from " + address + " - " + result);
            } else {
                Serial.println("No reply from " + address);
            }
        }
    }

    void saveCredentialsToPreferences(const String& ssid, const String& password) {
		preferences.begin("WiFiCreds", false);       
	    preferences.putString("ssid", ssid);
        preferences.putString("password", password);
        Serial.println("Saved to Preferences.");
		closePrefs(); // Ensure preferences are saved and closed properly
    }

	bool loadCredentialsFromPreferences() {
		preferences.begin("WiFiCreds", true);    
		String ssid = preferences.getString("ssid", "");
		String password = preferences.getString("password", "");
		closePrefs();

		if (ssid.isEmpty() || password.isEmpty()) {
			return false;
		}

		// Mask password: show first 3 chars and last 2 chars
		String maskedPassword;
		int len = password.length();
		if (len <= 5) {
			for (int i = 0; i < len; i++) maskedPassword += '*';
		} else {
			maskedPassword = password.substring(0, 3); // first 3 chars
			for (int i = 3; i < len - 2; i++) maskedPassword += '*'; // middle masked
			maskedPassword += password.substring(len - 2); // last 2 chars
		}

		Serial.println("\nLoaded credentials: " + ssid + " " + maskedPassword);
		connectedFromSaves = true;
		connectWiFi(ssid, password);
		return true;
	}


};
