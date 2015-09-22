using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public class Position
    {
        public float x;
        public float y;

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Position GetRelativePosition(Position position)
        {
            return new Position(position.x - x, position.y - y);
        }
    }
}
