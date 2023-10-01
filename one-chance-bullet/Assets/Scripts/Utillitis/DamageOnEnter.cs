using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnter : MonoBehaviour
{
    [SerializeField] protected int damageAmount;
    [SerializeField] protected List<string> withTag;
     
    private void OnTriggerEnter2D(Collider2D other) {
        if (withTag.Contains(other.tag))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
