using UnityEngine;

[System.Serializable]
public class SquareFloat
{
    public Vector2 min;
    public Vector2 max;

    public SquareFloat(float minX, float maxX, float minY, float maxY)
    {
        min = new Vector2(minX, minY);
        max = new Vector2(maxX, maxY);
    }

    public bool PositionIsOnTopBound(Vector2 a)
    {
        return a.y == min.y;
    }

    public bool PositionIsOnBottomBound(Vector2 a)
    {
        return a.y == max.y;
    }

    public bool PositionIsOnYBounds(Vector2 a)
    {
        return a.y <= max.y && a.y >= min.y;
    }

    public bool PositionIsOnYBounds(float y)
    {
        return y <= max.y && y >= min.y;
    }

    public bool PositionIsOnLeftBound(Vector2 a)
    {
        return a.x == min.x;
    }

    public bool PositionIsOnRightBound(Vector2 a)
    {
        return a.x == max.x;
    }

    public bool PositionIsOnXBounds(Vector2 a)
    {
        return a.x <= max.x && a.x >= min.x;
    }

    public bool PositionIsOnXBounds(float x)
    {
        return x <= max.x && x >= min.x;
    }

    public bool PositionIsOnBounds(float x, float y)
    {
        return PositionIsOnXBounds(x) && PositionIsOnYBounds(y);
    }

    public Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

    public Vector2 Clamp(Vector2 a)
    {
        return new Vector2(
            Mathf.Clamp(a.x, min.x, max.x),
            Mathf.Clamp(a.y, min.y, max.y)
            );
    }
}
