using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] [Range(0.1f, 30f)] float zoomInValue;
    [SerializeField] [Range(0.1f, 30f)] float zoomOutValue;
    [SerializeField] [Range (0.1f, 30f)] float zoomInTime;
    [SerializeField] [Range(0.1f, 30f)] float zoomOutTime;
    [SerializeField] AnimationCurve inCurve;
    [SerializeField] AnimationCurve outCurve;

    //[SerializeField] float amplitude;
    //[SerializeField] float frequency;
    //[SerializeField] float shakeTime;
    Coroutine shakeRoutine;
    CinemachineBasicMultiChannelPerlin noise;
    public static VirtualCamera Instance;

    void Awake()
    {
        if (virtualCamera == null)
            virtualCamera =GetComponent<CinemachineVirtualCamera>();
        Instance = this;
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().SubscribeToPlayerDeathPermanently(StartZoomOut);
        FindObjectOfType<PlayerHealth>().SubscribeToRevival(StartZoomIn);
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
    void StartZoomIn()
    {
        StartCoroutine(ZoomIn());
    }

    void StartZoomOut()
    {
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomIn()
    {
        float elapsedTime = 0;
        float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;;

        while (zoomInTime > elapsedTime)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, zoomInValue, inCurve.Evaluate(elapsedTime / zoomInTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = zoomInValue;

        yield return null;

    }

    IEnumerator ZoomOut()
    {
        float elapsedTime = 0;
        float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;

        while (zoomOutTime > elapsedTime)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, zoomOutValue, outCurve.Evaluate(elapsedTime / zoomOutTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = zoomOutValue;

        yield return null;
    }
}
