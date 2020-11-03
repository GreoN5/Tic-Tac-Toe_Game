using System;

namespace TicTacToe
{
	public class Game
	{
		// the board grid of the game - 3x3
		private string[] _board = new string[]
		{"-", "-", "-",
		"-", "-", "-",
		"-", "-", "-"};

		private bool _gameInProgress = true; // is the game in progress
		public string Winner { get; private set; } = null; // the winner which will be defined later in the game
		private Player _player; // the user that will interact with the console
		private Bot _bot; // the bot that will compete with the player (AI)
		private Random _random = new Random(); // for making ranodm numbers (it is used below)

		public Game(Player player, Bot bot)
		{
			this._player = player;
			this._bot = bot;
		}

		public void Play()
		{
			// call the method for choosing which one the user wants (either 'X' or 'O')
			ChoosePlayer();

			// choosing the bot difficulty through the method below
			ChooseBotDifficulty();

			// showing the empty game board at start of the game
			ShowGameBoard();

			while (this._gameInProgress)
			{
				ChoosePlayerPosition();

				CheckIfGameOver();

				BotPosition();

				CheckIfGameOver();
			}

			// displays the winner of the game or if the game is a tie
			if (this.Winner == "X" || this.Winner == "O")
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"\"{this.Winner}\" has won the game!");
				Console.ResetColor();
			}
			else if (this.Winner == null)
			{
				// if the winner is null there is a tie
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Tie!");
				Console.ResetColor();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine("The game has stopped!");
				Console.ResetColor();
			}
		}

		/// <summary>
		/// Resets the variables and the board
		/// </summary>
		public void ResetGame() 
		{
			this._board = new string[]
			{"-", "-", "-",
			"-", "-", "-",
			"-", "-", "-"};

			this.Winner = null;
			this._gameInProgress = true;
		}

		/// <summary>
		/// Stops the game
		/// </summary>
		public void StopGame()
		{
			this._gameInProgress = false;
		}

		/// <summary>
		/// Shows the game board on the console
		/// </summary>
		private void ShowGameBoard()
		{
			Console.WriteLine("| " + this._board[0] + " | " + this._board[1] + " | " + this._board[2] + " |");
			Console.WriteLine("| " + this._board[3] + " | " + this._board[4] + " | " + this._board[5] + " |");
			Console.WriteLine("| " + this._board[6] + " | " + this._board[7] + " | " + this._board[8] + " |");

			Console.WriteLine();
		}

		private void ChoosePlayer()
		{
			while (true)
			{
				// ask the user to write the type of a player he/she wants to be
				Console.Write("Type \"X\" or \"O\": ");
				string player = Console.ReadLine();

				// determine the player and the bot depending on the user's choice
				if (player == "x" || player == "X")
				{
					this._player.SetName("X");
					this._bot.SetName("O");

					return;
				}
				else if (player == "o" || player == "O")
				{ 
					this._player.SetName("O");
					this._bot.SetName("X");

					return;
				}
				else
				{
					continue; // continues looping if the character choosen from the input is invalid
				}
			}
		}

		private void ChooseBotDifficulty()
		{
			while (true)
			{
				// ask the user to choose which difficulty of the bot he/she wants
				Console.Write("\nChoose bot's difficulty \n(type easy, medium or hard): \n---> ");
				string difficulty = Console.ReadLine();

				// setting the bot's difficulty
				switch (difficulty)
				{
					case "easy":
						this._bot.SetDifficulty(difficulty);
						break;
					case "Easy":
						this._bot.SetDifficulty(difficulty);
						break;
					case "medium":
						this._bot.SetDifficulty(difficulty);
						break;
					case "Medium":
						this._bot.SetDifficulty(difficulty);
						break;
					case "hard":
						this._bot.SetDifficulty(difficulty);
						break;
					case "Hard":
						this._bot.SetDifficulty(difficulty);

						break;
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\nInvalid difficulty. Please try again!");
						Console.ResetColor();
						continue;
				}

				Console.WriteLine();

				break;
			}
		}

		private void ChoosePlayerPosition()
		{
			// displaying player's turn
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{this._player.GetName()}'s turn.");
			Console.ResetColor();

			while (this._gameInProgress)
			{
				try
				{
					// making the user choose between 1-9 to put his/her character
					Console.Write("Choose a position between 1-9: ");
					int position = int.Parse(Console.ReadLine());

					if (position == 1 ||
						position == 2 ||
						position == 3 ||
						position == 4 ||
						position == 5 ||
						position == 6 ||
						position == 7 ||
						position == 8 ||
						position == 9)
					{
						// if true checks if there is empty space
						if (this._board[position - 1] == "-")
						{
							this._board[position - 1] = this._player.GetName();

							break;
						}
						else if (this._board[position - 1] != "-")
						{
							// if there is not empty space it will continue looping and asking the player for a valid position
							Console.WriteLine($"You can't put \"{this._player.GetName()}\" on this position!");

							continue; 
						}
						else
						{
							continue; // continue looping if the numbers from the input does not match the criteria
						}
					}
				}
				catch (FormatException)
				{
					// if there is a string input in the console it will catch the exception and continue looping until there is a number
					Console.WriteLine("\nYou should type only numbers!");
					continue;
				}
			}

			// finally showing the board after the player has chosen a position
			ShowGameBoard();
		}

		private void BotPosition()
		{
			// checks if the game is still in proggress
			if (this._gameInProgress)
			{
				// if true then prints the bot's turn
				var difficulty = this._bot.GetDifficulty();

				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine($"{this._bot.GetName()}'s turn.");
				Console.ResetColor();

				while (this._gameInProgress)
				{
					if (difficulty == "easy")
					{
						// if the difficulty is easy then the bot will put on random positions if they are empty
						int randomNumber = this._random.Next(1, 9);

						if (this._board[randomNumber - 1] == "-")
						{
							// randomNumber - 1 is matching the array's numbers of the board
							this._board[randomNumber - 1] = this._bot.GetName();

							break; // exits the loop after the bot has put it's character
						}
						else
						{
							continue; // continues the loop if there isn't empty space on that random number
						}
					}
					else if (difficulty == "medium")
					{
						// if the difficulty is medium then calls the method
						CheckAnglesAndPutBotPosition();

						break; // exits the loop when the method is finished
					}
					else if (difficulty == "hard")
					{
						// if the difficulty is hard then calls the method
						CheckIfPlayerIsCloseToWinOrIfBotWins();

						break; // exits the loop when the method is finished
					}
				}

				ShowGameBoard(); // finally showing the game board after bot's turn
			}
			else
			{
				return; // if game is not in progress it doesn't go through the loop
			}
		}

		/// <summary>
		/// Hard difficulty
		/// </summary>
		private void CheckIfPlayerIsCloseToWinOrIfBotWins()
		{
			var player = this._player.GetName();
			var bot = this._bot.GetName();

			if (CheckPlayerOrBotForWin(player))
			{
				return; // exits the method preventing from going further because if so the bot will put character at least on 2 positions
			}

			if (BotWin())
			{
				return; // exits the method
			}

			// if the above are false it will make the bot put in a random position if it's empty
			while (true)
			{
				int randomNumber = this._random.Next(1, 9);

				if (this._board[randomNumber - 1] == "-")
				{
					this._board[randomNumber - 1] = bot;

					break;
				}
			}
		}

		private bool BotWin()
		{
			// checks if the bot can win
			if (CheckPlayerOrBotForWin(this._bot.GetName()))
			{
				// if so returns true
				return true;
			}

			return false;
		}

		private void CheckIfGameOver()
		{
			if (CheckForWinner()) // if there is a winner, there is no need to check for a tie 
				return;

			if (CheckIfTie()) // if there is no winner, it will check for a tie
				return;
		}

		private bool CheckForWinner()
		{
			// applying the methods to variables in order to determine if there is a winner
			string winnerRows = CheckRows();
			string winnerColumns = CheckColumns();
			string winnerDiagonals = CheckDiagonals();

			if (winnerRows != null)
			{
				this.Winner = winnerRows;

				this._gameInProgress = false; // if there is a winner on the rows the game stops

				return true; // exits the method
			}
			else if (winnerColumns != null)
			{
				this.Winner = winnerColumns;

				this._gameInProgress = false; // if there is a winner on the columns the game stops

				return true; // exits the method
			}
			else if (winnerDiagonals != null)
			{
				this.Winner = winnerDiagonals;

				this._gameInProgress = false; // if there is a winner on the diagonals the game stops

				return true; // exits the method
			}

			return false;
		}

		private bool CheckIfTie()
		{
			int checkIfCellsAreFilled = 0; // variable for all of the cells 

			for (int i = 0; i < this._board.Length; i++)
			{
				if (this._board[i] != "-")
				{
					if (checkIfCellsAreFilled < 8)
					{
						/* if the number of cells are less than 8 and a certain index of the board is not empty 
						it will increment the number of cells that are filled */
						checkIfCellsAreFilled++;
					}
					else
					{
						this._gameInProgress = false; // if the number of filled cells are 8 (max indexes of the board array)
												// it will stop the game, making it a tie

						this.Winner = null;

						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Medium difficulty
		/// </summary>
		private void CheckAnglesAndPutBotPosition()
		{
			/* if the player chooses some of the corners of the board 
			  the bot will put it's character near the player's */

			var player = this._player.GetName();
			var bot = this._bot.GetName();

			while (true)
			{
				if (this._board[0] == player)
				{
					if (this._board[1] == "-")
					{
						this._board[1] = bot;
						return;
					}
					else if (this._board[3] == "-")
					{
						this._board[3] = bot;
						return;
					}
					else
					{
						break;
					}
				}
				else if (this._board[2] == player)
				{
					if (this._board[1] == "-")
					{
						this._board[1] = bot;
						return;
					}
					else if (this._board[5] == "-")
					{
						this._board[5] = bot;
						return;
					}
					else
					{
						break;
					}
				}
				else if (this._board[6] == player)
				{
					if (this._board[3] == "-")
					{
						this._board[3] = bot;
						return;
					}
					else if (this._board[7] == "-")
					{
						this._board[7] = bot;
						return;
					}
					else
					{
						break;
					}
				}
				else if (this._board[8] == player)
				{
					if (this._board[5] == "-")
					{
						this._board[5] = bot;
						return;
					}
					else if (this._board[7] == "-")
					{
						this._board[7] = bot;
						return;
					}
					else
					{
						break;
					}
				}
			}

			// if there is nothing in the corners the bot will put on random positions
			while (true)
			{
				int randomNumber = this._random.Next(1, 9);

				if (this._board[randomNumber - 1] == "-")
				{
					this._board[randomNumber - 1] = bot;

					break;
				}
				else
				{
					continue;
				}
			}
		}

		private string CheckRows()
		{
			// if there are same characters on the rows and they are not empty it will return the character that is in these positions
			if ((this._board[0] == this._board[1]) && (this._board[1] == this._board[2]) &&
				this._board[0] != "-" && this._board[1] != "-" && this._board[2] != "-")
			{
				return this._board[0];
			}
			else if ((this._board[3] == this._board[4]) && (this._board[4] == this._board[5]) &&
					 this._board[3] != "-" && this._board[4] != "-" && this._board[5] != "-")
			{
				return this._board[3];
			}
			else if ((this._board[6] == this._board[7]) && (this._board[7] == this._board[8]) &&
					 this._board[6] != "-" && this._board[7] != "-" && this._board[8] != "-")
			{
				return this._board[6];
			}

			return null; // if there are no same characters returns null
		}

		private string CheckColumns()
		{
			// if there are same characters on the columns and they are not empty it will return the character that is in these positions
			if ((this._board[0] == this._board[3]) && (this._board[3] == this._board[6]) &&
				this._board[0] != "-" && this._board[3] != "-" && this._board[6] != "-")
			{
				return this._board[0];
			}
			else if ((this._board[1] == this._board[4]) && (this._board[4] == this._board[7]) &&
					 this._board[1] != "-" && this._board[4] != "-" && this._board[7] != "-")
			{
				return this._board[1];
			}
			else if ((this._board[2] == this._board[5]) && (this._board[5] == this._board[8]) &&
					 this._board[2] != "-" && this._board[5] != "-" && this._board[8] != "-")
			{
				return this._board[2];
			}

			return null; // if there are no same characters returns null
		}

		private string CheckDiagonals()
		{
			// if there are same characters on the diagonals and they are not empty it will return the character that is in these positions
			if ((this._board[0] == this._board[4]) && (this._board[4] == this._board[8]) &&
				this._board[0] != "-" && this._board[4] != "-" && this._board[8] != "-")
			{
				return this._board[0];
			}
			else if ((this._board[2] == this._board[4]) && (this._board[4] == this._board[6]) &&
					 this._board[2] != "-" && this._board[4] != "-" && this._board[6] != "-")
			{
				return this._board[2];
			}

			return null; // if there are no same characters returns null
		}

		private bool CheckPlayerOrBotForWin(string playerName)
		{
			var bot = this._bot.GetName();

			// checking rows
			if (this._board[0] == playerName && this._board[1] == playerName)
			{
				if (this._board[2] == "-")
				{
					this._board[2] = bot;
					return true;
				}
			}
			else if (this._board[1] == playerName && this._board[0] == playerName)
			{
				if (this._board[0] == "-")
				{
					this._board[0] = bot;
					return true;
				}
			}
			else if (this._board[3] == playerName && this._board[4] == playerName)
			{
				if (this._board[5] == "-")
				{
					this._board[5] = bot;
					return true;
				}
			}
			else if (this._board[4] == playerName && this._board[5] == playerName)
			{
				if (this._board[3] == "-")
				{
					this._board[3] = bot;
					return true;
				}
			}
			else if (this._board[6] == playerName && this._board[7] == playerName)
			{
				if (this._board[8] == "-")
				{
					this._board[8] = bot;
					return true;
				}
			}
			else if (this._board[7] == playerName && this._board[8] == playerName)
			{
				if (this._board[6] == "-")
				{
					this._board[6] = bot;
					return true;
				}
			}
			else if (this._board[0] == playerName && this._board[2] == playerName)
			{
				if (this._board[1] == "-")
				{
					this._board[1] = bot;
					return true;
				}
			}
			else if (this._board[3] == playerName && this._board[5] == playerName)
			{
				if (this._board[4] == "-")
				{
					this._board[4] = bot;
					return true;
				}
			}
			else if (this._board[6] == playerName && this._board[8] == playerName)
			{
				if (this._board[7] == "-")
				{
					this._board[7] = bot;
					return true;
				}
			}

			// checking columns
			if (this._board[0] == playerName && this._board[3] == playerName)
			{
				if (this._board[6] == "-")
				{
					this._board[6] = bot;
					return true;
				}
			}
			else if (this._board[3] == playerName && this._board[6] == playerName)
			{
				if (this._board[0] == "-")
				{
					this._board[0] = bot;
					return true;
				}
			}
			else if (this._board[0] == playerName && this._board[6] == playerName)
			{
				if (this._board[3] == "-")
				{
					this._board[3] = bot;
					return true;
				}
			}
			else if (this._board[1] == playerName && this._board[4] == playerName)
			{
				if (this._board[7] == "-")
				{
					this._board[7] = bot;
					return true;
				}
			}
			else if (this._board[4] == playerName && this._board[7] == playerName)
			{
				if (this._board[1] == "-")
				{
					this._board[1] = bot;
					return true;
				}
			}
			else if (this._board[1] == playerName && this._board[7] == playerName)
			{
				if (this._board[4] == "-")
				{
					this._board[4] = bot;
					return true;
				}
			}
			else if (this._board[2] == playerName && this._board[5] == playerName)
			{
				if (this._board[8] == "-")
				{
					this._board[8] = bot;
					return true;
				}
			}
			else if (this._board[5] == playerName && this._board[8] == playerName)
			{
				if (this._board[2] == "-")
				{
					this._board[2] = bot;
					return true;
				}
			}
			else if (this._board[2] == playerName && this._board[8] == playerName)
			{
				if (this._board[5] == "-")
				{
					this._board[5] = bot;
					return true;
				}
			}

			// checking diagonals
			if (this._board[0] == playerName && this._board[4] == playerName)
			{
				if (this._board[8] == "-")
				{
					this._board[8] = bot;
					return true;
				}
			}
			else if (this._board[4] == playerName && this._board[8] == playerName)
			{
				if (this._board[0] == "-")
				{
					this._board[0] = bot;
					return true;
				}
			}
			else if (this._board[0] == playerName && this._board[8] == playerName)
			{
				if (this._board[4] == "-")
				{
					this._board[4] = bot;
					return true;
				}
			}
			else if (this._board[2] == playerName && this._board[4] == playerName)
			{
				if (this._board[6] == "-")
				{
					this._board[6] = bot;
					return true;
				}
			}
			else if (this._board[4] == playerName && this._board[6] == playerName)
			{
				if (this._board[2] == "-")
				{
					this._board[2] = bot;
					return true;
				}
			}
			else if (this._board[2] == playerName && this._board[6] == playerName)
			{
				if (this._board[4] == "-")
				{
					this._board[4] = bot;
					return true;
				}
			}

			return false;
		}
	}
}
