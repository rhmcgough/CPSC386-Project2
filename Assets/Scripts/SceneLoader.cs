using UnityEngine;
using UnityEngine.SceneManagement;

// Goes on Goal (loads a scene when the player touches it) and on MenuManager (Button On Click).
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string triggerScene = "MainMenu";
    [SerializeField] bool saveScoreOnTrigger = false;
    [SerializeField] AudioClip triggerClip;

    public void LoadScene(string sceneName)
    {
        if (sceneName == "Game") GameSession.Score = 0;
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (saveScoreOnTrigger) GameSession.SaveHighScore();
        if (triggerClip != null && AudioManager.Instance != null) { AudioManager.Instance.PlaySfx(triggerClip); }
        SceneManager.LoadScene(triggerScene);
    }
}
