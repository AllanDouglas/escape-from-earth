using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourScript : MonoBehaviour , IDamageable {

	#region Inspector
	[SerializeField]
	private float _velocity;
	[SerializeField]
	private float _maxDistance;
	#endregion

	#region Variables Controller

	private Vector2 _startPosition;

	public int HitPoints {
		get ;
		private set;
	}

	#endregion

	#region Components

	private Collider2D _collider2d;
	private Rigidbody2D _rigidbody2d;

	#endregion

	void Awake()
	{
		this._rigidbody2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {



		//this._collider2d = GetComponent<Collider2D> ();
			
		_startPosition = transform.position;



	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (transform.position, _startPosition) >= _maxDistance) 
		{
			gameObject.SetActive (false);
		}

	}

	#region IDamageable implementation

	public void Damage (int damage)
	{
		this.HitPoints -= damage;
	}



	#endregion

	public void Shoot()
	{
		gameObject.SetActive (true);
		this._rigidbody2d.velocity = transform.up * _velocity;
	}

		


}
