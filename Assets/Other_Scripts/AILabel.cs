using TMPro;
using UnityEngine;

public static class AILabel
{
    public static TextMeshPro Create(Transform owner)
    {
        GameObject go = new GameObject("Label");
        go.transform.SetParent(owner, false);
        go.transform.localPosition = new Vector3(0f, 0.9f, 0f);

        TextMeshPro text = go.AddComponent<TextMeshPro>();
        text.fontSize = 3.5f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;
        text.sortingOrder = 10;
        return text;
    }
}
