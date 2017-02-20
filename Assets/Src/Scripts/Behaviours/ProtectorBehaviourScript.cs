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
	private float _speed;
	#endregion

	private Vector2 MovimentPosition;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Rotate ();

	}



	private void Rotate()
	{
		if (this._enemyTarget == null)
			return;
		
		var newRotation = Quaternion.LookRotation(transform.position - _enemyTarget.position, Vector3.forward);
		newRotation.x = 0;
		newRotation.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * _speed);
	}

	private void MoveTo()
	{
		
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
