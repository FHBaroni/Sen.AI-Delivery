using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private GameObject speedUpArrowGameObject;
    [SerializeField] private GameObject speedDownArrowGameObject;
    [SerializeField] private GameObject speedLeftArrowGameObject;
    [SerializeField] private GameObject speedRightArrowGameObject;
    private void Update()
    {
        UpdateStatsMesh();
    }
    private void UpdateStatsMesh()
    {
        speedDownArrowGameObject.SetActive(Drone.Instance.GetSpeedY() < 0);
        speedUpArrowGameObject.SetActive(Drone.Instance.GetSpeedY() >= 0);
        speedLeftArrowGameObject.SetActive(Drone.Instance.GetSpeedX() < 0);
        speedRightArrowGameObject.SetActive(Drone.Instance.GetSpeedX() >= 0);

        statsTextMesh.text =
            GameManager.Instance.GetScore() + "\n" +
            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
            Mathf.Abs(Mathf.Round(Drone.Instance.GetSpeedX() * 10f)) + "\n" +
            Mathf.Abs(Mathf.Round(Drone.Instance.GetSpeedY() * 10)) + "\n" +
            Drone.Instance.GetFuel();
    }

}
