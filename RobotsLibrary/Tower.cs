using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public class Tower : ITower
    {
        private ITower _tower;
        public void Shoot(Position position)
        {
            _tower.Shoot(position);
        }
    }
}
