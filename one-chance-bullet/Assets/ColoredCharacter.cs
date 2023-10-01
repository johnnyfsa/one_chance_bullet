using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredCharacter : Character
{
    private Color color;
    private List<SpriteRenderer> spriteRenderer;

    public Color Color { get => color; private set => color = value; }

    public new void Awake()
    {
        spriteRenderer = new List<SpriteRenderer>();
        spriteRenderer.AddRange(GetComponentsInChildren<SpriteRenderer>());

        base.Awake();
    }

    public new void OnEnable() {
        Color = ColoredDamageManager.Instance.GetRandomColor();
        spriteRenderer.ForEach(sr => sr.color = Color);

        base.OnEnable();
    }
}
