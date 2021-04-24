using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyView : MonoBehaviour
{
	public TrophyItem TrophyViewPrefab;

	public GameObject TrophyPannel;

	public RectTransform TrophiesListPanel;

	public TextMeshProUGUI InfoText;

	private Dictionary<FishData, TrophyItem> _setDataTrophy;

	public void InitializeTrophyView(List<FishData> allFishes)
	{
		TrophyPannel.SetActive(false);

		_setDataTrophy = new Dictionary<FishData, TrophyItem>();

		foreach (var fishData in allFishes)
		{
			fishData.Caught = false;
			var trophyView = Instantiate(TrophyViewPrefab);
			trophyView.InitializeTrophy(fishData);
			_setDataTrophy.Add(fishData, trophyView);
			trophyView.GetComponent<RectTransform>().SetParent(TrophiesListPanel);

		}
	}

	public void SetNewTrophy(FishData fishData)
	{
		_setDataTrophy[fishData].SetTrophy();
	}

	public void ShowTrophyPanel(string info)
	{
		InfoText.text = info;
		TrophyPannel.SetActive(true);
	}

	public void CloseTrophyPanel()
	{
		TrophyPannel.SetActive(false);
	}
}
