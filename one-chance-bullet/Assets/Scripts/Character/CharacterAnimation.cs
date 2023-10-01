using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] string movingParameter;

    CharacterMovement2D characterMovement;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement2D>();
    }

    void Update()
    {
        animator.SetBool(movingParameter, characterMovement.IsMoving);
           
    }
}
