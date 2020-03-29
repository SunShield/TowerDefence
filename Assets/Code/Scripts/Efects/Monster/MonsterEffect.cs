using TowerDefence.Unity.Monster;

namespace TowerDefence.Unity.Effect.Monster
{
	public enum EffectTags
	{
		OnApply,
		OnMove,
		OnTryHit,
		OnHitConfirmed,
		OnDamage,
		OnExpire,
		Durational
	}

	// Here we can add any kinds of events. They will be called in monster's methods execution.
	// Effects modify values of stuff received by them, and can do any other kinds of actions.
	// Also effects can have their own visual data
	// todo: mb refactor this a bit? make "OnApply" method work only if effect has "OnApply" type, etc?..
	public abstract class MonsterEffect
	{
		public virtual string Name { get; protected set; }
		public virtual int Priority { get; protected set; }
		public virtual bool IsStackable { get; protected set; }
		public virtual string[] Specials { get; protected set; }
		public virtual EffectTags[] EffectTagses { get; protected set; }
		public float[] Values { get; protected set; }

		protected MonsterEffect(float[] effectValues) /*I wanted to use params float[], but theoretically effects can rely on objects or smth*/
		{
			Values = effectValues;
		}

		public virtual void OnApply(MonsterController controller)
		{

		}

		public virtual void OnExpire(MonsterController controller)
		{

		}

		public virtual void OnProceed(MonsterController controller)
		{

		}

		public virtual MonsterHitDamageData OnHitConfirmed(MonsterHitDamageData hitDamageData, MonsterController controller)
		{
			MonsterHitDamageData newData = new MonsterHitDamageData(hitDamageData);
			return newData;
		}

		public virtual MonsterHitDamageData OnDamageReceived(MonsterHitDamageData hitDamageData, MonsterController controller)
		{
			MonsterHitDamageData newData = new MonsterHitDamageData(hitDamageData);
			return newData;
		}

		public virtual void OnMove(MonsterController controller)
		{

		}

		public virtual void Stack(MonsterEffect anotherEffect)
		{

		}

		// If effects are unable to stack, stronger one always replaces the weaker one.
		public virtual bool Compare(MonsterEffect anotherEffect)
		{
			return false;
		}
	}
}
