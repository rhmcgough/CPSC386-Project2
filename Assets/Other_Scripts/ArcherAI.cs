using TMPro;
using UnityEngine;

public enum ArcherAction { Idle, Approach, Shoot, Reload, Flee, Hide }

public class ArcherAI : MonoBehaviour
{
    public Transform player;
    public ArcherAction lastAction = ArcherAction.Idle;
    public int ammo = 5;
    public int maxAmmo = 5;
    public float moveSpeed = 2.5f;
    public float fleeSpeed = 4.5f;
    public float shootRange = 8f;
    public float approachRange = 12f;
    public float fleeDistance = 5f;
    public float lowHealth = 30f;
    public float shotCooldown = 0.8f;
    public float reloadTime = 1.5f;

    Health health;
    SpriteRenderer sr;
    TextMeshPro label;
    float nextShotTime;
    float reloadEndTime = -1f;

    void Start()
    {
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        label = AILabel.Create(transform);
        ShowAction();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // Ask the tree again every frame. It has no memory of what it answered last time
        ArcherAction action = Decide(health.health, dist, ammo);

        if (action != lastAction)
        {
            Debug.Log("Archer: " + lastAction + " -> " + action);
            lastAction = action;
        }

        if (action != ArcherAction.Reload) reloadEndTime = -1f;

        switch (action)
        {
            case ArcherAction.Approach:
                MoveToward(player.position, moveSpeed);
                break;
            case ArcherAction.Shoot:
                Shoot();
                break;
            case ArcherAction.Reload:
                Reload();
                break;
            case ArcherAction.Flee:
                Vector2 away = (transform.position - player.position).normalized;
                MoveToward((Vector2)transform.position + away, fleeSpeed);
                break;
        }

        ShowAction();
    }

    // The decision tree: nothing in here is stored between calls
    public ArcherAction Decide(float hp, float distance, int arrows)
    {
        if (hp < lowHealth)
        {
            return distance < fleeDistance ? ArcherAction.Flee : ArcherAction.Hide;
        }

        if (arrows == 0)
        {
            return distance < fleeDistance ? ArcherAction.Flee : ArcherAction.Reload;
        }

        if (distance < shootRange) return ArcherAction.Shoot;
        if (distance < approachRange) return ArcherAction.Approach;
        return ArcherAction.Idle;
    }

    void Shoot()
    {
        if (Time.time < nextShotTime) return;
        ammo--;
        nextShotTime = Time.time + shotCooldown;
        Debug.Log("Archer shoots (arrows left: " + ammo + ")");
    }

    void Reload()
    {
        if (reloadEndTime < 0f) reloadEndTime = Time.time + reloadTime;
        if (Time.time < reloadEndTime) return;
        ammo = maxAmmo;
        reloadEndTime = -1f;
    }

    void MoveToward(Vector2 target, float speed)
    {
        Vector3 next = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        next.x = Mathf.Clamp(next.x, -9.5f, 9.5f);
        next.y = Mathf.Clamp(next.y, -5.5f, 5.5f);
        transform.position = next;
    }

    void ShowAction()
    {
        string text = "Archer  " + lastAction + "  (" + ammo + ")";
        if (label.text != text) label.text = text;

        switch (lastAction)
        {
            case ArcherAction.Idle: sr.color = new Color32(0x9A, 0xA5, 0xB1, 0xFF); break;
            case ArcherAction.Approach: sr.color = new Color32(0xFF, 0x9F, 0x1A, 0xFF); break;
            case ArcherAction.Shoot: sr.color = new Color32(0xFF, 0x3B, 0x30, 0xFF); break;
            case ArcherAction.Reload: sr.color = new Color32(0xF2, 0xD0, 0x2E, 0xFF); break;
            case ArcherAction.Flee: sr.color = new Color32(0xB0, 0x70, 0xFF, 0xFF); break;
            case ArcherAction.Hide: sr.color = new Color32(0x4A, 0x55, 0x66, 0xFF); break;
        }
    }
}
