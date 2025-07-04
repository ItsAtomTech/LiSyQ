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
let selectedIndex = 0;

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
	
	// Save or Add the Config data into Config array
	save_ConfigEdits: function(){
		let stab_container = _('configs_input');
		let elm_collections = (stab_container.getElementsByClassName("config_div"));
		let configData = clone(CONFIG_DATA);
		
		for(each of elm_collections){
			
			let select_type = each.querySelector('[tag="ctype"]');
			let targetc = each.querySelector('[tag="target"]');
			let channel = _("conf_channel");
			
				configData.channel = channel.value;
			
			
			let objectConf = {
				type: select_type.value,
				target: targetc.value,
			}
			
			configData.dmx_config.push(objectConf);
			configs[selectedIndex] = configData;
			
			// To-Do: Close Modal and Reset it
			
		}
		

		
	},
	
	//Config Editor Section
	
	//Adds the Config tab into the editor, if data is passed, it puts the value as well
	addConfigEdit: function(data){
		let tabs_container = _("configs_input");
		let configs_input_template = _("configs_input_template").cloneNode(true);
			if(data !=undefined){
				
				let select_type = (configs_input_template.content.querySelector('[tag="ctype"]'));
				let target = (configs_input_template.content.querySelector('[tag="target"]'));
				
				if(data.type) select_type.value = data.type;
				if(data.target) target.value = data.target;				
			}
		tabs_container.appendChild(configs_input_template.content);
	},
	
	
	removeInputTabSelf: function(elm){
		elm.parentNode.remove();
	},
	
	
	clearAllEditTabs: function(){
		let tabs_container = _("configs_input");
		tabs_container.innerHTML = "";
		
	},
	
}





//Try load all saves to list and Memory
DMX_CONF.loadAllSaved();


DMX_CONF.openConfigWindow();
DMX_CONF.addConfigEdit();







