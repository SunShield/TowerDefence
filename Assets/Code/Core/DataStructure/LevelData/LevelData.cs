using System;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class LevelData : NamedData
	{
		public int CastleLife;
		public int StartingCoins;
		public WaveData[] Waves;
		// Another data here (possibly level effects, timers etc)
	}
}
