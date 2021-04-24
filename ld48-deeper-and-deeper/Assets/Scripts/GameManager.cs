using System.Collections.Generic;
using System.Linq;
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
			fishData.Caught = false;
			var trophyView = Instantiate(TrophyViewPrefab);
			trophyView.InitializeTrophy(fishData);
			_setDataTrophy.Add(fishData, trophyView);
			trophyView.GetComponent<RectTransform>().SetParent(TrophiesListPanel);

		}
	}

	public void CatchFish(FishData fishData)
	{
		if (fishData.Caught) return;

		_setDataTrophy[fishData].SetTrophy();
		fishData.Caught = true;

		if (AllFishes.Count(x => x.Caught == false) == 0)
		{
			Debug.Log("Victory!");
		}

		TrophyPannel.SetActive(true);
	}
}
