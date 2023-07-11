using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NaughtyAttributes;

public class FadeOnSceneChange : MonoBehaviour
{
    [Scene]
    public string sceneName;
    [SerializeField] Color col;
    [SerializeField] float fadeOutDuration;
    [SerializeField] float fadeInDuration;

    [SerializeField] CheckVREnable vrChecker;

    public GameObject VrFadeScreen;

    private bool isFading;

    //Create Fader object and assing the fade scripts and assign all the variables
    public void Fade(string scene, Color col, float fadeOutDuration, float fadeInDuration)
    {
        if (isFading)
        {
            return;
        }

        var init = new GameObject("ScreenFader", typeof(Canvas), typeof(CanvasGroup), typeof(Image), typeof(ScreenFader));
        init.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        var fader = init.GetComponent<ScreenFader>();
        isFading = true;
        fader.InitiateFader(this, init.GetComponent<CanvasGroup>(), init.GetComponent<Image>(), scene, col, fadeOutDuration, fadeInDuration);
    }

    public void FadeVR(string scene, Color col, float fadeOutDuration, float fadeInDuration)
    {
        if (isFading)
        {
            return;
        }

        var init = Instantiate(VrFadeScreen, new Vector3(0, 0, 0), Quaternion.identity);
        var fader = init.GetComponent<VrFadeScreen>();
        isFading = true;
        var rend = init.GetComponent<Renderer>();
        fader.InitiateFader(this, rend, scene, col, fadeOutDuration, fadeInDuration);
    }

    public void DoneFading()
    {
        isFading = false;
    }

    [Button]
    public void DoFade()
    {
        if (!vrChecker.CheckIsVR())
            Fade(sceneName, col, fadeOutDuration, fadeInDuration);
        else
            FadeVR(sceneName, col, fadeOutDuration, fadeInDuration);
    }
}
