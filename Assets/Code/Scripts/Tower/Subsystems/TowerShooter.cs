using TowerDefence.Unity.Bullet;
using TowerDefence.Unity.Monster;
using TowerDefence.Unity.Spawning;
using UnityEngine;

namespace TowerDefence.Unity.Tower
{
	public class TowerShooter : MonoBehaviour
	{
		public int Damage { get; set; }
		public float ShotDelay { get; set; }
		public string BulletType { get; private set; }

		public MonsterController Target { get; private set; }

		private float _shootTimer;
		private bool ShootNow => _shootTimer >= ShotDelay;

		public void Init(TowerTargeter targeter, string bulletType, string[] bulletEffects)
		{
			BulletType = bulletType;
			SubscribeOnNewTargetEvent(targeter);
		}

		private void SubscribeOnNewTargetEvent(TowerTargeter targeter)
		{
			targeter.OnTargetChanged += SetTarget;
		}

		private void SetTarget(MonsterController newTarget)
		{
			Target = newTarget;
		}

		private void Update()
		{
			AdvanceTimer(Time.deltaTime);

			if (ShootNow)
			{
				ResetTimer();
				TryShoot();
			}
		}

		private void AdvanceTimer(float time)
		{
			_shootTimer += time;
		}

		private void ResetTimer()
		{
			_shootTimer = 0f;
		}

		private void TryShoot()
		{
			if (Target != null)
			{
				BulletTargetData targetData = new BulletTargetData()
				{
					TransformData = Target.transform,
					VectorData = Target.transform.position
				};
				GeneralBulletSpawner.Instance.SpawnBullet(BulletType, transform.position, targetData);
			}
		}
	}
}
