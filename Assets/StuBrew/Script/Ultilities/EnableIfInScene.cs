using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class EnableIfInScene : MonoBehaviour
{
    [Scene] [SerializeField] string scene;

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(this.scene + " " + scene.name);
        if(this.scene == scene.name)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
