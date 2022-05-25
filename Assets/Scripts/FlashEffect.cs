using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] float flashDuration;
    [SerializeField] float poisonFlashDuration;
    [SerializeField] [Range (1, 10)] int flashAmount;
    [SerializeField] [Range(1, 10)] int poisonFlashAmount;

    [SerializeField] Material flashMaterial;
    [SerializeField] Material poisonFlashMaterial;
    Material originalMaterial;

    SpriteRenderer spriteRenderer;
    Coroutine flashRoutine;
    // Start is called before the first frame update

    void Awake()
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

    public void StartPoisonFlash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        StartCoroutine(PoisonFlash());
    }

    IEnumerator Flash()
    {
        float tempValue = 0;

        while (tempValue != flashAmount)
        {
            spriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.material = originalMaterial;

            yield return new WaitForSeconds(flashDuration);

            tempValue++;
        }

        flashRoutine = null;
    }

    IEnumerator PoisonFlash()
    {
        float tempValue = 0;

        while (tempValue != poisonFlashAmount)
        {
            spriteRenderer.material = poisonFlashMaterial;

            yield return new WaitForSeconds(poisonFlashDuration);

            spriteRenderer.material = originalMaterial;

            yield return new WaitForSeconds(poisonFlashDuration);

            tempValue++;
        }

        flashRoutine = null;
    }
}
