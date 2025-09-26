using UnityEngine;
using UnityEngine.InputSystem;

public class Drone : MonoBehaviour
{
    private Rigidbody2D droneRb;

    private void Awake()
    {
        droneRb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            float force = 700f;
            droneRb.AddForce(force * transform.up * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float turnSpeed = -100f;
            droneRb.AddTorque(turnSpeed * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float turnSpeed = 100f;
            droneRb.AddTorque(turnSpeed * Time.deltaTime);
        }
    }
}
