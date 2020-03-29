using System.Collections.Generic;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Service.ScriptableObjectSpawners;

namespace TowerDefence.Unity.Storaging.Data
{
	public class NamedDataStorage<T> where T: NamedData
	{
		private readonly Dictionary<string, T> _dataDictionary = new Dictionary<string, T>();

		public void FillFromDataObjects(GenericDataObject<T>[] dataObjects)
		{
			foreach (var dataObject in dataObjects)
			{
				_dataDictionary.Add(dataObject.Data.IngameName, dataObject.Data);
			}
		}

		public T this[string name] => _dataDictionary[name];
	}
}
