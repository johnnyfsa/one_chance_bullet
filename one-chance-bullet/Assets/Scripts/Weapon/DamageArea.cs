using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that create a Damage Area in the place of the Projectil, all the objects that collide with the Damage Area and have one the tags on the list will take damage. After the duration of the Damage Area the object is set to inactive.
public class DamageArea : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] float duration = 1.0f;
    [SerializeField] float interval = -1f;
    [SerializeField] Vector2 size = Vector2.one;
    [SerializeField] List<String> targetWithTag;

    private float timer = 0.0f;

    internal void Init(Vector2 size, float duration, float interval, float damage, List<string> targetWithTag)
    {
        transform.localScale = size;
        this.size = size;
        this.duration = duration;
        this.interval = interval;
        this.damage = damage;
        this.targetWithTag = targetWithTag;
    }

    private void OnEnable()
    {
        transform.localScale = size;
        Invoke(nameof(Deactivate), duration);
    }

    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetWithTag.Contains(other.tag))
        {
            StartCoroutine(DamageOverTimeCO(other));
        }        
    }

    //courotine to take damage to all the objects that collide with the Damage Area in a interval of time
    private IEnumerator DamageOverTimeCO(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        if (health == null)
        {
            yield break;
        }
        
        while (true)
        {
            health.TakeDamage(damage);
            
            //test if the collider object is still colliding with the Damage Area
            if (!other.IsTouching(GetComponent<Collider2D>()))
            {
                yield break;
            }

            if(interval <= 0)
            {
                yield break;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    
}
