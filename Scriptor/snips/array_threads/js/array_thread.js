let template_player = {
	timers: [], 
	playingQue: [],
	
	
	
	
	
	playOn:function(){
				
		let index_timer = 0;
	
		for(targets of this.playingQue){
			
			try{
				
				let max = targets.content_length;
				
				if(this.timers[index_timer] == undefined || this.timers[index_timer] == null){
					
					this.timers[index_timer] = 0;
					
				}
				
				this.timers[index_timer]++;
				
				if(this.timers[index_timer] >= max){
					this.timers[index_timer] = 0;
				}
		//console.log(targets.data[this.timers[index_timer]]);
			
				
				
				for(ports of targets.target_port){
					
					for(chan of targets.target_channel){
						
						try{
						
						if(output[ports] == undefined){
							
							output[ports] = [];
							
						}
						
						output[ports][chan] = targets.data[this.timers[index_timer]];
						
						}catch(e){
							
							//
							
						}
					}
					
					
				}
				
				
				
			}catch(e){
				
				
			}
			
		
			index_timer++;
			
			
			
		}
		
	
		

		
	},
	
	//add to Playing Que
	addQue: function(data,index){
		
	
		
		
		this.playingQue[index] = data;
		this.timers[index] = 0;
		
		return true;
		
		
		
	},
	
	removeQue: function(index){
		
		return (this.playingQue[index] = null);
		
	}
	
	
	
	
}

var test_data;

function test(id){
	
	let index = 0;
	
	if(id != undefined){
		
		index = id;
		
	}
	
	
	
	let my_data = {
		
		"content_length":34,
		
		"data" : ['ffff1f','ffffff','f2ffff','ffffff','f3ffff','ffffff','ffff4f','ffffff','f5ffff','ffffff','ffff6f','ffffff','ffff8f','ffffff','ffff9f','fff5ff','ffffff','ff4fff','ffffff','fff3ff','ffffff','ffff2f','ffffff','ffff1f','f0ffff','ffff1f','fff4ff','ffffff','ffffff','ffffff','ffffff','ffffff','0000ff','fff0ff']
		
		
		
	}
	
	
	my_data.target_port = [0,1];
	my_data.target_channel = [0,1];
	
	//console.log(my_data);
	

	
	
	template_player.addQue(my_data,index);
	

	
	
}




var temp_rail = setInterval(t_rail, 33);
var output = [[]];

var see;

function t_rail(){
	
	template_player.playOn();
	
	if (!see){return};
	
	document.body.innerHTML = output.join("|") + "<br> " +

	template_player.timers
	;
	
	
	
	
}





