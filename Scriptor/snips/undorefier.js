//Start of Undo Redo
	
var undo_stack = [];
var redo_stack = [];


function add_undo(data){
	if(data == undefined){
		return console.warn("Trying to add undefined to undo stack");
	}
	undo_stack.push(data);
	//clears the redo data after an undo entry is added to the stack
	redo_stack.length = 0;
	
}

	
function get_undo(){
	
	if(undo_stack.length <= 0){
		console.log("No undo item left");
		return;
	}

	var undo_data = undo_stack.pop();
	redo_stack.push(undo_data);

	return undo_data;
}
 
 
 
 //Re-do logic
 
function get_redo(data){
	if(redo_stack.length <= 0){
		console.log("No redo item left");
		return;
	}

	var redo_data = redo_stack.pop();
	//add the data to the undo stack after redo
	undo_stack.push(redo_data);

	return redo_data;
}

 //End of Undo Redo
 