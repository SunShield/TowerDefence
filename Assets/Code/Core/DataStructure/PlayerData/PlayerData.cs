using System;

namespace TowerDefence.Core.DataStructure
{
	//todo:  Move it elsewhere later.
	[Serializable]
	public class PlayerData : NamedData
	{
		public int CoinBonus;
		public int LivesBonus;
		public string[] AvailableTowerTypes;
	}
}
