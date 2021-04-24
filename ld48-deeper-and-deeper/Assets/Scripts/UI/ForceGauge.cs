using UnityEngine;

public class ForceGauge : MonoBehaviour
{
	public BaitController Bait;
	public SpriteMask SpriteMask;
	void Start()
	{
		SpriteMask.alphaCutoff = 0;
	}


	void Update()
	{
		SpriteMask.alphaCutoff = Bait.ForcePercentage;
	}
}
