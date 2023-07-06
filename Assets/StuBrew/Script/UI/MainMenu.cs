using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuController
{
    public string firstLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOption()
    {

    }

    public void CloseOption()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
