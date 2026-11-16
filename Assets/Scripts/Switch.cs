using UnityEngine;

// Goes on each Switch (collider set to Is Trigger).
public class Switch : MonoBehaviour
{
    [SerializeField] int switchId;
    [SerializeField] Door door;
    [SerializeField] AudioClip activateClip;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player")) return;
        activated = true;

        GetComponent<SpriteRenderer>().color = Color.green;
        if (activateClip != null && AudioManager.Instance != null) { AudioManager.Instance.PlaySfx(activateClip); }
        door.ActivateSwitch(switchId); 
    }
}
