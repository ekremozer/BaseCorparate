function changePos() {
    width = document.documentElement.clientWidth;
	height = document.documentElement.clientHeight;
	Hoffset = window.fakir.offsetHeight;
	Woffset = window.fakir.offsetWidth;
	leftPx = (xPos + document.body.scrollLeft) + "px";
	topPx = (yPos + document.body.scrollTop) + "px";
	window.fakir.style.left = leftPx;
	window.fakir.style.top = topPx;
    if (yon) {
        yPos = yPos + step;
    } else {
        yPos = yPos - step;
    }
    if (yPos < 0) {
        yon = 1;
        yPos = 0;
    }
    if (yPos >= height - Hoffset) {
        yon = 0;
        yPos = height - Hoffset;
    }
    if (xon) {
        xPos = xPos + step;
    } else {
        xPos = xPos - step;
    }
    if (xPos < 0) {
        xon = 1;
        xPos = 0;
    }
    if (xPos >= width - Woffset) {
        xon = 0;
        xPos = width - Woffset;
    }
}

function start() {
    interval = setInterval("changePos()", delay);
}

function pauseResume() {
    if (pause) {
        clearInterval(interval);
        pause = false;
    } else {
        interval = setInterval("changePos()", delay);
        pause = true;
    }
}
function fakirMessage(){
	var text = String.fromCodePoint(0x2764);
	var randomNumber = Math.floor(Math.random() * messages.length);
	alert("RE " + text);
}
var fakirDiv = document.createElement('div');
fakirDiv.style.cssText = 'width: 1px; height: 1px; position: absolute; z-index: 9999999; background: #fff;';
fakirDiv.setAttribute("id","fakir");
fakirDiv.setAttribute("onmouseover","fakirMessage()");
document.body.appendChild(fakirDiv);
var messages = ["1", "2", "3"];
var step = 1;
var delay = 30;
var height = 0;
var Hoffset = 0;
var Woffset = 0;
var yon = 0;
var xon = 0;
var pause = true;
var interval;
var leftPx = "0px";
var topPx = "0px";
var xPos = 20;
var yPos = document.documentElement.clientHeight;
start();