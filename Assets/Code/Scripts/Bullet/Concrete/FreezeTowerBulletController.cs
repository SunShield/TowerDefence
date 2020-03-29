using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Effect.Monster;
using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	/// <summary>
	/// Small damage to single target, inflicts freeze.
	/// </summary>
	public class FreezeTowerBulletController : BeamBulletController<FreezeTowerBulletData>
	{
		protected override void OnTriggerEnter2D(Collider2D other)
		{
			// we don't need this here.
			// TODO: move OnTriggerEnter to more proper place, not to the most basic class
		}

		protected override MonsterHitDamageData GetHitDamageData()
		{
			MonsterHitDamageData data = base.GetHitDamageData();
			//I Wanted this to be a tower effect saying "add chill to bullets". But I'm too short on time to do this right now.
			data.OnHitInflictableEffects.Add(new ChillMonsterEffect(new float[]{30, 3})); 
			return data;
		}
	}
}
