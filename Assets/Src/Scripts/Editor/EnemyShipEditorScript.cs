using UnityEngine;
using UnityEditor;
using Behaviour;

[CustomEditor(typeof(EnemyBehaviourScript))]

public class EnemyShipEditorScript : Editor
{

    EnemyBehaviourScript _enemyShip;

    private void OnEnable()
    {
        _enemyShip = target as EnemyBehaviourScript;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Kill Hahaha!"))
        {
            _enemyShip.Kill();
        }

    }
}