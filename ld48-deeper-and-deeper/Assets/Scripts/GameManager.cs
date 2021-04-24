using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<FishData> AllFishes;

	public Trophy TrophyViewPrefab;

	public GameObject TrophyPannel;

	public RectTransform TrophiesListPanel;

	public BaitController Bait;

	private Dictionary<FishData, Trophy> _setDataTrophy;

	void Start()
	{
		TrophyPannel.SetActive(false);

		_setDataTrophy = new Dictionary<FishData, Trophy>();

		foreach (var fishData in AllFishes)
		{
			var trophyView = Instantiate(TrophyViewPrefab);
			trophyView.InitializeTrophy(fishData);
			_setDataTrophy.Add(fishData, trophyView);
			trophyView.GetComponent<RectTransform>().SetParent(TrophiesListPanel);

		}
	}

	public void CatchFish(FishData fishData)
	{

		_setDataTrophy[fishData].SetTrophy();

		TrophyPannel.SetActive(true);
	}
}
