using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColoredWeapon : Weapon
{
    [SerializeField] Color color;
    private List<SpriteRenderer> spriteRenderer;
    private ColoredProjectil coloredProjectile;

    public Color Color { get => color; set => color = value; }

    public void Awake()
    {
        spriteRenderer = new List<SpriteRenderer>();
        spriteRenderer.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    public override void Init()
    {
        color = GameManager.Instance.GetComponent<ColoredDamageManager>().GetRandomColor();
        spriteRenderer.ForEach(sr => sr.color = color);
        base.Init();

    }
}
