using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public SceneData()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
    }

    public void Load()
    {
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SaveSystem.sceneChange.sceneName = sceneName;
        SaveSystem.sceneChange.DoFade();
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
