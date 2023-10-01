using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    public static bool IsEqual(this Color a, Color b, float tolerance = 0.04f) {
        if (a.r > b.r + tolerance) return false;
        if (a.g > b.g + tolerance) return false;
        if (a.b > b.b + tolerance) return false;
        if (a.r < b.r - tolerance) return false;
        if (a.g < b.g - tolerance) return false;
        if (a.b < b.b - tolerance) return false;

        return true;
    }
}
