using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredDamageManager : Manager<ColoredDamageManager>
{
   [SerializeField] List<Color> colors = new List<Color>();

   public Color GetRandomColor()
   {
       return colors[Random.Range(0, colors.Count)];
   }
}
