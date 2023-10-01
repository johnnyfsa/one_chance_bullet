using UnityEngine;

[System.Serializable]
public class SquareInt
{
    public Vector2Int min;
    public Vector2Int max;

    public SquareInt(int minX, int maxX, int minY, int maxY)
    {
        min = new Vector2Int(minX, minY);
        max = new Vector2Int(maxX, maxY);
    }

    public bool PositionIsOnTopBound(Vector2Int a)
    {
        return a.y == min.y;
    }

    public bool PositionIsOnBottomBound(Vector2Int a)
    {
        return a.y == max.y;
    }

    public bool PositionIsOnYBounds(Vector2Int a)
    {
        return a.y <= max.y && a.y >= min.y;
    }

    public bool PositionIsOnYBounds(int y)
    {
        return y <= max.y && y >= min.y;
    }

    public bool PositionIsOnLeftBound(Vector2Int a)
    {
        return a.x == min.x;
    }

    public bool PositionIsOnRightBound(Vector2Int a)
    {
        return a.x == max.x;
    }

    public bool PositionIsOnXBounds(Vector2Int a)
    {
        return a.x <= max.x && a.x >= min.x;
    }

    public bool PositionIsOnXBounds(int x)
    {
        return x <= max.x && x >= min.x;
    }

    public bool PositionIsOnBounds(int x, int y)
    {
        return PositionIsOnXBounds(x) && PositionIsOnYBounds(y);
    }

    public Vector2Int RandomPosition()
    {
        return new Vector2Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    } 
}
