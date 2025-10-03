using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] private Button nextButton;

    private Action NextButtonAction;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            NextButtonAction();
        });
    }

    private void Start()
    {
        Drone.Instance.OnLanded += Drone_OnLanded;
        Hide();
    }

    private void Drone_OnLanded(object sender, Drone.OnLandedEventArgs e)
    {
        if (e.landingType == Drone.LandingType.Success)
        {
            titleTextMesh.text = "SUCESSO!";
            nextButtonTextMesh.text = "CONTINUE";
            NextButtonAction = GameManager.Instance.GoToNextLevel;
        }
        else
        {
            titleTextMesh.text = "<color=red>FALHOU!</color>";
            nextButtonTextMesh.text = "TENTE NOVAMENTE";
            NextButtonAction = GameManager.Instance.RestartLevel;
        }

        statsTextMesh.text =
            MathF.Round(e.LandingSpeed * 2f) + "\n" +
            MathF.Round(e.dotVector * 100f) + "\n" +
            "x" + e.ScoreMultiplier + "\n" +
            e.score;

        Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private string FormatLandingSpeed(float landingSpeed, bool isTooFast)
    {
        float roundedSpeed = MathF.Round(landingSpeed * 2f);
        return isTooFast
            ? $"<b><color=red>{roundedSpeed}</color></b>"
            : roundedSpeed.ToString();
    }
}


/*if (e.landingType == Drone.LandingType.Success)
{
    titleTextMesh.text = "SUCESSO!";
    statsTextMesh.text =
    MathF.Round(e.LandingSpeed * 2f) + "\n" +
    MathF.Round(e.dotVector * 100f) + "\n" +
    "X" + e.ScoreMultiplier + "\n" +
    e.score;
}
else if (e.landingType == Drone.LandingType.TooFastLanding)
{
    titleTextMesh.text = "<color=red>FALHOU!</color>";
    statsTextMesh.text =
    "<color=red>" + MathF.Round(e.LandingSpeed * 2f) + "</color>" + "\n" +
    MathF.Round(e.dotVector * 100f) + "\n" +
    "X" + e.ScoreMultiplier + "\n" +
    e.score;
}
else
{
    titleTextMesh.text = "<color=red>FALHOU!</color>";
    statsTextMesh.text =
    MathF.Round(e.LandingSpeed * 2f) + "\n" +
    MathF.Round(e.dotVector * 100f) + "\n" +
    "X" + e.ScoreMultiplier + "\n" +
    e.score;
}*/