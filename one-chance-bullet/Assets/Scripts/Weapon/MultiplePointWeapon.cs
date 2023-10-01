using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePointWeapon : Weapon
{
    [SerializeField] Transform[] shootPoints;
    [SerializeField] bool alternatePoints = false;

    int currentPoint = 0;

    public override void Shoot()
    {
        if(alternatePoints)
        {
            shootPoint = shootPoints[currentPoint];
            currentPoint = (currentPoint + 1) % shootPoints.Length;
            base.Shoot();
        } else {
            ShootAllPoints();
        }
    }

    void ShootAllPoints()
    {
        foreach (var point in shootPoints)
        {
            shootPoint = point;
            base.Shoot();
        }
    }
}
