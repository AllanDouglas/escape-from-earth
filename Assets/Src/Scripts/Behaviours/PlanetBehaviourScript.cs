using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlanetBehaviourScript : MonoBehaviour {

	[SerializeField]
	private int _hitPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.forward, -1, Space.Self);
	}	
}
