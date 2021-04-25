using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSprite : MonoBehaviour
{
	public Image UIImage;
	public Sprite Sprite1;
	public Sprite Sprite2;

	void Start()
	{

	}

	private void OnEnable()
	{
		UIImage.sprite = Sprite1;
		StartCoroutine(ToggleSprite());
	}

	private IEnumerator ToggleSprite()
	{
		yield return new WaitForSecondsRealtime(.5f);
		UIImage.sprite = Sprite2;
		yield return new WaitForSecondsRealtime(.5f);
		UIImage.sprite = Sprite1;
		StartCoroutine(ToggleSprite());
	}

}
