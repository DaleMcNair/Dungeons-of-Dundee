using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class RoomEditor : Editor {

    public override void OnInspectorGUI() {
        RoomGenerator room = target as RoomGenerator;

        if (DrawDefaultInspector()) {
            room.GenerateRoom();
        }

        if (GUILayout.Button("Generate Room")) {
            room.GenerateRoom();
        }
    }
}
