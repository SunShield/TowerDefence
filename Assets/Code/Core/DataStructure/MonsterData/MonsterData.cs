using System;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class MonsterData : NamedData
	{
		public int MaxHealth;
		public int Damage;
		public float Speed;
		public string MovementRule;
		public string[] Efects;

		public int CoinValue;
	}
}
