using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlpacasWithBonnets
{
    public enum AlpacaType
    {
        Warrior,
        Wizard,
        Dunce
    }
    public class Character : MovingObject
    {
        /* Class for base methods that character and enemy will share
         * Include base movement and using an attack
         */ 
        //Author: Zoe McHenry

        //Doesn't look like anyone has done anything with this and I need it done so I'm gonna do it. -ZM
        //I'm changing this to a public instead of an abstract, as I'm changing the way the character loading works. -ZM

        private int health, power;
        private AlpacaType alpacaType;
        public int Health
        {
            get { return health; }
            set { this.health = value; }
        }
        public int Power
        {
            get { return this.power; }
            set { this.power = value; }
        }
        public AlpacaType AlpacaType
        {
            get { return this.alpacaType; }
        }

        public Character(int x, int y, int width, int height, int health, int power) : base(x, y, width, height)
        {
            this.health = health;
            this.power = power;
        }
    }
}
