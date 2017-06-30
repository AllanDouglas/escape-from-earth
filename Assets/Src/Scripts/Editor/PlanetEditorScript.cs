using UnityEngine;
using UnityEditor;
using Behaviour;

[CustomEditor(typeof(PlanetBehaviourScript))]

public class PlanetEditorScript : Editor
{
    PlanetBehaviourScript _planet;

    private void OnEnable()
    {
        _planet = target as PlanetBehaviourScript; 
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Take Damage"))
        {
            _planet.Damage(1);
        }

        if (GUILayout.Button("Throw Rocket"))
        {
            _planet.ThrowRocket();
        }
    }
}
