using System;
using TowerDefence.Core.DataStructure;

namespace TowerDefence.Unity.Storaging.Data
{
	[Serializable]
	public struct BulletsData
	{
		public ArrowTowerBulletData ArrowTowerBulletData;
		public DeathrayTowerBulletData DeathrayTowerBulletData;
		public FreezeTowerBulletData FreezeTowerBulletData;
		public SpikeballTowerBulletData SpikeballTowerBulletData;
	}

	public class BulletDataStorage
	{
		public ArrowTowerBulletData ArrowTowerData { get; set; }
		public DeathrayTowerBulletData DeathrayTowerData { get; set; }
		public FreezeTowerBulletData FreezeTowerData { get; set; }
		public SpikeballTowerBulletData SpikeballTowerData { get; set; }

		public void Init(BulletsData bulletsData)
		{
			ArrowTowerData = bulletsData.ArrowTowerBulletData;
			DeathrayTowerData = bulletsData.DeathrayTowerBulletData;
			FreezeTowerData = bulletsData.FreezeTowerBulletData;
			SpikeballTowerData = bulletsData.SpikeballTowerBulletData;
		}
	}
}
