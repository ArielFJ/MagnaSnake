using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeTry
{
    class Food : Square
    {
        public Food(Location l) : base(l)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            DrawSquare();
            Console.ForegroundColor = color;
        }

        public bool Collision(Square other)
        {
            if (Location.x < other.Location.x + 2 &&
                Location.x + 2 > other.Location.x &&
                Location.y + 1 > other.Location.y &&
                Location.y < other.Location.y + 1)
                return true;
            return false;
        }

    }
}
