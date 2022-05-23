using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] float flashDuration;
    [SerializeField] Material flashMaterial;
    Material originalMaterial;

    SpriteRenderer spriteRenderer;
    Coroutine flashRoutine;
    // Start is called before the first frame update

    void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame


    public void StartFlash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        StartCoroutine(Flash());

    }

    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }
}
