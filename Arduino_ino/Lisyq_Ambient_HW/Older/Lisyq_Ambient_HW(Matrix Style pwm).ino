int pwm_leds_pins[] = {3,5,6};
int row_pins[] = {7,8,12};
int pin_count = 0;
const byte numChars = 960;
char receivedChars[numChars];   // an array to store the received data

int channel = 0; //Zero Based 
int channel_size = 3;//This code is designed to control 3 channel of rgb sets in matrix(9 LEDS)
boolean newData = false;

// the setup routine runs once when you press reset:
void setup() {
  // initialize serial communication at 115200 bits per second:
  Serial.begin(115200);
  
    pin_count = sizeof(pwm_leds_pins)/sizeof(int);
  for(int pin = 0; pin < pin_count; pin++){
    pinMode(pwm_leds_pins[pin], OUTPUT);
  }  

    for(int pin = 0; pin < pin_count; pin++){
    pinMode(row_pins[pin], OUTPUT);
  } 
}



// the loop routine runs over and over again forever:
void loop() {

recvWithEndMarker();
    showNewData();

process_current();

    
      // delay in between reads for stability
}

void recvWithEndMarker() {
    static byte ndx = 0;
    char endMarker = '\n';
    char rc;
    
    while (Serial.available() > 0 && newData == false) {
        rc = Serial.read();

        //Should send out as soon as it arrives?
        Serial.print(rc);
        
        if (rc != endMarker) {
            receivedChars[ndx] = rc;
            ndx++;
            if (ndx >= numChars) {
                ndx = numChars - 1;
            }
        }
        else {
            receivedChars[ndx] = '\0'; // terminate the string
            ndx = 0;
            newData = true;
        }
    }
}

void showNewData() {
    if (newData == true) {
//        Serial.print("This just in ... ");

        

        //Serial.println(receivedChars);
        newData = false;
    }
}




void process_current(){

  for(int ix = 0; ix < channel_size; ix++){
    
            toggle_pins(ix);
           

            
  }
  //Serial.println(receivedChars);
}



void toggle_pins(int p_num){
    String data = getValue(receivedChars, ',', (channel_size * channel)+p_num);
  
/*
  for(int ledp = 0; ledp < pin_count; ledp++){   
    if(ledp == p_num){
       digitalWrite(row_pins[ledp], HIGH); 
       set_leds(data);
    }else{
       digitalWrite(row_pins[ledp], LOW);
    }  
  } */
  

  if(p_num == 0){
       digitalWrite(row_pins[0], HIGH);digitalWrite(row_pins[1], LOW); digitalWrite(row_pins[2], LOW);    
        set_leds(data);  
    }else if(p_num == 1){
      digitalWrite(row_pins[0], LOW);digitalWrite(row_pins[1], HIGH); digitalWrite(row_pins[2], LOW);
       set_leds(data);
    }else{
      digitalWrite(row_pins[0], LOW);digitalWrite(row_pins[1], LOW); digitalWrite(row_pins[2], HIGH);
       set_leds(data);
    }
   //delay(1); 
   delayMicroseconds(1500);
 
  
}


void set_leds(String value){
 //Serial.println(""); 

    for(int cls = 0; cls < channel_size;cls++){  
           
    char hex_color[2] = {value.charAt(cls*2), value.charAt((cls*2)+1)};    
      // Serial.println(value.charAt(cls*2)+value.charAt((cls*2)+1)+" ");   
    int color_num = strtoul(hex_color, NULL, 16);

    analogWrite(pwm_leds_pins[cls], color_num);

    }
    
  
}




//String Functions

//Split and get the String at Sepcific Location of index
String getValue(String data, char separator, int index)
{
    int found = 0;
    int strIndex[] = { 0, -1 };
    int maxIndex = data.length() - 1;

    for (int i = 0; i <= maxIndex && found <= index; i++) {
        if (data.charAt(i) == separator || i == maxIndex) {
            found++;
            strIndex[0] = strIndex[1] + 1;
            strIndex[1] = (i == maxIndex) ? i+1 : i;
        }
    }
    return found > index ? data.substring(strIndex[0], strIndex[1]) : "";
}
