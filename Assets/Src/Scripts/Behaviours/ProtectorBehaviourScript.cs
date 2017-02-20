using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorBehaviourScript : MonoBehaviour {


	#region Inspector
	[SerializeField]
	private Transform _enemyTarget;
	[SerializeField]
	private int _hitPoints;
	[SerializeField]
	private float shootCandence;
	[SerializeField]
	private float _rotationSpeed = 10;
	[SerializeField]
	private float _moveSpeed = 2;
	#endregion

	#region Components

	private Rigidbody2D _rigidBody2D;

	#endregion
	[SerializeField]
	private Vector2 MovimentPosition;


	// Use this for initialization
	void Start () {

		this.MovimentPosition = transform.position;
		
		this._rigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		Rotate ();

		if (Input.GetMouseButtonDown (0)) 
		{
			// teste
			MovimentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		Move ();

	}



	private void Rotate()
	{
		if (this._enemyTarget == null)
			return;
		
		var newRotation = Quaternion.LookRotation(transform.position - _enemyTarget.position, Vector3.forward);
		newRotation.x = 0;
		newRotation.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _rotationSpeed);
	}


	private void Move()
	{

		if (Vector2.Distance (transform.position, this.MovimentPosition) < 0.1f)
			return;

		Vector2 newPosition = Vector2.Lerp(transform.position,MovimentPosition,Time.deltaTime * _moveSpeed);

		this._rigidBody2D.MovePosition (newPosition);

	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(this._enemyTarget == null)
			this._enemyTarget = other.transform;

	}

	public void OnTriggerExit2D(Collider2D other)
	{
		this._enemyTarget = null;
	}



}
