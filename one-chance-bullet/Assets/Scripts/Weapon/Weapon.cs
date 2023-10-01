using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Weapon : MonoBehaviour {
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float startAttackAfterDelay = -1f;
    [SerializeField] protected GameObject projectiePrefab;
    [SerializeField] protected Transform shootPoint;

    //How many seconds the weapon stays with the player
    [SerializeField] float duration = 10f;

    public float CooldownTime { get => cooldownTime; protected set => cooldownTime = value; }
    public float StartAttackAfterDelay { get => startAttackAfterDelay; protected set => startAttackAfterDelay = value; }
    public float Duration { get => duration; protected set => duration = value; }

    //The weapon can continue to perform attacks while isAttacking is true
    protected bool isAttacking = false;
    public bool IsAttacking
    {
        get => isAttacking;
        private set
        {
            isAttacking = value;
        }
    }

    //The class manages the attack when is attack and inform when it starts and stops the attack
    public System.Action OnStartAttack; // Call when the attack is started
    public System.Action OnStopAttack;  // Call when the attack is stopped
    public System.Action OnDurationOver; // Call when the duration of the weapon is over
    public System.Action OnShoot; // Call when each attack is performed

    // Init the weapon, start attack if autoAttack is true and start the duration if it is greater than zero
    public virtual void Init() {
        IsAttacking = false;
        StopAllCoroutines();

        if (duration > 0) {
            StartCoroutine(DurationCO());
        }
    }

    private void OnDisable() {
        StopAttack();
    }

    //Coroutine that controls the attack
    IEnumerator AttackCO() {
        yield return new WaitForSeconds(StartAttackAfterDelay);
        while (IsAttacking) {
            Shoot();
            yield return new WaitForSeconds(CooldownTime);
        }
    }

    //Shoot the weapon
    public virtual void Shoot() {
        Pool.CreateObject(projectiePrefab, shootPoint.position, transform.rotation);
        OnShoot?.Invoke();
    }

    //Start the attack
    public virtual void StartAttack() {
        if (!IsAttacking) {
            IsAttacking = true;
            OnStartAttack?.Invoke();
            StartCoroutine(AttackCO());
        }
    }

    //Stop the attack
    public virtual void StopAttack() {
        if (IsAttacking) {
            IsAttacking = false;
            OnStopAttack?.Invoke();
        }
    }

    // Duration Courotine
    IEnumerator DurationCO() {
        yield return new WaitForSeconds(Duration);
        OnDurationOver?.Invoke();
    }
}


