using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update() {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                //Timer over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 -(shakeTimer / shakeTimerTotal));
            }
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }
}
