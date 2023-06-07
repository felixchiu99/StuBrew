using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public string firstLevel;

    [SerializeField] Canvas inGameUI;

    [SerializeField] PlayerInput playerInput;
    InputActionMap MovementActionMap;
    InputActionMap GameplayActionMap;

    void Start()
    {
        MovementActionMap = playerInput.actions.FindActionMap("GenericMovement");
        GameplayActionMap = playerInput.actions.FindActionMap("Gameplay");
        UiEnable(false);
    }

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

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        inGameUI.enabled = !inGameUI.enabled;
        UiEnable(inGameUI.enabled);
    }

    private void UiEnable(bool enable)
    {
        inGameUI.enabled = enable;
        Cursor.visible = enable;
        if (enable)
        {
            GameplayActionMap.Disable();
            MovementActionMap.Disable();
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            GameplayActionMap.Enable();
            MovementActionMap.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
