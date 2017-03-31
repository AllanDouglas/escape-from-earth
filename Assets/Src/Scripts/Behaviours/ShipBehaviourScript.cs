using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviourScript : MonoBehaviour, Interfaces.IDamageable {


	#region Inspector
	[SerializeField]
	private Transform _enemyTarget;
	[SerializeField]
	private int _hitPoints;
	[SerializeField]
	private float _shootCadence = .5f;
	[SerializeField]
	private float _rotationSpeed = 10f;
	[SerializeField]
	private float _moveSpeed = 2f;
	[SerializeField]
	private ProjectileBehaviourScript _projectilePrefab;
	[SerializeField]
	private float _vision = 1f;
	[SerializeField]
	private CircleCollider2D _visionCollider;
	[SerializeField]
	private string _enemyTag;
	[SerializeField]
	private string _enemyLayer;
	#endregion

	#region Components

	private Rigidbody2D _rigidBody2D;

	#endregion

	#region Properties


	public int HitPoints
	{
		get 
		{
			return this._hitPoints;
		}
	}

	private Vector2 MovimentPosition;

	private bool _moving = false;

	private float _shootCadenceController = 1f;

	private ProjectileBehaviourScript[] _projectilePool;

	#endregion

	// Use this for initialization
	void Start () {

		this.MovimentPosition = transform.position;
		
		this._rigidBody2D = GetComponent<Rigidbody2D> ();

		_visionCollider.radius = _vision;

		LoadProjectile ();

	}
	
	// Update is called once per frame
	void Update () {

		if (this._enemyTarget != null && _enemyTarget.gameObject.activeSelf  &!_moving )
			Rotate (this._enemyTarget.position);

		if(_moving)
			Move ();

		if (this._enemyTarget != null & _shootCadenceController >= _shootCadence){
			Shoot ();
			_shootCadenceController = 0.0f; 
		}

		_shootCadenceController += Time.deltaTime;

		_visionCollider.radius = _vision;

	}

	private void LoadProjectile()
	{

		_projectilePool = new ProjectileBehaviourScript[30];

		for (int i = 0; i < _projectilePool.Length; i++) 
		{
			_projectilePool [i] = Instantiate<ProjectileBehaviourScript> (this._projectilePrefab,transform.position,Quaternion.identity);

			_projectilePool [i].gameObject.SetActive (false);
		}


	}

	private void Shoot()
	{
		
		
		RaycastHit2D hit = Physics2D.Raycast (
			transform.position, 
			transform.up, 
			_vision,LayerMask.GetMask(_enemyLayer));

		Debug.DrawRay (transform.position, transform.up * _vision );



		if ( hit.collider == null )
			return;

		if (!hit.transform.CompareTag (_enemyTag))
			return;

		ProjectileBehaviourScript projectile = null;

		for (int i = 0; i < _projectilePool.Length; i++) 
		{
			if (_projectilePool [i].gameObject.activeSelf == false) 
			{
				projectile = _projectilePool [i];
				projectile.gameObject.tag = gameObject.tag;
				projectile.transform.position = transform.position;
				projectile.transform.rotation = transform.rotation;
				projectile.Shoot ();
				break;

			}
		}

	}	


	private void Rotate(Vector3 targetPosition)
	{
		
		var deltaRotation = Quaternion.LookRotation(transform.position - targetPosition, Vector3.forward);
		deltaRotation.x = 0;
		deltaRotation.y = 0;

		transform.rotation = Quaternion.Slerp(transform.rotation, deltaRotation, Time.deltaTime * _rotationSpeed);
	}

	public void MoveTo(Vector2 position)
	{

		if (Vector2.Distance (position, this.MovimentPosition) < 1)
			return;

		this._moving = true;

		this.MovimentPosition = position;

	}

	public void Stop()
	{
		_moving = false;
	}

	private void Move()
	{

		if (Vector2.Distance (transform.position, this.MovimentPosition) < 0.1f) {
			this._moving = false;
			return;
		}
		var deltaRotation = Quaternion.LookRotation(transform.position - (Vector3) MovimentPosition, Vector3.forward);
		deltaRotation.x = 0;
		deltaRotation.y = 0;

		transform.rotation = deltaRotation;



		this._rigidBody2D.MovePosition ((transform.position + transform.up * Time.deltaTime * _moveSpeed));

	}



	#region IDamageable implementation

	public void Damage (int damage)
	{
		this._hitPoints -= damage;

		if (_hitPoints <= 0) 
		{
			gameObject.SetActive (false);
		}
			
	}


	#endregion

	#region Collision Methods



	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("target na area");

		if (!other.gameObject.CompareTag (_enemyTag))
			return;

		if(this._enemyTarget == null)
			this._enemyTarget = other.transform;

	}

	public void OnTriggerExit2D(Collider2D other)
	{

		Debug.Log ("target fora area");

		if(other.transform == _enemyTarget)
			this._enemyTarget = null;
	}

	#endregion


}
