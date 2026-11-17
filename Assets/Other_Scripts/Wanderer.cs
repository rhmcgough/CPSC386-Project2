using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    static readonly ProfilerMarker nearestMarker = new ProfilerMarker("Wanderer.FindNearest");
    static readonly List<Wanderer> all = new List<Wanderer>();

    public float speed = 2f;
    public float halfWidth = 11f;
    public float halfHeight = 5f;
    public float nearestDistance;

    Vector2 direction;
    SpriteRenderer sr;
    Transform cachedTransform;
    int offset;

    void OnEnable() { all.Add(this); }
    void OnDisable() { all.Remove(this); }

    void Start()
    {
        direction = Random.insideUnitCircle.normalized;
        sr = GetComponent<SpriteRenderer>();
        cachedTransform = transform;
        offset = Random.Range(0, 10);
        sr.color = Color.HSVToRGB(Random.value, 0.7f, 1f);
    }

    void Update()
    {
        cachedTransform.position += (Vector3)(direction * speed * Time.deltaTime);

        Vector3 p = cachedTransform.position;
        if (Mathf.Abs(p.x) > halfWidth) direction.x = -direction.x;
        if (Mathf.Abs(p.y) > halfHeight) direction.y = -direction.y;

        if ((Time.frameCount + offset) % 10 != 0) return;

        using (nearestMarker.Auto())
        {
            nearestDistance = FindNearestDistance();
        }
    }

    float FindNearestDistance()
    {
        Vector2 mine = cachedTransform.position;
        float best = float.MaxValue;
        for (int i = 0; i < all.Count; i++)
        {
            Wanderer other = all[i];
            if (other == this) continue;
            float d = Vector2.Distance(mine, other.cachedTransform.position);
            if (d < best) best = d;
        }
        return best;
    }
}
