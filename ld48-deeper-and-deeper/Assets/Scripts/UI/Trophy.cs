using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trophy : MonoBehaviour
{
	public Image _fishImage;
	public TextMeshProUGUI _fishName;

	public void SetTrophy(FishData fishData)
	{
		_fishImage.sprite = fishData.Sprite;
		_fishName.text = fishData.Name;
	}
}
