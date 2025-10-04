using System;
using UnityEngine;

public class DroneAudio : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudioSource;

    private Drone drone;
    private void Awake()
    {
        drone = GetComponent<Drone>();

    }

    private void Start()
    {
        drone.OnNoForce += Drone_OnNoForce;
        drone.OnUpForce += Drone_OnUpForce;
        drone.OnLeftForce += Drone_OnLeftForce;
        drone.OnRightForce += Drone_OnRightForce;
        engineAudioSource.Pause();

    }

    private void Drone_OnRightForce(object sender, EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
            engineAudioSource.Play();
    }

    private void Drone_OnLeftForce(object sender, EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
            engineAudioSource.Play();
    }

    private void Drone_OnUpForce(object sender, EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
            engineAudioSource.Play();
    }

    private void Drone_OnNoForce(object sender, EventArgs e)
    {

        engineAudioSource.Pause();
    }
}
