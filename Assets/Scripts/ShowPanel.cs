using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeInSpeed = 1f;  // Slower fade for when looking at lips
    public float fadeOutSpeed = 3f; // Fast fade out when not

    private bool shouldFadeIn;
    private bool isFading = false;

    public void SetFadeState(bool fadeIn)
    {
        if (fadeIn != shouldFadeIn) // Only start fading if state changes
        {
            shouldFadeIn = fadeIn;
            if (!isFading) StartCoroutine(Fade());
        }
    }

    private System.Collections.IEnumerator Fade()
    {
        isFading = true;
        float targetAlpha = shouldFadeIn ? 1f : 0f;
        float speed = shouldFadeIn ? fadeInSpeed : fadeOutSpeed;

        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
    }
}
