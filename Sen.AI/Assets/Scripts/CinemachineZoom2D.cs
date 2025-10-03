using UnityEngine;
using Unity.Cinemachine;

public class CinemachineZoom2D : MonoBehaviour
{
    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;
    public static CinemachineZoom2D Instance { get; private set; }
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private float targetOrthogpraphicSize = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float zoomSpeed = 2f;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp
            (cinemachineCamera.Lens.OrthographicSize, targetOrthogpraphicSize, Time.deltaTime * zoomSpeed);
    }
    public void SetTargetOrthographicSize(float targetOrthogpraphicSize)
    {
        this.targetOrthogpraphicSize = targetOrthogpraphicSize;
    }
    public void SetNormalOrthographicSize()
    {
        SetTargetOrthographicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
