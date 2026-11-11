using TMPro;
using UnityEngine;

public class CollectorAI : MonoBehaviour
{
    public Transform player;
    public Transform baseSpot;
    public float speed = 3f;
    public float seeRange = 9f;
    public float dangerRange = 2.5f;
    public float safeRange = 4f;
    public bool carryingCoin;
    public int delivered;

    BTNode root;
    SpriteRenderer sr;
    TextMeshPro label;
    Coin targetCoin;
    Vector2 wanderTarget;
    bool evading;
    string branch = "Start";

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        label = AILabel.Create(transform);
        wanderTarget = RandomPoint();

        // The order of the children is the priority order
        root = new Selector(
            new Sequence(new ConditionNode(PlayerTooClose), new ActionNode(RunAway)),
            new Sequence(new ConditionNode(() => carryingCoin), new ActionNode(GoToBase)),
            new Sequence(new ConditionNode(FindCoin), new ActionNode(GoToCoin)),
            new ActionNode(Wander));
    }

    void Update()
    {
        root.Evaluate();
    }

    bool PlayerTooClose()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // Start running at dangerRange, keep running until safeRange so it does not flicker at the edge
        if (dist < dangerRange) evading = true;
        else if (dist > safeRange) evading = false;
        return evading;
    }

    NodeResult RunAway()
    {
        Show("Evade", new Color32(0xB0, 0x70, 0xFF, 0xFF));
        Vector2 away = (transform.position - player.position).normalized;
        MoveToward((Vector2)transform.position + away, speed * 1.5f);
        return NodeResult.Running;
    }

    NodeResult GoToBase()
    {
        Show("Deliver", new Color32(0xF2, 0xD0, 0x2E, 0xFF));
        MoveToward(baseSpot.position, speed);
        if (Vector2.Distance(transform.position, baseSpot.position) > 0.4f) return NodeResult.Running;

        carryingCoin = false;
        delivered++;
        Debug.Log("Collector delivered a coin (total " + delivered + ")");
        return NodeResult.Success;
    }

    bool FindCoin()
    {
        targetCoin = null;
        float best = seeRange;
        foreach (Coin coin in Coin.All)
        {
            float d = Vector2.Distance(transform.position, coin.transform.position);
            if (d < best)
            {
                best = d;
                targetCoin = coin;
            }
        }
        return targetCoin != null;
    }

    NodeResult GoToCoin()
    {
        Show("Fetch", new Color32(0xFF, 0x9F, 0x1A, 0xFF));
        MoveToward(targetCoin.transform.position, speed);
        if (Vector2.Distance(transform.position, targetCoin.transform.position) > 0.4f) return NodeResult.Running;

        targetCoin.Respawn();
        carryingCoin = true;
        return NodeResult.Success;
    }

    NodeResult Wander()
    {
        Show("Wander", new Color32(0x9A, 0xA5, 0xB1, 0xFF));
        MoveToward(wanderTarget, speed * 0.6f);
        if (Vector2.Distance(transform.position, wanderTarget) < 0.3f) wanderTarget = RandomPoint();
        return NodeResult.Success;
    }

    Vector2 RandomPoint()
    {
        return new Vector2(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f));
    }

    void MoveToward(Vector2 target, float moveSpeed)
    {
        Vector3 next = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        next.x = Mathf.Clamp(next.x, -9.5f, 9.5f);
        next.y = Mathf.Clamp(next.y, -5.5f, 5.5f);
        transform.position = next;
    }

    void Show(string name, Color color)
    {
        string text = "Collector  " + name + "  (" + delivered + ")";
        if (label.text != text) label.text = text;
        sr.color = color;
        if (name == branch) return;
        Debug.Log("Collector: " + branch + " -> " + name);
        branch = name;
    }
}
