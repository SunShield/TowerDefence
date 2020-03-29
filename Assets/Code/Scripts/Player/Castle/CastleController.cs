using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Player.Castle
{
	public class CastleController : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (IsEnemyCollision(other))
			{
				MonsterController controller = other.GetComponent<MonsterController>();
				controller.Banish();
			}
		}

		private bool IsEnemyCollision(Collider2D other)
		{
			return other.gameObject.layer == LayerMask.NameToLayer("Enemies");
		}
	}
}
