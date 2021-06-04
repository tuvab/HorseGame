using System.Drawing;
using System;

namespace Horse
{
    public class Horse
    {
        protected int horseHP = 100;
        public int horseDMG = 25;
        public int horseSpeed = 40;
        public bool horseAlive = true;
        public string horseAppearance = "None";
        public string horseType = "Normal";
        public int horsePoints = 1;


        public void DisplayHorseStats(int whichHorse)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("HORSE " + whichHorse + ":");
            Console.WriteLine("Horse Type = " + horseType);
            Console.WriteLine("Appearance = " + horseAppearance);
            Console.WriteLine("HP = " + horseHP);
            Console.WriteLine("DMG = " + horseDMG);
            Console.WriteLine("Speed = " + horseSpeed);
            Console.WriteLine("Points = " + horsePoints);
            // Console.WriteLine("Alive = " + horseAlive);
            Console.ResetColor();
        }
        public bool InjureHorse(int damage)
        {
            horseHP -= damage;
            if (horseHP > 0)
            {
                horseAlive = true;
            }
            else
            {
                horseAlive = false;
            }
            return horseAlive;
        }

        public bool HealHorse(int amount)
        {
            if (horseHP > 0)
            {
                horseHP += amount;
                horseAlive = true;
            }
            else
            {
                horseAlive = false;
            }
            return horseAlive;
        }

        public virtual void GiveSpeed()
        {
            Random generator = new Random();
            int randomSpeed = generator.Next(10, 30);

            horseSpeed = randomSpeed;
        }

        public virtual void GiveAppearance() // om det är en speciell häst får den annat utseende
        {
            string colour = "Colour";
            string pattern = "Pattern";

            Random generator = new Random();
            int colourRandom = generator.Next(1, 6); // 1-5
            int patternRandom = generator.Next(1, 5); // 1-4

            if (colourRandom == 1)
            {
                colour = "Brown";
            }
            else if (colourRandom == 2)
            {
                colour = "White";
            }
            else if (colourRandom == 3)
            {
                colour = "Black";
            }
            else if (colourRandom == 4)
            {
                colour = "Gray";
            }
            else if (colourRandom == 5)
            {
                colour = "Beige";
            }
            else
            {
                colour = "Invisible";         // inte menat att komma hit
            }


            if (patternRandom == 1)
            {
                pattern = "Spots";
            }
            else if (patternRandom == 2)
            {
                pattern = "Stripes";
            }
            else if (patternRandom == 3)
            {
                pattern = "Socks";
            }
            else if (patternRandom == 4)
            {
                pattern = "a Blaze";
            }
            else if (patternRandom == 5)
            {
                pattern = "Dots";
            }
            else
            {
                pattern = "non-existent Stripes";         // inte menat att komma hit
            }


            horseAppearance = colour + " with " + pattern;

        }

    }
}
