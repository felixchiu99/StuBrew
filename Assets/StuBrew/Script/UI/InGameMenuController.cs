using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenuController : MonoBehaviour
{
    [SerializeField] Canvas inGameUI;

    [SerializeField] PlayerInput playerInput;
    InputActionMap MovementActionMap;
    InputActionMap GameplayActionMap;

    public bool isPC = false;

    void Start()
    {
        if (playerInput)
        {
            MovementActionMap = playerInput.actions.FindActionMap("GenericMovement");
            GameplayActionMap = playerInput.actions.FindActionMap("Gameplay");
        }

        UiEnable(false);
    }

    public void SetIsPC(bool pc)
    {
        isPC = pc;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (inGameUI)
        {
            inGameUI.enabled = !inGameUI.enabled;
            UiEnable(inGameUI.enabled);
        }

    }

    protected void UiEnable(bool enable)
    {
        if (isPC)
        {
            if (inGameUI)
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
}
