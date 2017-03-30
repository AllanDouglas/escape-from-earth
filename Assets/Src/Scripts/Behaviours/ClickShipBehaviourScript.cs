using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent (typeof(ShipBehaviourScript))]
public class ClickShipBehaviourScript : MonoBehaviour {

	public static event Action<ShipBehaviourScript> OnClick;


	private ShipBehaviourScript _ship;


	void Awake()
	{
		_ship = GetComponent<ShipBehaviourScript> ();
	}

	public void OnMouseDown()
	{
		if (OnClick != null) 
		{
			OnClick (_ship);
		}

	}
	
}
