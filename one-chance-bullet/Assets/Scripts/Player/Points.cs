using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Manage the player Points
public class Points : MonoBehaviour
{
    [SerializeField] int pointsValue = 0;
    public int PointsValue{
        get { return pointsValue; }
        private set {
            var oldValue = pointsValue;
            pointsValue = value;
            if (pointsValue != oldValue)
            {
                OnPointsChange(pointsValue);
            }
        }
    }

    public Action<int> OnPointsChange = delegate { };

    public void Add(int points)
    {
        PointsValue += points;
    }

    public void Reset()
    {
        PointsValue = 0;
    }
}
