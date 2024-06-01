#include <FastLED.h>
#include "modules/allaboard.cpp"



String fxbin; //Cached store of string values from lisyq channel


#include "modules/lisyqRecv.cpp"



#define LED_PIN     D3
#define LED_PIN_2 	D5
#define LED_PIN_3 	D6


// #define UP_BUTTON D5
// #define SELECT_BUTTON D6
// #define DOWN_BUTTON D7


#define NUM_LEDS_MAX 1000
#define NUM_LEDS    50
#define NUM_LEDS_3    50
#define BRIGHTNESS  128
#define LED_TYPE    WS2811
#define COLOR_ORDER GRB
#define COLOR_ORDER_2 RGB
CRGB leds[3][NUM_LEDS_MAX];


#define UPDATES_PER_SECOND 30


//Some more effects externally
#include "modules/pacifica_waves.cpp"
#include "modules/fire2012.cpp"




// This example shows several ways to set up and use 'palettes' of colors
// with FastLED.
//
// These compact palettes provide an easy way to re-colorize your
// animation on the fly, quickly, easily, and with low overhead.
//
// USING palettes is MUCH simpler in practice than in theory, so first just
// run this sketch, and watch the pretty lights as you then read through
// the code.  Although this sketch has eight (or more) different color schemes,
// the entire sketch compiles down to about 6.5K on AVR.
//
// FastLED provides a few pre-configured color palettes, and makes it
// extremely easy to make up your own color schemes with palettes.
//
// Some notes on the more abstract 'theory and practice' of
// FastLED compact palettes are at the bottom of this file.



CRGBPalette16 currentPalette;
TBlendType    currentBlending;





extern CRGBPalette16 myRedWhiteBluePalette;
extern const TProgmemPalette16 myRedWhiteBluePalette_p PROGMEM;

String f = "";



void setup() {
    delay( 3000 ); // power-up safety delay
    FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds[0], NUM_LEDS).setCorrection( TypicalLEDStrip );
    FastLED.addLeds<LED_TYPE, LED_PIN_2, COLOR_ORDER>(leds[1], NUM_LEDS).setCorrection( TypicalLEDStrip );
    FastLED.addLeds<LED_TYPE, LED_PIN_3, COLOR_ORDER_2>(leds[2], NUM_LEDS).setCorrection( TypicalLEDStrip );
    FastLED.setBrightness(  BRIGHTNESS );
    
    Serial.begin(115200);

    gPal = HeatColors_p;

    currentPalette = RainbowColors_p;
    currentBlending = LINEARBLEND;
}

unsigned long prevMils = 0;


//running color effects
void rainbowFx(int index, int pal_index, int speed, int brightness){

      ChangePalettePeriodically(pal_index);
      
      static uint8_t startIndex[3] = {0,0,0};
      startIndex[index] = startIndex[index] + map(speed,0,255, -30.0, 30.0) ; /* motion speed */

      FillLEDsFromPaletteColors( startIndex[index], index, brightness);
  
}



void FillLEDsFromPaletteColors( uint8_t colorIndex, int index, uint8_t brightness)
{
    
    for( int i = 0; i < NUM_LEDS; ++i) {
        leds[index][i] = ColorFromPalette( currentPalette, colorIndex, brightness, currentBlending);
        colorIndex += 3;
    }
}


// There are several different palettes of colors demonstrated here.
//
// FastLED provides several 'preset' palettes: RainbowColors_p, RainbowStripeColors_p,
// OceanColors_p, CloudColors_p, LavaColors_p, ForestColors_p, and PartyColors_p.
//
// Additionally, you can manually define your own color palettes, or you can write
// code that creates color palettes on the fly.  All are shown here.

void ChangePalettePeriodically(int fx_index)
{
    
        if( fx_index ==  0)  { currentPalette = RainbowColors_p;         currentBlending = LINEARBLEND; }
        if( fx_index == 1)  { currentPalette = RainbowStripeColors_p;   currentBlending = NOBLEND;  }
        if( fx_index == 2)  { currentPalette = RainbowStripeColors_p;   currentBlending = LINEARBLEND; }
        if( fx_index == 3)  { SetupPurpleAndGreenPalette();             currentBlending = LINEARBLEND; }
        if( fx_index == 4)  { SetupTotallyRandomPalette();              currentBlending = LINEARBLEND; }
        if( fx_index == 5)  { SetupBlackAndWhiteStripedPalette();       currentBlending = NOBLEND; }
        if( fx_index == 6)  { SetupBlackAndWhiteStripedPalette();       currentBlending = LINEARBLEND; }
        if( fx_index == 7)  { currentPalette = CloudColors_p;           currentBlending = LINEARBLEND; }
        if( fx_index == 8)  { currentPalette = PartyColors_p;           currentBlending = LINEARBLEND; }
        if( fx_index == 9)  { currentPalette = myRedWhiteBluePalette_p; currentBlending = NOBLEND;  }
        if( fx_index == 10)  { currentPalette = myRedWhiteBluePalette_p; currentBlending = LINEARBLEND; }
    
}

// This function fills the palette with totally random colors.
void SetupTotallyRandomPalette()
{
    for( int i = 0; i < 16; ++i) {
        currentPalette[i] = CHSV( random8(), 255, random8());
    }
}

// This function sets up a palette of black and white stripes,
// using code.  Since the palette is effectively an array of
// sixteen CRGB colors, the various fill_* functions can be used
// to set them up.
void SetupBlackAndWhiteStripedPalette()
{
    // 'black out' all 16 palette entries...
    fill_solid( currentPalette, 16, CRGB::Black);
    // and set every fourth one to white.
    currentPalette[0] = CRGB::White;
    currentPalette[4] = CRGB::White;
    currentPalette[8] = CRGB::White;
    currentPalette[12] = CRGB::White;
    
}

// This function sets up a palette of purple and green stripes.
void SetupPurpleAndGreenPalette()
{
    CRGB purple = CHSV( HUE_PURPLE, 255, 255);
    CRGB green  = CHSV( HUE_GREEN, 255, 255);
    CRGB black  = CRGB::Black;
    
    currentPalette = CRGBPalette16(
                                   green,  green,  black,  black,
                                   purple, purple, black,  black,
                                   green,  green,  black,  black,
                                   purple, purple, black,  black );
}


// This example shows how to set up a static color palette
// which is stored in PROGMEM (flash), which is almost always more
// plentiful than RAM.  A static PROGMEM palette like this
// takes up 64 bytes of flash.
const TProgmemPalette16 myRedWhiteBluePalette_p PROGMEM =
{
    CRGB::Red,
    CRGB::Gray, // 'white' is too bright compared to red and blue
    CRGB::Blue,
    CRGB::Black,
    
    CRGB::Red,
    CRGB::Gray,
    CRGB::Blue,
    CRGB::Black,
    
    CRGB::Red,
    CRGB::Red,
    CRGB::Gray,
    CRGB::Gray,
    CRGB::Blue,
    CRGB::Blue,
    CRGB::Black,
    CRGB::Black
};



#include "modules/neocommands.cpp"
neocommands cmd;


void loop(){
    recvWithEndMarker();
    showNewData();

    if(millis() - prevMils >= UPDATES_PER_SECOND){  
      prevMils = millis();  
 
      

      // Serial.println(channel_cache_1);  
      // Serial.println(channel_cache_2);  
      // Serial.println(channel_cache_3);  

      
      for(int ch = 0; ch < channel_size; ch++){

        cmd.parseCommand(fxbin, ch);   
      
      }

//helps on better handling of fire2012 across channels;
  if(fireFxCalled){
    random16_add_entropy( random());
    fireFxCalled = false;
  }
      
   
      
      
      //FillLEDsFromPaletteColors( startIndex, 1);

      // rainbowFx(0);
      
      // pacifica_loop(1);

      // fire2012(2);

     
      
      FastLED.show();
      //FastLED.delay(1000 / UPDATES_PER_SECOND);

    }

}



// Additional notes on FastLED compact palettes:
//
// Normally, in computer graphics, the palette (or "color lookup table")
// has 256 entries, each containing a specific 24-bit RGB color.  You can then
// index into the color palette using a simple 8-bit (one byte) value.
// A 256-entry color palette takes up 768 bytes of RAM, which on Arduino
// is quite possibly "too many" bytes.
//
// FastLED does offer traditional 256-element palettes, for setups that
// can afford the 768-byte cost in RAM.
//
// However, FastLED also offers a compact alternative.  FastLED offers
// palettes that store 16 distinct entries, but can be accessed AS IF
// they actually have 256 entries; this is accomplished by interpolating
// between the 16 explicit entries to create fifteen intermediate palette
// entries between each pair.
//
// So for example, if you set the first two explicit entries of a compact 
// palette to Green (0,255,0) and Blue (0,0,255), and then retrieved 
// the first sixteen entries from the virtual palette (of 256), you'd get
// Green, followed by a smooth gradient from green-to-blue, and then Blue.
