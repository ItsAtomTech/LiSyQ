function applyScale(n){
	
	_("dyna_scaler").innerHTML = ":root {--scale: "+n+";}";
	zoom_scale = n;
	
	gen_ruler();
	
	//play_head((time - 11 ) * zoom_scale);
	
}


let zoom_val = 1;
let zoom_limit_min = 0.6;
let zoom_limit_max = 2;
function zoomTimeline(d){
	
	
	if(d == "+"){
		zoom_val = zoom_val+0.1;
		
	}else if(d == "-"){
		zoom_val = zoom_val-0.1;
	}
	
	
	if(zoom_val < zoom_limit_min){	
		zoom_val = zoom_limit_min;	
	}else if(zoom_val > zoom_limit_max){	
		zoom_val = zoom_limit_max;
	}
	
	applyScale(zoom_val);
	
}

