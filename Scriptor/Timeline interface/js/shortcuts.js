
document.body.addEventListener("keypress", shortcuts);

var editing_shortcuts = true;





function shortcuts(e){
	
		
		//console.log(e.key)
		
		
	if(e.code == "KeyS" && e.ctrlKey == true){
		
		save_to_file();
		
	}
	
	if(e.target.tagName != "INPUT"){

		e.preventDefault()
		
	}else{
		//this will prevent the shorcuts from activating when on input aside from what is on top of this block
		if(e.target.getAttribute('noauto')){
			
			
		
			
			
			
			
			
			
			
			
		};
		
		
		return;
	}

	
	//For Hot keys on Manual Player
		try{
			if(e.ctrlKey == false){
				
				let keypress = (e.key.toUpperCase());
				
				manual_template_manager.playOnKey(keypress);
				
				
				
			}
			
			
		}catch(er){
			console.log(e.key.toUpperCase())
			
		}
	
	

	
	
	if(e.code == "KeyO" && e.ctrlKey == true){
		
		window.chrome.webview.hostObjects.NativeObject.Open_File()
		
	}
		
	if(e.code == "KeyQ" && e.ctrlKey == true){
		
		window.chrome.webview.hostObjects.NativeObject.OpenPorts()
		
	}	
	
	if(e.code == "KeyV" && e.ctrlKey == true){
		
		paste_content();
		
	}	
	
	if(e.code == "KeyC" && e.ctrlKey == true){
		
		copy_content();
		
	}
	
	if(e.code == "Space" ){
		
		
		if(playing){
			pause();
		}else{
			play();
			
		}
		
		
	}
	
	
	//Editing
	
	if(editing_shortcuts != false){
	
		if(e.code == "KeyZ" && e.ctrlKey == true){
			
			if(shown == true){
				return;
			}	
			undo();
			
		}
			
			
		if(e.code == "KeyY" && e.ctrlKey == true){
			
			if(shown == true){
				return;
			}	
			redo();
			
		}	
		
		if(e.code == "KeyT"){
			
			edit_track_option('channel');
			

		}
		
		if(e.code == "KeyP"){
			
			edit_track_option('port');
			
		}
	
	}
	
	
}

	
	
	
	




