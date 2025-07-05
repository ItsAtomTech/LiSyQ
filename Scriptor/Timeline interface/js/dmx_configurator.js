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

if(typeof(make) == 'undefined'){
   function make(elm){
		return document.createElement(elm);
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
let selectedSaveIndex = 0;

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
	mode: "add",
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
	addToSaves: function(){
		let DataOptions = clone(OPTIONS_DATA);
		let configsData = clone(configs);
		
		let data = {
			options: DataOptions,
			configs: configsData,
		}
		
		if(selectedSaveIndex == undefined){
			SAVES.push(data);
		}else{
			SAVES[selectedSaveIndex] = data;
		}
		let saves = localStorage.setItem("DMX_SAVES", JSON.stringify(SAVES));
		
	},
	
	//To-Do: Should load a selected Saved data into list view and working variables.
	loadConfig: function(index){
		
		
		
	},
	
	renderConfigs: function(){
		
		_("configs_tab_section").innerHTML = "";
		
		for(let z = 0; z < configs.length;z++){
			let confData = configs[z];
			let config_tab_template = _("config_tab_template").cloneNode(true).content;
			
			if(!confData){
				return false;
			}
			
			let channel_config = (config_tab_template.querySelector('[tag="channel_config"]'));
			
			let dmx_config = (config_tab_template.querySelector('[tag="dmx_config"]'));
			
			let remove_thistab = (config_tab_template.querySelector('[tag="remove_thistab"]'));
			
			
			channel_config.innerText = confData.channel;
			
			for(each of confData.dmx_config){
				let pods = make('span');
					pods.innerText = each.type + each.target;
					pods.title = this.getFeatureNameByChar(each.type);
					dmx_config.appendChild(pods);
			}
			
			remove_thistab.setAttribute("index", z);
			dmx_config.setAttribute("index", z);
			
			_("configs_tab_section").appendChild(config_tab_template);
			
			
		}
		
		
	},
	
	
	removeThistab: function(elm){
		let ownindex = elm.getAttribute("index");
		
		let confirmDelete = confirm("Are you sure to remove this item?");
		if(!confirmDelete){
			return;
		}
		configs.splice(ownindex,1);
		this.renderConfigs();
	},
	
	//helper function for position
	getFeatureNameByChar: function (char) {
		const featureMap = {
			c: "Color",
			p: "Position",
			f: "Fader",
			a: "Feature A",
			b: "Feature B",
			d: "Feature D",
			e: "Feature E",
			g: "Feature G",
			h: "Feature H",
			i: "Feature I",
			j: "Feature J",
			k: "Feature K"
		};

		return featureMap[char] || "Unknown Feature";
	},
		
		
	// ===============================	
	//Configurator Logic Goes Down here
	// ===============================	
	
	openConfigWindow: function(edit=false){
		_("configModal").showModal();
		_("configModal").addEventListener("cancel", this.closeDefaultModal);
		if(edit){
			this.mode = "edit";
		}else{
			this.mode = "add";
		}
		
	},
	
	openForEdit: function(elm){
		let index = elm.getAttribute("index");
		selectedIndex = index;
		let confData = configs[selectedIndex];
		
		this.clearAllEditTabs();
		for(each of confData.dmx_config){
			this.addConfigEdit(each);
		}
		
		this.openConfigWindow(true);
		_("conf_channel").value = confData.channel;
		
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
	
			
			
			
		}
		
				
		if(this.mode == "add"){
			configs.push(configData);
		}else{
			configs[selectedIndex] = configData;
		}
		
		_("conf_channel").value = 0;
		this.renderConfigs();
		this.clearAllEditTabs();
		this.closeModal();

		
	},
	
	
	
	// ===========================
	//Config Editor Section
	// ===========================
	
	
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


// DMX_CONF.openConfigWindow();

DMX_CONF.addConfigEdit();


// dummy 
DMX_CONF.renderConfigs();
