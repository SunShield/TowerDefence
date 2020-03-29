using TMPro;
using TowerDefence.Unity.GlobalEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerBuyMenuItemController : MonoBehaviour
{
	private readonly Color ActiveColor = new Color(1f, 0.9f, 0f);
	private readonly Color InactiveColor = new Color(1f, 0f, 0f);

	[SerializeField] private Button Button;
	[SerializeField] private TextMeshProUGUI TowerCostUi;
	[SerializeField] private Image TowerImage;
	private int _towerCostStored;

	public void Init(Sprite towerSprite, int towerCost)
	{
		TowerImage.sprite = towerSprite;
		_towerCostStored = towerCost;
		TowerCostUi.text = _towerCostStored.ToString();

		GlobalLevelEvents.Instance.OnPlayerGoldChange += OnPlayerGoldAmountChanged;
	}

	// Generally this dhould be done with event here and menu controller subscribing on it... 
	// But it seems OK for now, I think.
	public void AddListener(UnityAction actionToAdd)
	{
		Button.onClick.AddListener(actionToAdd);
	}

	private void OnPlayerGoldAmountChanged(int value, int playerGold)
	{
		ChangeState(playerGold >= _towerCostStored);
	}

	private void ChangeState(bool newState)
	{
		Button.interactable = newState;
		TowerCostUi.color = newState ? ActiveColor : InactiveColor;
	}
}
