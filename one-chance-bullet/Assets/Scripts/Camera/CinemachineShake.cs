using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] float defautIntensity = 1f;
    [SerializeField] float defautDuration = 1f;
    
    public void Shake()
    {
        Shake(defautIntensity, defautDuration);
    }

    public void Shake(float duration)
    {
        Shake(defautIntensity, duration);
    }
    
    public void Shake(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        StartCoroutine(ShakeTimer(duration));
    }

    private IEnumerator ShakeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
