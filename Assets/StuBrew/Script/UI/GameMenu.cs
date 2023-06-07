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

    void Start()
    {
        MovementActionMap = playerInput.actions.FindActionMap("GenericMovement");
        inGameUI.enabled = false;
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
            MovementActionMap.Disable();
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            MovementActionMap.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
