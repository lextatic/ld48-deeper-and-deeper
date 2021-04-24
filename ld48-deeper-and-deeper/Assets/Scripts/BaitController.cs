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

	public float ControlModifier = 2f;
	public float BaitDrownSpeed = 5f;
	public float RecoilSpeed = 15f;
	public float TempForce = 670f;

	public float EndDistanceFromSurface = 3f;

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
					_rigidbody.AddForce(new Vector2(TempForce, TempForce));
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

				Input.GetAxis("Horizontal");

				_rigidbody.MovePosition(_rigidbody.position + new Vector2(Input.GetAxis("Horizontal") * ControlModifier * Time.deltaTime, -BaitDrownSpeed * Time.deltaTime));
				break;

			case BaitState.Recoil:

				Vector2 baitDistanceFromSurface = _waterHitPosition - _rigidbody.position;

				if (baitDistanceFromSurface.magnitude < EndDistanceFromSurface)
				{
					Restart?.Invoke();
					InitializeBait();
					return;
				}

				_rigidbody.MovePosition(_rigidbody.position + (baitDistanceFromSurface).normalized * Time.deltaTime * RecoilSpeed);

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
