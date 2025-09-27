using TMPro;
using UnityEngine;

public class PlatformVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreMultiplierTextMesh;

    private void Awake()
    {
        Platform platform = GetComponent<Platform>();
        scoreMultiplierTextMesh.text = "x" + platform.GetScoreMultiplier();

    }
}
