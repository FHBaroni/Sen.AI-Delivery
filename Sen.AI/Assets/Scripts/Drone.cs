using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drone : MonoBehaviour
{
    private Rigidbody2D droneRb;

    private void Awake()
    {
        droneRb = GetComponent<Rigidbody2D>();

        // Debug.Log(Vector2.Dot(new Vector2(0, 1), new Vector2(0, 1)));
        // Debug.Log(Vector2.Dot(new Vector2(0, 1), new Vector2(.5f, .5f)));
        // Debug.Log(Vector2.Dot(new Vector2(0, 1), new Vector2(1, 0)));
        // Debug.Log(Vector2.Dot(new Vector2(0, 1), new Vector2(0, -1)));
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
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log("Fora da plataforma");
            return;
        }

        //Debug.Log(collision2D.relativeVelocity.magnitude);
        float crashMagnitude = 3f;
        if (collision2D.relativeVelocity.magnitude > crashMagnitude)
        {
            Debug.Log("Quebrou ao pousar");
            return;
        }
        float dotVector = Vector2.Dot(Vector2.up, transform.up); //Dot product == produto escalar
        float minDotVector = 0.90f;
        if (dotVector < minDotVector)
        {
            Debug.Log("Muito inclinado para pousar");
            return;
        }
        Debug.Log("Pousou corretamente");
    }
}
