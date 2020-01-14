using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeTry
{

    class Snake
    {
        int length;
        List<Square> body;
        Location location;
        int width;
        public Direction Direction { get; set; }

        public Snake(int width, Location l, int length)
        {
            this.width = width;
            this.location = l;
            this.length = length;
            body = new List<Square>();
        }

        public void DrawSnake()
        {
            for(int i = 0; i < length; i++)
            {

                body.Add(new Square(location));
                    
            }

            for (int i = 0; i < body.Count-1; i++)
                for (int j = 1; j < body.Count; j++)
                    if (body[j].Location.x == body[i].Location.x &&
                        body[j].Location.y == body[i].Location.y)
                    {
                        body[j].Location = new Location() { x = body[i].Location.x + width, y = body[i].Location.y };
                    }

            for (int i = 0; i < body.Count; i++)
            {                
                body[i].DrawSquare();
                
            }
        }

        public void MoveSnake(Location l)
        {
            Location[] oldLocations = new Location[body.Count];

            for(int i = 0; i < oldLocations.Length; i++)
            {
                oldLocations[i] = body[i].Location;
            }

            Location oldLocation = body[0].Location;
            Location newLocation = new Location()
            {
                x = oldLocation.x + l.x,
                y = oldLocation.y + l.y
            };
            for(int i = 0; i < body.Count; i++)
            {
                Square square = body[i];             
                if (i == 0) MoveBodyPart((Location)newLocation, ref square);
                else MoveBodyPart(oldLocations[i - 1],ref square);
            }
        }     

        public void MoveBodyPart(Location nextLocation,ref Square squareToMove)
        {
            Square.ClearBody(squareToMove.Location);
            squareToMove.Location = nextLocation;
            squareToMove.DrawSquare();
        }

        public bool IsDead(int leftLimit, int rightLimit, int topLimit, int bottomLimit)
        {
            if (body[0].Location.x <= leftLimit ||
                body[0].Location.x >= rightLimit ||
                body[0].Location.y <= topLimit ||
                body[0].Location.y >= bottomLimit)
                return true;

            //for(int i = 0; i < body.Count; i++)
            //{                
                for(int j = 1; j < body.Count; j++)
                {
                    if (body[0].Location.x < body[j].Location.x + 2 &&
                        body[0].Location.x + 2 > body[j].Location.x &&
                        body[0].Location.y < body[j].Location.y + 1 &&
                        body[0].Location.y + 1 > body[j].Location.y)
                        return true;
                }
            //}

            return false;
        }

        public Square GetHead()
        {
            return body[0];
        }

        public void EatFood(ref Food f)
        {
            body.Add(new Square(new Location()
            {
                x = body[body.Count - 1].Location.x + width,
                y = body[body.Count - 1].Location.y
            }));
            Square.ClearBody(f.Location);
            Console.SetCursorPosition(body[0].Location.x, body[0].Location.y);
            Console.Write("██");
            f = null;
            GC.Collect();
        }

    }
}
