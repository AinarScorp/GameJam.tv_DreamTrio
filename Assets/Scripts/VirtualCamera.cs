using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;
    //[SerializeField] float amplitude;
    //[SerializeField] float frequency;
    //[SerializeField] float shakeTime;
    Coroutine shakeRoutine;
    CinemachineBasicMultiChannelPerlin noise;
    public static VirtualCamera Instance;

    void Awake() => Instance = this;

    private void Start()
    {
        noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void StartShake(float amplitude, float frequency, float shakeTime)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(Shake(amplitude, frequency, shakeTime));
    }

    IEnumerator Shake(float amplitude, float frequency, float shakeTime)
    {
        float elapsedTime = 0;

        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        while (shakeTime > elapsedTime)
        {
            //noise.m_AmplitudeGain = Mathf.Lerp(amplitude, 0, elapsedTime / shakeTime);
            //noise.m_FrequencyGain = Mathf.Lerp(frequency, 0, elapsedTime / shakeTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;

        shakeRoutine = null;

       yield return null;
    }
}
