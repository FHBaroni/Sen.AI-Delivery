using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupAudioClip;
    [SerializeField] private AudioClip energyPickupAudioClip;
    private void Start()
    {
        Drone.Instance.OnEnergyPickup += Drone_OnEnergyPickup;
        Drone.Instance.OnCoinPickup += Drone_OnCoinPickup;

    }

    private void Drone_OnCoinPickup(object sender, EventArgs e)
    {
        //AudioSource.PlayClipAtPoint();
    }

    private void Drone_OnEnergyPickup(object sender, EventArgs e)
    {

    }

}
