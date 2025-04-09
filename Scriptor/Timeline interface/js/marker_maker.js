//This Script is dedicated for markers management on timelines




const Marker_Maker = {
	//Adds a marker to timelines, specify a timeline object to put into supplied timeline object
	mUUID: generateUUID8(),
	
	addMarker: function(time_target=undefined,silent=false,targetTimeline=undefined){
		let target = targetTimeline ? targetTimeline : markers; //defaults to main timeline markers
		
		let targetTime = time_target ? time_target : time;//time to where we insert the marker 
		let marker = this.makeMarkerObject();
		
		if(target == undefined){
			target = [];
		}
	
		marker.time = time_target;
		
		
		target.push(marker);
		
		console.log(target, targetTime,marker);
		
		
		
		
	},




//render the markers on a timeline or other container
	renderMarkers: function(silent=true,target=undefined,type="main",pool=undefined){
		let targetEl = target ? target : timeline_container;
		let marks = pool ? pool : markers;
		
		for(each of marks){
					
			let devMarker = make("div");
			devMarker.classList.add("marker_element");
			devMarker.id = each.id;
			devMarker.style.borderLeftColor = each.color;
			devMarker.style.transform = "translateX(calc(" + (each.time + 6) + "px * var(--scale)))";

			devMarker.title = each.name;

			let marker_header = make("div");
				marker_header.classList.add("marker_head_top", "tiny");
				marker_header.innerText = each.name;
				marker_header.style.borderColor = each.color;


			devMarker.appendChild(marker_header);
			targetEl.appendChild(devMarker);
			
			
		}
		

		
		
		
		
	},
	

	//Generate a marker object;
	makeMarkerObject: function(){
		let markerObject = {
			"name": "Unamed Marker",
			"time": 0,
			"color": "#26b254",//default
			"id": "marker_"+this.mUUID +"_"+generateUUID8()+"_"+generateUUID8(),
			
		}
		
		return markerObject;
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

//To-Do: Implement Renaming of marker, removing of marker
