using System;
using System.Linq;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class WaveData : BaseData
	{
		public WaveMonsterGroupData[] GroupsData;
		public float NextWaveTime;
		public int MaxGroupNumber => GroupsData.Length - 1;
		public int WaveCapacity => GroupsData.Sum(x => x.MonsterAmount); // bad perfomance, but pretty small; ok for now
		public float WaveTime => GroupsData.Sum(x => x.GroupTime);
	}
}
