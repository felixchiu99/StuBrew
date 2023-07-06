using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class InGameMenuController : MenuController
{
    [Required]
    [SerializeField] InGameMenuManager manager;
    [Required]
    [SerializeField] Canvas inGameUI;

    [SerializeField] PlayerInput playerInput;
    InputActionMap MovementActionMap;
    InputActionMap GameplayActionMap;

    public bool isPC = false;

    void Start()
    {
        manager.Add(inGameUI);
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
            if (enable)
            {
                Cursor.visible = true;
                GameplayActionMap.Disable();
                MovementActionMap.Disable();
                Cursor.lockState = CursorLockMode.Confined;
            }
            else if(!manager.IsAnyEnabled())
            {
                Cursor.visible = false;
                GameplayActionMap.Enable();
                MovementActionMap.Enable();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }
}
