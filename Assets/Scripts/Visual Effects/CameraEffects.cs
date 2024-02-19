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
    private CinemachinePOV freelook;
    private float lookSens;

    private void Awake()
    {
        VC = GetComponent<CinemachineVirtualCamera>();
        perlin = VC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        freelook = VC.GetCinemachineComponent<CinemachinePOV>();
        lookSens = freelook.m_VerticalAxis.m_MaxSpeed;
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
        WhenPaused();

    }

    private void WhenPaused()
    {
        if (GameManager._.ToggleInputs())
        {
            freelook.m_VerticalAxis.m_MaxSpeed = 0;
            freelook.m_HorizontalAxis.m_MaxSpeed = 0;
        }
        else if(freelook.m_VerticalAxis.m_MaxSpeed != lookSens)
        {
            freelook.m_VerticalAxis.m_MaxSpeed = lookSens;
            freelook.m_HorizontalAxis.m_MaxSpeed = lookSens;
        }
    }

}
