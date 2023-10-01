using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterMovement2D characterMovement;
    [SerializeField] WeaponHandle primaryWeaponHandle;
    [SerializeField] WeaponHandle secundaryWeaponHandle;
    [SerializeField] bool isAutoAttack = false;

    //bool isAtacking = false;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement2D>();
    }

    void Update()
    {
        characterMovement.MovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(primaryWeaponHandle == null)
            return;

        if(isAutoAttack || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            primaryWeaponHandle.StartAttack();
        } else {
            primaryWeaponHandle.StopAttack();
        }
    }
}
