using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandle : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab;
    [SerializeField] Transform weaponParent;

    Weapon currentWeapon;
    Weapon mainWeapon;

    public bool IsAttacking => currentWeapon.IsAttacking;

    //TODO deverá também considerar o estado do personagem ex: stum
    //public bool CanUseWeapon => weapon != null;

    void Awake()
    {
        if(weaponParent == null)
        {
            weaponParent = transform;
        }

        mainWeapon = weaponParent.GetComponentInChildren<Weapon>();

        if (mainWeapon == null)
        {
            mainWeapon = Instantiate(weaponPrefab);
        }
    }

    private void OnEnable() {
        SetMainWeapon();
    }

    // Equip and init the new weapon and return the old one
    public Weapon Equip(Weapon weapon) {
        if(weapon == null)
            return null;

        var oldWeapon = currentWeapon;

        currentWeapon = weapon;

        currentWeapon.gameObject.SetActive(true);
        currentWeapon.transform.SetParent(weaponParent);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.Init();

        currentWeapon.OnDurationOver += SetMainWeapon;
        
        if(oldWeapon != null) {
            oldWeapon.OnDurationOver -= SetMainWeapon;
            oldWeapon.gameObject.SetActive(false);
        }   

        return oldWeapon;
    }

    public void SetMainWeapon() {
        Equip(mainWeapon);
    }

    public void StartAttack()
    {
        if(currentWeapon != null)
            currentWeapon.StartAttack();
    }

    public void Shoot() {
        if(currentWeapon != null)
            currentWeapon.Shoot();        
    }

    public void StopAttack()
    {
        if(currentWeapon != null)
            currentWeapon.StopAttack();
    }

    
}
