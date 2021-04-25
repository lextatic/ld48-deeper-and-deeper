using DG.Tweening;
using UnityEngine;

public class Light : MonoBehaviour
{
	public SpriteMask lightMask;

	void Start()
	{
		DOTween.To(() => lightMask.alphaCutoff, x => lightMask.alphaCutoff = x, 0.01f, 0.05f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, false);
	}
}
