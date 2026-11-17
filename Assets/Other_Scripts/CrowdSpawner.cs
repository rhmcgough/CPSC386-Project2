using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    public GameObject dotPrefab;
    public int count = 300;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 position = new Vector2(Random.Range(-10f, 10f), Random.Range(-4f, 4f));
            Instantiate(dotPrefab, position, Quaternion.identity, transform);
        }
        Debug.Log("Spawned " + count + " dots");
    }
}
