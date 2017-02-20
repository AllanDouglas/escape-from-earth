using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetBehaviourScript : MonoBehaviour, IDamageable {

	[SerializeField]
	private int _hitPoints;

	public int HitPoints 
	{
		get 
		{
			return _hitPoints;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.forward, -1, Space.Self);
	}	

	public void Damage(int damage)
	{
		this._hitPoints -= damage;
	}
}
