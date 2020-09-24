using System;

namespace TicTacToe
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Tic-Tac-Toe game made by Georgi Dimitrov Dimitrov. Enjoy!");
			Console.ResetColor();
			Console.WriteLine();

			Player myPlayer = new Player();
			Bot myBot = new Bot();

			Game myGame = new Game(myPlayer, myBot);

			while (true)
			{
				Console.Write("Type \'play\' or \'Play\' in order to play the game: ");
				string play = Console.ReadLine();
				Console.WriteLine();

				if (play == "play" || play == "Play")
				{
					myGame.Play(); // starts the game
				}
				else
				{
					continue; // continues looping if the input is invalid
				}

				while (true)
				{
					if (myGame.Winner != null)
					{
						Console.Write("\nDo you want to play again? Type \'Yes\' or \'No\': ");
						string playAgain = Console.ReadLine();
						Console.WriteLine();

						if (playAgain == "yes" || playAgain == "Yes")
						{
							myGame.ResetGame(); // resets the variables and the board

							myGame.Play();
						}

						if (playAgain == "no" || playAgain == "No") 
						{
							Console.Write("Do you want to stop the game?: ");
							string stop = Console.ReadLine();

							if (stop == "stop" || stop == "Stop" || stop == "yes" || stop == "Yes")
							{
								myGame.StopGame(); // stops the game

								return;
							}
							else if (stop == "no" || stop == "No")
							{
								continue;
							}
						}
					}
				}
			}
		}
	}
}
