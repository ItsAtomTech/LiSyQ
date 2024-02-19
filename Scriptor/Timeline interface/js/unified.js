//Utilities
if(typeof _ === 'undefined') {
    window._ = function(elm) {
        return document.getElementById(elm);
    };
}

if(typeof make === 'undefined') {
    window.make = function(elm) {
        return document.createElement(elm);
    };
}

function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}


Object.prototype.clone = Array.prototype.clone = function() {
    const deepClone = (value) => {
        if (value === null || typeof value !== 'object') {
            return value;
        }

        if (typeof value === 'function') {
            return value;
        }

        if (Array.isArray(value)) {
            return value.map(deepClone);
        }

        return { ...value, ...Object.fromEntries(Object.entries(value).map(([key, val]) => [key, deepClone(val)])) };
    };

    return deepClone(this);
	// return {...this}
	
}

//Encode and Decoder
var for_rep = ["'",'"',",","/","\\","<","\n"];
var to_rep = ["&#39;","&#34;","&#44;","&#47;","&#92;","&lt;","&#13;"];
//Encode and Decoder end


function encode(st){var torep = st;torep = torep.replace(/'/gi, to_rep[0]);torep = torep.replace(/"/gi, to_rep[1]);torep = torep.replace(/,/gi, to_rep[2]);torep = torep.replace(/\n/gi, to_rep[6]);torep = torep.replace(/\//gi, to_rep[3]);torep = torep.replace(/\\/gi, to_rep[4]);torep = torep.replace(/\</gi, to_rep[5]);return torep;}function decode(sty){try{var torep = sty;torep = torep.replace(/&#39;/gi, for_rep[0]);torep = torep.replace(/&#34;/gi, for_rep[1]);torep = torep.replace(/&#44;/gi, for_rep[2]);torep = torep.replace(/&#47;/gi, for_rep[3]);torep = torep.replace(/&#92;/gi, for_rep[4]);torep = torep.replace(/&lt;/gi, for_rep[5]);torep = torep.replace(/&#13;/gi, for_rep[6]);return torep;}catch(e){return '';}}


var timeFormat = function(raw_time){
	if(raw_time < 60){
		return parseInt(raw_time)+"s"
	}
	else if(raw_time >= 60 && raw_time < 3600){
			if(raw_time%60 < 10){
				zero = "0";
			}else{
				zero = "";
			}
		return parseInt(raw_time/60)+":"+zero+parseInt(raw_time%60)+"";
	}else if(raw_time >= 3600){
		
		return parseInt((raw_time/60/60)%60) +":"+add_zero(parseInt((raw_time/60)%60))+":"+add_zero(parseInt(raw_time%60))+"";
		
	}
	
	
};

function add_zero(num){
	if(parseInt(num) < 10){	
		return "0"+num;
	}else{
		return num;
	}
}

 
function decople_data(data){
	var decopled = JSON.parse(JSON.stringify(data));
	
	 return decopled;
}


function createSilentAudio (time, freq = 3000){
  const length = time * freq;
  const AudioContext = window.AudioContext || window.webkitAudioContext || window.mozAudioContext;
  if(! AudioContext ){
    console.log("No Audio Context")
  }
	  
	  function bufferToWave(abuffer, len) {
	  let numOfChan = abuffer.numberOfChannels,
		length = len * numOfChan * 2 + 44,
		buffer = new ArrayBuffer(length),
		view = new DataView(buffer),
		channels = [], i, sample,
		offset = 0,
		pos = 0;

	  // write WAVE header
	  setUint32(0x46464952);
	  setUint32(length - 8);
	  setUint32(0x45564157);

	  setUint32(0x20746d66);
	  setUint32(16);
	  setUint16(1);
	  setUint16(numOfChan);
	  setUint32(abuffer.sampleRate);
	  setUint32(abuffer.sampleRate * 2 * numOfChan);
	  setUint16(numOfChan * 2);
	  setUint16(16);

	  setUint32(0x61746164);
	  setUint32(length - pos - 4);

	  // write interleaved data
	  for(i = 0; i < abuffer.numberOfChannels; i++)
		channels.push(abuffer.getChannelData(i));

	  while(pos < length) {
		for(i = 0; i < numOfChan; i++) {             // interleave channels
		  sample = Math.max(-1, Math.min(1, channels[i][offset])); // clamp
		  sample = (0.5 + sample < 0 ? sample * 32768 : sample * 32767)|0; // scale to 16-bit signed int
		  view.setInt16(pos, sample, true);          // write 16-bit sample
		  pos += 2;
		}
		offset++                                     // next source sample
	  }

	  // create Blob
	  return new Blob([buffer], {type: "audio/wav"});

	  function setUint16(data) {
		view.setUint16(pos, data, true);
		pos += 2;
	  }

	  function setUint32(data) {
		view.setUint32(pos, data, true);
		pos += 4;
	  }
	}
	  
  const context = new AudioContext();
  const audioFile = context.createBuffer(1, length, freq);
  return URL.createObjectURL(bufferToWave(audioFile, length));
}





// Utilities End


// Host Object functions
//Helper for file picker that get's file path of selected file

function openFileSelector(){
	try{	
		window.chrome.webview.hostObjects.NativeObject.Open_FileDirectory();	
        
	}catch(e){
        console.warn("Host Object not present! Aborted");
	}
}




//Generated Outputs
var output = [[],[]];

var time_delay = 0;
var not_empty = false;


function play_on_current(customTime=undefined){//Plays through all track and content blocks at current time
	not_empty = false;

	let timeDelta = (typeof customTime !== 'undefined') ? customTime : time;

	let timeFixed = parseInt(timeDelta);

	if(playing == true){	
		time_delay = parseInt(delay);
		// console.log(delay);
	}else{
		time_delay = 0;
	}


	//Loop through tracks
	for(tr = 0;tr < timeline_data.length;tr++){
		try{
			var port_chan = 0;
			
			if(timeline_data[tr].port_channel != undefined){
				port_chan = timeline_data[tr].port_channel;
				
			}
			
	
			let ch_track;
			
			if(timeline_data[tr].target_channel != undefined){
				ch_track = timeline_data[tr].target_channel;
			}else{	
				ch_track = tr;
			}

			//Puts Default Value from track if no content at current time index and not muted
			if(timeline_data[tr].muted != true){
				try{	
					output[port_chan][ch_track] = timeline_data[tr].default_value;			
				}catch(e){
					output[port_chan] = [];
					output[port_chan][ch_track] = timeline_data[tr].default_value;	}
			}
		

			for(str = 0;str < timeline_data[tr].sub_tracks.length;str++){
					var subtracks = timeline_data[tr].sub_tracks[str];
				

				
				try{
					if(subtracks.start_at  + time_delay <= timeDelta && timeDelta <= subtracks.end_at  + time_delay){
						
						// console.log("Track #"+ tr +", content block #"+ str +" : "+ time + "\n content_index: " + (time - subtracks.start_at));

						
						to_output(ch_track,port_chan,timeline_data[tr].sub_tracks[str].data[parseInt((timeFixed -  time_delay) - subtracks.start_at)],timeline_data[tr].default_value,tr);
						
						not_empty = true;

						
					}else{
						
						// Something in the future
						
					}
				}catch(e){
					console.log(e);
				}
				
			}
		
	
		
	}catch(e){

	}	
		
		
	}

	if(not_empty == false){
		
		send_to_port();
		
	}else{
		send_to_port();
	}
	
}



function to_output(tr,chp,df,def,tr_id){
	
	
	//chp is for channel_port for port selected for track
	not_empty = true;
		
		var channel_port = validate_number(chp);
		
		//provide index for the port_channel array if not present
		if(output[channel_port] == undefined){
			output[channel_port] = [];		
		}
		
		if(tr_id != undefined){
			if(timeline_data[tr_id].muted == true){
				return;
			}
		}

		
		
		if(df == undefined || df ==  ""){
			//set to default value if no data
			
			output[chp][tr] = def;			
		}else{
			//set to value 
			output[chp][tr] = df;	
		}
	

	
}



function send_to_port(){//This should send the output to the configured port to Arduino

	 
		try{
			//Separate each port channel by " | "
			sendPort(0,output.join("|").toString());
		}catch(e){
			
			
		} 
		
		//console.log(output.length);
		
		output.length = 0;	
	
}
	
let SeeDataFlow;
	
function sendPort(pch,str){


	try{
		window.chrome.webview.hostObjects.NativeObject.set_values(pch, str);
		window.chrome.webview.hostObjects.NativeObject.outputs();
	}catch(e){
	//
	}
	
	if(SeeDataFlow){
		console.log(str);
	}
	
	
	//Clear output buffer array after sending to all ports
	try{
		output[pch].length = 0;
	}catch(e){
		//
		
	}
}


let outputValues;
async function getOutputValues(){
	try{
		window.chrome.webview.hostObjects.NativeObject.get_values()	
		.then(resultx => {
			// Do your enchanting magic with the resultx here
			
		let splits = resultx.split('|');
		let multiDimensionalArray = [];

		for (let i = 0; i < splits.length; i++) {
			multiDimensionalArray.push(splits[i].split(','));
		}			
		outputValues = multiDimensionalArray;
			
		})
		.catch(error => {
			console.log(error);
			// Handle any storms or troubles here
		});
			
	}catch(e){
		console.log("Native not found");
	}
}

//helper function for opening file from gevin path, returns the string text
async function openFilePath(path) {
    return new Promise((resolve, reject) => {
        try {
            window.chrome.webview.hostObjects.NativeObject.open_filePath(path)
                .then(resultx => {
                    // Do your enchanting magic with the resultx here
                    let fileData = resultx;
                    resolve(fileData);
                })
                .catch(error => {
                    console.log(error);
                    reject(error);
                });
        } catch (e) {
            console.log("Native not found");
            reject(null);
        }
    });
}

		
async function show_loading(total,current,info){

	if(info == undefined || info == null){
		info = "";
	}
	
	let pr = parseInt(((current/total) * 100));
		
		console.log("Progress: ", pr);
		
	try{
		window.chrome.webview.hostObjects.NativeObject.set_progress(pr, info);
		window.chrome.webview.hostObjects.NativeObject.show_progress();
	}catch(e){
	//
	
	
	
	}

}
			
//Hides the Progress window if shown			
function finish_loading(){

	try{
		window.chrome.webview.hostObjects.NativeObject.set_progress(100, "Done");
		window.chrome.webview.hostObjects.NativeObject.close_progress();
	}catch(e){
	//
	}

}




