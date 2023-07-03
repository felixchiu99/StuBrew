using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : InGameMenuController
{
    public string firstLevel;

    public void MainMenu()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void CloseMenu()
    {
        UiEnable(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
