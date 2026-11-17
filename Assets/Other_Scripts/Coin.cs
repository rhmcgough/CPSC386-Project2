using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static readonly List<Coin> All = new List<Coin>();

    void OnEnable() { All.Add(this); }
    void OnDisable() { All.Remove(this); }

    public void Respawn()
    {
        transform.position = new Vector3(Random.Range(-9f, 9f), Random.Range(-4.5f, 4.5f), 0f);
    }
}