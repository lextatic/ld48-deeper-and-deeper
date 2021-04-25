using UnityEngine;

public class LineUpdate : MonoBehaviour
{
	public GameObject Anchor;
	public BaitController Bait;
	public LineRenderer LineRenderer;

	private int _baitIndex;

	void Start()
	{
		Bait.HitWater += OnBaitHitWater;
		Bait.Restart += OnRestartBait;
		LineRenderer.SetPosition(0, Anchor.transform.position);
		_baitIndex = 1;
	}

	// Update is called once per frame
	void Update()
	{

		LineRenderer.SetPosition(_baitIndex, Bait.transform.position);

		if (_baitIndex == 2)
		{
			var lerp = Mathf.Lerp(LineRenderer.GetPosition(_baitIndex - 1).x, Bait.transform.position.x, Time.deltaTime);
			LineRenderer.SetPosition(_baitIndex - 1, new Vector3(lerp, LineRenderer.GetPosition(_baitIndex - 1).y, LineRenderer.GetPosition(_baitIndex - 1).z));
			Bait.WaterHitXPosition = lerp;
		}
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
