var player_seeked = false;
var nomedia = true;
let mediaPath = undefined;
(function localFileVideoPlayer() {
	var URL = window.URL || window.webkitURL
	var playSelectedFile = function(event) {
		var file = this.files[0]
		var type = file.type
		var videoNode = document.querySelector('video')
		var canPlay = videoNode.canPlayType(type)
		if (canPlay === '') canPlay = 'no'
		var isError = canPlay === 'no';
		var path = (window.URL || window.webkitURL).createObjectURL(file);
		console.log('path', path);
		if (isError) {
			return
		}
		var fileURL = URL.createObjectURL(file)
		videoNode.src = fileURL
	}
	var inputNode = _("media_link");
	inputNode.addEventListener('change', playSelectedFile, false)
})();

var this_vid = _("thisvid");
this_vid.onseeking = function() {
	//console.log(this_vid.currentTime);
	player_seeked = true;
	if (playing == false) {
		time = parseInt(this_vid.currentTime * 33.33);
	}
}

this_vid.onplay = function() {
	// console.log(this_vid.paused);
	if (is_preview) {
		playing = false;
		is_preview = false;
		return;
	}
	if (playing == false) {
		play(true);
	}
}

this_vid.onpause = function() {
	// console.log(this_vid.currentTime);
	pause(true);
}

this_vid.oncanplay = function() {
	// console.log(this_vid.currentTime);
	nomedia = false;
}

function load_media() {
	_("media_link").click();
}

//Helper_function for loading media using string path
function loadMediaFromPath(path, mediaTarget = undefined) {
	path = replaceBackslashes(path);
	if (mediaTarget) {
		_(mediaTarget).src = path;
		return;
	}
	_('thisvid').src = path;
}
//Called from native side, sets mediaPath by native means, since not possible on web env. 
function setMediaPath(path) {
	mediaPath = path;
	linkMediaFile();
	return mediaPath;
}


function select_link(){
	openFileSelector();
}

//helper for Automatic media linked loading
function loadLinkedMedia(data){
	let pth = data.link_file
	loadMediaFromPath(pth);
}



//Called from native side (from setMediaPath f) to set linked file
function linkMediaFile() {
	if (!mediaPath) {
		alert('Path is Invalid');
		return;
	}
	if (typeof fileOptions != 'object') {
		fileOptions = {};
	}
	fileOptions.link_file = mediaPath;
	loadMediaFromPath(mediaPath);
}

function replaceBackslashes(inputString) {
	if (typeof inputString !== 'string') {
		// Ensure we're dealing with a string
		return inputString;
	}
	// Use a regular expression to replace all occurrences of \
	const replacedString = inputString.replace(/\\/g, '/');
	return replacedString;
}