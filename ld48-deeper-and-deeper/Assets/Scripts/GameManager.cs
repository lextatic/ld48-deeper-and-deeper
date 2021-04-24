using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<FishData> AllFishes;

	public Trophy TrophyViewPrefab;

	public RectTransform TrophyPanel;

	void Start()
	{
		foreach (var fishData in AllFishes)
		{
			var trophyView = Instantiate(TrophyViewPrefab);
			trophyView.SetTrophy(fishData);
			trophyView.GetComponent<RectTransform>().SetParent(TrophyPanel);

		}
	}
}
