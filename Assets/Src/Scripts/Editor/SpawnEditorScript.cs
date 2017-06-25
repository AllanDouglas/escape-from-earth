using UnityEngine;
using UnityEditor;
using Behaviour;

[CustomEditor(typeof(SpawnBehaviourScript))]
public class SpawnEditorScript : Editor
{

    SpawnBehaviourScript _spawn;

    void OnEnable()
    {
        _spawn = target as SpawnBehaviourScript;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spanw"))
        {
            _spawn.Spawn();
        }

    }

}
