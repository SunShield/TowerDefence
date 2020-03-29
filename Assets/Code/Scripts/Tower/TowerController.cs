using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Bullet;
using UnityEngine;

namespace TowerDefence.Unity.Tower
{
	public enum ShotResult
	{
		Shooted = 0,
		NoTarget = 1
	}

	public class TowerController : BaseEntity
	{
		[SerializeField] private SpriteRenderer Renderer;
		[SerializeField] private TowerShooter Shooter;
		[SerializeField] private TowerTargeter Targeter;

		public void SetSprite(Sprite s)
		{
			Renderer.sprite = s;
		}

		public void Init(TowerData data)
		{
			InitTargeter(data);
			InitShooter(data);
		}

		private void InitTargeter(TowerData data)
		{
			Targeter.Init();
			Targeter.SetRange(data.Range);
		}

		private void InitShooter(TowerData data)
		{
			Shooter.ShotDelay = data.ShotDelay;
			Shooter.Init(Targeter, data.BulletType, null);
		}
	}
}
