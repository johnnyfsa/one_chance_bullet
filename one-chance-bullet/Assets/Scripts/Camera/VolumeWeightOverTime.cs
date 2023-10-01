using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeWeightOverTime : MonoBehaviour
{
    [SerializeField] float minWeight = 0f;
    [SerializeField] float maxWeight = 1f;
    [SerializeField] float defautDuration = 1f;
    
    public void Play() {
        Play(defautDuration);       
    }

    public void Play(float duration) {
        StartCoroutine(PlayCoroutine(duration));        
    }

    private IEnumerator PlayCoroutine(float duration)
    {
        float timer = 0f;
        var volume = GetComponent<Volume>();
        var halfDuration = duration / 2f;
        var inverseDuration = 1f / halfDuration;
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(minWeight, maxWeight, timer * inverseDuration);
            volume.weight = weight;
            yield return null;
        }

        timer = 0f;
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float weight = Mathf.Lerp(maxWeight, minWeight, timer * inverseDuration);
            volume.weight = weight;
            yield return null;
        }
    }
}
