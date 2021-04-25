using DG.Tweening;
using System;
using UnityEngine;

public class BaitController : MonoBehaviour
{
	private enum BaitState
	{
		Disabled,
		Ready,
		Charging,
		Launched,
		InWater,
		Recoil,
		Hooked
	}

	public Action HitWater;
	public Action ChargingStarted;
	public Action BaitLaunched;
	public Action HookedFish;
	public Action<FishData> Restart;

	public GameObject Bait;
	public GameObject BaitAnchor;
	public GameObject Light;

	public float ControlModifier = 2f;
	public float BaitDrownSpeed = 5f;
	public float RecoilSpeed = 15f;
	public float MinChargeForce = 200;
	public float MaxChargeForce = 800; //670
	public float ForceChargeSpeed = 3f;//300f;

	public float EndDistanceFromSurface = 3f;

	public float WaterHitXPosition
	{
		set
		{
			_waterHitPosition = new Vector2(value, _waterHitPosition.y);
		}
	}

	private BaitState _baitState;

	private Rigidbody2D _rigidbody;

	private Vector2 _waterHitPosition;

	private float _currentForce;

	private Tweener _currentForceTweener;

	public float ForcePercentage
	{
		get
		{
			return (_currentForce - MinChargeForce) / (MaxChargeForce - MinChargeForce);
		}
	}

	public FishData HookedFishData { set; private get; }

	void Start()
	{
		_rigidbody = Bait.GetComponent<Rigidbody2D>();
		InitializeBait();
		_baitState = BaitState.Ready;
	}



	void Update()
	{
		switch (_baitState)
		{
			case BaitState.Ready:
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
				{
					_baitState = BaitState.Charging;
					_currentForce = MinChargeForce;
					ChargingStarted?.Invoke();

					_currentForceTweener = DOTween.To(() => _currentForce, x => _currentForce = x, MaxChargeForce, ForceChargeSpeed).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, false);
				}
				break;

			case BaitState.Charging:

				if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
				{
					_currentForceTweener.Kill();

					BaitLaunched?.Invoke();
					_rigidbody.simulated = true;
					_rigidbody.AddForce(new Vector2(_currentForce, _currentForce));
					//_rigidbody.AddForce(new Vector2(1300, 1300));
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

			case BaitState.Hooked:
			case BaitState.Recoil:

				Vector2 baitDistanceFromSurface = _waterHitPosition - _rigidbody.position;

				if (baitDistanceFromSurface.magnitude < EndDistanceFromSurface)
				{
					Restart?.Invoke(HookedFishData);
					InitializeBait();
					return;
				}

				_rigidbody.MovePosition(_rigidbody.position + (baitDistanceFromSurface).normalized * Time.deltaTime * RecoilSpeed);

				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_baitState == BaitState.Launched && collision.CompareTag("Water"))
		{
			_rigidbody.isKinematic = true;
			_rigidbody.velocity = new Vector2(0, 0);
			_baitState = BaitState.InWater;
			HitWater?.Invoke();
			Light.SetActive(true);
			_waterHitPosition = _rigidbody.position;
		}

		if (_baitState == BaitState.InWater && collision.CompareTag("Bottom"))
		{
			_rigidbody.velocity = new Vector2(0, 0);
			_baitState = BaitState.Recoil;
		}

		if ((_baitState == BaitState.InWater ||
			_baitState == BaitState.Recoil
			) && collision.CompareTag("Fish"))
		{
			_rigidbody.velocity = new Vector2(0, 0);
			_baitState = BaitState.Hooked;

			HookedFish?.Invoke();

			collision.GetComponent<FishController>().Baited(this);
		}
	}

	private void InitializeBait()
	{
		_rigidbody.transform.position = BaitAnchor.transform.position;
		_rigidbody.isKinematic = false;
		_rigidbody.simulated = false;
		_baitState = BaitState.Disabled;
		HookedFishData = null;
		Light.SetActive(false);
	}

	public void ReadyBait()
	{
		_baitState = BaitState.Ready;
	}
}
