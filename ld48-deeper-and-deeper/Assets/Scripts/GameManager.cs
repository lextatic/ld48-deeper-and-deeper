using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<FishData> AllFishes;

	public BaitController Bait;

	public TrophyView TrophyView;

	private bool _isInMenu;

	void Start()
	{
		TrophyView.InitializeTrophyView(AllFishes);
		Bait.Restart += BaitRestarted;
		_isInMenu = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!_isInMenu)
			{
				OpenMenu("Paused");
				Time.timeScale = 0;
				return;
			}

			CloseMenu();

			return;
		}

		if (Input.GetKeyUp(KeyCode.Space) && _isInMenu)
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
		_isInMenu = true;
	}

	public void CloseMenu()
	{
		TrophyView.CloseTrophyPanel();
		_isInMenu = false;

		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			return;
		}

		Bait.ReadyBait();
	}
}
