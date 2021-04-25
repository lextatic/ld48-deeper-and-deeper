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
		LineRenderer.SetPosition(_baitIndex, new Vector3(Bait.transform.position.x, -1.7f, 0));
		LineRenderer.positionCount++;
		_baitIndex++;
	}

	private void OnRestartBait(FishData fishData)
	{
		LineRenderer.positionCount--;
		_baitIndex--;
	}
}
