using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SquareFloat levelArea;
    private Transform playerTransform;



    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if(playerTransform != null)
            transform.position = Vector2.Lerp(transform.position, levelArea.Clamp(playerTransform.position), speed);
    }
}
