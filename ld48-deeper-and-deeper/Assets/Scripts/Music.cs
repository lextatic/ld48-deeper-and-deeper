using DG.Tweening;
using UnityEngine;

public class Music : MonoBehaviour
{
	public float ZAngle;
	public float Speed = 0.5f;

	void Start()
	{
		DOTween.To(() => ZAngle, x => ZAngle = x, ZAngle + 15, Speed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, false);
	}

	private void Update()
	{
		transform.rotation = Quaternion.Euler(0, 0, ZAngle);
	}
}
