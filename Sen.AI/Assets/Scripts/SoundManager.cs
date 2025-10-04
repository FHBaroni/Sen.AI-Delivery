using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupAudioClip;
    [SerializeField] private AudioClip energyPickupAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    [SerializeField] private AudioClip crashAudioClip;
    private void Start()
    {
        Drone.Instance.OnEnergyPickup += Drone_OnEnergyPickup;
        Drone.Instance.OnCoinPickup += Drone_OnCoinPickup;
        Drone.Instance.OnLanded += Drone_OnLanded;

    }

    private void Drone_OnLanded(object sender, Drone.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Drone.LandingType.Success:
                AudioSource.PlayClipAtPoint(landingSuccessAudioClip, Camera.main.transform.position);
                break;
            default:
                AudioSource.PlayClipAtPoint(crashAudioClip, Camera.main.transform.position);
                break;
        }
    }
    private void Drone_OnCoinPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Camera.main.transform.position);
    }

    private void Drone_OnEnergyPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(energyPickupAudioClip, Camera.main.transform.position);
    }

}
