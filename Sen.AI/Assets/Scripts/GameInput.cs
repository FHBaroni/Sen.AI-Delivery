using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Enable();
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
