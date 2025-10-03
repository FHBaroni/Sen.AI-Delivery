using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform droneStartPositionTransform;

    public int GetLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 GetDroneStartPosition()
    {
        return droneStartPositionTransform.position;
    }

}
