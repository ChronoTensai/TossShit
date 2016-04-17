using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour {

	// Use this for initialization

	bool _angleSetted = false;
	bool _powerSetted = false;
	bool _settingSomething = false;
	bool _showGUI = true;

	float _actualAngle = 26;
	float _actualPower = 20;

	const float MAX_ANGLE = 90;
	const float MIN_ANGLE = 0;
	const float MAX_POWER = 100;
	const float MIN_POWER = 1;
	const float DELTA_ANGLE = 1; 
	const float DELTA_POWER = 1;
	const float TIME_CHANGE = 0.02f;


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if(!_angleSetted  || !_powerSetted)
			{
				if(!_settingSomething)
				{
					_settingSomething = true;
					InvokeRepeating("IncrementDecrementNumber", 0, TIME_CHANGE);
				}
				else
				{
					_settingSomething = false;
					CancelInvoke();
					if(!_angleSetted) _angleSetted = true;
					else if(!_powerSetted) _powerSetted = true;
					if(_angleSetted && _powerSetted)
					{
						Shoot();
					}
				}
			}			
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(0);
		}
	}

	public GameObject graphic;

	void Shoot()
	{
		Vector3  vectorAngle = new Vector3(0,0,_actualAngle);
		graphic.transform.localEulerAngles = vectorAngle;
		Rigidbody2D rg2d = graphic.GetComponent<Rigidbody2D>();
		rg2d.AddForce(graphic.transform.right * _actualPower * 100, ForceMode2D.Force);
		rg2d.AddTorque(_actualPower/2, ForceMode2D.Force);
		rg2d.gravityScale = 1;

	}

	enum CanonState{Increment, Decrement}
	private CanonState _state;

	void IncrementDecrementNumber()
	{
		if(!_angleSetted)
		{
			switch(_state)
			{
				case CanonState.Increment:
					_actualAngle += DELTA_ANGLE;
					if(_actualAngle >= MAX_ANGLE)
					{
						_actualAngle = MAX_ANGLE;
						_state = CanonState.Decrement;
					}
					break;
				case CanonState.Decrement:
					_actualAngle -= DELTA_ANGLE;
					if(_actualAngle <= MIN_ANGLE)
					{
						_actualAngle = MIN_ANGLE;
						_state = CanonState.Increment;
					}
					break;
			}
		}
		else if(!_powerSetted)
		{
			switch(_state)
			{
				case CanonState.Increment:
				_actualPower += DELTA_POWER;
				if(_actualPower >= MAX_POWER)
				{
					_actualPower = MAX_POWER;
					_state = CanonState.Decrement;
				}
				break;
			case CanonState.Decrement:
				_actualPower -= DELTA_POWER;
				if(_actualPower <= MIN_POWER)
				{
					_actualPower = MIN_POWER;
					_state = CanonState.Increment;
				}
				break;
			}
		}

	}

	public Rect CanonRect;
	void OnGUI()
	{
		if (_showGUI) 
		{		
			GUILayout.BeginArea (CanonRect);
			GUILayout.Label("Press R to Reset");
			GUILayout.Space(5);
			GUILayout.Label("Angle: " + _actualAngle);
			if (_angleSetted) 
			{
				GUILayout.Label("Power: " + _actualPower);
				if (_powerSetted) 
				{
					GUILayout.Space(5);
					GUILayout.Label("Height: " + Mathf.RoundToInt(graphic.transform.position.y > 0 ? graphic.transform.position.y : 0 ));
	                GUILayout.Label("Distance: " + Mathf.RoundToInt(graphic.transform.position.x - this.transform.position.x));
				}
			}

			GUILayout.EndArea();
		}
	}

	void hideGUI()
	{
		_showGUI = false;
	}
}
