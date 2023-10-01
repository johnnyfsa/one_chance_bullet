using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change the weapon of the player when the player collides with the weapon
public class PickupWeapon : Pickup
{
    [SerializeField] Weapon weapon;

    protected override void OnPickup(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            var weaponObject = Instantiate(weapon);
            player.GetComponent<WeaponHandle>().Equip(weaponObject);
        }
    }
}
