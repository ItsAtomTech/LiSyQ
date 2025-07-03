// DMX Configurator JS 
// Writen by Atomtech @ 2025
// v0.1

// =================
// Utilities
// =================

if(typeof(_) == 'undefined'){
   function _(elm){
		return document.getElementById(elm);
	}
}



function clone(obj) {
  try {
    return structuredClone(obj);
  } catch (e) {
    try {
      return JSON.parse(JSON.stringify(obj));
    } catch (jsonError) {
      console.warn(jsonError);
    }
  }
}



//Main Vars 
let configs = [];
let options = {};

let SAVES = []; //stores the saves data


// ========================
//Constant data structure
// ========================

const CONFIG_DATA = {
	channel: 0,
	dmx_config:[],
} 


const OPTIONS_DATA = {
	name : "No name",
	date : undefined,
	type: "DX_CONFIG",
}

//====================
//Constants Ends here
//====================




const DMX_CONF = {
	
	loadAllSaved: function(){
		let saves = localStorage.getItem("DMX_SAVES");
		if(saves == null || saves.length <= 0){
			return;
		}
		
		try{
			SAVES.length = 0;
			SAVES = JSON.parse(saves);
		}catch(e){
			//
		}
		if(SAVES.length <= 0){
			console.log("Saves are empty.");
			return false;
		}
		
	},
	
	//To-Do: Add data to save items
	addToSaves: function(data){
		
		
		
	},
	
	//To-Do: Should load a selected Saved data into list view and working variables.
	loadConfig: function(index){
		
		
		
	},
	//Configurator Logic Goes Down here
	
	openConfigWindow: function(data){
		_("configModal").showModal();
		
		_("configModal").addEventListener("cancel", this.closeDefaultModal)
		
	},
	
	
	closeDefaultModal: function(){
		console.log("Cancel event triggered");
		event.target.close();
		
	},	
	
	closeModal: function(){
		_("configModal").close();
		
	},
	
	
}





//Try load all saves to list and Memory
DMX_CONF.loadAllSaved();


DMX_CONF.openConfigWindow();








