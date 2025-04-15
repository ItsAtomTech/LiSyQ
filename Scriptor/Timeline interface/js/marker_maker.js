//This Script is dedicated for markers management on timelines




const Marker_Maker = {
	//Adds a marker to timelines, specify a timeline object to put into supplied timeline object
	mUUID: generateUUID8(),
	objectID: "Marker_Maker", //Should match the Variable name given;
	addToWindow: function(){window[this.objectID] = this},
	markerPools: undefined,
	
	addMarker: function(time_target=undefined,silent=false,targetTimeline=undefined){
		let target = targetTimeline ? targetTimeline : markers; //defaults to main timeline markers
		
		let targetTime = time_target ? time_target : time;//time to where we insert the marker 
		let marker = this.makeMarkerObject();
		
		if(target == undefined){
			target = [];
		}
	
		marker.time = time_target;
		
		
		target.push(marker);
		
		// console.log(target, targetTime,marker);
		
		
		
		
	},




//render the markers on a timeline or other container
	renderMarkers: function(silent=true,target=undefined,type="main",pool=undefined){
		let targetEl = target ? target : timeline_container;
		let marks = pool ? pool : markers;
		
		for(each of marks){
					
					
			if(_(each.id)){ //just update it if its already created
				let markElement = _(each.id);
				let headerM = markElement.getElementsByClassName("marker_head_top")[0];
					markElement.style.borderLeftColor = each.color;
					markElement.style.transform = "translateX(calc(" + (each.time + 7) + "px * var(--scale)))";
					markElement.title = each.name;
				
				headerM.innerText = each.name;
				headerM.style.borderColor = each.color;
				
				silent ? false :console.log(each.id + " was updated");
				continue;
			}
					
					
			let devMarker = make("div");
			devMarker.classList.add("marker_element");
			devMarker.id = each.id;
			devMarker.style.borderLeftColor = each.color;
			devMarker.style.transform = "translateX(calc(" + (each.time + 7) + "px * var(--scale)))";

			devMarker.title = each.name;

			let marker_header = make("div");
				marker_header.classList.add("marker_head_top", "tiny");
				marker_header.innerText = each.name;
				marker_header.style.borderColor = each.color;
				marker_header.setAttribute("onclick",this.objectID + ".editableMarker(this)");
				
			devMarker.appendChild(marker_header);
			targetEl.appendChild(devMarker);
			
			
		}
		
		//add window reference object after render
		if(window[this.objectID] == undefined){
			this.addToWindow();
			this.markerPools = marks;
		}
	},
	

	//Generate a marker object;
	makeMarkerObject: function(){
		let markerObject = {
			"name": "Unnamed Marker",
			"time": 0,
			"color": "#26b254",//default
			"id": "marker_"+this.mUUID +"_"+generateUUID8()+"_"+generateUUID8(),
		}
		return markerObject;
	},
	
	
	//Save name changes
	saveName: function(id,name="No name"){
		let markerParent = id;
		if(typeof(id) != "object"){
			markerParent = _(id);
		}
		for(each of this.markerPools){
			if(markerParent.id == each.id){
				each.name = name;
			}
		}
		markerParent.title = name;
	},
	
	
	//Helper functions
	editableMarker: function(elm){
		let currentName = elm.innerText;
		let objectID = this.objectID;
		let editMarkerNameElm = make("input");
			editMarkerNameElm.type = "text";
			editMarkerNameElm.classList.add("editing_marker");
			editMarkerNameElm.value = currentName;
			editMarkerNameElm.onblur = function() {
				currentName = event.target.value;
				elm.innerText = currentName;
				elm.setAttribute("onclick",objectID + ".editableMarker(this)");
				
				window[objectID].saveName(elm.parentElement, currentName);
				
			};
		elm.innerHTML = "";
		elm.setAttribute("onclick","");
		elm.appendChild(editMarkerNameElm);
		editMarkerNameElm.focus();
		
		
	}
	
	
}


function generateUUID8() {
    return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    ).slice(0, 8);
}


//dummy 
Marker_Maker.addMarker(135);
Marker_Maker.addMarker(168);
Marker_Maker.renderMarkers();

//To-Do: removing of marker
