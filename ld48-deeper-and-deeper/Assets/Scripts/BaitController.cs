using System;
using UnityEngine;

public class BaitController : MonoBehaviour
{
	private enum BaitState
	{
		InHand,
		Launched,
		InWater,
		Recoil
	}

	public Action HitWater;
	public Action Restart;

	public GameObject Bait;
	public GameObject BaitAnchor;

	private BaitState _baitState;

	private Rigidbody2D _rigidbody;

	private Vector2 _waterHitPosition;

	void Start()
	{
		_rigidbody = Bait.GetComponent<Rigidbody2D>();
		InitializeBait();
	}


	void Update()
	{
		switch (_baitState)
		{
			case BaitState.InHand:
				if (Input.GetKeyDown(KeyCode.Space))
				{
					_rigidbody.simulated = true;
					_rigidbody.AddForce(new Vector2(670, 670));
					_baitState = BaitState.Launched;
				}
				break;

			case BaitState.InWater:

				break;
		}
	}

	private void FixedUpdate()
	{
		switch (_baitState)
		{
			case BaitState.InWater:
				_rigidbody.MovePosition(_rigidbody.position + new Vector2(0, -5 * Time.deltaTime));
				break;

			case BaitState.Recoil:

				//Vector2 baitAnchorPosition = new Vector2(BaitAnchor.transform.position.x, BaitAnchor.transform.position.y);
				Vector2 baitDistanceFromSurface = _waterHitPosition - _rigidbody.position;

				if (baitDistanceFromSurface.magnitude < 3f)
				{
					Restart?.Invoke();
					InitializeBait();
					return;
				}

				_rigidbody.MovePosition(_rigidbody.position + (baitDistanceFromSurface).normalized * Time.deltaTime * 15);

				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log($"OnTriggerEnter2D {collision.name}");

		if (_baitState == BaitState.Launched && collision.CompareTag("Water"))
		{
			//_rigidbody.simulated = false;
			_rigidbody.isKinematic = true;
			_rigidbody.velocity = new Vector2(0, 0);
			_baitState = BaitState.InWater;
			HitWater?.Invoke();

			_waterHitPosition = _rigidbody.position;
		}

		if (_baitState == BaitState.InWater && collision.CompareTag("Bottom"))
		{
			_rigidbody.velocity = new Vector2(0, 0);
			_baitState = BaitState.Recoil;
		}
	}

	private void InitializeBait()
	{
		_rigidbody.transform.position = BaitAnchor.transform.position;
		_rigidbody.isKinematic = false;
		_rigidbody.simulated = false;
		_baitState = BaitState.InHand;
	}
}
