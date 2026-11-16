using UnityEngine;
// using UnityEngine.InputSystem;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int platformCount = 25;

    // Perlin Noise Settings 
    public float scale = 5f;
    public float heightMultiplier = 4f;
    public float seed = 0f;

    // Layout
    public float startX = -6f;
    public float startY = 0f;
    public float horizontalSpacing = 2.5f;

    Transform container;

    void Start()
    {
        Generate();
    }

/*
    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            seed = Random.Range(0f, 1000f);
            Generate();
        }
    }
*/

    void Generate()
    {
        if (container == null)
        {
            container = new GameObject("Platform Container").transform;
            container.SetParent(transform, false);
        }

        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }

        float currentX = startX;
        int spawnedCount = 0;

        // This shares the core logic of SimpleLevelGenerator.cs from class. 
        for (int i = 0; i < platformCount; i++)
        {
            float noiseValue = Mathf.PerlinNoise((i + seed) / scale, seed);
            float currentY = startY + (noiseValue - 0.5f) * 2f * heightMultiplier;

            GameObject platform = Instantiate(platformPrefab, container);
            platform.transform.position = new Vector3(currentX, currentY, 0f);

            currentX += horizontalSpacing;
            spawnedCount++;
        }

    }
}