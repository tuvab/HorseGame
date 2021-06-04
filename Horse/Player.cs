using System.Drawing;
using System;

namespace Horse
{
    public class Player
    {
        public bool playerAlive = true;
        private int playerHP = 100;
        public int playerDMG = 50;
        public int playerPoints = 0;
        public int playerSpeed = 20;
        public int healAmount = 20;

        public void DisplayPlayerStats()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("PLAYER:");
            Console.WriteLine("HP = " + playerHP);
            Console.WriteLine("DMG = " + playerDMG);
            Console.WriteLine("Speed = " + playerSpeed);
            Console.WriteLine("Heal Power = " + healAmount);
            Console.WriteLine("Points = " + playerPoints);
            Console.ResetColor();
        }
        public bool InjurePlayer(int damage)
        {
            playerHP -= damage;
            if (playerHP > 0)
            {
                playerAlive = true;
            }
            else
            {
                playerAlive = false;
            }
            return playerAlive;
        }

        public bool HealPlayer(int amount)
        {
            if (playerHP > 0)
            {
                playerHP += amount;
                playerAlive = true;
            }
            else
            {
                playerAlive = false;
            }
            return playerAlive;
        }
    }
}