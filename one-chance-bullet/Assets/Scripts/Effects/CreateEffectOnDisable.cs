using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEffectOnDisable : MonoBehaviour
{
    [SerializeField] Effect effectPrefab;
    [SerializeField] Transform position;

    private void Awake()
    {
        if (position == null)
            position = transform;
    }

    private void OnDisable()
    {
        Pool.CreateObject(effectPrefab.gameObject, position.position, position.rotation).GetComponent<Effect>().Play();
    }
}
