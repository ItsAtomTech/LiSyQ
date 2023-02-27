//Control Panel for Lisyq Ambient 1
class controlPanel{

  //Input Pins
	int buttonUp = A1;
	int buttonDown = A0;
	int modeButton = 13;
  
  unsigned long prevButtonMs = 0;
  public:unsigned long currentMs = 0;

  public: int currentMode = 0;
  bool upWake = false;


  //Prev Button States
  int prevState = 0;
  int currState = 0;
  int longPressInterval = 500;
  int buttonValue;
  unsigned long msLongPress;
  int continued = 0;

  //Initilize the pinmodes for the Pins used: 
  public: void init(){
    pinMode(modeButton, INPUT);    
  }

  //Listen to key Press, returns true if detected a click
  public: void onKeyPress(){
      
        
      int buttonValueUp = analogRead(buttonUp);
      int buttonValueDown = analogRead(buttonDown);
      int buttonMode = digitalRead(modeButton);
    


      if(buttonValueUp >= 900){
        currState = 1;
      }else if(buttonValueDown >= 900){
        currState = 2;
      }else if(buttonMode == HIGH){
        currState = 3;
      }else{
        currState = 0;          
      }
      
      
      if(currentMs - prevButtonMs >= 100){

        if(prevState != currState && currState > 0){
        
          buttonResponder();        
      
          
          prevButtonMs = currentMs;
        }else if(currentMs - prevButtonMs >= longPressInterval && (prevState == currState && currState > 0)){
        
          if(currentMs - msLongPress >= 100 && continued <= 30){
            buttonResponder();
            continued++;
            msLongPress = currentMs;
          }else if(currentMs - msLongPress >= 20 && continued >= 15){
            buttonResponder();
            msLongPress = currentMs;
          }

            
          
        }else if(prevState != currState && currState <= 0){
          prevButtonMs = currentMs; 
          continued = 0;               
        }


        prevState = currState;
      
      } 
	  
	  	  
    return;
  }
  
  //Returns a bool if an activity from buttons occur recently.
  public: bool isPressed(){
	 if(upWake){
		upWake = false;
		return true;
	}
	  
	  return false;
  }


  public: int number = 0;
  public: void buttonResponder(){
	
	
	
    if(currState == 2 && currentMode == 1){
      number--;
	 
    }else if(currState == 3){
        
      if(currentMode < 1){
          currentMode++;        
      }else{
          currentMode = 0;
      }
      
    }else if(currState == 1 && currentMode == 1){
      number++;
	
    }else{
		upWake = true;
	}

    
  };  


};
