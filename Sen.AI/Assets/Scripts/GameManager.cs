using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static int levelNumber = 1;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;
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

        LoadCurrentLevel();
    }
    private void Drone_OnStateChanged(object sender, Drone.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Drone.State.Playing;

        if (e.state == Drone.State.Playing)
        {
            cinemachineCamera.Target.TrackingTarget = Drone.Instance.transform;
            CinemachineZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    private void LoadCurrentLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            gameLevel.gameObject.SetActive(gameLevel.GetLevelNumber() == levelNumber);
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Drone.Instance.transform.position = spawnedGameLevel.GetDroneStartPosition();
                cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
                CinemachineZoom2D.Instance.SetTargetOrthographicSize(spawnedGameLevel.GetZoomedOutOrthographicSize());
            }
        }
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
    public void GoToNextLevel()
    {
        levelNumber++;
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);

    }
    public void RestartLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public int GetCurrentLevelNumber()
    {
        return levelNumber;
    }
}
