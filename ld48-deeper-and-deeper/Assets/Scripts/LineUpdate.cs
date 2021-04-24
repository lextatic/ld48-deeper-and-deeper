using UnityEngine;

public class LineUpdate : MonoBehaviour
{
	private enum LineState
	{

	}

	public GameObject Anchor;
	public GameObject Bait;
	public LineRenderer LineRenderer;

	private int _baitIndex;

	void Start()
	{
		Bait.GetComponent<BaitController>().HitWater += OnBaitHitWater;
		Bait.GetComponent<BaitController>().Restart += OnRestartBait;
		LineRenderer.SetPosition(0, Anchor.transform.position);
		_baitIndex = 1;
	}

	// Update is called once per frame
	void Update()
	{

		LineRenderer.SetPosition(_baitIndex, Bait.transform.position);
	}

	private void OnBaitHitWater()
	{
		LineRenderer.positionCount++;
		_baitIndex++;
	}

	private void OnRestartBait()
	{
		LineRenderer.positionCount--;
		_baitIndex--;
	}
}
