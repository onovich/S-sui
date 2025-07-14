using UnityEngine;

public struct AABB {
    public Vector2 min;
    public Vector2 max;

    public AABB(Vector2 min, Vector2 max) {
        this.min = min;
        this.max = max;
    }

    public AABB(Vector2 center, float halfWidth, float halfHeight) {
        min = new Vector2(center.x - halfWidth, center.y - halfHeight);
        max = new Vector2(center.x + halfWidth, center.y + halfHeight);
    }

    public bool Contains(Vector2 point) {
        return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
    }

    public bool Intersects(AABB other) {
        return !(other.min.x > max.x || other.max.x < min.x || other.min.y > max.y || other.max.y < min.y);
    }
}