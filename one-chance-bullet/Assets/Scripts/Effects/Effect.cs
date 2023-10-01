using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] GameObject[] effects;
    [SerializeField] float effectTimeLimit = 1f;

    public void Awake()
    {
        foreach (var effect in effects)
        {
            effect.SetActive(false);
        }
    }

    public void Play()
    {
        StartCoroutine(PlayCO());
    }

    private IEnumerator PlayCO()
    {
        foreach (var effect in effects)
        {
            effect.SetActive(true);
        }

        yield return new WaitForSeconds(effectTimeLimit);

        AfterPlay();
    }

    public void AfterPlay()
    {
        foreach (var effect in effects)
        {
            effect.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
