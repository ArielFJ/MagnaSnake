using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeTry
{
    class Square
    {
        public Location Location { get; set; }

        const string SQUARE_FORM = "██";

        public Square(Location l)
        {         
            Location = l;
        }
       

        public void DrawSquare()
        {
            Console.SetCursorPosition(Location.x, Location.y);
            Console.Write(SQUARE_FORM);
        }

        public static void ClearBody(Location location)
        {
            Console.SetCursorPosition(location.x, location.y);
            Console.Write("  ");
        }
    }
}
