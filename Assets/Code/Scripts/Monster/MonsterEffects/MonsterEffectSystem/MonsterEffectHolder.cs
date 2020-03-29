using System.Collections.Generic;
using TowerDefence.Unity.Effect.Monster;

namespace TowerDefence.Unity.Monster.Effect
{
	/// <summary>
	/// Point of this class is simplifying effects calling. We want to call proper methods on proper effects and don't touch empty virtual methods.
	/// </summary>
	public class MonsterEffectHolder
	{
		private MonsterController _monster;

		public readonly Dictionary<string, MonsterEffect> _effectsByName = new Dictionary<string, MonsterEffect>();

		// todo: iterate through EffectType Enum instead to reduce amount of places to implement changes on adding new effect type 
		public readonly Dictionary<EffectTags, List<MonsterEffect>> _effectsByTypes =
			new Dictionary<EffectTags, List<MonsterEffect>>()
			{
				{EffectTags.OnApply, new List<MonsterEffect>()},
				{EffectTags.OnMove, new List<MonsterEffect>()},
				{EffectTags.OnTryHit, new List<MonsterEffect>()},
				{EffectTags.OnHitConfirmed, new List<MonsterEffect>()},
				{EffectTags.OnDamage, new List<MonsterEffect>()},
				{EffectTags.OnExpire, new List<MonsterEffect>()},
				{EffectTags.Durational, new List<MonsterEffect>()},
			};


		public MonsterEffectHolder(MonsterController monster)
		{
			_monster = monster;
		}

		public void AddEffect(MonsterEffect effect)
		{
			if (CheckEffectPresent(effect))
			{
				if(effect.IsStackable)
					StackEffects(effect);
				else
				{
					ReplaceEffectIfStronger(effect);
				}
			}
			else
			{
				AddEffectInternal(effect);
			}
		}

		private bool CheckEffectPresent(MonsterEffect effect)
		{
			return _effectsByName.ContainsKey(effect.Name);
		}

		private void StackEffects(MonsterEffect effect)
		{
			_effectsByName[effect.Name].Stack(effect);
		}

		private void ReplaceEffectIfStronger(MonsterEffect effect)
		{
			if (_effectsByName[effect.Name].Compare(effect))
			{
				RemoveEffect(_effectsByName[effect.Name]);
				AddEffectInternal(effect);
			}
		}

		private void AddEffectInternal(MonsterEffect effect)
		{
			AddEffectToByNameDictionary(effect);
			AddEffectToTypesDictionary(effect);
			effect.OnApply(_monster);
		}

		private void AddEffectToByNameDictionary(MonsterEffect effect)
		{
			_effectsByName.Add(effect.Name, effect);
		}

		private void AddEffectToTypesDictionary(MonsterEffect effect)
		{
			foreach (EffectTags effectType in effect.EffectTagses)
			{
				_effectsByTypes[effectType].Add(effect);
			}
		}

		public void RemoveEffect(MonsterEffect effect)
		{
			RemoveEffectFromEffectsByTypeDictionary(effect);
			RemoveEffectFromEffectsByNameDictionary(effect);
			effect.OnExpire(_monster);
		}

		private void RemoveEffectFromEffectsByTypeDictionary(MonsterEffect effect)
		{
			foreach (EffectTags effectType in effect.EffectTagses)
			{
				_effectsByTypes[effectType].Remove(effect);
			}
		}

		private void RemoveEffectFromEffectsByNameDictionary(MonsterEffect effect)
		{
			_effectsByName.Remove(effect.Name);
		}

		public List<MonsterEffect> GetEffectsByTag(EffectTags tags)
		{
			return _effectsByTypes[tags];
		}

		public int GetEffectsByTypeAmount(EffectTags tags)
		{
			return _effectsByTypes[tags].Count;
		}
	}
}
