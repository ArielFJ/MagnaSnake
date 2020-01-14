using System;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace SnakeTry
{
    partial class GameManager
    {
        Random r = new Random();
        Food currentFood;
        private static GameManager instance;

        string serializationPath = @"D:\Documents\C# Scripts\SnakeTry\SnakeTry\bin\Debug\maxScore.dat";

        System.Timers.Timer timer = new System.Timers.Timer();

        const char UPPER_RIGHT_CORNER = '╗';
        const char LOWER_RIGHT_CORNER = '╝';
        const char LOWER_LEFT_CORNER = '╚';
        const char UPPER_LEFT_CORNER = '╔';
        const char VERTICAL_BAR = '║';
        const char HORIZONTAL_BAR = '═';

        Size size;
        int horSpacing, verSpacing;
        public Snake snake;

        float seconds = 0;

        ConsoleKeyInfo lastKey;
        int snakeX = -2, snakeY = 0;

        bool verX = false, verY = false; 

        private GameManager()
        {
            Console.CursorVisible = false;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 100;
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            seconds += .1f;
        }

        public static GameManager GetInstance()
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }

        public int StartGame()
        {
            int score = 0;
            if (File.Exists(serializationPath))
            {
                using(Stream input = File.OpenRead(serializationPath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    score = (int)formatter.Deserialize(input);
                }
            }

            snake = new Snake(2,
                            new Location()
                            {
                                x = ((size.Width / 10) / 2) * 10 ,
                                y = ((size.Height / 10) / 2) * 10 
                            },
                            3);
            snake.DrawSnake();
            SpawnFood();
            timer.Enabled = true;
            return score;
        }


        public void Update(ref bool isPlayable, ref int score, int maxScore)
        {

            bool Finish = CheckSnakeMovement(score, out score);
            if(score > maxScore)
            {
                maxScore = score;
            }

            if (Finish)
            {
                score = 0;
                ShowPlayAgainScreen(ref isPlayable, maxScore);
            }

        }       

        public void SetParameters(Size size, int horizontalSpacing, int verticalSpacing)
        {
            this.size = size;
            this.horSpacing = horizontalSpacing;
            this.verSpacing = verticalSpacing;           
        }

        public void DrawRectangle()
        {
            DrawCorners();
            DrawLeftRectangleColumn();
            DrawLowerRectangleRow();
            DrawRightRectangleColumn();
            DrawUpperRectangleRow();
        }

        
    }
}
