using System;
using UnityEngine;

public class DroneVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem rigtThrusterParticleSystem;
    [SerializeField] private GameObject droneExplosionVFX;

    private Drone drone;
    private void Awake()
    {
        drone = GetComponent<Drone>();
        drone.OnUpForce += Drone_OnUpForce;
        drone.OnRightForce += Drone_OnRightForce;
        drone.OnLeftForce += Drone_OnLeftForce;
        drone.OnNoForce += Drone_OnNoForce;

        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rigtThrusterParticleSystem, false);
    }
    private void Start()
    {
        drone.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Drone.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Drone.LandingType.WrongLandingArea:
            case Drone.LandingType.TooSteepAngle:
            case Drone.LandingType.TooFastLanding:
                //falhou
                Instantiate(droneExplosionVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                break;
        }
    }
    private void Drone_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, true);
        SetEnabledThrusterParticleSystem(rigtThrusterParticleSystem, true);
    }
    private void Drone_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, true);
    }
    private void Drone_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(rigtThrusterParticleSystem, true);
    }
    private void Drone_OnNoForce(object sender, System.EventArgs e)
    {
        SetEnabledThrusterParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(middleThrusterParticleSystem, false);
        SetEnabledThrusterParticleSystem(rigtThrusterParticleSystem, false);
    }
    private void SetEnabledThrusterParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
