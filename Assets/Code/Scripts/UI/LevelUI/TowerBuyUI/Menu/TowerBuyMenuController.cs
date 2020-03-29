using System;
using System.Linq;
using TMPro;
using TowerDefence.Unity.Storaging.Graphics;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Storaging;
using UnityEngine;

public class TowerBuyMenuController : MonoBehaviour
{
	private const int TowerAmount = 4;

	[SerializeField] private TowerBuyMenuItemController[] ItemControllers;
	private string[] _towerVariants;

	public void Init()
	{
		var dataStorage = GeneralDataStorage.Instance;
		var graphicsStorage = GeneralGraphicsStorage.Instance;
		_towerVariants = dataStorage.PlayerData.AvailableTowerTypes;

		Sprite[] towerSprites = _towerVariants.Select(x => graphicsStorage.GetTowerSprite(x)).ToArray();
		int[] towerPrices = _towerVariants.Select(x => dataStorage.GetTowerData(x).Cost).ToArray();
		InitItemControllers(towerSprites, towerPrices);

		BoundButtonEvents();
	}

	private void InitItemControllers(Sprite[] towerSprites, int[] towerPrices)
	{
		for (int i = 0; i < TowerAmount; i++)
		{
			ItemControllers[i].Init(towerSprites[i], towerPrices[i]);
		}
	}

	public void MoveTo(Vector3 pos)
	{
		RectTransform tran = GetComponent<RectTransform>();
		tran.localPosition = pos;
	}

	private void BoundButtonEvents()
	{
		for (int i = 0; i < TowerAmount; i++)
		{
			int a = i;
			ItemControllers[i].AddListener(delegate { SelectTowerVariant(a); });
		}
	}

	private void SelectTowerVariant(int variant)
	{
		GlobalLevelUiEvents.Instance.InvokeOnTowerVariantChosenFromBuymenu(_towerVariants[variant]);
	}
}
