using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected List<string> targetWithTag;
    [SerializeField] float duration;

    private void OnEnable() {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    protected abstract void OnPickup(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetWithTag.Contains(collision.tag))
        {
            OnPickup(collision);
            gameObject.SetActive(false);
        }
    }
}