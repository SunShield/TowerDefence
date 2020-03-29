using System;

namespace TowerDefence.Core.DataStructure
{
	[Serializable]
	public class TowerData : NamedData
	{
		public int Cost;
		public float Range;
		public float ShotDelay;
		public string BulletType; // determines behaviour: fly, fly and explode, chain, appeфr around, etc.
		public string PossibleTargets; // tags for targets tower can attack
		public string TowerEffects; // Strings like BulletsCanFreeze(30)
	}
}
