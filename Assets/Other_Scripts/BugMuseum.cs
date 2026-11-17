using System.Collections.Generic;
using UnityEngine;

public class BugMuseum : MonoBehaviour
{
    public enum BugToRun { None, NullReference, OffByOne, ModifiedCollection }

    public BugToRun bugToRun = BugToRun.None;
    public int[] coinValues = { 1, 5, 10 };

    Rigidbody2D body;

    void Start()
    {
        Debug.Log("BugMuseum started. Running: " + bugToRun, this);

        switch (bugToRun)
        {
            case BugToRun.NullReference:
                NullReferenceBug();
                break;
            case BugToRun.OffByOne:
                OffByOneBug();
                break;
            case BugToRun.ModifiedCollection:
                ModifiedCollectionBug();
                break;
        }
    }

    void NullReferenceBug()
    {
        body.gravityScale = 0f;
        Debug.Log("Gravity turned off");
    }

    void OffByOneBug()
    {
        int total = 0;
        for (int i = 0; i <= coinValues.Length; i++)
        {
            total += coinValues[i];
        }
        Debug.Log("Total coin value: " + total);
    }

    void ModifiedCollectionBug()
    {
        List<string> enemies = new List<string> { "Slime", "Bat", "Ghost", "Bat" };
        foreach (string enemy in enemies)
        {
            if (enemy == "Bat")
            {
                enemies.Remove(enemy);
            }
        }
        Debug.Log("Enemies left: " + enemies.Count);
    }

    [ContextMenu("Log One Of Each")]
    void LogOneOfEach()
    {
        Debug.Log("This is a normal message", this);
        Debug.LogWarning("This is a warning", this);
        Debug.LogError("This is an error", this);
    }
}
