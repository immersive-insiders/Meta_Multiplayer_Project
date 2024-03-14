using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2;
    [SerializeField] private Color fadeColor;
    [SerializeField] private bool fadeOnStart = true;

    private Renderer fadeRenderer;

    public float FadeDuration { get => fadeDuration; }

    private void Start()
    {
        fadeRenderer = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Debug.Log(" ifadein function");

        Fade(1, 0);
    }

    public void FadeOut()
    {
        Debug.Log(" iFadeout function");

        Fade(0, 1);

    }


    private void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut)); 
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut) 
    {
        Debug.Log(" Fding couroutine");

        float timer = 0;
        while(timer <= fadeDuration) 
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut,timer/fadeDuration);
            fadeRenderer.material.SetColor("_BaseColor", newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut; 
        fadeRenderer.material.SetColor("_Color", newColor2);

    }

}
