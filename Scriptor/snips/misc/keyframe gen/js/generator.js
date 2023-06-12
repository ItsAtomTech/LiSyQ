var key_frames = [];
var seqData = [];

var timelen = 6;


var time_constant = 33.33;

let sample_key_frames = [
{


	time_start:0,
	time_end:5,
	valueA: "fx:0",




},

{
	time_start:5,
	time_end:6,
	valueA: "fx:1",
}

];







function generateData() {

		let limit = timelen * time_constant;
		seqData.length = 0;

	for(i=0; i<= limit; i++) {

		seqData[i] = "";
		for(x=0; x < key_frames.length;x++){


				if (key_frames[x].time_start*time_constant <= i && key_frames[x].time_end*time_constant >= i){

					console.log(key_frames[x].valueA);

					seqData[i] = (key_frames[x].valueA);



				}
					
					
				

		}

			
	}
};



let ts = setInterval(playOn, time_constant);


var isPlaying = false;
var playTime = 0;

function play(){

	isPlaying = true;

}


function playOn(){
	if(isPlaying == true){

			var dataAt = seqData[playTime];
			console.log(dataAt);

			//console.log(playTime);

		playTime++;
			if(playTime >= time_constant*timelen){
				isPlaying = false;
				playTime = 0;

			}
	}

	



}