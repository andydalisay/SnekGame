//Canvas stuff
var canvas = new Object();
canvas.element = document.getElementById('canvas');
canvas.context = canvas.element.getContext('2d');
canvas.width = canvas.element.getAttribute('width');
canvas.height = canvas.element.getAttribute('height');
canvas.cellWidth = 10;
canvas.context.font = "7px Press Start 2P";

//some useful variables about the playarea
var cw = 10; 				//width of the cells
var maxx = canvas.width/cw; //the maximum allowed position on the grid
var maxy = canvas.height/cw;//the maximum allowed position on the grid

//these variables represent the state of the game
var d;			//direction of the snake (right, left, up, down)
var food;		//the cell representing the current item of food, has a food.x and food.y position
var score;		//the players current score
var snake_array; //an array of cells to make up the snake, each have an x and y position
var portalA;
var portalB;

var highScore = 0;

//set it so the init function is called when the page is loaded
//window.onload=init;

	function init()
	{
		d = "right"; //default direction

		create_snake(); //build and show the snake
		create_food(); 	//build and show the first food item
		
		//initialise the score
		score = 0;
		
		//Lets move the snake now using a timer which will trigger 
		//the paint function every 60ms
		if(typeof game_loop != "undefined") clearInterval(game_loop);
		game_loop = setInterval(paint, 100);
	}
	
	//Lets paint the snake now
	function paint()
	{
		//To avoid the snake trail we need to paint the background on every frame
		//Lets paint the canvas 
		canvas.context.fillStyle = "white";
		canvas.context.fillRect(0, 0, canvas.width, canvas.height);
		canvas.context.strokeStyle = "black";
		canvas.context.strokeRect(0, 0, canvas.width, canvas.height);
		
		//update the snakes position based on the current direction
		update_snake(d);
		
		//Lets paint the snake	
		for(var i = 0; i < snake_array.length; i++)
		{
			var c = snake_array[i];
			//Lets paint 10px wide cells
			paint_cell(c.x, c.y);
		}
		
		//Lets paint the food
		paint_cell(food.x, food.y);
		
		//Lets paint the score
		var score_text = "Score: " + score;
		canvas.context.fillText(score_text, 5, canvas.height-5);
		
		
	}
	
	//a generic function to paint cells
	function paint_cell(x, y)
	{
		
		//canvas.context.fillStyle = "green";
		//canvas.context.fillRect(x*cw, y*cw, cw, cw);
		//canvas.context.strokeStyle = "red";
		canvas.context.lineWidth = 1000;
		//canvas.context.strokeRect(x*cw, y*cw, cw, cw);
		
		var my_gradient = canvas.context.createLinearGradient(0, 0, 300, 300);
		my_gradient.addColorStop(0, "pink");
		my_gradient.addColorStop(0.2, "red");
		my_gradient.addColorStop(0.4, "yellow");
		my_gradient.addColorStop(0.6, "green");
		my_gradient.addColorStop(0.8, "blue");
		my_gradient.addColorStop(1, "magenta");
		canvas.context.fillStyle = my_gradient;
		canvas.context.fillRect(x*cw, y*cw, cw, cw);
		
		portalA = {x: 12, y: 5};
		portalB = {x: 23, y: 21};
		
		canvas.context.fillRect(portalA.x*cw, portalA.y*cw, 2.5, 10);
		canvas.context.fillRect(portalA.x*cw, portalA.y*cw, 10, 2.5);
		canvas.context.fillRect(portalA.x*cw+9, portalA.y*cw, 2.5, 11);
		canvas.context.fillRect(portalA.x*cw, portalA.y*cw +9, 11, 2.5);
		canvas.context.fillRect(portalB.x*cw, portalB.y*cw, 10, 2.5);
		canvas.context.fillRect(portalB.x*cw, portalB.y*cw, 2.5, 10);
		canvas.context.fillRect(portalB.x*cw+9, portalB.y*cw, 2.5, 12);
		canvas.context.fillRect(portalB.x*cw, portalB.y*cw +9, 12, 2.5);
		
		portalC = {x: 5, y: 17};
		portalD = {x: 20, y: 8};
		
		canvas.context.fillRect(portalC.x*cw, portalC.y*cw, 2.5, 10);
		canvas.context.fillRect(portalC.x*cw, portalC.y*cw, 10, 2.5);
		canvas.context.fillRect(portalC.x*cw+9, portalC.y*cw, 2.5, 11);
		canvas.context.fillRect(portalC.x*cw, portalC.y*cw +9, 11, 2.5);
		canvas.context.fillRect(portalD.x*cw, portalD.y*cw, 2.5, 10);
		canvas.context.fillRect(portalD.x*cw, portalD.y*cw, 10, 2.5);
		canvas.context.fillRect(portalD.x*cw+9, portalD.y*cw, 2.5, 11);
		canvas.context.fillRect(portalD.x*cw, portalD.y*cw +9, 11, 2.5);
		
		portalE = {x: 8, y: 23};
		portalF = {x: 15, y: 15};
		
		canvas.context.fillRect(portalE.x*cw, portalE.y*cw, 2.5, 10);
		canvas.context.fillRect(portalE.x*cw, portalE.y*cw, 10, 2.5);
		canvas.context.fillRect(portalE.x*cw+9, portalE.y*cw, 2.5, 11);
		canvas.context.fillRect(portalE.x*cw, portalE.y*cw +9, 11, 2.5);
		canvas.context.fillRect(portalF.x*cw, portalF.y*cw, 2.5, 10);
		canvas.context.fillRect(portalF.x*cw, portalF.y*cw, 10, 2.5);
		canvas.context.fillRect(portalF.x*cw+9, portalF.y*cw, 2.5, 11);
		canvas.context.fillRect(portalF.x*cw, portalF.y*cw +9, 11, 2.5);
	}