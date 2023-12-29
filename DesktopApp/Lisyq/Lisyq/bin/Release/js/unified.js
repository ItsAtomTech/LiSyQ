//Utilities
if(typeof _ === 'undefined') {
    window._ = function(elm) {
        return document.getElementById(elm);
    };
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



// Host Object functions

//Helper for file picker that get's file path of selected file

function openFileSelector(){
	try{	
		window.chrome.webview.hostObjects.NativeObject.Open_FileDirectory();	
        
	}catch(e){
        console.warn("Host Object not present! Aborted");
	}
}



