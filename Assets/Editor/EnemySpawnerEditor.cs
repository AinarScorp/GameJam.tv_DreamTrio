using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]

public class EnemySpawnerEditor : Editor
{


    SerializedProperty propEnemiesSpawn;

    private void OnEnable()
    {
        propEnemiesSpawn = serializedObject.FindProperty("enemiesSpawn");
    }

    public override void OnInspectorGUI()
    {
        EnemySpawner enemySpawner = target as EnemySpawner;
        base.OnInspectorGUI();

        GUILayout.Space(20);


        using (new GUILayout.HorizontalScope())
        {

            if (GUILayout.Button("Start spanwing\n Enemies", GUILayout.Width(110), GUILayout.Height(50)))
            {
                enemySpawner.StartSpawningEnemies();
            }
            GUILayout.Space(10);

            if (GUILayout.Button("Stop spanwing\n Enemies", GUILayout.Width(110), GUILayout.Height(50)))
            {
                enemySpawner.StopSpawningEnemies();
            }
        }

        GUILayout.Space(10);
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Enemeis spawn: ");
            EditorGUILayout.Toggle(propEnemiesSpawn.boolValue);

        }


    }
}
