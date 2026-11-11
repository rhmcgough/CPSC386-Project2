using TMPro;
using UnityEngine;

public enum GuardState { Patrol, Chase, Attack, Flee }

public class GuardStateMachine : MonoBehaviour
{
    public GuardState currentState = GuardState.Patrol;
    public Transform player;
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float fleeSpeed = 4.5f;
    public float chaseRange = 6f;
    public float attackRange = 1.5f;
    public float fleeHealth = 30f;
    public float recoverHealth = 60f;
    public float attackCooldown = 1f;

    Health health;
    SpriteRenderer sr;
    TextMeshPro label;
    int waypointIndex;
    float nextAttackTime;

    void Start()
    {
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        label = AILabel.Create(transform);
        ShowState();
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // Flee can interrupt every other state, so it is checked before the switch
        if (currentState != GuardState.Flee && health.health < fleeHealth)
        {
            ChangeState(GuardState.Flee);
        }

        switch (currentState)
        {
            case GuardState.Patrol:
                Patrol();
                if (dist < chaseRange) ChangeState(GuardState.Chase);
                break;

            case GuardState.Chase:
                MoveToward(player.position, chaseSpeed);
                if (dist < attackRange) ChangeState(GuardState.Attack);
                else if (dist > chaseRange) ChangeState(GuardState.Patrol);
                break;

            case GuardState.Attack:
                if (Time.time >= nextAttackTime)
                {
                    Debug.Log("Guard swings at the player");
                    nextAttackTime = Time.time + attackCooldown;
                }
                if (dist > attackRange) ChangeState(GuardState.Chase);
                break;

            case GuardState.Flee:
                Vector2 away = (transform.position - player.position).normalized;
                MoveToward((Vector2)transform.position + away, fleeSpeed);
                if (health.health >= recoverHealth) ChangeState(GuardState.Patrol);
                break;
        }
    }

    void Patrol()
    {
        Vector2 target = waypoints[waypointIndex].position;
        MoveToward(target, patrolSpeed);
        if (Vector2.Distance(transform.position, target) < 0.2f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }

    void MoveToward(Vector2 target, float speed)
    {
        Vector3 next = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        next.x = Mathf.Clamp(next.x, -9.5f, 9.5f);
        next.y = Mathf.Clamp(next.y, -5.5f, 5.5f);
        transform.position = next;
    }

    void ChangeState(GuardState next)
    {
        if (next == currentState) return;
        Debug.Log("Guard: " + currentState + " -> " + next);
        currentState = next;
        ShowState();
    }

    void ShowState()
    {
        label.text = "Guard  " + currentState;
        switch (currentState)
        {
            case GuardState.Patrol: sr.color = new Color32(0x4D, 0xA3, 0xFF, 0xFF); break;
            case GuardState.Chase: sr.color = new Color32(0xFF, 0x9F, 0x1A, 0xFF); break;
            case GuardState.Attack: sr.color = new Color32(0xFF, 0x3B, 0x30, 0xFF); break;
            case GuardState.Flee: sr.color = new Color32(0xB0, 0x70, 0xFF, 0xFF); break;
        }
    }
}
