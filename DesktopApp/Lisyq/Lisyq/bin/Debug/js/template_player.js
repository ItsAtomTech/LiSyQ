let template_player = {
	
	templates:[], //Tempates are loaded into here
	playerTabs:[],//Play Tabs collection
	
	timers: [], //Timer counter array for every playing Que
	playingQue: [], //Playing Ques data 
	
// ================ 	

	playOn:function(){
				
		let index_timer = 0;
		output.length = 0;
	
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
		
	}, 
	
	update_channels: function(val, index){
		let values = val.value.split(",");
		
		template_player.playerTabs[index].target_channel = values;
		
		
	},	
	update_ports: function(val, index){
		let values = val.value.split(",");
		
		template_player.playerTabs[index].target_port = values;
		
		
	},
	
	update_hotkey: function(val, index){
		let keys = val.value.toUpperCase();
		
		template_player.playerTabs[index].hotkey = keys;
		
		
	},
	
	removeTemplate: function(id){
			
		let prm = window.confirm("Are you sure to delete this Template? Can't be removed while being used!");
		
		if(prm == false){
			return;
		}
		let canRemove = true;
		//scan for usage: 
		
		for(trigs of this.playerTabs){		
			if(trigs.data == parseInt(id)){			
				let node = "<center><div> Template in use! <br> can't be deleted. </div></center>";				
				createDialogue("error",node);
				canRemove = false;	
			};	
		}
		
		
		if(canRemove){
			
			template_player.templates.splice(id,1);
			manual_template_manager.load_to_view();
			
		}
		
		
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
	my_data.target_channel = [0,1,2];
	
	//console.log(my_data);
	

	
	
	template_player.addQue(my_data,index);
	

	
	
}



let manual_template_manager = {
	
		
	generate_template:function(tl_data,id){	
		
		var template_thumb = document.createElement("div");
			template_thumb.classList.add("template_thumb");
			template_thumb.style.backgroundColor = tl_data.color+"b9";
			template_thumb.title = tl_data.name + "\nType:" + tl_data.type ;
			template_thumb.setAttribute("template_id",id);
			template_thumb.setAttribute("onclick","manual_template_manager.add_to_triggers("+id+")");
			
			
			
		var template_thumb_name = document.createElement("div");
			template_thumb_name.classList.add("plug_thumb_name");
			template_thumb_name.innerHTML = tl_data.name;
			template_thumb.appendChild(template_thumb_name);
			
		
		_("manual_templates_con").appendChild(template_thumb);
		
	},
	


	//Load all templates to view
	load_to_view: function(){
		_("manual_templates_con").innerHTML = "";
		
		for(plg = 0; plg < template_player.templates.length;plg++){
		
			manual_template_manager.generate_template(template_player.templates[plg],plg);
			
			// console.log(plg);
		}
			
		
		
		_("manual_templates_con").addEventListener("contextmenu", function context_menu_templates (e) {
		
		e.preventDefault();
		e.stopPropagation();
		
			if(e.target.classList[0] == "template_thumb"){
				
				selected_template = e.target.getAttribute('template_id');
				
				current_mode = "edit_template";
				editFrom = "manual";
				
				console.log("Open Context Menu for Manual Template");
		
				
				set_coords_context(e.screenX,e.screenY);
				
				window.chrome.webview.hostObjects.NativeObject.Show_manual_template_menu();
				
			}
			
		},
		
		false
		
		)
		
		
		
		
	},
	
	
	add_to_triggers: function(id){
		let index = 0;

		if(id != undefined){
			
			index = id;
			
		}		
		
		
		var my_data = {
			

			"content_length":index,
			"data" : index,				
			"type": template_player.templates[index].type,				
			"name": index,
			"color": index
				
			
		}
		
		
		my_data.target_port = [0];
		my_data.target_channel = [0];
		my_data.hotkey = "";
		my_data.isPlaying = false;
			
		template_player.playerTabs.push(my_data);
		
		manual_template_manager.load_triggers();
		
		
	},
	
	
	// Trigger function
	
	
	generate_trigger: function(data,index){
	

		let trigger_blocks = "<div class='trigger_option'> <div class='option_input'><span class='seamless_label'> Target ch(s): </span><input onchange='template_player.update_channels(this,"+index+")' class='seamless_input primary_color' placeholder='0,1,2..' trigger='"+index+"' value='"+data.target_channel+"'/> </div><div class='option_input'><span class='seamless_label'> Target Port(s): </span><input class='seamless_input primary_color' trigger='"+index+"' placeholder='0,1,2..' value='"+data.target_port+"' onchange='template_player.update_ports(this,"+index+")'/> </div></div><div class='trigger_details' style='background-color:"+template_player.templates[data.color].color+"a5';><div class='template_details'><span>"+template_player.templates[data.name].name+"</span> <span>"+data.type+"</span> </div></div><div class='trigger_actions'><div class='option_input' onclick='manual_template_manager.remove("+index+")'>Remove</div>"+
		"<div class='option_input play_stop' onclick='manual_template_manager.play("+index+")' >Play/Stop</div>"+
		"<div class='option_input hot_key_option'>Hotkey: <input class='seamless_input primary_color' trigger='"+index+"' placeholder='A' value='"+data.hotkey+"' onchange='template_player.update_hotkey(this,"+index+")' maxlength='1'/></div></div>";
	
	
		let trigger_con = document.createElement("div");
			trigger_con.classList.add("trigger_con");
			trigger_con.setAttribute("id","trigger_"+index);
	
		if(data.isPlaying == true){
			trigger_con.classList.add('current_playing');
			
		}
	
		trigger_con.innerHTML = trigger_blocks;
		
		_("triggers_con").appendChild(trigger_con);
	
	
	},
	
	load_triggers:function(){
		_("triggers_con").innerHTML = "";
		
		for(plg = 0; plg < template_player.playerTabs.length;plg++){
		
			manual_template_manager.generate_trigger(template_player.playerTabs[plg],plg);
			
			// console.log(plg);
		}
		
		
	}, 
	
	play: function(index){
		
		
		//Stop if already playing
		if(template_player.playingQue[index] != undefined || template_player.playingQue[index] != null){
			
			template_player.removeQue(index);
			// console.log("Removed");
			_("trigger_"+index).classList.remove("current_playing");
			template_player.playerTabs[index].isPlaying = false;
			
			let allEmpty = true;
			
			for(playQue of template_player.playingQue){
				
				if(playQue != null){
					allEmpty = false;
				}
				
			}
			//Makes it empty if there are all null
			if(allEmpty){
				template_player.playingQue.length = 0;
				
			}
			return false;
				
			
		}
		// otherwise procced
		
		
		let dt = JSON.parse(JSON.stringify(template_player.playerTabs[index]));
	
		try{
			dt.data = template_player.templates[dt.data].data;
		}catch(e){
			dt.data.length = dt.content_length;
			
		}
		
		dt.content_length = template_player.templates[dt.content_length].content_length;	
		
		_("trigger_"+index).classList.add("current_playing");
		
		template_player.addQue(dt,index);
		template_player.playerTabs[index].isPlaying = true;
		// console.log(dt);
		
	},
	
	remove: function(id){
		
		template_player.removeQue(id);
		template_player.playerTabs.splice(id,1);
		
		manual_template_manager.load_triggers();
		
		
		
		
	},
	
	//Plays or Stop a trigger based on the Hot Key Assigned
	playOnKey:function(key){
		
		
		let index_ref = 0;
		for (trigger of template_player.playerTabs){
			
				if(trigger.hotkey == key){
					
					manual_template_manager.play(index_ref);
					// console.log("Triggering "+ index_ref);
					
					if(false){
						//put logic to check auto scroll is allowed
						_("trigger_"+index_ref).scrollIntoView();
					}
					
					
				}
				index_ref++;
			
		}
		
		
		
		
	},
	
	save: function(){
		let save_data = {
			templates: template_player.templates,
			playerTabs: template_player.playerTabs	
		}
		
		localStorage.setItem("local_save", JSON.stringify(save_data));
		
		
	},
	load: function(){
		let prm = window.confirm("This will Overwrite current unsaved triggers? Continue");
		if(!prm){return};
		
		let saves = localStorage.getItem("local_save");
		try{
			saves = JSON.parse(saves);
			
			template_player.templates = saves.templates;
			template_player.playerTabs = saves.playerTabs;
			template_player.playingQue.length = 0;
			
			
			
			manual_template_manager.load_triggers();
			manual_template_manager.load_to_view();
			
		
		}catch(e){
			console.log("Problem loading saved");
			
		}
		
		
		
	}

	
	
	
	
	
}






//var temp_rail = setInterval(t_rail, 30) //Outputs 33 times a second;
var active;
let hasSentLast = false;
async function t_rail(){
	
	await template_player.playOn();
	
	if (!active){return};
	
	my_output = (output.join("|"));
	
	//Send it to the port
	

	
	//sendPort(0,my_output);
	//console.log(my_output);
	
	
}



// Extra


function activate_player(elm){
	
	if(active){
		active = false;
		
		elm.classList.remove("tog_active");
		elm.title = "Player not Active";
		
		elm.childNodes[1].innerText = "Off";
		
		
	}else{
		active= true;
		elm.classList.add("tog_active");
		elm.title = "Player is Active";
		elm.childNodes[1].innerText = "Active";
	}
	
	
	
	
	
}




