using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        GUILayout.Space(20);
        if (GUILayout.Button("Save All Beat"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.SaveAllBeatObjects();
        }

        if (GUILayout.Button("Save All Bomb"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.SaveAllBombObjects();
        }
        GUILayout.Space(20);

        if (GUILayout.Button("Clear All Beat Lists"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.ClearAllBeatLists();
        }

        if (GUILayout.Button("Clear All Bomb Lists"))
        {
            BeatGenerator beatGenerator = (BeatGenerator)target;
            beatGenerator.ClearAllBombLists();
        }
    }
}
