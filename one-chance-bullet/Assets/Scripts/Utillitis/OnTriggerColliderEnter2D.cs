using System.Collections.Generic;
using UnityEngine;

public class OnTriggerColliderEnter2D : MonoBehaviour
{
    [SerializeField] protected List<string> onlyWithTag;

    [Space]
    [SerializeField] UnityEngine.Events.UnityEvent events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onlyWithTag.Find(t => collision.CompareTag(t)) == null)
            return;

        events.Invoke();
    }
}
