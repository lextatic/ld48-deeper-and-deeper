using UnityEngine;

public class FishController : MonoBehaviour
{
	public Transform LeftBoundsPosition;
	public Transform RightBoundsPosition;

	public float Speed = 2f;

	private bool _moveRight = true;
	private bool _hasBeenHooked = false;
	private BaitController _bait;

	private void Start()
	{
		var randomXPosition = Random.Range(LeftBoundsPosition.position.x, RightBoundsPosition.position.x);
		transform.position = new Vector3(randomXPosition, transform.position.y, transform.position.z);

		if (Random.Range(0, 2) == 1)
		{
			Flip();
		}
	}

	void Update()
	{
		if (_hasBeenHooked) return;

		if (_moveRight)
		{
			transform.Translate(Speed * Time.deltaTime, 0, 0);
			if (transform.position.x >= RightBoundsPosition.position.x)
			{
				Flip();
			}
		}
		else
		{
			transform.Translate(-Speed * Time.deltaTime, 0, 0);
			if (transform.position.x <= LeftBoundsPosition.position.x)
			{
				Flip();
			}
		}
	}

	void Flip()
	{
		_moveRight = !_moveRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
	}

	public void Baited(BaitController bait)
	{
		_bait = bait;
		transform.parent = _bait.transform;
		transform.localPosition = Vector3.zero;
		transform.eulerAngles = new Vector3(0, 0, _moveRight ? 90f : -90f);
		_hasBeenHooked = true;

		bait.Restart += DespawnFish;
	}

	public void DespawnFish()
	{
		_bait.Restart -= DespawnFish;
		Destroy(gameObject);
	}
}
