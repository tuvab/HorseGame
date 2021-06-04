using System.Drawing;
using System;

namespace Horse
{
    public class Unicorn : Horse
    {
        public Unicorn()
        {
            horseDMG = 40;
            horseSpeed = 40;
            horseAppearance = "White with a Horn";
            horseType = "Unicorn";
            horsePoints = 3;
        }

        public override void GiveAppearance()  // inte ärver från horse så att den inte slumpas
        {
            
        }

        public override void GiveSpeed()
        {
            
        }
    }
}