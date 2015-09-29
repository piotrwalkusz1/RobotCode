using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotTech
{
    public class Tower
    {
        private volatile static ITower _tower;

        public Tower(string name)
        {
            _tower.Find(name);
        }

        public void Disable()
        {
            _tower.Disable();
        }
    }
}
