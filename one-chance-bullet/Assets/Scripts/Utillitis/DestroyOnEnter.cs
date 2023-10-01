using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    [SerializeField] protected List<string> withTag;
    [SerializeField] bool deactivateInstedOfDestoy = false;
    [SerializeField] Effect onDestroyEffectPrefab;

    private void OnTriggerEnter2D(Collider2D other) {
        if (withTag.Contains(other.tag))
        {
            DestroyThis();
        }
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
