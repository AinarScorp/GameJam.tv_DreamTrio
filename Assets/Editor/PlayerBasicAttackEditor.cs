using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlayerBasicAttack))]
public class PlayerBasicAttackEditor : Editor
{
    SerializedObject so;
    SerializedProperty propAttackPointDistance;
    SerializedProperty propDrawPositionLines;
    SerializedProperty propDrawRadiusCircle;
    SerializedProperty propDrawAllCircles;

    SerializedProperty propAttackRadius;


    PlayerBasicAttack playerBasicAttack;
    float buttonHeight = 45f;
    float buttonWidth = 100f;


    private void OnEnable()
    {
        so = serializedObject;

        propAttackPointDistance = so.FindProperty("attackPointDistance");
        propAttackRadius = so.FindProperty("attackRadius");

        propDrawPositionLines = so.FindProperty("drawPositionLines");
        propDrawRadiusCircle = so.FindProperty("drawRadiusCircle");
        propDrawAllCircles = so.FindProperty("drawAllCircles");

        playerBasicAttack = target as PlayerBasicAttack;

    }
    private void OnSceneGUI()
    {
        Vector3 currentPos = playerBasicAttack.transform.position;
        float attackPointDistance = propAttackPointDistance.floatValue;

        if (propDrawPositionLines.boolValue)
        {
            DrawPositionLines(currentPos, attackPointDistance);
        }
        if (propDrawRadiusCircle.boolValue)
        {
            DrawHitCircles(currentPos, attackPointDistance);
        }

    }

    private void DrawHitCircles(Vector3 currentPos, float attackPointDistance)
    {
        float thinkness = 2f;

        Handles.color = Color.red;
        Vector3 newPos = new Vector3(currentPos.x + attackPointDistance, currentPos.y);
        Handles.DrawWireDisc(newPos, Vector3.forward, propAttackRadius.floatValue, thinkness);
        if (propDrawAllCircles.boolValue)
        {
            Handles.color = Color.yellow;

            newPos = new Vector3(currentPos.x - attackPointDistance, currentPos.y);
            Handles.DrawWireDisc(newPos, Vector3.forward, propAttackRadius.floatValue, thinkness);
            Handles.color = Color.green;

            newPos = new Vector3(currentPos.x, currentPos.y + attackPointDistance);
            Handles.DrawWireDisc(newPos, Vector3.forward, propAttackRadius.floatValue, thinkness);
            Handles.color = Color.magenta;

            newPos = new Vector3(currentPos.x, currentPos.y - attackPointDistance);
            Handles.DrawWireDisc(newPos, Vector3.forward, propAttackRadius.floatValue, thinkness);
        }
    }

    private void DrawPositionLines(Vector3 tranformPos, float attackPointDistance)
    {
        Handles.color = Color.cyan;
        float thinkness = 4f;

        Vector3 newPos = new Vector3(tranformPos.x + attackPointDistance, tranformPos.y);
        Handles.DrawLine(tranformPos, newPos, thinkness);
        newPos = new Vector3(tranformPos.x - attackPointDistance, tranformPos.y);
        Handles.DrawLine(tranformPos, newPos, thinkness);
        newPos = new Vector3(tranformPos.x, tranformPos.y - attackPointDistance);
        Handles.DrawLine(tranformPos, newPos, thinkness);
        newPos = new Vector3(tranformPos.x, tranformPos.y + attackPointDistance);
        Handles.DrawLine(tranformPos, newPos, thinkness);
    }

    public override void OnInspectorGUI()
    {

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                GUILayout.Label("Touch only if you're know what these are for",EditorStyles.boldLabel);
            }


            base.OnInspectorGUI();
        }
        GUILayout.Space(20);

        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {


            GUILayout.Label("Adjustments");
            GUILayout.Space(10);

            so.Update();

            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {

                EditorGUILayout.PropertyField(propAttackRadius,
                    new GUIContent("Attack Radius", "Inside circles enemies will be hit"));

                using (new GUILayout.HorizontalScope())
                {

                    if (!propDrawRadiusCircle.boolValue && GUILayout.Button("Show\n Attack circle", GUILayout.Width(100), GUILayout.Height(buttonHeight)))
                        playerBasicAttack.SetDrawRadiusCircle(true);

                    else if (propDrawRadiusCircle.boolValue)
                    {
                        if (GUILayout.Button("Hide\n Attack circles", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                        {
                            playerBasicAttack.SetDrawRadiusCircle(false);
                            playerBasicAttack.SetDrawAllCircles(false);
                        }

                        if (!propDrawAllCircles.boolValue &&
                            GUILayout.Button("Show all\n Attack circles", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                            playerBasicAttack.SetDrawAllCircles(true);

                        else if (propDrawAllCircles.boolValue &&
                            GUILayout.Button("Show one\n Attack circle", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
                            playerBasicAttack.SetDrawAllCircles(false);

                    }

                }
            }

            GUILayout.Space(20);
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.PropertyField(propAttackPointDistance,
                    new GUIContent("Point Distance", "Cyan line, At the end of these lines Attack circle will be placed"));

                using (new GUILayout.HorizontalScope())
                {

                    if (!propDrawPositionLines.boolValue &&
                        GUILayout.Button("Show \n Distance Lines", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))

                        playerBasicAttack.SetDrawPositionLines(true);

                    else if (propDrawPositionLines.boolValue &&
                        GUILayout.Button("Hide \n Distance Lines", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))

                        playerBasicAttack.SetDrawPositionLines(false);

                }
            }


            so.ApplyModifiedProperties();


        }

    }

}
