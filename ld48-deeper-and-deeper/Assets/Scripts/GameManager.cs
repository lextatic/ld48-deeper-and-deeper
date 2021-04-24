using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<FishData> AllFishes;

	public BaitController Bait;

	public TrophyView TrophyView;

	void Start()
	{
		TrophyView.InitializeTrophyView(AllFishes);
		Bait.Restart += BaitRestarted;
	}

	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
			&& TrophyView.gameObject.activeInHierarchy) // Still gambiarra
		{
			CloseMenu();
		}
	}

	public void BaitRestarted(FishData fishData)
	{
		// Already got everything!
		//if (AllFishes.Count(x => x.Caught == false) == 0)
		//{
		//	OpenMenu("You've already caught everything! Go home.");
		//	return;
		//}

		if (!fishData)
		{
			OpenMenu("Nothing this time...");
			return;
		}

		if (fishData.Caught)
		{
			OpenMenu("Already caught this one.");
			return;
		}

		fishData.Caught = true;
		TrophyView.SetNewTrophy(fishData);

		if (AllFishes.Count(x => x.Caught == false) == 0)
		{
			OpenMenu("Victory!");
			return;
		}

		OpenMenu($"New fish: {fishData.Name}");
	}

	public void OpenMenu(string info)
	{
		TrophyView.ShowTrophyPanel(info);
	}

	public void CloseMenu()
	{
		TrophyView.CloseTrophyPanel();
		Bait.ReadyBait();
	}
}
