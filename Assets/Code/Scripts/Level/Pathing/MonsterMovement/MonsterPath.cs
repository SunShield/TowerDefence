using System;
using UnityEngine;

namespace TowerDefence.Unity.Level.Pathing.MonsterPath
{
	[Serializable]
	public class MonsterPath
	{
		public Transform[] PathNodes;

		public int Size => PathNodes.Length;
		public Transform this[int index] => PathNodes[index];
	}
}
