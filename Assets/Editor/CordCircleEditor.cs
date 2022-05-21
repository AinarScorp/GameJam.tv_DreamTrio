using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CordCircle))]
public class CordCircleEditor : Editor
{
    SerializedObject so;

    SerializedProperty propNewSize;
    private void OnEnable()
    {
        so = serializedObject;

        propNewSize = so.FindProperty("newRadius");
    }
    public override void OnInspectorGUI()
    {
        CordCircle cordCircle = target as CordCircle;
        GUILayout.Label("You might not want to touch it", EditorStyles.boldLabel);

        base.OnInspectorGUI();

        GUILayout.Space(10);
        GUILayout.Label("Here you can play with Size of the circle", EditorStyles.boldLabel);
        GUILayout.Space(5);

        using (new GUILayout.HorizontalScope())
        {
            so.Update();
            EditorGUILayout.PropertyField(propNewSize);
            so.ApplyModifiedProperties();
            if (GUILayout.Button("Apply New Size"))
            {
                cordCircle.ApplyNewSize();
            }
        }
        GUILayout.Space(10);

        if (GUILayout.Button("Encircle The Target", GUILayout.Width(200)))
        {
            cordCircle.EncircleTarget();
        }
        GUILayout.Space(10);

        using (new GUILayout.HorizontalScope())
        {

            if (GUILayout.Button("Change Size Automatically"))
            {
                cordCircle.ToggleAutoApplySize();
            }
            EditorGUILayout.Toggle(cordCircle.AutoApplySize);
        }
        if (cordCircle.AutoApplySize)
        {
            cordCircle.ApplyNewSize();

        }

    }
}
