using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score;
    private float time;
    private bool isTimerActive;

    private void Awake()
    {
        Instance = this;
        score = 0;
    }
    private void Start()
    {
        Drone.Instance.OnCoinPickup += Drone_OnCoinPickup;
        Drone.Instance.OnLanded += Drone_OnLanded;
        Drone.Instance.OnStateChanged += Drone_OnStateChanged;
    }

    private void Drone_OnStateChanged(object sender, Drone.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Drone.State.Playing;
    }

    private void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
        }
    }
    private void Drone_OnLanded(object sender, Drone.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Drone_OnCoinPickup(object sender, EventArgs e)
    {
        AddScore(500);
        Debug.Log("Score: " + score);
    }

    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return time;
    }
}
