using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;

namespace Lab2_2910_ReburnK
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string filePath = projectFolder + Path.DirectorySeparatorChar + "videogames.csv";

            List<VideoGames> games = new List<VideoGames>();

            using (var sr = new StreamReader(filePath))
            {

                string fileHeader = sr.ReadLine();


                while (!sr.EndOfStream)
                {

                    string? line = sr.ReadLine();


                    string[] lineElements = line.Split(',');


                    VideoGames v = new VideoGames()
                    {
                        Name = lineElements[0],
                        Platform = lineElements[1],
                        Year = Int32.Parse(lineElements[2]),
                        Genre = lineElements[3],
                        Publisher = lineElements[4],
                        NA_Sales = Double.Parse(lineElements[5]),
                        EU_Sales = Double.Parse(lineElements[6]),
                        JP_Sales = Double.Parse(lineElements[7]),
                        Other_Sales = Double.Parse(lineElements[8]),
                        Global_Sales = Double.Parse(lineElements[9]),


                    };

                    games.Add(v); //this will be for when i put them into any kind of actual list
                }

            } // end using

            ///////////////////////////////////////////////////////////////////// that's all adding my items to the list

            //games.Sort(); I'd prefer them not sorted

            Stack<VideoGames> gameStack = new Stack<VideoGames>();

            Queue<VideoGames> gameQueue = new Queue<VideoGames>();

            int gameStashTaken = 0;

            //int endingCount = 0;
            //I wouldve liked to have the program restart from the top, but where you don't need to actually re-run it, so the ending counter would be dynamic
            //like an achievement system
            ///////////////////////////////Things have now been initialized

            Intro();

            //Console.WriteLine("You enter the house........right? (Y/N)");
            char cowardOption = Console.ReadLine().ToUpper()[0];


            while (cowardOption != 'N' && cowardOption != 'Y')
            {
                Console.WriteLine("\nI know you can do better than this. Try again.\n");
                cowardOption = Console.ReadLine().ToUpper()[0];
            }

            if (cowardOption == 'N')
            {
                CowardEnding();

                Console.ReadLine();

                Stats(gameQueue);
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
                Environment.Exit(0);

            }


            Console.WriteLine("\nOkay, great. Moving on.");
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("When you enter the property, you see all kinds of games 'displayed'.");
            Console.WriteLine("Conveniently they're sorted by platform in piles on the floor.");
            Console.Write("\nWhat game platform stash will you search first? The biggest piles are for PSV, XOne, PS4, DS, and 3DS. ");
            bool loop = true;
            string platformInput = Console.ReadLine();
            

            while (loop)
            {
                int stupidCounter = 0;

                while (platformInput != "PSV" && platformInput != "XOne" && platformInput != "PS4" && platformInput != "DS" && platformInput != "3DS")
                {
                    stupidCounter++;

                    Console.WriteLine("\nYou've never seen something quite like this before, but every other platform's games are unusable!");
                    Console.WriteLine("You'll never be able to get money *or* enjoyment out of broken disks.");
                    Console.WriteLine("Maybe you should check out one of the other five piles instead?\n");

                    if (stupidCounter >= 3)
                    {
                        Console.WriteLine("Seriously, just pick PlayStation 4 or 5, or Xbox One, or a 3DS/DS I'm begging you.\n");                        
                    }


                    platformInput = Console.ReadLine();

                }



                Console.Clear();
                //timer.Wait();

                Console.WriteLine($"You head over to the {platformInput} pile.");

                var theStack = games.Where(x => x.Platform == platformInput);
                foreach (var game in theStack) gameStack.Push(game); 
                //currently, if you go into a platform, take a game, exit, and come back in, it'll have a duplicate :(

                Console.Write($"\nWould you like to take some games from the {platformInput} pile? (Y/N) ");
                char takeGames = Console.ReadLine().ToUpper()[0];

                while (takeGames != 'N' && takeGames != 'Y')
                {
                    Console.WriteLine("\nI know you can do better than this. Try again.\n");
                    takeGames = Console.ReadLine().ToUpper()[0];
                }

                while (takeGames == 'Y')
                {
                    Console.WriteLine("\nYou stick your hand into the middle of the pile.");
                    Console.WriteLine("When you pull it out, you see you picked: ");
                    Console.WriteLine($"\n{gameStack.Peek()}");

                    Console.WriteLine("Would you like to take this game? (Y/N)");
                    char keepGame = Console.ReadLine().ToUpper()[0];

                    while (keepGame != 'N' && keepGame != 'Y')
                    {
                        Console.WriteLine("\nI know you can do better than this. Try again.\n");
                        keepGame = Console.ReadLine().ToUpper()[0];
                    }

                    if (keepGame == 'Y')
                    {
                        gameQueue.Enqueue(gameStack.Peek());

                        Console.WriteLine("----------------- TOOK ITEM -----------------");
                        gameStack.Pop();                       

                        gameStashTaken++;
                    }
                    else if (keepGame == 'N')
                    {
                        gameStack.Pop();
                    }

                    if(gameStashTaken == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nYou've been looking around for a while, maybe you should be careful.\n");
                        Console.ResetColor();
                    }

                    if (gameStashTaken == 6)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nYou're starting to get greedy...maybe this is enough for now?\n");
                        Console.ResetColor();
                    }

                    if (gameStashTaken == 10)
                    {
                        Console.WriteLine("\nWell, you've gone this far. Surely no one will catch you if they haven't already...");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Right...?");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nWould you like to keep looking? (Y/N) ");
                    takeGames = Console.ReadLine().ToUpper()[0];

                    Console.Clear();

                }// end of while loop

                Console.Clear();

                Console.WriteLine("Would you like to continue searching? (Y/N)");
                char endSearch = Console.ReadLine().ToUpper()[0];

                while (endSearch != 'N' && endSearch != 'Y')
                {
                    Console.WriteLine("\nI know you can do better than this. Try again.\n");
                    endSearch = Console.ReadLine().ToUpper()[0];
                }

                if (endSearch == 'N')
                {
                    loop = false;
                    
                }
                else if(endSearch == 'Y')
                {
                    Console.Write("\nWhat game platform stash will you search in? The biggest piles are for PSV, XOne, PS4, DS, and 3DS. ");
                    platformInput = Console.ReadLine();
                    stupidCounter = 0;
                }
                
            }//end of entire while loop

            Console.Clear();

            if(gameStashTaken == 0)
            {
                PickyEnding();
                Stats(gameQueue);
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            
            if(gameStashTaken >= 10)
            {
                Console.WriteLine("You decided you found plenty of games, at least for this sweep of the place.");
                Console.WriteLine("You left the building, and made you way off the property, as quickly as possible.");
                Console.WriteLine("\n*THUNK*\n");
                Console.WriteLine("You looked behind you at the sound.\nOh no! Your little burgler bag broke open at the bottom!\n");

                Console.WriteLine(gameQueue.Peek());

                Console.WriteLine("\nOne of your precious games was lost. Do you pick it up? (Y/N) ");
                char finalChoice = Console.ReadLine().ToUpper()[0];

                while (finalChoice != 'N' && finalChoice != 'Y')
                {
                    Console.WriteLine("\nThis one's important. Try again.\n");
                    finalChoice = Console.ReadLine().ToUpper()[0];
                }

                if (finalChoice == 'Y')
                {
                    Queue<VideoGames> singleGame = new Queue<VideoGames> ();
                    singleGame.Enqueue (gameQueue.Peek ());

                    GreedyEnding();
                    Stats(singleGame);
                    Console.WriteLine("Press ENTER to exit.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (finalChoice == 'N')
                {
                    gameQueue.Dequeue(); //bc you just lost a book
                    Console.WriteLine("Just one game isn't worth getting caught, you left it on the ground and continued to exit.");
                    Console.WriteLine("You suddenly saw bright lights heading toward what you left behind, good thing you didn't grab that, huh?");
                    Console.WriteLine("\n*THUNK*\n");
                    Console.ReadLine();
                    gameQueue.Dequeue();

                    Console.WriteLine("You chose to ignore that you continue to drop games. Maybe you should've fixed your bag.");
                    Console.WriteLine("\n*THUNK*\n");
                    Console.ReadLine();
                    gameQueue.Dequeue();

                    Console.WriteLine("At this point maybe a new bag is in order.");
                    Console.WriteLine("\n*THUNK*\n");
                    Console.ReadLine();
                    gameQueue.Dequeue();
                    gameQueue.Dequeue();
                    gameQueue.Dequeue();

                    DroppedLoot();
                    Stats(gameQueue);
                    FinalStatsSuccess(gameQueue);
                    Console.WriteLine("Press ENTER to exit.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

            }

            Queue<VideoGames> nintendoTime = new Queue<VideoGames>();

            var nintendoCheck = gameQueue.Where(x => x.Publisher == "Nintendo");
            foreach (var x in nintendoCheck) nintendoTime.Enqueue(x);

            if (nintendoTime.Count == gameQueue.Count)
            {
                NintendoEnding();
                Stats(gameQueue);
                FinalStatsSuccess(gameQueue);
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }
            else
            {
                SuccessEnding();
                Stats(gameQueue);
                FinalStatsSuccess(gameQueue);
                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }

            
         


        }//end of main

        public static Task timer = Task.Run(async delegate
        {
            await Task.Delay(1000);
        }); //tried and failed to use. Oh well

        public static void FinalStatsSuccess(Queue<VideoGames> gameQueue)
        {
            //these will show as your official stash at the end of a successful run, only w ending 4, 5 or 6 where you escape w games

            Dictionary<string, int> platformDictionary = new Dictionary<string, int>();

            var dsGames = gameQueue.Where(x => x.Platform == "DS"); int numDSStash = dsGames.Count();
            var threeDSGames = gameQueue.Where(x => x.Platform == "3DS"); int num3dsStash = threeDSGames.Count();
            var xOneGames = gameQueue.Where(x => x.Platform == "XOne"); int numXOneStash = xOneGames.Count();
            var ps4Games = gameQueue.Where(x => x.Platform == "PS4"); int numps4Stash = ps4Games.Count();
            var ps5Games = gameQueue.Where(x => x.Platform == "PS5"); int numps5Stash = ps5Games.Count();

            platformDictionary.Add("DS", numDSStash);
            platformDictionary.Add("3DS", num3dsStash);
            platformDictionary.Add("XOne", numXOneStash);
            platformDictionary.Add("PS4", numps4Stash);
            platformDictionary.Add("PS5", numps5Stash);

            Console.WriteLine("***----------------------------------------***");
            Console.WriteLine("Games successfully taken: ");
            foreach (var x in platformDictionary)
            {
                Console.WriteLine($"\n{x.Key}: {x.Value}");
            }
            Console.WriteLine("***----------------------------------------***");

        }
        public static void Intro()
        {
            
            Console.Write("A billionaire with the largest video game collection in the world has recently passed, naming ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("you ");
            Console.ResetColor();
            Console.Write("the sole beneficiary \nof his prized stash.");
            Console.WriteLine("\n...At least, that's what you told the authorities.\n");

            Console.WriteLine("You enter the house........right? (Y/N)");
           
        }

        public static void Stats(Queue<VideoGames> gameQueue)
        {
            Console.WriteLine($"*You took {gameQueue.Count} games*");
            Console.WriteLine("---------------- YOUR STASH: ----------------\n");

            foreach (VideoGames v in gameQueue)
            {
                Console.WriteLine($"{v}\n");
            }

            Console.WriteLine("---------------------------------------------\n");

            

        }

        public static void CowardEnding()
        {
            Console.Clear();

            Console.WriteLine("\nYou decided that despite aready lying to authorities, you'd rather just leave.");
            Console.WriteLine("Completely empty-handed.");
            Console.WriteLine("\n\nLoser.");
            Console.WriteLine("\n\n\n");
            Console.WriteLine("------------------ COWARD END ------------------");
            Console.WriteLine("ENDING 1/6 COMPLETE\n");
            

        }

        public static void PickyEnding()
        {
            Console.Clear();

            Console.WriteLine("You decided that even though you broke in and rummaged through some games, you didn't want anything.");
            Console.WriteLine("And so you returned home, empty handed, leaving many grubby fingerprints behind.");
            Console.WriteLine("\nWell\nAt least you didn't get caught?");
            Console.WriteLine("\n\n\n");

            Console.WriteLine("------------------ PICKY END ------------------");
            Console.WriteLine("ENDING 2/6 COMPLETE\n");
        }

        public static void GreedyEnding()
        {
            Console.Clear();

            Console.WriteLine("\nYou bent down to pick up your lost game.");
            Console.WriteLine("But when you put got a hold of it...");
            Console.WriteLine("\nEverything else that was in your bag fell out!");
            Console.WriteLine("\nAnd lights were shining at your face!");
            Console.WriteLine("\nIt looks like your lie about being the beneficiary doesn't hold up when the billionaire is still alive.");
            Console.WriteLine("If you had only left behind that game...maybe you would've made it out.");
            Console.WriteLine("\nI hope that game was worth it, it's all you've got now.");

            Console.WriteLine("\n\n\n");
            Console.WriteLine("------------------ GREEDY END ------------------");
            Console.WriteLine("ENDING 3/6 COMPLETE\n");
        }

        public static void DroppedLoot()
        {
            Console.Clear();

            Console.WriteLine("Well, you made it out just fine but you lost a lot of games.");
            Console.WriteLine("Maybe next time you shouldn't take so much.\n");
            Console.WriteLine("...or just invest in a better bag.");

            Console.WriteLine("\nMaybe the real treasure was the cops you lied to along the way.");

            Console.WriteLine("\n\n\n");
            Console.WriteLine("------------------ CHEAP-O END ------------------");
            Console.WriteLine("ENDING 4/6 COMPLETE\n");
        }

        public static void SuccessEnding()
        {
            Console.Clear();

            Console.WriteLine("Congrats! You left the house with your stash intact, and fortune aplenty.");
            Console.WriteLine("Though maybe these games aren't all the best quality.");
            Console.WriteLine("But that's okay.\nYou made it out!");

            Console.WriteLine("\n\n\n");
            Console.WriteLine("------------------ ESCAPE END ------------------");
            Console.WriteLine("ENDING 5/6 COMPLETE\n");
        }

        public static void NintendoEnding()
        {
            Console.WriteLine("Congrats! You left the house with your stash intact, and fortune aplenty.");
            Console.WriteLine("Nintendo games are always the best.");
            Console.WriteLine("\nYou make your way off the property successfully, and go home to play your new games.");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("\nWhen you arrive home, you begin to start and setup your new treasure.");
            Console.WriteLine("You try to open one, and fail. The disks were in perfect condition! What's wrong?\n");
            Console.WriteLine("'Reported suspicious activity. Your account has been banned'");
            Console.WriteLine("\n\nThanks Nintendo.");

            Console.WriteLine("\n\n\n");
            Console.WriteLine("------------------ NINTENDING ------------------");
            Console.WriteLine("ENDING 6/6 COMPLETE\n");
        }

    }
}