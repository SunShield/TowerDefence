using UnityEngine;

namespace TowerDefence.Unity.Level.Pathing.MonsterPath
{
	public class MonsterPathes : MonoBehaviour
	{
		public MonsterPath[] Pathes;

		public MonsterPath this[int index] => Pathes[index];
	}
}
