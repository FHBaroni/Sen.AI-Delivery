using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drone : MonoBehaviour
{

    private const float GRAVITY = 0.7f;
    public static Drone Instance { get; private set; }
    public int OnBeforeForce { get; internal set; }

    public event EventHandler OnUpForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnNoForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler OnEnergyPickup;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs
    {
        public LandingType landingType;
        public int score;
        public float dotVector;
        public float LandingSpeed;
        public float ScoreMultiplier;
    }

    public enum LandingType
    {
        Success,
        WrongLandingArea,
        TooSteepAngle,
        TooFastLanding,
    }

    public enum State
    {
        WaitingToStart,
        Playing,
        GameOver,
    }

    private Rigidbody2D droneRb;
    private float fuelAmountMax = 100f;
    private float fuelAmount;
    private State state;


    private void Awake()
    {
        Instance = this;

        fuelAmount = fuelAmountMax;
        state = State.WaitingToStart;

        droneRb = GetComponent<Rigidbody2D>();
        droneRb.gravityScale = 0f;
    }
    private void FixedUpdate()
    {
        OnNoForce?.Invoke(this, EventArgs.Empty);

        switch (state)
        {
            case State.WaitingToStart:
                if (GameInput.Instance.IsUpActionPressed() ||
                    GameInput.Instance.IsLeftActionPressed() ||
                    GameInput.Instance.IsRightActionPressed())
                {
                    droneRb.gravityScale = GRAVITY;
                    SetState(State.Playing);
                }
                break;
            case State.Playing:
                if (fuelAmount <= 0f) return;

                if (GameInput.Instance.IsUpActionPressed())
                {
                    float force = 700f;
                    droneRb.AddForce(force * transform.up * Time.deltaTime);
                    ConsumeFuel();
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.IsRightActionPressed())
                {
                    float turnSpeed = -100f;
                    droneRb.AddTorque(turnSpeed * Time.deltaTime);
                    ConsumeFuel();
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.IsLeftActionPressed())
                {
                    float turnSpeed = 100f;
                    droneRb.AddTorque(turnSpeed * Time.deltaTime);
                    ConsumeFuel();
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out Platform platform))
        {
            Debug.Log("Fora da plataforma");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.WrongLandingArea,
                dotVector = 0f,
                LandingSpeed = 0f,
                ScoreMultiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }

        float crashMagnitude = 3f;
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > crashMagnitude)
        {
            Debug.Log("Quebrou ao pousar");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.TooFastLanding,
                dotVector = 0f,
                LandingSpeed = relativeVelocityMagnitude,
                ScoreMultiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
            return;
        }
        float dotVector = Vector2.Dot(Vector2.up, transform.up); //Dot product == produto escalar
        float minDotVector = 0.90f;
        if (dotVector < minDotVector)
        {
            Debug.Log("Muito inclinado para pousar");
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingType = LandingType.TooSteepAngle,
                dotVector = dotVector,
                LandingSpeed = relativeVelocityMagnitude,
                ScoreMultiplier = 0,
                score = 0,
            });
            SetState(State.GameOver);
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
        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            landingType = LandingType.Success,
            dotVector = dotVector,
            LandingSpeed = relativeVelocityMagnitude,
            ScoreMultiplier = platform.GetScoreMultiplier(),
            score = score,
        });
        SetState(State.GameOver);
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent(out EnergyPickup energyPickup))
        {
            float addEnergyAmount = 30f;
            fuelAmount += addEnergyAmount;
            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            energyPickup.DestroyItem();
            OnEnergyPickup?.Invoke(this, EventArgs.Empty);
        }

        if (collision2D.gameObject.TryGetComponent(out CoinPickup coinPickup))
        {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroyItem();
        }
    }
    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }
    private void ConsumeFuel()
    {
        float fuelConsumption = 10f;
        fuelAmount -= fuelConsumption * Time.deltaTime;
    }
    public float GetFuel()
    {
        return fuelAmount;
    }
    public float GetFuelNormalized()
    {
        return fuelAmount / fuelAmountMax;
    }
    public float GetSpeedX()
    {
        return droneRb.linearVelocityX;
    }
    public float GetSpeedY()
    {
        return droneRb.linearVelocityY;
    }
}
