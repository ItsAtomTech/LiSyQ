const vertical_resizer = {
	can_move:false,
	min:30,
	max:60,
	
	resize:function(elm){
		if(this.can_move == false){
			return;
		}
		
		
		let parent_width =_("template_player_container").getBoundingClientRect().width;
		
		
		let ev = event;
		let sliderWidth = elm.getBoundingClientRect().width;		
		let calculated_pos = ((ev.clientX - (sliderWidth * 0.50))  / parent_width  * 100);
		
		
		if(calculated_pos <= this.min || calculated_pos >= this.max){
			
			return;
		}
		
		
		elm.style.left = calculated_pos+"%"
		
		_("v_con_1").style.width = calculated_pos+"%"
		_("v_con_2").style.width = (100 - calculated_pos)+"%"
		
		
		
	},
	
	dropSlider: function(elm){
		this.can_move = false;
		
		
	},
	
	pickSlider:function(elm){
		this.can_move = true;
		
	}
	
	
	
	
	
	
	
	
	
	
	
}