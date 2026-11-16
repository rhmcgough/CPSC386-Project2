    using UnityEngine;

public class FallSfx : StateMachineBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float minFallSeconds = 0.4f;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime * stateInfo.length < minFallSeconds) return;
        if (clip != null && AudioManager.Instance != null)
            AudioManager.Instance.PlaySfx(clip);
    }
}