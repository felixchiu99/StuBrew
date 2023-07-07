using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class FadeOnSceneChange : MonoBehaviour
{
    [Scene]
    public string sceneName;

    [SerializeField] CanvasGroup fadeCanvas;


    private float alpha;
    private float fadeOutDuration = 0.1f;
    private float fadeInDuration = 0.1f;
    //Set callback
    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Remove callback
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        StartCoroutine(FadeIt(FadeDirection.In, fadeInDuration));
    }
    public void InitiateFader(CanvasGroup canvasGroup, string scene, float fadeInDuration, float fadeOutDuration)
    {
        DontDestroyOnLoad(gameObject);

        this.fadeInDuration = fadeInDuration;
        sceneName = scene;

        //Getting the visual elements
        fadeCanvas = canvasGroup;
        //_bg = image;
        //_bg.color = fadeColor;

        //Checking and starting the coroutine
        fadeCanvas.alpha = 0.0f;
        StartCoroutine(FadeIt(FadeDirection.Out, fadeOutDuration));
    }

    private enum FadeDirection
    {
        In,
        Out
    }

    private IEnumerator FadeIt(FadeDirection fadeDirection, float fadeDuration)
    {
        var timePassed = 0.0f;

        switch (fadeDirection)
        {
            case FadeDirection.Out:
                do
                {
                    alpha = Mathf.Lerp(0, 1, timePassed / fadeDuration);
                    fadeCanvas.alpha = alpha;

                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                alpha = 1;

                SceneManager.LoadSceneAsync(sceneName);
                break;

            case FadeDirection.In:
                do
                {
                    alpha = Mathf.Lerp(1, 0, timePassed / fadeDuration);
                    fadeCanvas.alpha = alpha;

                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                alpha = 0;

                //Initiate.DoneFading();

                Debug.Log("Your scene has been loaded , and fading in has just ended");

                //Destroy(gameObject);
                break;
        }
    }

    [Button]
    public void Fade()
    {
        /*
        if (areWeFading)
        {
            Debug.Log("Already Fading");
            return;
        }
        */
        /*
        var init = new GameObject("Fader", typeof(Canvas), typeof(CanvasGroup), typeof(Image), typeof(Fader));
        init.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        */
        /*
        var fader = init.GetComponent<Fader>();
        areWeFading = true;
        */
        InitiateFader(fadeCanvas, sceneName, fadeOutDuration, fadeInDuration);
    }
}
