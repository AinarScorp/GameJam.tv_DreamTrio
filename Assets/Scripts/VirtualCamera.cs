using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Transform player;

    //Screenshake Values for light attack
    [SerializeField] float lightAttackAmplitude;
    [SerializeField] float lightAttackFrequency;
    [SerializeField] float lightAttackShakeTime;

    //Screenshake Values for player hit
    [SerializeField] float hitAmplitude;
    [SerializeField] float hitFrequency;
    [SerializeField] float hitShakeTime;

    float startingCameraValue;

    [SerializeField] [Range(0.1f, 30f)] float zoomInValue;
    [SerializeField] [Range(0.1f, 30f)] float zoomOutValue;
    [SerializeField] [Range(0.1f, 30f)] float zoomInTime;
    [SerializeField] [Range(0.1f, 30f)] float zoomOutTime;
    [SerializeField] [Range(0.1f, 30f)] float centerTime;
    [SerializeField] [Range(0.1f, 30f)] float revivalWait;
    [SerializeField] Transform ghostTransform;
    [SerializeField] AnimationCurve inCurve;
    [SerializeField] AnimationCurve outCurve;

    Coroutine shakeRoutine;
    CinemachineBasicMultiChannelPerlin noise;
    public static VirtualCamera Instance;

    void Awake()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        Instance = this;
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().SubscribeToPlayerDeathPermanently(StartZoomOut);
        FindObjectOfType<PlayerHealth>().SubscribeToRevival(StartZoomIn);
        startingCameraValue = virtualCamera.m_Lens.OrthographicSize;
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void StartShake(float amplitude, float frequency, float shakeTime)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(Shake(amplitude, frequency, shakeTime));
    }

    public void PlayerHitShake()
    {
        StartShake(hitAmplitude, hitFrequency, hitShakeTime);
    }

    public void LightAttackShake()
    {
        StartShake(lightAttackAmplitude, lightAttackFrequency, lightAttackShakeTime);
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
        virtualCamera.Follow = player.transform;
        StartCoroutine(ZoomIn(zoomInValue));
    }

    void StartZoomOut()
    {
        virtualCamera.Follow = ghostTransform;
        StartCoroutine(ZoomOut(zoomOutValue));
    }

    IEnumerator ZoomIn(float endValue)
    {
        float elapsedTime = 0;
        float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize; ;

        while (zoomInTime > elapsedTime)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, endValue, inCurve.Evaluate(elapsedTime / zoomInTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = zoomInValue;

        yield return new WaitForSeconds(revivalWait);
        yield return ZoomOut(startingCameraValue);

    }
    //IEnumerator ZoomIn()
    //{
    //    float elapsedTime = 0;
    //    float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize; ;

    //    while (zoomInTime > elapsedTime)
    //    {
    //        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, zoomInValue, inCurve.Evaluate(elapsedTime / zoomInTime));
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    virtualCamera.m_Lens.OrthographicSize = zoomInValue;

    //    yield return null;

    //}
    IEnumerator ZoomOut(float endValue)
    {
        float elapsedTime = 0;
        float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;

        while (zoomOutTime > elapsedTime)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, endValue, outCurve.Evaluate(elapsedTime / zoomOutTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = endValue;

        yield return null;
    }
    //IEnumerator ZoomOut(float endValue)
    //{
    //    float elapsedTime = 0;
    //    float currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;

    //    while (zoomOutTime > elapsedTime)
    //    {
    //        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthoSize, zoomOutValue, outCurve.Evaluate(elapsedTime / zoomOutTime));
    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }

    //    virtualCamera.m_Lens.OrthographicSize = zoomOutValue;

    //    yield return null;
    //}
}
