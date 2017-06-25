using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using System;

public class PlanetBehaviourScript : MonoBehaviour, IDamageable
{

    #region Inspector
    [SerializeField]
    private int _hitPoints;
    #endregion

    // events
    public Action OnDestroy;

    public int HitPoints
    {
        get
        {
            return _hitPoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, -1, Space.Self);
    }

    public void Damage(int damage)
    {
        this._hitPoints -= damage;

        if (HitPoints == 0)
        {
            if (OnDestroy != null) OnDestroy();
        }
    }
   
}
