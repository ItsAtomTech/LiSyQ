var player_seeked = false;
var nomedia = true;

(function localFileVideoPlayer() {
	
  var URL = window.URL || window.webkitURL

  var playSelectedFile = function (event) {
    var file = this.files[0]
    var type = file.type
    var videoNode = document.querySelector('video')
    var canPlay = videoNode.canPlayType(type)
    if (canPlay === '') canPlay = 'no'
   
    var isError = canPlay === 'no';


    if (isError) {
      return
    }

    var fileURL = URL.createObjectURL(file)
    videoNode.src = fileURL
  }
  var inputNode = _("media_link");
  
  inputNode.addEventListener('change', playSelectedFile, false)
})()


    


var this_vid = _("thisvid");

this_vid.onseeking = function() {
 //console.log(this_vid.currentTime);
 
 player_seeked = true;
 
 if(playing == false){
	time = parseInt(this_vid.currentTime * 33.33);
 }
}


this_vid.onplay = function(){
 // console.log(this_vid.paused);
 
 if(is_preview){
	
	playing = false;
	is_preview = false;
	return;
	 
 }
 
 if(playing == false){
  play();
 }

}


this_vid.onpause = function(){
   // console.log(this_vid.currentTime);
 pause();

}
this_vid.oncanplay = function(){
   // console.log(this_vid.currentTime);
 nomedia = false;

}



function load_media(){
	
	_("media_link").click();
	

}



