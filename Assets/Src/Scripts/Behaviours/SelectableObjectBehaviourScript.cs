using UnityEngine;
using System;
using Interfaces;

public class SelectableObjectBehaviourScript : MonoBehaviour
{

    public static event Action<ISelectable> OnClick;


    private ISelectable _ship;


    void Awake()
    {
        _ship = GetComponent<ISelectable>();
    }

    public void OnMouseDown()
    {   
        if (OnClick != null)
        {
            OnClick(_ship);
        }
    }

}
