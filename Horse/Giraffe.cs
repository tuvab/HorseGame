using System.Drawing;
using System;

namespace Horse
{
    public class Giraffe : Horse
    {
        public Giraffe()
        {
            horseHP = 150;
            horseAppearance = "Yellow with Spots";
            horseType = "Long Neck Horse";
            horsePoints = 2;
        }

        public override void GiveAppearance()  // inte ärver från horse så att den inte slumpas
        {
            
        }

    }
}