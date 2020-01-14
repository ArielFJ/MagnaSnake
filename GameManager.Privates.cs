using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace SnakeTry
{
    partial class GameManager
    {

        bool canSnakeMove = true;


        void DrawUpperRectangleRow()
        {
            for (int i = 0; i < size.Width; i++)
            {
                if (i > horSpacing && i < size.Width - horSpacing)
                {
                    Console.SetCursorPosition(i, verSpacing);
                    Console.Write(HORIZONTAL_BAR);
                }
            }
        }

        void DrawLowerRectangleRow()
        {
            for (int i = 0; i < size.Width; i++)
            {
                if (i > horSpacing && i < size.Width - horSpacing)
                {
                    Console.SetCursorPosition(i, size.Height - verSpacing);
                    Console.Write(HORIZONTAL_BAR);
                }
            }
        }

        void DrawLeftRectangleColumn()
        {
            for (int i = 0; i < size.Height; i++)
            {
                if (i > verSpacing && i < size.Height - verSpacing)
                {
                    Console.SetCursorPosition(horSpacing, i);
                    Console.Write(VERTICAL_BAR);
                }
            }
        }

        void DrawRightRectangleColumn()
        {
            for (int i = 0; i < size.Height; i++)
            {
                if (i > verSpacing && i < size.Height - verSpacing)
                {
                    Console.SetCursorPosition(size.Width - horSpacing, i);
                    Console.Write(VERTICAL_BAR);
                }
            }
        }

        void DrawCorners()
        {
            Console.SetCursorPosition(horSpacing, verSpacing);
            Console.Write(UPPER_LEFT_CORNER);

            Console.SetCursorPosition(horSpacing, size.Height - verSpacing);
            Console.Write(LOWER_LEFT_CORNER);

            Console.SetCursorPosition(size.Width - horSpacing, verSpacing);
            Console.Write(UPPER_RIGHT_CORNER);

            Console.SetCursorPosition(size.Width - horSpacing, size.Height - verSpacing);
            Console.Write(LOWER_RIGHT_CORNER);
        }

        private bool AcceptInput()
        {
            if (!Console.KeyAvailable)
                return false;

            lastKey = Console.ReadKey(true);

            return true;
        }

        private void DetectDirection(ConsoleKeyInfo key)
        {
            CheckSnakeXY();
            
            if (verX)
            {
                switch (key.Key)
                    {
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            snakeX = 0;
                            snakeY = 1;
                        break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            snakeX = 0;
                            snakeY = -1;
                        break;
                        default: break;
                    }
            }
            if (verY)
            {
                switch (key.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        snakeX = -2;
                        snakeY = 0;                    
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        snakeX = 2;
                        snakeY = 0;
                        break;
                    default: break;
                }
                
            }
            CheckSnakeXY();
        }

        private void CheckSnakeXY()
        {
            if (snakeX != 0) verX = true;
            else verX = false;

            if (snakeY != 0) verY = true;
            else verY = false;
        }

        private bool CheckSnakeMovement(int score, out int Score)
        {
            if (AcceptInput() && canSnakeMove)
            {
                DetectDirection(lastKey);
                canSnakeMove = false;
            }

            if (seconds >= .3f)
            {
                snake.MoveSnake(new Location() { x = snakeX, y = snakeY });

                if (currentFood.Collision(snake.GetHead()))
                {
                    snake.EatFood(ref currentFood);
                    SpawnFood();
                    score += 1;
                    
                }

                if (snake.IsDead(horSpacing, size.Width - horSpacing, verSpacing, size.Height - verSpacing))
                {
                    Score = score;
                    return true;
                }

                seconds = 0;
                canSnakeMove = true;
            }
            Score = score;
            return false;
        }

        private void SpawnFood()
        {
            Food f = new Food(new Location()
            {
                x = r.Next(horSpacing + 1, size.Width - horSpacing - 1),
                y = r.Next(verSpacing + 1, size.Height - verSpacing - 1)
            });
            currentFood = f;
        }


        void ShowPlayAgainScreen(ref bool isPlayable, int maxScore)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write("Dou you want play again?(y/n)     ");
            string res = Console.ReadLine();
            switch (res.ToLower())
            {
                case "y":
                    Console.Clear();
                    Program.Main(null);
                    break;
                case "n":
                    SerializeMaxScore(maxScore);
                    ShowCredits();
                    isPlayable = false;
                    break;
                default: ShowPlayAgainScreen(ref isPlayable, maxScore); break;
            }
        }

        void SerializeMaxScore(int maxScore)
        {
            using(Stream output = File.OpenWrite(serializationPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, maxScore);
            }
        }

        void ShowCredits()
        {
            string name = @"___  ___                                              
|  \/  |                                              
| .  . | __ _  __ _ _ __   __ _ _ __  _   _ _ __ ___  
| |\/| |/ _` |/ _` | '_ \ / _` | '_ \| | | | '_ ` _ \ 
| |  | | (_| | (_| | | | | (_| | | | | |_| | | | | | |
\_|  |_/\__,_|\__, |_| |_|\__,_|_| |_|\__, |_| |_| |_|
               __/ |                   __/ |          
              |___/                   |___/           ";


            Console.Clear();
            Console.WriteLine("Made by the awesome");
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(name);
            Console.ForegroundColor = color;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

    }
}
