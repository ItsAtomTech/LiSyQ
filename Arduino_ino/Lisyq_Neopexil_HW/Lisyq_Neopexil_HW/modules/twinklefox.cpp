#include "fx/1d/twinklefox.h"
using namespace fl;

bool pauseTfox = true;

TwinkleFox twinkleFox(NUM_LEDS_MAX); //initilize it with MAX LEDs

//Function for the twinkleFox effect
void tfox_effect(int index){		
		if(pauseTfox){
			twinkleFox.pause(millis());
		}else{
			twinkleFox.resume(millis());
		}
		
		 EVERY_N_SECONDS(SECONDS_PER_PALETTE) { 
			twinkleFox.chooseNextColorPalette(twinkleFox.targetPalette); 
		}
		
		twinkleFox.draw(Fx::DrawContext(millis(), leds[index]));
}