using System.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Horse
{
    class Program
    {
        public static List<Horse> horses = new List<Horse>();  //skapa listan horses, bestående av objekt av typen Horse
        public static void Main(string[] args)
        {
            Player player = new Player();
            string fullAnswer;      // användarinput
            string answer;          // trimmad användarinput (fullAnswer)
            int whichHorse = 0;     // används för att ge stats för en specifik häst
            int horseAmount = 0;
            bool testing = false;

            Console.Write("You don’t like horses, and they know that.");
            MakeItYellow(" Escape the ranch before they get you.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Press Enter to start the game ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("(or type 't' for testing)");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("]");
            Console.ResetColor();
            fullAnswer = Console.ReadLine();
            answer = fullAnswer.Trim();
            Console.Clear();

            if ((String.Compare(answer, "t", true) == 0) || (String.Compare(answer, "test", true) == 0) || (answer == "testing"))
            {
                testing = true;
            }

            bool noAnswer = true;
            while (noAnswer == true)
            {
                MakeItYellow("How many horses do you want to spawn?");
                Console.WriteLine();
                fullAnswer = Console.ReadLine();
                answer = fullAnswer.Trim();

                try
                {
                    horseAmount = Convert.ToInt32(answer);

                    if (horseAmount > 10)
                    {
                        MakeItYellow("[");
                        Console.Write("That's a bit much, don't you think?");
                        MakeItYellow(" (try with a smaller number)]");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else if (horseAmount < 1)
                    {
                        MakeItYellow("[");
                        Console.Write("You have to spawn some horses at least");
                        MakeItYellow(" (try with a bigger number)]");
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        noAnswer = false;
                    }
                }
                catch (FormatException)
                {
                    MakeItYellow("[Type a number]");
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            Console.Clear();
            Console.Write("Spawning " + horseAmount + " horses");
            System.Threading.Thread.Sleep(500);
            for (int i = 0; i < horseAmount; i++)     // gör "..."
            {
                System.Threading.Thread.Sleep(70);
                Console.Write(".");
                System.Threading.Thread.Sleep(70);
            }
            Console.Clear();

            for (int i = 0; i < horseAmount; i++)     //skapar instanser av Horse och lagra i listan horses
            {
                Random generator = new Random();
                int horseRandom = generator.Next(1, 6); // 1-5

                if (horseRandom == 1)
                {
                    horses.Add(new Unicorn());
                }
                else if (horseRandom == 2)
                {
                    horses.Add(new Giraffe());
                }
                else
                {
                    horses.Add(new Horse());
                }

                horses[i].GiveAppearance();    // ger hästen sitt utseende
                horses[i].GiveSpeed();
            }

            // för testning
            if (testing == true)
            {
                Console.WriteLine("Antal hästar skapade: " + horses.Count);

                for (int i = 0; i < horses.Count; i++)
                {
                    // Console.WriteLine("Horse " + i + " hp = " + horses[i].horseHP);    //skriv ut alla hästars hp
                    Console.WriteLine("Horse " + i + " type = " + horses[i].horseType);
                }

                horses[2].HealHorse(400);

                bool wrongAnswer = true;
                while (wrongAnswer == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Få stats på häst (nummer), eller avsluta (avsluta);");
                    fullAnswer = Console.ReadLine();
                    answer = fullAnswer.Trim();

                    try
                    {
                        whichHorse = Convert.ToInt32(answer);

                        if (whichHorse < horses.Count && whichHorse >= 0)
                        {
                            horses[whichHorse].DisplayHorseStats(whichHorse);
                        }
                        else
                        {
                            MakeItYellow("[Finns endast " + horses.Count + " skapade, första på 0]");
                        }
                    }
                    catch (FormatException)
                    {
                        if (answer == "avsluta")
                        {
                            wrongAnswer = false;
                        }
                        else
                        {
                            MakeItYellow("[Skriv ett nummer, eller 'avsluta']");
                        }
                    }
                }

            }
            // in-game
            else
            {
                for (int i = 0; i < horses.Count; i++)      // går igenom alla skapade hästar
                {
                    CatchingUp(i, player);
                    if (player.playerAlive == false)
                    {
                        break;
                    }
                }

                if (player.playerAlive == true)
                {
                    Console.Write("You have defeated all " + horseAmount + " horses, and have ");
                    MakeItYellow("escaped the ranch");
                    Console.WriteLine("!");
                }
                else
                {
                    Console.Write(horseAmount + " horses proved too much, as you");
                    MakeItYellow(" died");
                    Console.Write(" and ");
                    MakeItYellow("failed to escape the ranch");
                    Console.WriteLine("!");
                }
                Console.WriteLine();
                Console.WriteLine("You got " + player.playerPoints + " points.");
                Console.WriteLine();
                MakeItYellow("[Press Enter to Exit the Game]");
                Console.ReadLine();
            }


            static bool PlayerAttack(int horseNumber, Player player)        //returnerar true om hästen överlever, false om den är död
            {
                Console.WriteLine("[Horse " + horseNumber + " is attacked with damage " + player.playerDMG + "]");
                System.Threading.Thread.Sleep(500);
                if (Program.horses[horseNumber].horseAlive == false)
                {
                    Console.WriteLine("Horse is already dead, aborting.");   // om hästen redan är död så försöker den inte attackeras
                    return false;
                }
                if (horses[horseNumber].InjureHorse(player.playerDMG) == false)  //.injure returnerar false om hästen dör
                {
                    Console.WriteLine("Horse " + horseNumber + " died!");
                    player.playerPoints = player.playerPoints + horses[horseNumber].horsePoints;
                    System.Threading.Thread.Sleep(1200);
                    Console.Clear();
                    return false;
                }
                else
                {
                    return true;
                }
            }

            static bool HorseAttack(int horseNumber, Player player)     // tar hand om hästens tur att attackera player
            {
                Console.WriteLine("[Player is attacked with damage " + horses[horseNumber].horseDMG + "]");
                System.Threading.Thread.Sleep(1200);
                if (player.InjurePlayer(horses[horseNumber].horseDMG) == false)  //.injure gör skadan på player, funktionen returnerar false om player dör
                {
                    Console.WriteLine("Player died!");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    return false;
                }
                else
                {
                    return true;
                }
            }

            static void Fight(int horseNumber, Player player)       // tar hand om turordning
            {                                                               // horseNumber används på samma sätt som whichHorse
                string fullAnswer;
                string answer;
                bool wrongAnswer = false;

                while (player.playerAlive == true && horses[horseNumber].horseAlive == true)
                {
                    bool noAnswer = true;
                    while (noAnswer == true)
                    {
                        Console.Clear();
                        player.DisplayPlayerStats();
                        Console.WriteLine();
                        horses[horseNumber].DisplayHorseStats(horseNumber);
                        Console.WriteLine();
                        MakeItYellow("What do you want to do?");
                        Console.WriteLine();
                        Console.WriteLine("- Attack");
                        Console.WriteLine("- Heal");
                        if (wrongAnswer == true)
                        {
                            MakeItYellow("[Choose an action]");
                            Console.WriteLine();
                            wrongAnswer = false;
                        }
                        fullAnswer = Console.ReadLine();
                        answer = fullAnswer.Trim();

                        if ((String.Compare(answer, "attack", true) == 0) || (String.Compare(answer, "a", true) == 0) || (answer == "1"))
                        {
                            PlayerAttack(horseNumber, player);
                            noAnswer = false;
                        }
                        else if ((String.Compare(answer, "heal", true) == 0) || (String.Compare(answer, "h", true) == 0) || (answer == "2"))
                        {
                            Console.WriteLine("[Player is healed with amount " + player.healAmount + "]");
                            System.Threading.Thread.Sleep(500);
                            player.HealPlayer(player.healAmount);
                            noAnswer = false;
                        }
                        else
                        {
                            wrongAnswer = true;
                        }
                    }
                    if (horses[horseNumber].horseAlive == true)
                    {
                        HorseAttack(horseNumber, player);
                    }
                }
            }

            static void CatchingUp(int horseNumber, Player player)
            {
                Console.Write("Horse " + horseNumber + " is approaching");  // häst närmar sig player
                for (int i = 0; i < 3; i++)     // gör "..." efter "approaching"
                {
                    System.Threading.Thread.Sleep(250);     // för att den ska vänta innan när det är första gången
                    Console.Write(".");
                    System.Threading.Thread.Sleep(250);     // hälften för att det även finns innan
                }
                Console.WriteLine();
                Console.Write("(" + player.playerSpeed);
                if (player.playerSpeed > horses[horseNumber].horseSpeed)
                {
                    Console.Write(" > ");
                }
                else
                {
                    Console.Write(" < ");
                }
                Console.WriteLine(horses[horseNumber].horseSpeed + ")");
                if (player.playerSpeed < horses[horseNumber].horseSpeed)               // häst var snabbare och hann ikapp
                {
                    Console.Write("Horse " + horseNumber + " caught up to you,");
                    MakeItYellow(" you'll have to fight");
                    Console.WriteLine(".");
                    PressToContinue();
                    Fight(horseNumber, player);
                }
                else   // player var snabbare, interagerar inte med hästen
                {
                    Console.WriteLine("You managed to run away from horse " + horseNumber + ".");
                    PressToContinue();
                }
            }

            static void MakeItYellow(string colorThis)  //färglägger
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(colorThis);
                Console.ResetColor();
            }

            static void PressToContinue()
            {
                Console.WriteLine();
                MakeItYellow("[Press Enter to continue]");
                Console.ReadLine();
                Console.Clear();
            }

        }
    }
}
