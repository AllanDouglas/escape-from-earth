using Interfaces;
using UnityEngine;
using System;

public class PlanetBehaviourScript : MonoBehaviour, IDamageable, IDestructive
{

    #region Inspector
    [SerializeField]
    private int _maxHitPoints = 10;
    [SerializeField]
    private Transform _body;
    [SerializeField]
    private Transform _rocket;
    [SerializeField]
    private float _rocketSpeed = 2;
    [SerializeField]
    private SpriteRenderer _atmosphere;
    [SerializeField]
    private Gradient _atmosphereColor;
    #endregion

    // events
    public Action OnDestroy;

    #region Properties
    /// <summary>
    /// Planet Hitpoinst
    /// </summary>
    public int HitPoints
    {
        get
        {
            return _hitPoints;
        }
    }
    #endregion

    // hit points
    private int _hitPoints;
    private ParticleSystem[] _pool;


    // start script
    private void Start()
    {
        _hitPoints = 0;
        _rocket.gameObject.SetActive(false);
        _pool = new ParticleSystem[_maxHitPoints / 2];
    }

    // Constant frame rate
    private void FixedUpdate()
    {
        _body.Rotate(Vector3.forward, -1, Space.Self);

        if (_rocket.gameObject.activeSelf)
        {
            MoveRocket();

            if (_rocket.position.y > 5)
            {
                _rocket.gameObject.SetActive(false);
                _rocket.localPosition = Vector2.zero;
            }
        }
    }


    /// <summary>
    /// Move the rocket
    /// </summary>
    private void MoveRocket()
    {
        _rocket.Translate(Vector2.up * Time.fixedDeltaTime * _rocketSpeed, Space.World);        
    }
    
    /// <summary>
    /// throw the rocket
    /// </summary>
    public void ThrowRocket()
    {
        _rocket.gameObject.SetActive(true);
    }

    /// <summary>
    /// Take Damage
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        this._hitPoints += damage;

        float percent = (_hitPoints * 100) / _maxHitPoints;
        percent *= .01f;

        _atmosphere.color = _atmosphereColor.Evaluate(percent);

        if (_hitPoints == _maxHitPoints)
        {
            Kill();
        }
    }

    /// <summary>
    /// Kill
    /// </summary>
    public void Kill()
    {
        if (OnDestroy != null) OnDestroy();
        FXManagerBehaviourScript.Instance.PlanetDestroyFX(transform.position);
        gameObject.SetActive(false);
    }
}
