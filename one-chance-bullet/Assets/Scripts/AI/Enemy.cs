using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int spawnValue = 0;
    public int SpawnValue => spawnValue;
    
    public Character Character { get; private set; }
    public Health Health { get; private set; }
    Animator animator;
    CharacterMovement2D movement;
    WeaponHandle weaponHandle;
    
    public Player TargetPlayer { get; private set; }

    private void Awake() {
        Character = GetComponent<Character>();
        Health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement2D>();
        weaponHandle = GetComponent<WeaponHandle>();
    }

    public void SetTarget(Player player)
    {
        this.TargetPlayer = player;
    }

    public void Attack() {
        weaponHandle.Shoot();
    }

    public Action OnAttackEnd;
    public void AttackEnd() {
        OnAttackEnd?.Invoke();
    }

    public Action OnDamageEnd;
    public void DamageEnd() {
        OnDamageEnd?.Invoke();
    }
}
