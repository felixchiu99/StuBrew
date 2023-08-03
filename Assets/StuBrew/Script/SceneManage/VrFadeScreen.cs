using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VrFadeScreen : MonoBehaviour
{
    private Color fadeColor;
    private float fadeOutDuration;
    private float fadeInDuration;

    private float _fadeInDuration;
    private string _fadeScene;
    private FadeOnSceneChange sceneChange;

    [SerializeField] private Renderer rend;
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.015f;
        transform.localRotation = Camera.main.transform.rotation;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.015f;
        transform.localRotation = Camera.main.transform.rotation;
    }

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

    public void InitiateFader(FadeOnSceneChange initializer, Renderer renderer, string scene, Color fadeColor, float fadeInDuration, float fadeOutDuration)
    {
        DontDestroyOnLoad(gameObject);

        _fadeInDuration = fadeInDuration;
        _fadeScene = scene;
        this.fadeColor = fadeColor;

        rend = renderer;

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
        Color newColor = fadeColor;
        switch (fadeDirection)
        {
            case FadeDirection.Out:
                do
                {
                    newColor = fadeColor;
                    newColor.a = Mathf.Lerp(0, 1, timePassed / fadeDuration);
                    
                    rend.material.color = newColor;
                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                newColor = fadeColor;
                newColor.a = 1;
                rend.material.color = newColor;

                SceneManager.LoadSceneAsync(_fadeScene);
                break;

            case FadeDirection.In:
                do
                {
                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < 2 && SaveSystem.hasLoaded == false);

                timePassed = 0.0f;
                do
                {
                    newColor = fadeColor;
                    newColor.a = Mathf.Lerp(1, 0, timePassed / fadeDuration);
                    rend.material.color = newColor;

                    timePassed += Time.deltaTime;
                    yield return null;
                } while (timePassed < fadeDuration);

                newColor = fadeColor;
                newColor.a = 0;

                rend.material.color = newColor;

                initializer.DoneFading();

                Destroy(gameObject);
                break;
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //We can now fade in
        SaveSystem.Load(3);
        StartCoroutine(FadeIt(sceneChange, FadeDirection.In, _fadeInDuration));
    }
}
