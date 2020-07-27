using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator))]
public class DungeonEditor : Editor {

    public override void OnInspectorGUI() {
        DungeonGenerator dungeon = target as DungeonGenerator;

        if (DrawDefaultInspector()) {
            dungeon.GenerateDungeon();
        }

        if (GUILayout.Button("Generate Dungeon")) {
            dungeon.GenerateDungeon();
        }
    }
}
