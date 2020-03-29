using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	public class GenericDataObject<T> : ScriptableObject where T: BaseData
	{
		public T Data;
	}
}
