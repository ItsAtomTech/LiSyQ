//Communicator class by Atomtech 2024
class Communicator{
	typedef void (*FunctionPointer)(String); // Define a type for the function pointer

	  // Method to set the function pointer
    public: void setResponder(FunctionPointer function) {
        myFunction = function;
    }

	//Process commands sent on Serial
	public: void processCommands(String cmd){
		  String type = getValueAt(cmd, '-', 0);
		 
		  if (type == "DX") {  
			  respondCommand(cmd);
		  }else{
			  respondCommand(cmd);
		  }
		  
		  return;
		};
		
	//Process commands sent on Serial on Wireless
	public: void processCommandWireless(String cmd){
		String type = getValueAt(cmd, '|', 0);

		respondCommand("<-"+cmd);
		  return;
		};
	
	    // Method to call the function pointer
    void respondCommand(String dta) {
        if (myFunction) {
            myFunction(dta); // Call the function through the pointer
        } else {
            // Handle the case where no function has been set
        }
    }

private:
    FunctionPointer myFunction = nullptr; // Initialize to nullptr

	//Split and get the String at Sepcific Location of index
	private: String getValueAt(String data, char separator, int index) {
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


};