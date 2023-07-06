using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class ReloadSceneByName : MonoBehaviour
{
    [Scene]
    public string sceneName;

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
