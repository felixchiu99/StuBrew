using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;

public class ScreenFader : MonoBehaviour
{
    private string _fadeScene;
    private float _alpha;

    private CanvasGroup _myCanvas;
    private Image _bg;
    private float _lastTime;
    private bool _startedLoading;

    private float _fadeInDuration;
    private FadeOnSceneChange sceneChange;


    //Set callback
    private void OnEnable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    //Remove callback
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public void InitiateFader(FadeOnSceneChange initializer, CanvasGroup canvasGroup, Image image, string scene, Color fadeColor, float fadeInDuration, float fadeOutDuration)
    {
        DontDestroyOnLoad(gameObject);

        _fadeInDuration = fadeInDuration;
        _fadeScene = scene;

        //Getting the visual elements
        _myCanvas = canvasGroup;
        _bg = image;
        _bg.color = fadeColor;

        //Checking and starting the coroutine
        _myCanvas.alpha = 0.0f;
        sceneChange = initializer;
        StartCoroutine(FadeIt(initializer, FadeDirection.Out, fadeOutDuration));
    }

    private enum FadeDirection
    {
        In,
        Out
    }

    private IEnumerator FadeIt(FadeOnSceneChange initializer, FadeDirection fadeDirection, float fadeDuration)
    {
        var timePassed = 0.0f;

        switch (fadeDirection)
        {
            case FadeDirection.Out:
                do
                {
                    _alpha = Mathf.Lerp(0, 1, timePassed / fadeDuration);
                    _myCanvas.alpha = _alpha;

                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                _alpha = 1;
                _myCanvas.alpha = _alpha;

                SceneManager.LoadSceneAsync(_fadeScene);
                break;

            case FadeDirection.In:
                _myCanvas.alpha = 1;
                do
                {
                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < 2 && SaveSystem.hasLoaded == false);

                timePassed = 0.0f;
                do
                {
                    _alpha = Mathf.Lerp(1, 0, timePassed / fadeDuration);
                    _myCanvas.alpha = _alpha;

                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                _alpha = 0;

                initializer.DoneFading();

                Destroy(gameObject);
                break;
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //We can now fade in
        StartCoroutine(FadeIt(sceneChange, FadeDirection.In, _fadeInDuration));
    }
}
