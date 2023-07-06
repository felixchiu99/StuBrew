using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameMenu : InGameMenuController
{
    [Scene]
    public string firstLevel;

    public void MainMenu()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void CloseMenu()
    {
        UiEnable(false);
    }
}
