using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using UnityEngine.Serialization;

[Serializable]
public class DamageAreaProps {
    public Vector2 Size;
    public float Duration;
    public float Interval;
    public DamageArea Prefab;
}

public class Projectil : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float lifeTime = 1f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected DamageAreaProps damageArea;
    [SerializeField] protected List<string> targetWithTag;
    [SerializeField] bool deactivateOnExplode = true;
    [SerializeField] bool hitWhenLifeTime = false;
    
    [FormerlySerializedAs("onExplodePrefab")]
    [SerializeField] GameObject onHitEffect;
    [SerializeField] GameObject onLifeTimeEffect;

    [SerializeField] protected bool aimToTarget = false;

    //Unity Events When Explode, Conect Damage Area
    //public UnityEngine.Events OnExplode

    public UnityEvent OnExplode;

    protected virtual IEnumerator MoveCO()
    {
        while (true)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            yield return null;
        }
    }

    public void OnEnable() {
        
        if(hitWhenLifeTime == true)
        {
            Invoke(nameof(Hit), lifeTime);
        } else {
            Invoke(nameof(EndLifeTime), lifeTime);
        }
        
        if(aimToTarget == true)
        {
            Aim();
        }
        StartCoroutine(MoveCO());
    }

    private void Aim()
    {
        Transform[] targets = GameObject.FindGameObjectsWithTag(targetWithTag[0]).Select(go => go.transform).ToArray();
        Transform closestTarget = targets.OrderBy(t => Vector3.Distance(transform.position, t.position)).FirstOrDefault();
        if (closestTarget != null)
        {
            transform.up = closestTarget.position - transform.position;
        }
    }

    private void OnDisable() {
        CancelInvoke();
        StopAllCoroutines();
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        if (targetWithTag.Contains(other.tag))
        {
            var health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Hit();
        }
    }

    protected void Hit()
    {
        if(onHitEffect != null)
            Pool.CreateObject(onHitEffect, transform.position, transform.rotation);

        if(damageArea.Prefab != null)
        {
            var damageAreaInstance = Pool.CreateObject(damageArea.Prefab.gameObject, transform.position, transform.rotation).GetComponent<DamageArea>();
            damageAreaInstance.Init(damageArea.Size, damageArea.Duration, damageArea.Interval, damage, targetWithTag);
        }
        OnExplode?.Invoke();

        if (deactivateOnExplode == true)
        {
            this.gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }

    } 

    private void EndLifeTime() {
        if(onLifeTimeEffect != null)
            Pool.CreateObject(onLifeTimeEffect, transform.position, transform.rotation);

        if (deactivateOnExplode == true)
        {
            this.gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }
    }  
}

