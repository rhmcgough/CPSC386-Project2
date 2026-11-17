using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    public float speed = 6f;
    public float halfWidth = 10f;
    public float halfHeight = 5.5f;
    public float hitRange = 6f;
    public float hitDamage = 25f;

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        Vector2 move = Vector2.zero;
        if (kb.leftArrowKey.isPressed || kb.aKey.isPressed) move.x -= 1f;
        if (kb.rightArrowKey.isPressed || kb.dKey.isPressed) move.x += 1f;
        if (kb.downArrowKey.isPressed || kb.sKey.isPressed) move.y -= 1f;
        if (kb.upArrowKey.isPressed || kb.wKey.isPressed) move.y += 1f;

        Vector3 p = transform.position + (Vector3)(move.normalized * speed * Time.deltaTime);
        p.x = Mathf.Clamp(p.x, -halfWidth, halfWidth);
        p.y = Mathf.Clamp(p.y, -halfHeight, halfHeight);
        transform.position = p;

        if (kb.hKey.wasPressedThisFrame) HitNearby();
    }

    void HitNearby()
    {
        foreach (Health target in Health.All)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= hitRange) target.TakeDamage(hitDamage);
        }
    }
}
