using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlpacasWithBonnets
{
    class Collectable
    {
        // Defining the Collectable object
        // Sara Nuffer

        // If it should appear or not
        private bool isColliding;

        // IsColliding Proporty
        public bool IsColliding
        {
            get { return isColliding; }
            set { isColliding = value; }
        }

        // Constructor
        public Collectable()
        {

        }
    }
}
