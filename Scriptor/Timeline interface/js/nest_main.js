
let vResizer1 = vertical_resizer.clone();
let vResizer2 = vertical_resizer.clone();
let hResizer1 = horizontal_resizer.clone();
let hResizer2 = horizontal_resizer.clone();


function inits(){
	
	vResizer1.min = 60;
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