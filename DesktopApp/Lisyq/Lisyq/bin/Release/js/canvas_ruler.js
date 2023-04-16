var rulerHeight = 23;
var rulerWidth = get_size(document.body)[0];;

var canvas = document.getElementById("canvas_ruler");

canvas.setAttribute("width", rulerWidth);
canvas.setAttribute("height", rulerHeight);

function setupCanvas(canvas) {
  // Get the device pixel ratio, falling back to 1.
  var dpr = window.devicePixelRatio || 1;
  // Get the size of the canvas in CSS pixels.
  var rect = canvas.getBoundingClientRect();
  // Give the canvas pixel dimensions of their CSS
  // size * the device pixel ratio.
  canvas.width = rect.width * dpr;
  canvas.height = rect.height * dpr;
  canvas.style.width = rect.width + "px";
  canvas.style.height = rect.height + "px";
  var ctx = canvas.getContext("2d");
  // Scale all drawing operations by the dpr, so you
  // don't have to worry about the difference.
  ctx.scale(dpr, dpr);
  return ctx;
}

// ruler x
function addScaleLineText(ctx, startX, text) {
  var x = startX + 2;
  var y = 12;
  ctx.fillStyle = "white";
  ctx.font = "0.8em Arial";
  
  if(text/100 > 59 && text/100 <= 3600){
	  ctx.font = "0.7em Arial";
  }else if(text/100 > 3600){
	  ctx.font = "0.5em Arial";
	  
  }
  
  ctx.fillText(timeFormat(text/100), x, y);
}
function addUnitScaleLineX(ctx, startX, scaleFactor) {
  var unit = 10 * scaleFactor;
  // handle line blur
  let x = startX + 0.5;
  for (let i = 0; i <= 10; i++) {
    ctx.moveTo(x, 20);

    if (i === 0 || i === 10) {
      // full height
      ctx.lineTo(x, 0);
    } else if (i === 5) {
      // half height
      ctx.lineTo(x, 10);
    } else {
      // quarter height
      ctx.lineTo(x, 15);
    }

    x += unit;
  }
}

function addFullScaleLineX(options) {
  var {
    ctx,
    addUnitScaleLineX,
    startX,
    endX,
    scaleFactor,
    rulerUnit,
    startDialNumber
  } = options;
  var unit = rulerUnit * scaleFactor;
  var loopLength = Math.ceil(endX / unit);

  let x = startX;
  let dialNumber = startDialNumber;
  for (let i = 0; i <= loopLength; i++) {
    addUnitScaleLineX(ctx, x, scaleFactor);
    addScaleLineText(ctx, x, dialNumber.toString());
    x += unit;
    dialNumber += rulerUnit;
  }
}

// 计算 startX, startDialNumber 值
function calculateStartX(
  zeroScaleLinePosX = 0,
  scaleFactor = 1,
  rulerUnit = 100
) {
  var scaledRulerUnit = rulerUnit * scaleFactor;
  var remainder = zeroScaleLinePosX % scaledRulerUnit;
  let startX = 0;
  if (remainder > 0) {
    startX = remainder - scaledRulerUnit;
  } else {
    startX = remainder;
  }
  return startX;
}

function calculateStartDialNumber(
  zeroScaleLinePosX = 0,
  scaleFactor = 1,
  rulerUnit = 100
) {
  var scaledRulerUnit = rulerUnit * scaleFactor;
  var delta = Math.ceil(zeroScaleLinePosX / scaledRulerUnit) * rulerUnit;
  var startDialNumber = 0 - delta;
  return startDialNumber;
}

function paint_ruler() {
  ctx.lineWidth = 1;
  ctx.strokeStyle = "#d1d1d1";
  ctx.beginPath();
  ctx.moveTo(0, rulerHeight - 0.5);
  ctx.lineTo(ctx.canvas.width, rulerHeight - 0.5);
  
  scaledFactor = scaleFactor * zoom_scale;//zoom_scale is located on timeline js

  addFullScaleLineX({
    ctx: ctx,
    addUnitScaleLineX: addUnitScaleLineX,
    startX: calculateStartX(zeroScaleLinePosX, scaledFactor),
    endX: rulerWidth,
    scaleFactor: scaledFactor,
    rulerUnit: 100,
    startDialNumber: calculateStartDialNumber(zeroScaleLinePosX, scaledFactor)
  });

  ctx.stroke();
  
  
  
  
}

function clear_ruler() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);
}

// 2x2 * 2x1
function multiply(out, a, b) {
  let a0 = a[0],
    a1 = a[1],
    a2 = a[2],
    a3 = a[3];

  let b0 = b[0],
    b1 = b[1];

  out[0] = a0 * b0 + a2 * b1;
  out[1] = a1 * b0 + a3 * b1;
  return out;
}

function add(out, a, b) {
  out[0] = a[0] + b[0];
  out[1] = a[1] + b[1];
  return out;
}

function subtract(out, a, b) {
  out[0] = a[0] - b[0];
  out[1] = a[1] - b[1];
  return out;
}

// ruler y 可由 ruler x 135° 镜像翻转
// Now this line will be the same size on the page
// but will look sharper on high-DPI devices!

// var ctx = setupCanvas(canvas);
var ctx = canvas.getContext("2d");
var scaleDelta = 0;
var offsetDelta = 66;

var zeroScaleLinePosX = -0;
let scaleFactor = 0.3333;
let curOriginPointX;
let curZeroScaleLinePosX = zeroScaleLinePosX;

paint_ruler();


