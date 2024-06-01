var time_per_pexil = 30; //1 pexil = 30... milisecond for this program
						//33 pexils = 1 sec.

var time = 0;
let playlistTime = 0;


//Play States
var playing = false;
var PLplaying = false;
var loop = false;
var is_preview = false;
var delay = 0;

var zoom_scale = 1;


var timeline_data = [];

var player_start = setInterval(rails, time_per_pexil);

let vResizer1 = vertical_resizer.clone();
let vResizer2 = vertical_resizer.clone();
let hResizer1 = horizontal_resizer.clone();
let hResizer2 = horizontal_resizer.clone();

let prefixRoot = '../';

let playlistData = [];
let playlistList = {
	'name': 'No Playlist Name',
	'playlists': [],
	
};

let currentPlayingIndex = null;

let plnomedia = true;

function inits(){
	
	vResizer1.min = 50;
	vResizer1.max = 75;
	vResizer1.initSize(_('v_rez1'));
	
	vResizer2.min = 40;
	vResizer2.max = 60;
	vResizer2.view1 = 'v2_con_1';
	vResizer2.view2 = 'v2_con_2';
	vResizer2.initSize(_('v_rez2'));
	
	
	hResizer1.min = 30;
	hResizer1.max = 45;
	hResizer1.initSize(_('h_rez1'));

	hResizer2.min = 30;
	hResizer2.max = 65;
	hResizer2.view1 = 'h2_con_1';
	hResizer2.view2 = 'h2_con_2';
	hResizer2.initSize(_('h_rez2'));



}

inits();


function get_size(elm){ //getting the size of an element	
	var refh = elm;
	var size = ['',''];

try{
	if(refh.offsetHeight){
		
		size[1] = refh.offsetHeight;
		size[0] = refh.offsetWidth;
		
	}else if(refh.style.pixelHeight){
		size[1] = refh.style.pixelHeight
		size[0] = refh.style.pixelWidth
		
	}
	
	}catch(e){
		console.log("Invalid Element");
	}
	return size;
}

async function loadToPlaylist(){
	
	let decodedData = JSON.parse(loadedFile);
	let timelineData = JSON.parse(decode(decodedData.timeline));
	let timelineOptions = decodedData.options ? JSON.parse(decode(decodedData.options)) : {};
	let maxEnd = undefined;
		
	if(loadmode == 'single'){
		maxEnd = regen_empty_data(timelineData);
	}

	
	let playitem = {
		'timeline': timelineData,
		'options': timelineOptions,
		'filePath': loadedFilePath,
		
	}
	
	if(loadmode == 'single'){
		playitem.maxTime = maxEnd;
	}else{
		stopPL();
		currentPlayingIndex = 0;
	}
	
	playlistData.push(playitem.clone());
	
	//refresh list on view
	generatePListView();
	loadmode = "";	
}

//Generate pl element for list
function genPLItem(name="No name",id=undefined){
	let plitem = make('div');
		plitem.setAttribute('class', 'list_item_pl');
		var plitem_DIV = make('div');
		let spanText = make("span");
			spanText.innerText = name;
		plitem_DIV.setAttribute('class', 'saved_name small');
		plitem_DIV.appendChild(spanText);
		plitem.appendChild(plitem_DIV);
		
		var plitem_DIV = make('div');
		plitem_DIV.setAttribute('class', 'saved_actions small');
			  var plitem_DIV_DIV = make('div');
			  
			  plitem_DIV_DIV.setAttribute('class', 'button_box button_xz');
			  plitem_DIV_DIV.setAttribute('onclick', 'playPLItem('+id+')');
			  plitem_DIV_DIV.appendChild(document.createTextNode(' Play '));
		plitem_DIV.appendChild(plitem_DIV_DIV);
		
			  var plitem_DIV_DIV = make('div');
			  plitem_DIV_DIV.setAttribute('class', 'button_box button_xz');
			  plitem_DIV_DIV.setAttribute('onclick', 'removePLItem('+id+')');
			  plitem_DIV_DIV.appendChild(document.createTextNode(' Remove '));
		plitem_DIV.appendChild(plitem_DIV_DIV);

			  var plitem_DIV_DIV = make('div');
			  plitem_DIV_DIV.setAttribute('class', 'button_box button_xz');
			  plitem_DIV_DIV.setAttribute('onclick', 'showPLOptions('+id+')');
			  plitem_DIV_DIV.appendChild(document.createTextNode(' ... '));
		plitem_DIV.appendChild(plitem_DIV_DIV);

		plitem.appendChild(plitem_DIV);

	return plitem;	

}


function generatePListView(){
	//Generate Playlist list view and data here

	let extId = 0;
		_("playlistView").innerHTML = "";
	for(each of playlistData){
		let fileName = replaceBackslashes(each.filePath).split("/");
			fileName = fileName[fileName.length - 1];
		let item = genPLItem(fileName, extId);
		
		if(currentPlayingIndex == extId){
			item.classList.add('currentPlaying');
		}
		
		_("playlistView").appendChild(item);
		extId++;
	}
	
}

let savingData;
async function generatePLSave() {
    return new Promise((resolve) => {
        let lists = playlistList.playlists;
        lists.length = 0;

        let extId = 0;
        for (each of playlistData) {
            let data = { 'filePath': each.filePath };
            lists.push(data);
            extId++;
        }

        savingData = JSON.stringify(playlistList);

        // Resolve the promise once everything is done
        resolve();
    });
}



// Regeneration Handler
function regen_empty_data(input_timeline_data=undefined) {
  let used_timeline_data = input_timeline_data || timeline_data;
  let max_time = 0;

  for (let tmsd = 0; tmsd < used_timeline_data.length; tmsd++) {
    for (let sbsd = 0; sbsd < used_timeline_data[tmsd].sub_tracks.length; sbsd++) {
		let dat = used_timeline_data[tmsd].sub_tracks[sbsd];
		let endingAt = dat.end_at;
		
		if(endingAt > max_time){
			max_time = endingAt;
		}
		
      if (dat.content_length != dat.data.length) {
        if (find_plug(dat.type) == undefined) {
          console.log("Plugin " + dat.type + " not installed!");
          return;
        }

        used_timeline_data[tmsd].sub_tracks[sbsd].data = JSON.parse(
          JSON.stringify(regen_from_plugin(find_plug(dat.type), used_timeline_data[tmsd].sub_tracks[sbsd].misc))
        );
      }
    }
  }
  
  return max_time; //returns the greatest end time found while regenerating each;
}



//End regen function here






// Loading Files Logic Start

let loadedFile = "";
let loadedFilePath = "";
let loadFilemode = "playlist";

//callback from the Native Host
function load_from_file(fileData, filePath){
	// console.log(fileData, filePath);
	loadedFile = fileData;
	loadedFilePath = filePath;
	
	if(loadFilemode == "playlist"){
		try{
			loadToPlaylist();
		}catch(e){
			console.log(e);
		}

	}else{
		//Load it to Somewhere
	}
	
}


// Load Lsys to playlist helper function
let loadmode;
function openLsysFilePL(){
	loadmode = 'single';
	loadFilemode = "playlist";
	openFile();
	
}


async function load_pl_from_file(data){
	//Load the Passed Data to Playlist
	let pl_data = JSON.parse(JSON.parse(decode(data)));
	let pl_len = pl_data.playlists.length;
	let pl_list = pl_data.playlists;	
	
	playlistData.length = 0;
	
	for(i = 0; i < pl_len;i++){
		show_loading(pl_len, i, "Loading Files...");
		let path = pl_list[i].filePath;
		try{
			let fContents = await openFilePath(replaceBackslashes(path));
			load_from_file(fContents,path);
		}catch(e){
			console.log("Failed loading: "+ path);
		}

	}
	console.log(pl_data);
	regenDatas();
	
	
}

async function regenDatas(){
	
	let pllen = playlistData.length;
	show_loading(pllen-1, 1, "Regenerating Timeline data...");
	for(z = 0; z < pllen; z++){
		show_loading(pllen-1, z, "Regenerating Timeline data...");
		let maxEnd = regen_empty_data(playlistData[z].timeline);
		playlistData[z].maxTime = maxEnd;
		
		await sleep(1);
	}
	
	finish_loading();	
}



// Native Bridge
function openFile(){
	try{
		window.chrome.webview.hostObjects.NativeObject.Open_File();
	}catch(e){
		//--
	}	
}

function OpenFilePL(){
	try{
		window.chrome.webview.hostObjects.NativeObject.Open_File_PL();
	}catch(e){
		//--
	}	
}



async function save_pl_to_file(){
	let sd_d = await generatePLSave();
	try{	
		var to_transfer = encode(JSON.stringify(savingData));	
		window.chrome.webview.hostObjects.NativeObject.put_pl_data(to_transfer);
	}catch(e){
		//-
	}
	command_save_pl();
}

function command_save_pl(){
		try{				
		window.chrome.webview.hostObjects.NativeObject.Save_File_PL();
		}catch(e){
		alert(e);				
	}
}


// Loading Files Logic End


// Media Player Logic

let plvid = _("thisvidpl");


plvid.oncanplay = function() {
	// console.log(this_vid.currentTime);
	plnomedia = false;
}

plvid.onerror = function() {
	// console.log(this_vid.currentTime);
	plnomedia = true;
}


plvid.onseeking = function() {
	//console.log(this_vid.currentTime);
	player_seeked = true;
	if (playing == false) {
		time = parseInt(plvid.currentTime * 33.33);
		setTimeDisplay(time+10, 'time_display_pl');
	}
}

plvid.onplay = function() {
	// console.log(this_vid.paused);
	if (is_preview) {
		playing = false;
		is_preview = false;
		return;
	}
	if (playing == false) {
		playPL(true);
	}
}

plvid.onpause = function() {
	// console.log(this_vid.currentTime);
	pausePL(true);
}

plvid.onended = function() {
	// console.log(this_vid.currentTime);
	plprocessonend()
}





//Playback for playlist

function playPL(from_player){
	PLplaying = true;
	
	if(from_player){
		return true;
	}
	
	if(plvid.paused ){	
		try{
			plvid.play();	
		}catch(e){
			//-
		}
	}
}

function pausePL(from_player){
	PLplaying = false;
	
	if(from_player){
		return true;
	}
	
	if(plvid.paused == false){
		try{
		plvid.pause();
		}catch(e){
			//-
		}
	}
}


function stopPL(){
	PLplaying = false;
	time = 0;
	
	//clearAllBuffer();
	
	
	plvid.currentTime = 0;
	plvid.pause();
	
}



function set_delay(val){	
	let local = localStorage.getItem('play_delay_plyls');	
	if(val == undefined||val == null){
		_("delay_disp").value = parseInt(local);
		let set = parseInt(local);					
			if(set.toString() == "NaN"){				
				set = 0;				
			}		
		delay = (set*0.001)*33.333;		
		optimizedData = false;		
		return;
	}else{				
		localStorage.setItem('play_delay_plyls',val);		
		if((val == "" || val == null) || val.length == 0){				
			localStorage.setItem('play_delay_plyls',0);			
		}			
		let set = parseInt(localStorage.getItem('play_delay_plyls'));		
		delay = (set*0.001)*33.333;		
		optimizedData = false;			
	}
}


function playPLItem(index=undefined){
	if(index==undefined){
		return false;
	}
	let maxL = playlistData.length;
	if(index >= maxL){
		index = 0;
	}else if(index < 0){
		index = maxL-1;
	}
	timeline_data = playlistData[index].timeline;
	let mediaLink = undefined;
	
	try{
		mediaLink = playlistData[index].options.link_file;
	}catch(e){
		//
	}
	currentPlayingIndex = index;
	
	time = 0;
	playPL();
	if(mediaLink != undefined){
		loadMediaFromPath(mediaLink, 'thisvidpl');
	}else{
		plvid.src = createSilentAudio(parseInt(playlistData[index].maxTime / 33.33));//create silent track if nothing found
		plnomedia = false;
	}
	generatePListView();
}

function removePLItem(index){	
	let flr = window.confirm("Are you sure to remove this?");
	if(!flr){
		return;
	}	
	playlistData.splice(index,1);
	if(index == currentPlayingIndex){
		playPLItem(index);
	}

	generatePListView();
	
	
}



function nextPL(){
	let next = currentPlayingIndex + 1;
	playPLItem(next);
}


function prevPL(){
	let prev = currentPlayingIndex - 1;
	playPLItem(prev);
}

let repeatPL = false;

//Loop Event for playing
let offsets = 0;
function rails(){
	

	
	// for Plalist Playing rail
	if(PLplaying == true){
		play_on_current();
		
	
		time++;
		
		if(offsets >= 3){
			setTimeDisplay(time+10, 'time_display_pl');
			offsets = 0;
		}else{
			offsets++;
		}
	
		
			if(time < parseInt((plvid.currentTime * 33.333) - 2) || time >  parseInt((plvid.currentTime * 33.333) + 2)){
			if(plnomedia == false && player_seeked == false){//only when media is playing
					time = parseInt(plvid.currentTime * 33.333);
				 plvid.play();
			}
		}
		
		player_seeked = false;
		
		try{
			let plEnd = playlistData[currentPlayingIndex].maxTime;
			if(time > plEnd){
				plprocessonend();
			}
		
		}catch(e){
			//--
		}

	}
	
}


function plprocessonend(){
	if(repeatPL && currentPlayingIndex >= playlistData.length - 1){
		nextPL();
	}else if(currentPlayingIndex < playlistData.length - 1){
		nextPL();
	}
}




// Timeline Logic

var prev_wi = 0;

function gen_ruler(){
	var width_ref = parseInt((_("timeline_container").scrollWidth)/((1000/30) ));
	var  rv = _("ruler_view");
	     // rv.addEventListener("mousedown",click_on_ruler);
	
	rv.style.width = (_("timeline_container").scrollWidth)+"px";
	
	_("timeline_container").addEventListener("scroll",move_ruler);
	
	try{
		rulerWidth = get_size(document.body)[0];
		canvas.setAttribute("width", rulerWidth);
		clear_ruler();
		paint_ruler();
	}catch(e){
		//--
		
	}
	

	if(prev_wi == width_ref){
		return;
	}
	
	prev_wi = width_ref;

	
}

var prev_scroll;
function move_ruler(){
	//var  rv = _("ruler_view");
	//rv.style.transform = "translateX(-"++"px)";
	
	zeroScaleLinePosX = (_("timeline_container").scrollLeft) * -1;
	clear_ruler();paint_ruler();
	
	var scr = _("timeline_container").scrollLeft;
	
	
	if(prev_scroll != _("timeline_container").scrollTop){
		// Do Something here
	}
	
	prev_scroll = _("timeline_container").scrollTop;
	
}


var fRuler = gen_ruler()//init the Ruler 
