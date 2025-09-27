using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int scoreMultiplier;
    public int GetScoreMultiplier()
    {
        return scoreMultiplier;
    }
}
