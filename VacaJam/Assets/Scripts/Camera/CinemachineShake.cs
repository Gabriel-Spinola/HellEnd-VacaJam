using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private static CinemachineVirtualCamera s_virtualCamera;

    private static float s_shakeTimer;
    private static float s_shakeTimerTotal;
    private static float s_startingIntensity;

    private void Awake()
    {
        s_virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (s_shakeTimer > 0) {
            s_shakeTimer -= Time.deltaTime;

            if (s_shakeTimer <=0f) {
                var multiChannelPerlin = s_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(s_startingIntensity, 0f, 1 - (s_shakeTimer / s_shakeTimerTotal));
            }
        }
    }

    public static void ShakeCamera(float intensity, float time)
    {
        var multiChannelPerlin = s_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        multiChannelPerlin.m_AmplitudeGain = intensity;
        s_startingIntensity = intensity;
        s_shakeTimerTotal = time;
        s_shakeTimer = time;
    }
}
