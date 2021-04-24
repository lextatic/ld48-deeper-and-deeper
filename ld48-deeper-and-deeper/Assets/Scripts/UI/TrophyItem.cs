using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyItem : MonoBehaviour
{
	public Image _fishImage;
	public TextMeshProUGUI _fishName;

	private Color lowAlpha = new Color(1f, 1f, 1f, 0.1f);
	private Color fullAlpha = new Color(1f, 1f, 1f, 1f);

	public void InitializeTrophy(FishData fishData)
	{
		_fishImage.color = lowAlpha;
		_fishImage.sprite = fishData.Sprite;
		_fishName.text = fishData.Name;
	}

	public void SetTrophy()
	{
		_fishImage.color = fullAlpha;
	}
}
