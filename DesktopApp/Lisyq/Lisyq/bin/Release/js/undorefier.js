//Start of Undo Redo
	
var undo_stack = [];
var redo_stack = [];


function add_undo(data){
	if(data == undefined){
		return console.warn("Trying to add undefined to undo stack");
	}
	undo_stack.push(data);
	//clears the redo data after an undo entry is added to the stack
	redo_stack.length = 0;
	
}

	
function get_undo(){
	
	if(undo_stack.length <= 0){
		console.log("No undo item left");
		return false;
	}

	var undo_data = undo_stack.pop();
	redo_stack.push(undo_data);

	return undo_data;
}
 
 
 
 //Re-do logic
 
function get_redo(data){
	if(redo_stack.length <= 0){
		console.log("No redo item left");
		return false;
	}

	var redo_data = redo_stack.pop();
	//add the data to the undo stack after redo
	undo_stack.push(redo_data);

	return redo_data;
}



function reset_undo_redo(){
	//clears all stacks
	redo_stack.length = 0;
	undo_stack.length = 0;
	
	return true;
}

 //End of Undo Redo
 
 
 //Data Handling
 
function undo(){
	
	var undo_data = get_undo();
	
	if(undo_data == false){
		return false;
	}
	
	if(undo_data.type == 'subtrack'){
		
		
		switch(undo_data.data.type_of_action){
			
			case "add":
			
				selected_track_index = undo_data.data.index;
				remove_content(undo_data.data.subtrack_index,'undo');
					
				
			
			break;	
			
			case "delete":
			
				selected_track_index = undo_data.data.index;
				
				var sub_track_index = parseInt(undo_data.data.subtrack_index);
				
			
				add_sub_tracks(undo_data.data.track_data,'undo','insert',sub_track_index);
				
			
				
			
			break;
			
				
			case "edit":
			
				selected_track_index = undo_data.data.index;			
				var parent_node = parentTrack(undo_data.data.index);			
					selected_content = parent_node.querySelector('[content_id="'+undo_data.data.subtrack_index+'"]');
							
					modify_sub_track(undo_data.data.track_data,'undo');
					
				

					//refresh_track(selected_track_index);
				
				
					
			break;
			
			
			
		}
		
		
		
	}else if(undo_data.type == 'track'){
		
			switch(undo_data.data.type_of_action){
			
				case "add":
				
					selected_track_index = undo_data.data.index;
					remove_track(undo_data.data.index,'redo');
						
				
				break;	
				
				case "delete":
			
					selected_track_index = undo_data.data.index;
					add_track(undo_data.data.index,'undo');				
					timeline_data[undo_data.data.index] = undo_data.data.track_data;												
					refresh_track(undo_data.data.index);
						
				
			
				break;
							
				case "edit":
			
					selected_track_index = undo_data.data.index;				
					timeline_data[undo_data.data.index] = undo_data.data.track_data;												
					refresh_track(undo_data.data.index);
						
				
			
				break;
			
				
		}
		
	}
		
}
	
	
	
	


function redo(){
	
		var redo_data = get_redo();
	
	if(redo_data == false){
		return false;
	}
	
	if(redo_data.type == 'subtrack'){
		
		
		switch(redo_data.data.type_of_action){
			
			case "add":
			
				selected_track_index = redo_data.data.index;
				var sub_track_index = redo_data.data.subtrack_index;
				
				add_sub_tracks(redo_data.data.track_data,'redo','insert',sub_track_index);
				
				
					
				
			
			break;	
			
			case "delete":
			
				selected_track_index = redo_data.data.index;			
				remove_content(redo_data.data.subtrack_index,'redo');
				
			
			break;
			
				
			case "edit":
						
				
				selected_track_index = redo_data.data.index;			
				var parent_node = parentTrack(redo_data.data.index);			
					selected_content = parent_node.querySelector('[content_id="'+redo_data.data.subtrack_index+'"]');
							
					modify_sub_track(redo_data.data.track_data,'redo');
				
				
			
			break;
			
			
			
		}
		
		
		
	}else if(redo_data.type == 'track'){
		
			
			switch(redo_data.data.type_of_action){
			
				case "add":
				
					selected_track_index = redo_data.data.index;
					add_track(redo_data.data.index,'undo');				
					timeline_data[redo_data.data.index] = redo_data.data.track_data;												
					refresh_track(redo_data.data.index);
						
				
				break;	
				
				case "delete":
			
					selected_track_index = redo_data.data.index;
			
					
					remove_track(redo_data.data.index,'redo');
				
			
				break;
							
				case "edit":
			
					selected_track_index = redo_data.data.index;				
					timeline_data[redo_data.data.index] = redo_data.data.track_data;												
					refresh_track(redo_data.data.index);
						
				
			
				break;
			
				
		}
	
	}
}



function undo_format_data(type,data){
	var formated_data;
		
		formated_data = {
			"type": type,
			"data": decople_data(data)
			
		};
			
	return formated_data;
}
 
function track_undo_format(action_command, index, track){
	var track_format = {
		"type_of_action": action_command,
		"index": index, //index of this data (current selected track)
		"track_data": track
	};
	
	return track_format;
}

 
function subtrack_undo_format(action_command, index, track,subtrack_index){
	var track_format = {
		"type_of_action": action_command,
		"index": index, //index of this data (current selected track)
		"subtrack_index": subtrack_index,
		"track_data": track
	};
	
	return track_format;
}



function push_undo(type, action_command, index, data, subtrack_index){
	//type: track/subtrack
	//action_command: add/edit/delete
	//index: data index
	//data: data
	//subtrack_index: subtrack_index - for subtracks only, leave empty on track
	
	optimizedData = false;
	
	var udata;
	
	switch(type){
		case "track":
		
			udata = undo_format_data(type, track_undo_format(action_command, index, data));
	
		break;	
		
		case "subtrack":
		
			udata = undo_format_data(type, subtrack_undo_format(action_command, index, data, subtrack_index));
	
		break;
		default:  return false;	
		
	}
	
	
	
	
	add_undo(udata);
	
	
}

 
 

 