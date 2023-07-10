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

    public void DoneFading()
    {
        isFading = false;
    }

    [Button]
    public void DoFade()
    {
        if(!vrChecker.CheckIsVR())
            Fade(sceneName, col, fadeOutDuration, fadeInDuration);
    }
}
