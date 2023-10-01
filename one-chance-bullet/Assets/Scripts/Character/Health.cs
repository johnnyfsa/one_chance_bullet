using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public System.Action OnDamage = delegate { };
    public System.Action OnHeal = delegate { };
    public System.Action OnHealthChange = delegate { };
    public System.Action OnDeath = delegate { };

    public UnityEvent OnDeathEvents;
    public UnityEvent OnDamageEvents;

    [SerializeField]
    float healthValue;

    public float HealthValue {
        get => healthValue;
        set {

            var oldValue = healthValue;
            healthValue = Mathf.Clamp(value, 0, MaxHealth);

            if(healthValue != oldValue)
            {
                OnHealthChange?.Invoke();
            }

            if (healthValue <= 0)
            {
                OnDeath?.Invoke();
                OnDeathEvents.Invoke();
            }
        }
    }

    public bool HasLife => HealthValue > 0;


    public void OnEnable()
    {
        SetFullHealth();
    }

    public void SetFullHealth()
    {
        HealthValue = MaxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if(damageAmount <= 0) {
            return;
        }

        HealthValue -= damageAmount;

        OnDamageEvents.Invoke();
        OnDamage?.Invoke();
    }

    public void Heal(float healAmount)
    {
       if(healAmount <= 0) {
            return;
        }

        HealthValue += healAmount;

        OnHeal?.Invoke();
    }

    public bool IsDead => HealthValue <= 0;

}
