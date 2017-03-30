using UnityEngine;

[CreateAssetMenu(fileName = "New Gun",menuName= "Create new Gun")]
public class GunScript : ScriptableObject, Interfaces.IGun
{
	
	[SerializeField]
	private string Name;
	[SerializeField]
	private int _power;
	[SerializeField]
	private ProjectileBehaviourScript _projectilePrefab;

	private ProjectileBehaviourScript[] _projectilePool;

	#region IGun implementation
	public void Shoot ()
	{
		Debug.Log ("disparando");
	}

	public int Power {
		get {
			return _power;
		}
	}
	#endregion

	private void LoadProjectile()
	{

		_projectilePool = new ProjectileBehaviourScript[30];

		for (int i = 0; i < _projectilePool.Length; i++) 
		{
			_projectilePool [i] = Instantiate<ProjectileBehaviourScript> (this._projectilePrefab);

			_projectilePool [i].transform.localPosition = Vector2.zero;
			_projectilePool [i].transform.rotation = Quaternion.identity;

			_projectilePool [i].gameObject.SetActive (false);
		}


	}
}

