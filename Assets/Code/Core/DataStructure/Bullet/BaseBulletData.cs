using System;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class BaseBulletData : NamedData
	{
		public float LifeTime;
		public int Damage;
	}
}
