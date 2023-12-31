using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredProjectil : Projectil
{
    public Color color;
    private ColoredWeapon cWeapon;
    List<SpriteRenderer> spriteRenderers;

    public void Awake()
    {
        spriteRenderers = new List<SpriteRenderer>();
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    public new void OnEnable()
    {
        cWeapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ColoredWeapon>();
        if (cWeapon)
        {
            color = cWeapon.Color;
        }
        spriteRenderers.ForEach(sr => sr.color = color);
        base.OnEnable();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (targetWithTag.Contains(other.tag))
        {

            var character = other.GetComponent<ColoredCharacter>();
            if (character != null)
            {
                if (character.Color.IsEqual(color))
                    character.Health.TakeDamage(damage);
            }
            Hit();
        }
    }
}
