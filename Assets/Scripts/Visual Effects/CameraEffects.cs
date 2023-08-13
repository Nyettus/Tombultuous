using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera VC;
    private float shakeTimer;
    [SerializeField]
    private CinemachineBasicMultiChannelPerlin perlin;

    private void Awake()
    {
        VC = GetComponent<CinemachineVirtualCamera>();
        perlin = VC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void DashShake(float intensity, float time = 0.1f)
    {
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = Time.time+time;
    }

    private void Update()
    {
        if (shakeTimer < Time.time)
        {
            perlin.m_AmplitudeGain = 0f;
        }
    }

}
