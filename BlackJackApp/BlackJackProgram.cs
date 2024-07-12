namespace BlackJackApp
{
    internal class BlackJackProgram
    {
        // Simulation of the classic blackjack card game.
        public void Run()
        {
            // User picks if he's playing alone or with multiple players.
            // Translations: 1) You're playing alone
            Console.WriteLine("1) Hraješ sám");
            // Translations: 2) You're playing with muliple people
            Console.WriteLine("2) Hraješ ve více lidech");
            // Translation: 0) End game
            Console.WriteLine("0) Ukončit hru");

            // Reads the input for the previous choice.
            int players = SafelyConvertToInt(Console.ReadLine()!);
            Console.WriteLine("-------------");
            // Chooses the right option of game mode using ifs or a switch.

            // Switch
            switch (players)
            {
                default:
                    Run();
                    break;
                case 1:
                    SinglePlayer();
                    break;
                case 2:
                    Multiplayer();
                    break;
                case 0:
                    // Translation: Ending game
                    Console.WriteLine("Ukončuji hru");
                    break;
            }

            // If, else if and else

            /*if (players == 1)
            {
                SinglePlayer();
            }
            else if (players == 2)
            {
                Multiplayer();
            }
            else if (players == 0)
            {
                Console.WriteLine("Ukončuji hru");
            }
            else
            {
                Run();
            }*/
        }
        public void SinglePlayer()
        {
            // A loop that let's player play multiple games.
            bool AnotherGame = true;
            int BestScore = 0;

            while (AnotherGame)
            {
                bool Proceed = true;
                int Sum = 0;
                //int AmountOfCards = 0; // I didn't find application for this feature, so I commented it.

                // A loop that let's player draw multiple cards.
                while (Proceed)
                {
                    // Translation: Do you want to draw a card? (yes/no)
                    Console.WriteLine("Chceš si vzít kartu? (y/n)");
                    string input = Console.ReadLine()!.ToLower();


                    if (input == "y")
                    {
                        // Drawing a card means a player gets a score of a random card added to their score.
                        var rand = new Random();
                        int NewCard = rand.Next(1, 12);

                        // Translation: You got a + (card number)
                        Console.WriteLine("Získal jsi " + NewCard);
                        Sum += NewCard;     // Score
                        //AmountOfCards++;  // I didn't find application for this feature, so I commented it.
                        Console.WriteLine("Celkem na kartách: " + Sum);
                        Console.WriteLine("-------------");

                        // If a player passes the score of 20 the game can be already evaluated.
                        if (Sum >= 21)
                        {
                            // Stops the player from drawing more and finds out if the beat their record.
                            Proceed = false;
                            BestScore = GetResult(Sum, BestScore);

                        }
                    }
                    else if (input == "n")
                    {
                        // Stops the game and evaluates the game.
                        Proceed = false;
                        BestScore = GetResult(Sum, BestScore);
                    }
                }

                // Translation: Do you want to play another game? (yes/no)
                Console.WriteLine("Chceš další hru? (y/n)");
                string NewGameInput = Console.ReadLine()!.ToLower();
                Console.WriteLine("-------------");
                // If the user says no, terminate the game and throw the menu again.
                if (NewGameInput == "n")
                {
                    AnotherGame = false;
                    Run();
                }
            }
        }
        public int GetResult(int Sum, int Max)
        {
            // If the player get exactly 21 points, the player wins.
            if (Sum == 21)
            {
                // Translation: You won!
                Console.WriteLine("Vyhrál jsi!");
            }
            else
            {   
                // Translation: You lost, too bad.
                Console.WriteLine("Skoda, prohral jsi.");
            }

            // Finds the best score out game and the previous best score.
            if (Sum > Max && Sum <= 21)
            {
                Console.WriteLine("Nejlepší skóre: " + Sum);
                return Sum;
            }

            Console.WriteLine("Nejlepší skóre: " + Max);
            return Max;
        }
        public void Multiplayer()
        {
            // Prompts user to input amount of players.
            // Translation: How many player are there?
            Console.WriteLine("Kolik je hráču?");
            int amountOfPlayers = SafelyConvertToInt(Console.ReadLine()!);

            // Creates a score card for the amount of players.
            int[] scores  = new int[amountOfPlayers];

            int amountOut = 0; // Counts how many people are out and no longer playing.
            bool[] playersOut = new bool[amountOfPlayers]; // This tells us if a player of index i is out.

            // Setting all the players to not be out yet.
            for (int i = 0; i < amountOfPlayers; i++)
            {
                playersOut[i] = false;
            }

            // If the amount of people out is the amount of players, end game.
            while (amountOut != amountOfPlayers)
            {
                // Loop going player by player, ignoring the players that are out.
                for (int i = 1; i <= amountOfPlayers && !playersOut[i - 1]; i++)
                {
                    // Prompts player i if he wants to take a card.

                    // Translation: Player ith do you want to take a card? (y/n)
                    Console.WriteLine("Hráč " + i + ". chceš si vzít kartu? (y/n)");
                    string input = Console.ReadLine()!.ToLower();

                    // Generate a random card to give to player i if yes.
                    if (input == "y")
                    {
                        var rand = new Random();
                        int NewCard = rand.Next(1, 12);
                        // Translation: You got a (card number)
                        Console.WriteLine("Získal jsi " + NewCard);
                        scores [i - 1] += NewCard;
                        // Translation: You have (total amount on hand)
                        Console.WriteLine("Máš: " + scores [i-1]);


                        // If the players score is greater or equal 21, he stops playing and is out.
                        if (scores [i - 1] >= 21)
                        {
                            playersOut[i - 1] = true;
                            amountOut++;
                        }
                    }
                    // Or if the player doesn't draw a card, he is also out.
                    else if (input == "n")
                    {
                        playersOut[i - 1] = true;
                        amountOut++;
                    }
                    Console.WriteLine("-------------");
                }
            }

            // Find the best score that respects the rule of the game.
            int bestScore = 0;

            foreach (int score in scores)
            {
                if (score > bestScore && score <= 21)
                {
                    bestScore = score;
                } 
            }

            // Find all the players that have that score.
            for (int i = 1; i <= amountOfPlayers; i++)
            {
                if (scores[i-1] == bestScore)
                {
                    // Translation: "Winner: ith player"
                    Console.WriteLine("Výherce: " + i + ". hráč");
                }
            }
            Console.WriteLine("-------------");
            // So the players can start a new game or one of the players can play alone.
            Run();
        }
        
        public int SafelyConvertToInt(string s)
        {
            if (int.TryParse(s, out int result))
            {
                return result;
            }
            // Returning 0 would automatically turn the game off in Run() method.
            return -1;
        }
    }
}
