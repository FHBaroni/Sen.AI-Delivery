using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnMenuButtonPresed;

    private InputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.MenuAction.performed += MenuAction_performed;
    }

    private void MenuAction_performed(InputAction.CallbackContext obj)
    {
        OnMenuButtonPresed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.DroneUp.IsPressed();
    }
    public bool IsLeftActionPressed()
    {
        return inputActions.Player.DroneLeft.IsPressed();
    }
    public bool IsRightActionPressed()
    {
        return inputActions.Player.DroneRight.IsPressed();
    }
    private void OnDestroy()
    {
        inputActions.Disable();
    }
}
