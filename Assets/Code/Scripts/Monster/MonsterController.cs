using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Effect.Monster;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Monster.Effect;
using UnityEngine;

namespace TowerDefence.Unity.Monster
{
	public class MonsterController : BaseEntity
	{
		[SerializeField] private GameObject NonRotatableEffectsOrigin;
		[SerializeField] private GameObject RotatableEffectsOrigin;
		[SerializeField] private SpriteRenderer Renderer;
		[SerializeField] private CircleCollider2D Collider;
		[SerializeField] private MonsterMover Mover;
		
		public MonsterEffectHolder Effects;

		public string Name { get; private set; }
		public int MaxHealth { get; private set; }
		public int CurrentHealth { get; private set; }
		public int Damage { get; private set; }
		public int CoinValue { get; private set; }
		public MonsterMover GetMover() => Mover;

		public void Init(MonsterData data)
		{
			Effects = new MonsterEffectHolder(this);

			Name = data.IngameName;
			MaxHealth = data.MaxHealth;
			CurrentHealth = MaxHealth;
			Damage = data.Damage;
			CoinValue = data.CoinValue;

			Mover.Init(transform, data.Speed);
			Renderer = GetComponentInChildren<SpriteRenderer>();
			Collider = GetComponent<CircleCollider2D>();
		}

		public void SetSprite(Sprite s)
		{
			Renderer.sprite = s;
			// todo: add more proper way to change collider size (just add it as setting to avoid weirdness in automatic calculations in future?..)
			Collider.radius *= (s.rect.height / 32) /*this magic number is a smallest monster sprite size xd*/;
		}

		public void AddEffect(MonsterEffect effect)
		{
			Effects.AddEffect(effect);
		}

		public void AddEffects(List<MonsterEffect> effects)
		{
			foreach (MonsterEffect effect in effects)
			{
				AddEffect(effect);
			}
		}

		public void RemoveEffect(MonsterEffect effect)
		{
			Effects.RemoveEffect(effect);;
		}

		// This is called whenever we just pretend to "hit" an enemy.
		// For example, we have a "barrier" effect nullifying hits at all, and "armor" effect just reducing their damage by certain amount. 
		// First one goes here, next one - to ReceiveHit
		public void TryReceiveHit(MonsterHitDamageData hitDamageData)
		{
			ReceiveHit(hitDamageData);
		}

		// Not there's no difference between "hit" and "damage" but it can (and WILL) appear in the future.
		// For example, some kind of effect can work if you intend to hit monster even if hit was countered on dmg was zero
		private void ReceiveHit(MonsterHitDamageData hitDamageData)
		{
			AddEffects(hitDamageData.OnHitInflictableEffects);

			if (hitDamageData.DamageAmount > 0)
				ReceiveDamage(hitDamageData);
		}

		// "ReceiveDamage" means damage is more than zero.
		public void ReceiveDamage(MonsterHitDamageData hitDamageData)
		{
			MonsterHitDamageData newHitDamageData = ProcessOnDamageEffects(hitDamageData);
			InflictDamage(newHitDamageData);

			AddEffects(hitDamageData.OnDamageInflictableEffects);
		}

		private MonsterHitDamageData ProcessOnDamageEffects(MonsterHitDamageData hitDamageData)
		{
			if (Effects.GetEffectsByTypeAmount(EffectTags.OnDamage) == 0)
				return hitDamageData;

			List<MonsterEffect> damageEffects = Effects.GetEffectsByTag(EffectTags.OnDamage);
			MonsterHitDamageData newHitDamageData = new MonsterHitDamageData(hitDamageData);
			foreach (MonsterEffect effect in damageEffects)
			{
				newHitDamageData = effect.OnDamageReceived(newHitDamageData, this);
			}

			return newHitDamageData;
		}

		private void InflictDamage(MonsterHitDamageData hitDamageData)
		{
			CurrentHealth -= hitDamageData.DamageAmount;
			if (CurrentHealth <= 0)
			{
				Die();
			}
		}

		// Like death, but w/o OnDeath - for entering the castle only
		public void Banish()
		{
			Mover.StopMoving();
			GlobalLevelEvents.Instance.InvokeOnMonsterEntersCastle(this);
			// NEVER repeat this at home, always use pooling, ESPECIALLY for a mobile game
			// ...just not needed for a test task
			Destroy(gameObject); 
		}

		public void Die()
		{
			Mover.StopMoving();
			GlobalLevelEvents.Instance.InvokeOnMonsterDies(this);
			Destroy(gameObject);
		}

		// For now, we have this Update here, but in future, we will move all the effects Updates to the one place... 
		// And may be in the very future we will have one Update at all.
		private void Update()
		{
			ProceedDuratingEffects();
		}

		private void ProceedDuratingEffects()
		{
			var duratingEffects = Effects.GetEffectsByTag(EffectTags.Durational);
			foreach (var effect in duratingEffects.ToList())
			{
				effect.OnProceed(this);
			}
		}
	}
}

