using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//https://www.youtube.com/watch?v=NObwdF9RqCg
public class MenuController : MonoBehaviour
{
    public GameObject _menuPanel;
    public InputActionReference _openMenuAction;

   private void Awake() {
        _openMenuAction.action.Enable();
        _openMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy() {
        _openMenuAction.action.Disable();
        _openMenuAction.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void ToggleMenu(InputAction.CallbackContext context) {
        _menuPanel.SetActive(!_menuPanel.activeSelf);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change) {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                _openMenuAction.action.Disable();
                _openMenuAction.action.performed -= ToggleMenu;
                break;
            case InputDeviceChange.Reconnected:
                _openMenuAction.action.Enable();
                _openMenuAction.action.performed += ToggleMenu;
                break;
        }
    }

}
