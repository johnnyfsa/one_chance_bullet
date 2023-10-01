using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public Health Health { get; private set; }

    public Transform Skeleton;

    [SerializeField] bool deactivateWhenDie = true;
    [SerializeField] bool destroyWhenDie = false;
    [SerializeField] Effect onDieEffectPrefab;
    public System.Action<Character> OnDie;

    public void Awake()
    {
        Health = GetComponent<Health>();       
    }

    public void OnEnable()
    {
        if (Health != null)
        {
            Health.OnDeath += Die;

            if (deactivateWhenDie)
            {
                Health.SetFullHealth();
            }
        }
    }

    public void OnDisable()
    {
        if (Health != null)
        {
            Health.OnDeath -= Die;
        }
    }

    public void Die()
    {
        if(onDieEffectPrefab != null)
            Pool.CreateObject(onDieEffectPrefab.gameObject, transform.position, transform.rotation).GetComponent<Effect>().Play();

        OnDie?.Invoke(this);

        if (deactivateWhenDie)
            this.gameObject.SetActive(false);

        if(destroyWhenDie)
            Destroy(this.gameObject);
    }
}
