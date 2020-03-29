using System;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class WaveMonsterGroupData
	{
		public string MonsterType;
		public int MonsterAmount;
		public float MonsterSpawnDelay;
		public float NextGroupDelay;
		public int SpawnPointIndex;
		public int MonsterPathIndex;

		public float GroupTime => MonsterAmount * MonsterSpawnDelay + NextGroupDelay;
	}
}
