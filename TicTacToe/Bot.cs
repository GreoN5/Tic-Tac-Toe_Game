namespace TicTacToe
{
	public class Bot : IGamePlayers
	{
		private string _name;
		private string _difficulty;

		public Bot()
		{

		}

		public void SetName(string newName)
		{
			this._name = newName;
		}

		public void SetDifficulty(string newDifficulty)
		{
			this._difficulty = newDifficulty;
		}

		public string GetName()
		{
			return this._name;
		}

		public string GetDifficulty()
		{
			return this._difficulty;
		}
	}
}
