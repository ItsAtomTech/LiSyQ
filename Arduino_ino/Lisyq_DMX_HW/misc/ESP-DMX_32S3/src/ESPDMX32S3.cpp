// - - - - -
// ESPDMX - A Arduino library for sending and receiving DMX using the builtin serial hardware port.
// ESPDMX.cpp: Library implementation file
//
// Copyright (C) 2015  Rick <ricardogg95@gmail.com>
// This work is licensed under a GNU style license.
//
// Last change: Marcel Seerig <https://github.com/mseerig>
//
// Documentation and samples are available at https://github.com/Rickgg/ESP-Dmx
// - - - - -

// Modified by ItsAtomtech 2026

/* ----- LIBRARIES ----- */
#include <Arduino.h>

#include "ESPDMX32S3.h"



#define dmxMaxChannel  512
#define defaultMax 32

#define DMXSPEED       250000
#define DMXFORMAT      SERIAL_8N2
#define BREAKSPEED     83333
#define BREAKFORMAT    SERIAL_8N1

bool dmxStarted = false;
int sendPin = 4;		//dafault on ESP32 - S3

//DMX value array and size. Entry 0 will hold startbyte
uint8_t dmxData[dmxMaxChannel] = {};
int chanSize;

enum DMXState { IDLE, BREAK_START, BREAK_DONE, SEND_DATA, DATA_DONE };
  DMXState dmxState = IDLE;
  unsigned long dmxTimestamp = 0;


void DMXESPSerial::init() {
  chanSize = defaultMax;

  Serial1.begin(DMXSPEED);
  pinMode(sendPin, OUTPUT);
  dmxStarted = true;
}

// Set up the DMX-Protocol
void DMXESPSerial::init(int chanQuant) {

  if (chanQuant > dmxMaxChannel || chanQuant <= 0) {
    chanQuant = defaultMax;
  }

  chanSize = chanQuant;

  Serial1.begin(DMXSPEED);
  pinMode(sendPin, OUTPUT);
  dmxStarted = true;
}

// Function to read DMX data
uint8_t DMXESPSerial::read(int Channel) {
  if (dmxStarted == false) init();

  if (Channel < 1) Channel = 1;
  if (Channel > dmxMaxChannel) Channel = dmxMaxChannel;
  return(dmxData[Channel]);
}

// Function to send DMX data
void DMXESPSerial::write(int Channel, uint8_t value) {
  if (dmxStarted == false) init();

  if (Channel < 1) Channel = 1;
  if (Channel > chanSize) Channel = chanSize;
  if (value < 0) value = 0;
  if (value > 255) value = 255;

  dmxData[Channel] = value;
}

void DMXESPSerial::end() {
  delete dmxData;
  chanSize = 0;
  Serial1.end();
  dmxStarted = false;
}

void DMXESPSerial::update() {
  if (dmxStarted == false) init();

  //Send break
  digitalWrite(sendPin, HIGH);
  Serial1.begin(BREAKSPEED, BREAKFORMAT);
  Serial1.write(0);
  Serial1.flush();
  delay(1);
  Serial1.end();

  //send data
  Serial1.begin(DMXSPEED, DMXFORMAT);
  digitalWrite(sendPin, LOW);
  Serial1.write(dmxData, chanSize);
  Serial1.flush();
  delay(1);
  Serial1.end();
}


void DMXESPSerial::update(bool noneBlock) {
  switch (dmxState) {
    case IDLE:
      if (!dmxStarted) init();
      digitalWrite(sendPin, HIGH);
      Serial1.begin(BREAKSPEED, BREAKFORMAT);
      Serial1.write(0);  // DMX Break = 0 byte
      Serial1.flush();
      dmxTimestamp = micros();
      dmxState = BREAK_DONE;
      break;

    case BREAK_DONE:
      if (micros() - dmxTimestamp >= 110) {
        Serial1.end();
        Serial1.begin(DMXSPEED, DMXFORMAT);
        digitalWrite(sendPin, LOW);
        Serial1.write(dmxData, chanSize);
        Serial1.flush();
        dmxTimestamp = micros();
        dmxState = DATA_DONE;
      }
      break;

    case DATA_DONE:
      if (micros() - dmxTimestamp >= 100) {
        Serial1.end();
        dmxState = IDLE; // ready for next update
      }
      break;

    default:
      dmxState = IDLE;
      break;
  }
}

// Function to update the DMX bus
