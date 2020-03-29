using Sirenix.OdinInspector;
using TowerDefence.Unity.Bullet;
using TowerDefence.Unity.Service;
using TowerDefence.Unity.Storaging;
using UnityEngine;

namespace TowerDefence.Unity.Spawning
{
	public class GeneralBulletSpawner : MonoSingleton<GeneralBulletSpawner>
	{
		[SerializeField] private ArrowBulletSpawner ArrowBulletSpawner;
		[SerializeField] private DeathrayBulletSpawner DeathrayBulletSpawner;
		[SerializeField] private FreezeBulletSpawner FreezeBulletSpawner;
		[SerializeField] private SpikeballBulletSpawner SpikeballBulletSpawner;

		public void Init()
		{
			ArrowBulletSpawner.Init(GeneralDataStorage.Instance.BulletsDataStorage.ArrowTowerData);
			DeathrayBulletSpawner.Init(GeneralDataStorage.Instance.BulletsDataStorage.DeathrayTowerData);
			FreezeBulletSpawner.Init(GeneralDataStorage.Instance.BulletsDataStorage.FreezeTowerData);
			SpikeballBulletSpawner.Init(GeneralDataStorage.Instance.BulletsDataStorage.SpikeballTowerData);
		}

		// It's really not good.
		public BaseBulletController SpawnBullet(string bulletName, Vector3 position, BulletTargetData data)
		{
			switch (bulletName)
			{
				case "Arrow Tower Bullet":
					return ArrowBulletSpawner.SpawnBullet(position, data);
				case "Deathray Tower Bullet":
					return DeathrayBulletSpawner.SpawnBullet(position, data);
				case "Freeze Tower Bullet":
					return FreezeBulletSpawner.SpawnBullet(position, data);
				case "Spikeball Tower Bullet":
					return SpikeballBulletSpawner.SpawnBullet(position, data);
			}

			return null;
		}
	}
}
