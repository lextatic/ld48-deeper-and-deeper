using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData")]
public class FishData : ScriptableObject
{
	public string Name;
	public Sprite Sprite;
	public float Speed;
}
