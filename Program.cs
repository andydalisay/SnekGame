using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Media;

namespace Test_Game
{
   
    class Game
    {
        

        int minHeight = 20;
        int height = 40;
        int width;
        Vector currentPos;
        List<int[]> ghostPos = new List<int[]>();
        

        public void StartCode()
        {

            // to get current dimensions of the window
            height = Console.WindowHeight;
            width = Console.WindowWidth;


            Console.WriteLine("||===========================================================||");
            Console.WriteLine("||-----------------------------------------------------------||");
            Console.WriteLine("||---------------------Welcome to PekMen---------------------||");
            Console.WriteLine("||-----------------------------------------------------------||");
            Console.WriteLine("||===========================================================||");
            Console.WriteLine("||=================Press any key to start game===============||");
            Console.WriteLine("||<<<<<<<<<<<<<<<<<<<Press ECS to quit game>>>>>>>>>>>>>>>>>>||");

            currentPos = new Vector(width / 2, height / 2);

            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, minHeight);
                Console.Write("=");
            }


            for (int i = 1; i < width; i++)
            {
                Console.SetCursorPosition(i, height);
                Console.Write("=");
            }
            for (int j = 21; j < height - 1; j += 2)
            {
                for (int i = 1; i < width; i += 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.SetCursorPosition(i, j);
                    Console.Write("o");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            // User chooses the difficulty in get number of obstacles and this returns the 
            // number of obstacles needed into the obstacle generating method.
            Obstacles(getNumberOfObstacles());

            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                drawHero(currentPos.x, currentPos.y, "<");
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        moveHero("up");
                        break;
                    case ConsoleKey.RightArrow:
                        moveHero("right");
                        break;
                    case ConsoleKey.DownArrow:
                        moveHero("down");
                        break;
                    case ConsoleKey.LeftArrow:
                        moveHero("left");
                        break;
                    case ConsoleKey.P:
                        Console.SetCursorPosition(5, 15);
                        Console.WriteLine("PAUSED (PECMAN IS HUNGRY)");
                        moveHero("stop");
                        Console.SetCursorPosition(5, 15);
                        Console.WriteLine("                           ");
                        break;

                }

                System.Threading.Thread.Sleep(10);
                drawHero(currentPos.x, currentPos.y, "}");

                System.Threading.Thread.Sleep(10);
            }

        }

        // Returns number of obstacles.
        public int getNumberOfObstacles()
        {
            Console.SetCursorPosition(5, 12);
            Console.WriteLine("Choose a difficulty:");
            Console.WriteLine("1. Easy (:()");
            Console.WriteLine("2. Medium :) ");
            Console.WriteLine("3. Hard :D ");
            Console.WriteLine("4. Very hard :DD");
            Console.WriteLine("5. Impossible :O ");

            while (true)
            {
                // Gets input.
                string input = Console.ReadLine();

                switch (input) {
                    case "1":
                        return 50;

                    case "2":
                        return 100;

                    case "3":
                        return 250;

                    case "4":
                        return 500;

                    case "5":
                        return 10000;

                    case "6":
                        ConsoleColor[] rainbow = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.White,
                                                   ConsoleColor.Blue, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta};

                        String[] letters = { "Y", "o", "u", " ", "w", "i", "n" };

                        for (int i = 0; i < letters.Length; i++)
                        {
                            Console.ForegroundColor = rainbow[i];
                            Console.Write(letters[i]);
                        }

                        Console.ReadLine();
                        Environment.Exit(0);
                        break;
                }
            }
            
        }

        // Generates obstacles randomly around.
        public void Obstacles(int number)
        {
            Random randomGenerator = new Random();
            int randomX;
            int randomY;

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < number; i++)
            {
                randomX = randomGenerator.Next(1, width - 1);
                randomY = randomGenerator.Next(minHeight + 1, height - 1);
                Console.SetCursorPosition(randomX, randomY);
                ghostPos.Add(new int[] { randomX, randomY });
           
                Console.Write("M");
            }

            Console.ForegroundColor = ConsoleColor.White;

        }

        public void CheckCollision()
        {
            if (ghostPos.Any(p => p.SequenceEqual(new int[] { currentPos.x, currentPos.y })))
            {
                Console.SetCursorPosition(5, 14);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(@"  .-_'''-.      ____   ,---.    ,---.    .-''-.     ");
                Console.WriteLine(@" '_( )_   \   .'  __ `.|    \  /    |  .'_ _   \   ");
                Console.WriteLine(@"|(_ o _)|  ' /   '  \  \  ,  \/  ,  | / ( ` )   '  ");
                Console.WriteLine(@". (_,_)/___| |___|  /  |  |\_   /|  |. (_ o _)  |  ");
                Console.WriteLine(@"|  |  .-----.   _.-`   |  _( )_/ |  ||  (_,_)___| ");
                Console.WriteLine(@"'  \  '-   .'.'   _    | (_ o _) |  |'  \   .---.  ");
                Console.WriteLine(@" \  `-'`   | |  _( )_  |  (_,_)  |  | \  `-'    / ");
                Console.WriteLine(@"  \        / \ (_ o _) /  |      |  |  \       /  ");
                Console.WriteLine(@"   `'-...-'   '.(_,_).''--'      '--'   `'-..-'  ");
                Console.WriteLine("                                                  ");
                Console.WriteLine(@"     ,-----.    ,---.  ,---.   .-''-.  .-------.   ");
                Console.WriteLine(@"   .'  .-,  '.  |   /  |   | .'_ _   \ |  _ _   \   ");
                Console.WriteLine(@"  / ,-.|  \ _ \ |  |   |  .'/ ( ` )   '| ( ' )  |   ");
                Console.WriteLine(@" ;  \  '_ /  | :|  | _ |  |. (_ o _)  ||(_ o _) /   ");
                Console.WriteLine(@" |  _`,/ \ _/  ||  _( )_  ||  (_,_)___|| (_,_).' __ ");
                Console.WriteLine(@" : (  '\_/ \   ;\ (_ o._) /'  \   .---.|  |\ \  |  | ");
                Console.WriteLine(@" \ ` /  \  ) /   \ (_, _) / \  `-'    /|  | \ `'  / ");
                Console.WriteLine(@"   '. \_/``'.'    \     /    \       / |  |  \    /  ");
                Console.WriteLine(@"     '-----'       `---`      `'-..-'  ''-'   `'-' ");


               Console.ReadLine();
               Environment.Exit(0);
               
            }
            else
            {
                Console.SetCursorPosition(5, 14);
                Console.WriteLine(":)");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void moveHero(string direction)
        {
            bool curlyShape = false;
            while (!Console.KeyAvailable)
            {
                CheckCollision();

                switch (direction)
                {
                    case "right":
                        if (currentPos.x < width)
                        {
                            drawHero(currentPos.x, currentPos.y, " ");
                            currentPos.x++;

                            if (curlyShape)
                            {
                                drawHero(currentPos.x, currentPos.y, "{");
                            }
                            else
                            {
                                drawHero(currentPos.x, currentPos.y, "<");

                            }
                        }
                        break;

                    case "left":
                        if (currentPos.x > 0)
                        {
                            drawHero(currentPos.x, currentPos.y, " ");
                            currentPos.x--;

                            if (curlyShape)
                            {
                                drawHero(currentPos.x, currentPos.y, "}");
                            }
                            else
                            {
                                drawHero(currentPos.x, currentPos.y, ">");
                            }
                        }
                        break;

                    case "up":
                        if (currentPos.y > minHeight + 1)
                        {
                            drawHero(currentPos.x, currentPos.y, " ");
                            currentPos.y--;

                            if (curlyShape)
                            {
                                drawHero(currentPos.x, currentPos.y, "l");
                            }
                            else
                            {
                                drawHero(currentPos.x, currentPos.y, "v");
                            }
                        }
                        break;

                    case "down":
                        if (currentPos.y < height - 1)
                        {
                            drawHero(currentPos.x, currentPos.y, " ");
                            currentPos.y++;

                            if (curlyShape)
                            {
                                drawHero(currentPos.x, currentPos.y, "A");
                            }
                            else
                            {
                                drawHero(currentPos.x, currentPos.y, "^");
                            }
                        }
                        break;

                    case "stop":
                        break;
                }
                curlyShape = !curlyShape;

                System.Threading.Thread.Sleep(200);
            }
        }

        public static void drawHero(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }


        private class Vector
        {
            public int x = 0;
            public int y = 0;

            public Vector(int xInput, int yInput)
            {
                x = xInput;
                y = yInput;
            }
        }
    }
    class Program
    {


        static void Main(string[] args)
        {
            Game game = new Game();
            Console.SetWindowSize(70, 70);
            game.StartCode();


        }

    }


}