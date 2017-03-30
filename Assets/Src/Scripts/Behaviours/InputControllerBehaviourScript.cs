using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerBehaviourScript : MonoBehaviour {


	private ShipBehaviourScript objectSelected = null;

	// Use this for initialization
	void Start () {

		ClickShipBehaviourScript.OnClick += MouseClickObjectHandler;

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButton (0) & objectSelected != null) {

			Vector2 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			MoveObject (position);


		}

	}


	private void MoveObject(Vector2 position)
	{
		this.objectSelected.MoveTo (position);

	}


	private void MouseClickObjectHandler(ShipBehaviourScript ship)
	{

		this.objectSelected = ship;

		Debug.Log (ship + " Selected");

	}
}
