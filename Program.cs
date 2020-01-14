using System;

namespace SnakeTry
{
    internal class Program
    {
        const int WIDTH = 100;
        const int HEIGHT = 30;
        const int HORIZONTAL_SPACING = 3;
        const int VERTICAL_SPACING = 3;

        static int score = 0, maxScore = 0;

        public static void Main(string[] args)
        {
            GameManager manager = GameManager.GetInstance();

            ConsoleInit();            

            manager.SetParameters(new Size() { Width = WIDTH, Height = HEIGHT}, HORIZONTAL_SPACING, VERTICAL_SPACING);           
            manager.DrawRectangle();
            maxScore = manager.StartGame();

            bool isPlayable = true;
            while (isPlayable)
            {
                //if (Console.ReadKey(true).Key == ConsoleKey.Escape) Environment.Exit(0);
                manager.Update(ref isPlayable, ref score, maxScore);
                UpdateUI();
            }
            Environment.Exit(0);

        }

        static void ConsoleInit()
        {

            Console.SetWindowSize(WIDTH, HEIGHT);
            Console.SetBufferSize(WIDTH, HEIGHT);
            Console.Title = "Snake";
            UpdateUI();

            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(WIDTH / 2 - 20, 0);
            Console.Write(@"/ __| | \| |   /_\   | |/ / | __|");
            Console.SetCursorPosition(WIDTH / 2 - 20, 1);
            Console.Write(@"\__ \ | .` |  / _ \  | ' <  | _| ");
            Console.SetCursorPosition(WIDTH / 2 - 20, 2);
            Console.Write(@"|___/ |_|\_| /_/ \_\ |_|\_\ |___|");
            Console.ForegroundColor = color;
        }

        static void UpdateUI()
        {
            Console.SetCursorPosition(1, 1);
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("Score: {0}", score);
            Console.SetCursorPosition(WIDTH - 15, 1);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Max Score: {0}", maxScore);
            Console.ForegroundColor = color;
        }

    }
}