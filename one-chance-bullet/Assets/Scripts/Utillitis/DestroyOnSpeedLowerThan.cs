using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpeedLowerThan : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] bool deactivateInstedOfDestoy = false;
    [SerializeField] Effect onDestroyEffectPrefab;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector2.Distance(Vector2.zero, rb.velocity) > velocity)
            return;

        PlayEffect();

        if (deactivateInstedOfDestoy == true)
        {
            this.gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayEffect()
    {
        if(onDestroyEffectPrefab != null)
            Pool.CreateObject(onDestroyEffectPrefab.gameObject, transform.position, transform.rotation).GetComponent<Effect>().Play();
    }


}
