
String channel_cache_1 = "";
String channel_cache_2 = "";
String channel_cache_3 = "";


int hexdecode(String s){
	char hex_bin[s.length()+1];
	s.toCharArray(hex_bin, s.length()+1);
	
	return strtoul(hex_bin, NULL, 16);
	
}

class neocommands{
	
	

	
	
	private: String getValues(String data, char separator, int index) {
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

	
		
	
	//Parse Command for this Fixture, index is the channel from 0 - 2 (3 in total)
	public:void parseCommand(String com, int index){
		String current_data = getValues(com, ',' , index);
			
			
		//if ~ char is recieved.. it won't change the cache	
			
		if(current_data != "~"){ //not recieved a still command
			if(index == 0){
				channel_cache_1 = current_data;		
			}else if(index == 1){
				channel_cache_2 = current_data;
			}else if(index == 2){
				channel_cache_3 = current_data;		
			}
		
		}

		
		processCommand(index);
		
	}
	
	
	//process current Commands on cache
	public: void processCommand(int index){
		String com_string;
		
			if(index == 0){
				com_string = channel_cache_1;		
			}else if(index == 1){
				com_string = channel_cache_2;
			}else if(index == 2){
				com_string = channel_cache_3;		
			}
		
		String command = getValues(com_string, ':' , 0);
		
		if(command == "fx"){
			
			//Serial.println("fx commands");
			//send to builtin fx
			
			fxParser(com_string, index);
			
			
		}else{
			//Clear all effects before procceding
			  fill_solid( leds[index], NUM_LEDS, CRGB( 0, 0, 0));

			
			//Serial.println("raw commands"); 
			//send to raw parser unishox
		}
		
		
		
	}
	
	//parse fx from command Parser function
	public: void fxParser(String com_string, int index){
		
		String fx_name  = getValues(com_string, ':' , 1);
		
		if(fx_name == "pallet"){	
			palleteFx(index, com_string);		
		}else if(fx_name == "ocean"){
			pacificaFx(index, com_string);
		}
		else if(fx_name == "fire"){
			fireFx(index,com_string);
			fireFxCalled = true;
		}
		
		//Add more effects at this if-else chain
	
	}
	
	
	
	// ===========
	// Fx Functions - Functions for each effects
	// ===========
	
	
	//Pallete Fx: command "fx:pallet:pallet index:speed (7f is middle):brigtness"
	//Sample: pallet:1:7d:ff
	private: void palleteFx(int index, String com_string){
		int pal_index = getValues(com_string, ':' , 2).toInt();
		int speed = hexdecode(getValues(com_string, ':' , 3));
		int brightness = hexdecode(getValues(com_string, ':' , 4));

		
		rainbowFx(index, pal_index, speed, brightness);
		
	}
	
	//Ocean Fx: command "fx:ocean:brightness"
	//Sample: ocean:ff;
	private: void pacificaFx(int index, String com_string){
		int brightness = hexdecode(getValues(com_string, ':' , 2));
		
		int capBright = map((brightness),0,255,-255,10);
		
		pacifica_loop(index, capBright);
		
	}
		
	
	//Fire Fx: command "fx:fire:cooling:sparking:0"
	//Sample: fire:50:7f:0 ;
	private: void fireFx(int index, String com_string){
		int cooling = hexdecode(getValues(com_string, ':' , 2));
		int sparking = hexdecode(getValues(com_string, ':' , 3));
		int reverse = getValues(com_string, ':' , 4).toInt();
		
		// int index, int cooling, int sparking, int reverse	
		fire2012(index, cooling, sparking,reverse);
		
	}
	
	
	
};