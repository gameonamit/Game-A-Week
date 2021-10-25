using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BeatGenerator))]
public class BeatEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("GenerateBeat"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.GenerateBeats();
        }

        if (GUILayout.Button("Remove All Beats"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.ClearAllBeatObjects();
        }
    }
}
