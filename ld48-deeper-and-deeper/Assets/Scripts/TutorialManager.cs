using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public GameObject Tutorial1;
	public GameObject Tutorial2;
	public GameObject Tutorial3;

	//private static int _tutorialIndex = 0;

	public BaitController Bait;
	public GameManager GameManager;

	private Coroutine Tutorial2Coroutine;
	private Coroutine Tutorial3Coroutine;

	private void Start()
	{
		Tutorial1.SetActive(true);
		Tutorial2.SetActive(false);
		Tutorial3.SetActive(false);

		Bait.HitWater += OnBaitHitWater;
		GameManager.MenuOpened += OnMenuOpened;
	}

	private void Update()
	{
		if (Input.anyKeyDown && (Tutorial2Coroutine != null || Tutorial3Coroutine != null))
		{
			Time.timeScale = 1;
		}
	}

	private void OnMenuOpened()
	{
		if (Tutorial2Coroutine != null || Tutorial3Coroutine != null)
		{
			Tutorial1.SetActive(false);
			Tutorial2.SetActive(false);
			Tutorial3.SetActive(false);
		}
	}

	private void OnBaitHitWater()
	{
		Tutorial2Coroutine = StartCoroutine(TutorialPart2());
		Bait.HitWater -= OnBaitHitWater;
	}

	private IEnumerator TutorialPart2()
	{
		Bait.HookedFish += OnHookedFish;
		yield return new WaitForSecondsRealtime(0.5f);
		Tutorial1.SetActive(false);
		Tutorial2.SetActive(true);
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(5f);
		Tutorial2.SetActive(false);
		Time.timeScale = 1;
		Tutorial2Coroutine = null;
	}

	private void OnHookedFish()
	{
		Tutorial3Coroutine = StartCoroutine(TutorialPart3());
		Bait.HookedFish -= OnHookedFish;
	}

	private IEnumerator TutorialPart3()
	{
		yield return new WaitForSecondsRealtime(0.2f);
		Tutorial3.SetActive(true);
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(3f);
		Tutorial3.SetActive(false);
		Time.timeScale = 1;
		Tutorial3Coroutine = null;
	}
}
