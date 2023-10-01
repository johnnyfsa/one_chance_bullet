using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public float Force;

    Rigidbody2D rb;  

    private Vector2 movementDirection;
    public Vector2 MovementDirection { get => movementDirection;
        set {
            movementDirection = value.normalized;
            moveAmount = movementDirection * Force;

            IsMoving = moveAmount != Vector2.zero;
        }
    }

    public bool IsMoving { get => isMoving; private set => isMoving = value; }
    private Vector2 moveAmount;

    private bool isMoving;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        rb.AddForce(moveAmount, ForceMode2D.Impulse);
        //rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

}
