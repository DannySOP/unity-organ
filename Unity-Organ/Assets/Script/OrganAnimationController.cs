using UnityEngine;

public class OrganAnimationController : MonoBehaviour {
    public Animator animator;           // Drag Animator di Inspector
    public string animationName = "AnimasiNormal"; // Ganti dengan nama animasi kamu

    // Play normal speed
    public void PlayNormal() {
        animator.speed = 1f;  // Normal
        animator.Play(animationName, 0, 0f);
    }

    // Play fast forward
    public void PlayFastForward() {
        animator.speed = 2f;  // Lebih cepat
        animator.Play(animationName, 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    // Play reverse
    public void PlayReverse() {
        animator.speed = 1f; // normal speed
        animator.Play("AnimasiNormal_Reverse", 0, 0f);
    }


    // Stop animation
    public void Stop() {
        animator.speed = 0f;
    }
}
