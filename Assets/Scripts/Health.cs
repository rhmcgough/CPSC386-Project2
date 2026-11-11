using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static readonly List<Health> All = new List<Health>();

    public float maxHealth = 100f;
    public float health = 100f;
    public float regenPerSecond = 5f;

    void OnEnable() { All.Add(this); }
    void OnDisable() { All.Remove(this); }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Min(maxHealth, health + regenPerSecond * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        health = Mathf.Max(0f, health - amount);
    }
}
