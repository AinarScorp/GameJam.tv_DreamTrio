using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CordCircle))]
public class CordCircleEditor : Editor
{
    SerializedObject so;

    SerializedProperty propNewSize;
    SerializedProperty propTargetFollow;
    SerializedProperty propDefaultCordLength;
    SerializedProperty propScaleSpeed;
    SerializedProperty propSubtractCordAmount;
    SerializedProperty propCordLength;



    //cordLength


    private void OnEnable()
    {
        so = serializedObject;

        propNewSize = so.FindProperty("newRadius");
        propTargetFollow = so.FindProperty("target");
        propDefaultCordLength = so.FindProperty("defaultCordLength");
        propScaleSpeed = so.FindProperty("scaleSpeed");
        propSubtractCordAmount = so.FindProperty("subtractCordAmount");
        propCordLength = so.FindProperty("cordLength");
    }

    private void OnSceneGUI()
    {
        CordCircle cordCircle = target as CordCircle;
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(cordCircle.transform.position, Vector3.forward, propDefaultCordLength.floatValue/2, 2f);
    }
    public override void OnInspectorGUI()
    {
        CordCircle cordCircle = target as CordCircle;
        

        GUILayout.Space(10);
        GUILayout.Label("Here you can play with Size of the circle", EditorStyles.boldLabel);
        GUILayout.Space(5);

        
        using (new GUILayout.HorizontalScope())
        {
            using( new GUILayout.VerticalScope())
            {
                so.Update();
                EditorGUILayout.PropertyField(propNewSize);
                so.ApplyModifiedProperties();

                GUILayout.Space(10);

                using (new GUILayout.HorizontalScope())
                {

                    if (GUILayout.Button("Change Size Automatically", GUILayout.Width(200)))
                    {
                        cordCircle.ToggleAutoApplySize();
                    }
                    GUILayout.Space(10);

                    EditorGUILayout.Toggle(cordCircle.AutoApplySize);
                }
            }

            if (!cordCircle.AutoApplySize && GUILayout.Button($"Apply \n New Size", GUILayout.Height(50), GUILayout.Width(75)))
            {
                cordCircle.ApplyNewSize();
            }
        }


        GUILayout.Space(40);

        so.Update();
        EditorGUILayout.PropertyField(propTargetFollow);
        so.ApplyModifiedProperties();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Start Shrinking", GUILayout.Width(200), GUILayout.Height(25)))
            {
                cordCircle.EncircleTarget();
            }

        }
        GUILayout.Space(10);

        so.Update();
        EditorGUILayout.PropertyField(propDefaultCordLength);
        EditorGUILayout.PropertyField(propScaleSpeed);
        EditorGUILayout.PropertyField(propSubtractCordAmount);
        EditorGUILayout.PropertyField(propCordLength);

        so.ApplyModifiedProperties();

        if (cordCircle.AutoApplySize)
        {
            cordCircle.ApplyNewSize();

        }



    }
}
