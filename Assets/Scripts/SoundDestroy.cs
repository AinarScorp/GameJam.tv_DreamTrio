using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Bye());
    }

    IEnumerator Bye()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
