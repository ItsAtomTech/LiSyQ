
//write a fill color to a choosen LEDs 
void color_effects(int index, String color, int brightness){
	
	int colorR = strtol(color.substring(0, 2).c_str(), NULL, 16);
	int colorG = strtol(color.substring(2, 4).c_str(), NULL, 16);
	int colorB = strtol(color.substring(4, 6).c_str(), NULL, 16);
	

	fill_solid( leds[index], NUM_LEDS_MAX, CRGB( colorR, colorG, colorB));
	nscale8(leds[index], NUM_LEDS_MAX, brightness);
}