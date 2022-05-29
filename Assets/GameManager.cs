using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameStarted;


    private void Start()
    {
        if (!Application.isEditor)
            Cursor.visible = false;
    }

    public void SpacePressed()
    {
        if (gameStarted == true)
            return;
        FindObjectOfType<WaveManager>().NextWave();

    }
}
