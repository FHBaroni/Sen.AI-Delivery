using Unity.VisualScripting;
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
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out Platform platform))
        {
            Debug.Log("Fora da plataforma");
            return;
        }

        float crashMagnitude = 3f;
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > crashMagnitude)
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

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMultiplier = 10f;
        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;

        float maxScoreAmountLandingSpeed = 100;
        float landingSpeedScore = (crashMagnitude - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        Debug.Log("landingAngleScore: " + landingAngleScore);
        Debug.Log("landingspeedScore: " + landingSpeedScore);

        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * platform.GetScoreMultiplier());

        Debug.Log("pontuação " + score);


    }
}
