using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerDefence.Unity.Service
{
	public class MonoSingleton<T> : SerializedMonoBehaviour where T: MonoBehaviour
	{
		private static T _instance;
		private static bool IntanceExists => _instance != null;

		public static T Instance
		{
			get
			{
				if (!IntanceExists)
				{
					TryFindInstance();

					if (!IntanceExists)
						CreateInstance();
				}

				return _instance;
			}
		}

		private static void TryFindInstance()
		{
			_instance = (T)FindObjectOfType(typeof(T));
		}

		private static void CreateInstance()
		{
			GameObject go = new GameObject();
			_instance = go.AddComponent<T>();
			go.name = typeof(T).ToString();
		}
	}
}
