using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AABB
{
    public Vector2 center { get; set; }
    public Vector2 size { get; set; }
    public Vector2 extents { get => size * 0.5f; }

    public Vector2 min { get => center - extents; set => SetMinMax(value, max); }
    public Vector2 max { get => center + extents; set => SetMinMax(min, value); }

    public AABB(Vector2 center, Vector2 size)
    {
        this.center = center;
        this.size = size;
    }

    public bool Contains(AABB aabb)
    {
        bool intersects = (aabb.max.x >= min.x && aabb.min.x <= max.x) && (aabb.max.y >= min.y && aabb.min.y <= max.y);
        return intersects;
    }

    public bool Contains(Vector2 point)
    {
        bool intersects = (point.x >= min.x && point.x <= max.x) && (point.y >= min.y && point.y <= max.y);
        return intersects;
    }

    public void Expand(Vector2 point)
    {
        SetMinMax(Vector2.Min(min, point), Vector2.Max(max, point));
    }

    public void Expand(AABB aabb)
    {
        SetMinMax(Vector2.Min(min, aabb.min), Vector2.Max(max, aabb.max));
    }

    public void Draw(Color color)
    {
        Debug.DrawLine(new Vector2(min.x, min.y), new Vector2(max.x, min.y), color);
        Debug.DrawLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y), color);
        Debug.DrawLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y), color);
        Debug.DrawLine(new Vector2(max.x, min.y), new Vector2(max.x, max.y), color);
    }

    public void SetMinMax(Vector2 min, Vector2 max)
    {
        size = max - min;
        center = min + extents;
    }
}
