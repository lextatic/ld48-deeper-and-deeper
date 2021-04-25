using UnityEngine;

public class ForceGauge : MonoBehaviour
{
	public BaitController Bait;
	public SpriteMask SpriteMask;
	void Start()
	{
		SpriteMask.alphaCutoff = 0;
		Bait.ChargingStarted += OnChargingStarted;
		Bait.BaitLaunched += OnBaitLaunched;
		gameObject.SetActive(false);
	}

	void Update()
	{
		SpriteMask.alphaCutoff = Bait.ForcePercentage;
	}

	private void OnChargingStarted()
	{
		gameObject.SetActive(true);
	}
	private void OnBaitLaunched()
	{
		gameObject.SetActive(false);
		SpriteMask.alphaCutoff = 0;
	}

}
