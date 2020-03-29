using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Effect.Monster;
using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	/// <summary>
	/// Big damage, inaccurate. Bullets stay on the track, able to damage another enemies hitting them. Inflicts brittleness.
	/// </summary>
	public class SpikeballTowerBulletController : TrajectoryProjectileBulletController<SpikeballTowerBulletData>
	{
		public override void HandleCollision(Collider2D other)
		{
			if (CheckIfEnemy(other.gameObject))
			{
				DoOnEnemyHit(other);
			}
		}

		protected override MonsterHitDamageData GetHitDamageData()
		{
			MonsterHitDamageData data = base.GetHitDamageData();
			//I Wanted this to be a tower effect saying "add chill to bullets". But I'm too short on time to do this right now.
			data.OnDamageInflictableEffects.Add(new BrittlenessMonsterEffect(new float[] { 20, 5 }));
			return data;
		}
	}
}
