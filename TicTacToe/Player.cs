namespace TicTacToe
{
	public class Player : IGamePlayers
	{
		private string _name;

		public Player()
		{

		}

		public void SetName(string newName)
		{
			this._name = newName;
		}

		public string GetName()
		{
			return this._name;
		}
	}
}
