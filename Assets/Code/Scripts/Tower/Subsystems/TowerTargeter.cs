using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Tower
{
	// for now, we have this here and don't go ahead with implementing this functionality
	// in most TD games, towers have "Target priorities" with min set of "Closest to castle" 
	// (usually default one), most far from castle, most damaged, etc. This can be used to implement this functionality
	public interface ITargetingPriority
	{
		float GetTargetPriority(MonsterController candidate);
	}

	public class ClosestToCastleTargetingPriority : ITargetingPriority
	{
		public float GetTargetPriority(MonsterController candidate)
		{
			return candidate != null ? -candidate.GetMover().GetDistanceToCastle() : -1f;
		}
	}

	public class TowerTargeter : MonoBehaviour
	{
		[SerializeField] private CircleCollider2D _collider;
		private float _range;
		private ITargetingPriority _currentTargetPriority = new ClosestToCastleTargetingPriority();
		public Dictionary<int, MonsterController> PossibleTargets { get; } = new Dictionary<int, MonsterController>();

		private MonsterController _bestTarget;
		public MonsterController BestTarget
		{
			get => _bestTarget;
			private set
			{
				_bestTarget = value;
				InvokeOnTargetChanged();
			}
		}

		public void Init()
		{
			LookupForBestTarget(); // we need to search for target whenever tower was build
			SubscribeOnGlobalEvents();
		}

		private void SubscribeOnGlobalEvents()
		{
			GlobalLevelEvents.Instance.OnMonsterDies += OnMonsterDisappear;
			GlobalLevelEvents.Instance.OnMonsterEntersCastle += OnMonsterDisappear;
		}

		public void SetRange(float range)
		{
			_range = range;
			_collider.radius = range;
			RecheckTargets();
		}

		private void RecheckTargets()
		{
			// todo: here we should access to certain storage containing each monster summoned.
			// It will be a way less perfomance-heavy then using Physics2D.OverlapCircleAll to check if new monsters appeared in the radius, 
			// if we made it bigger than it was
			// for smaller radius we can just recheck PossibleTargets dictionary
		}

		//private float GetMonsterDistanceToTower(MonsterController monster)
		//{
		//	return (monster.transform.position - transform.position).magnitude;
		//}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (CheckLayer(other.gameObject))
			{
				MonsterController controller = other.GetComponent<MonsterController>();
				PossibleTargets.Add(controller.EntityId, controller);
				CheckCandidateTarget(controller);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (CheckLayer(other.gameObject))
			{
				MonsterController controller = other.GetComponent<MonsterController>();
				PossibleTargets.Remove(controller.EntityId);
				if (BestTarget == controller)
				{
					BestTarget = null;
					// if we have lost previous best target due to it leaving the range
					LookupForBestTarget();
				}
			}
		}

		private bool CheckLayer(GameObject go)
		{
			return go.layer == LayerMask.NameToLayer("Enemies");
		}

		private void CheckCandidateTarget(MonsterController candidateTarget)
		{
			if (BestTarget == null)
			{
				BestTarget = candidateTarget;
				return;
			}
			else
			{
				bool isBetterThenBest = CompareWithBestTarget(candidateTarget);
				if (isBetterThenBest)
				{
					BestTarget = candidateTarget;
				}
			}
		}

		private bool CompareWithBestTarget(MonsterController candidateTarget)
		{
			float bestTargetPriority = _currentTargetPriority.GetTargetPriority(BestTarget);
			float candidatePriority = _currentTargetPriority.GetTargetPriority(candidateTarget);

			return candidatePriority > bestTargetPriority;
		}

		private void LookupForBestTarget()
		{
			foreach (var target in PossibleTargets)
			{
				if (BestTarget == null)
					BestTarget = target.Value;
				else
				{
					if (CompareWithBestTarget(target.Value))
						BestTarget = target.Value;
				}
			}
		}

		// if monster disappears due to it's death/entering castle, we need to check if that monster was our best target.
		private void OnMonsterDisappear(MonsterController monster)
		{
			if (BestTarget != monster)
				return;
			LookupForBestTarget();
		}

		// We need to recheck targets here, because monsters already in range can differ their speed (for example, due to chill effect). 
		// Don't do it now.
		//private void Update()
		//{

		//}

		private void InvokeOnTargetChanged()
		{
			OnTargetChanged?.Invoke(BestTarget);
		}

		public event Action<MonsterController> OnTargetChanged;
	}
}
