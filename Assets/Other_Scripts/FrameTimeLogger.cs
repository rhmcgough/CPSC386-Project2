using UnityEngine;

public class FrameTimeLogger : MonoBehaviour
{
    float elapsed;
    int frames;

    void Update()
    {
        elapsed += Time.unscaledDeltaTime;
        frames++;

        if (elapsed >= 1f)
        {
            float ms = elapsed / frames * 1000f;
            float fps = frames / elapsed;
            Debug.Log("Average frame time: " + ms.ToString("F1") + " ms (" + fps.ToString("F0") + " fps)");
            elapsed = 0f;
            frames = 0;
        }
    }
}
