using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimeOver : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool deactivateInstedOfDestoy = false;
    [SerializeField] Effect onDestroyEffectPrefab;

    void OnEnable()
    {
        Invoke("DestroyThis", duration);
    }

    private void DestroyThis()
    {
        PlayEffect();

        if (deactivateInstedOfDestoy == true)
        {
            this.gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }
    }   

    private void PlayEffect()
    {
        if(onDestroyEffectPrefab != null)
            Pool.CreateObject(onDestroyEffectPrefab.gameObject, transform.position, transform.rotation).GetComponent<Effect>().Play();
    }


}

