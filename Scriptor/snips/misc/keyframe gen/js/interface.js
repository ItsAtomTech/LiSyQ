

function add_keyframe() {
	
	
	let new_key_frame = {
		
	time_start:_("time_start").value,
	time_end:_("time_end").value,
	valueA:_("fx").value
	}
	
	key_frames.push(new_key_frame);

}

function change_time(elm) {
	
	timelen = parseFloat(elm.value);
	
	
}

















function _(id){
	
	return document.getElementById(id);
	
};