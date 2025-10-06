using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const int SOUND_VOLUME_MAX = 10;

    public static SoundManager Instance { get; private set; }
    private static int soundVolume = 6;
    private AudioSource soundAudioSource;

    public event EventHandler OnSoundVolumeCanged;

    [SerializeField] private AudioClip coinPickupAudioClip;
    [SerializeField] private AudioClip energyPickupAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    [SerializeField] private AudioClip crashAudioClip;

    private void Awake()
    {
        Instance = this;
    }
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
                AudioSource.PlayClipAtPoint(landingSuccessAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
            default:
                AudioSource.PlayClipAtPoint(crashAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }
    private void Drone_OnCoinPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Drone_OnEnergyPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(energyPickupAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume()
    {
        soundVolume = (soundVolume + 1) % SOUND_VOLUME_MAX;
        OnSoundVolumeCanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return ((float)soundVolume) / SOUND_VOLUME_MAX;
    }
}
