using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Unity.Storaging.Graphics
{
	public class SpriteStorage
	{
		private readonly Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
		public Sprite this[string entityName] => _sprites[entityName];
		private readonly string _path;

		public SpriteStorage(string path)
		{
			_path = path;
		}

		public void Fill()
		{
			Sprite[] spritesArray = LoadMonsterSprites();
			foreach (var sprite in spritesArray)
			{
				_sprites.Add(sprite.name, sprite);
			}
		}

		protected virtual Sprite[] LoadMonsterSprites()
		{
			return Resources.LoadAll<Sprite>(_path) as Sprite[];
		}
	}
}
